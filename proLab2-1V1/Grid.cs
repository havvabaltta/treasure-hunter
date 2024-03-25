using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace proLab2_1V1
{
    public partial class Grid : Form
    {
        int x, y;
        int cellSize;
        private Pen pen;
        Character ch;
        List<Obstacle> obstacles = new List<Obstacle>();
        List<Treasure> treasures = new List<Treasure>();
        byte[,] mapMatrix;
        int[,] smokeClear;
        App app;
        Graphics g;
        SolidBrush solidbrush = new SolidBrush(Color.Blue);
         // ımage for doubleBuffer

        public Grid(int x, int y, Character c)
        {
            InitializeComponent();

            ch = c;
            label1.Text = "ID: " + c.getID();
            label6.Text = "Name: " + c.getName();
            this.x = x; 
            this.y = y;
            mapMatrix = new byte[x, y];
            Array.Clear(mapMatrix, 0, mapMatrix.Length);
            smokeClear = new int[(x / 7 + 1), (y / 7 + 1)];
            Array.Clear(smokeClear, 0, smokeClear.Length);
            cellSize = (int)Math.Min(panel1.Width / Math.Sqrt(x * y), panel1.Height / Math.Sqrt(x * y));
            if (cellSize < 10)
            {
                forLittleCellSize();
            }
            else if ((cellSize * x < (panel1.Width / 2)) || (cellSize * y < (panel1.Height / 2)))
            {
                cellSize = Math.Min(panel1.Width / x, panel1.Height / y);
            }
            
            timer1.Tick += Timer1_Tick;
            timer1.Interval = 1000;

            timer2.Tick += Timer2_Tick;
            timer2.Interval = 1000;
        }

        

        private void forLittleCellSize()
        {
            cellSize = 10;
            int tempX = panel1.Width; 
            int tempY = panel1.Height;  
            // Panelin boyutunu gridin boyutuna ayarlayın
            panel1.Width = x * cellSize;
            panel1.Height = y * cellSize;
            this.Width += panel1.Width - tempX; 
            this.Height += panel1.Height - tempY; 
            // Gridin yeniden çizilmesini tetikleyin
            panel1.Invalidate();
        }
        private void Grid_Load(object sender, EventArgs e)
        {
            panel1.Paint += Panel1_Paint; // Paint olayına olay işleyicisi ekle
            panel1.BackColor = Color.White; // Panel arka plan rengini ayarla
            pen = new Pen(Color.Black, 1); // Kalem oluştur
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true); // Çift tamponlama ve çizim stillerini ayarla
            UpdateStyles();

        }
        bool k = true;
        Bitmap smokeMap;


        private void Panel1_Paint(object sender, PaintEventArgs e)
        {


            using (Pen pen = new Pen(Color.Black, 1))
            {
                this.g = e.Graphics;

                if (smokeOn)
                {

                    using (Graphics gr = Graphics.FromImage(smokeMap))
                    {

                        for (int i = 0; i < x / 7 + 1; i++)
                        {
                            for (int j = 0; j < y / 7 + 1; j++)
                            {


                                // gr.FillRectangle(new SolidBrush(Color.Blue), ch.getLocX() * cellSize, ch.getLocY() * cellSize, cellSize, cellSize);
                                int sX = i * 7 * cellSize;
                                int sY = j * 7 * cellSize;
                                int width = 7 * cellSize;
                                int height = 7 * cellSize;


                                if (smokeClear[i, j] == 0)
                                {
                                    gr.FillRectangle(new SolidBrush(Color.White), sX, sY, width, height);
                                }


                            }
                        }
                    }

                }
                using (Bitmap backBuffer = new Bitmap(panel1.Width, panel1.Height))
                {
                    using (Graphics g = Graphics.FromImage(backBuffer))
                    {
                        // Panoyu çiz
                        DrawGrid(g, pen); // DrawGrid metodunu, geçerli Graphics nesnesi ve kalem nesnesiyle çağır

                        // Ekrana çizimi aktar
                        e.Graphics.DrawImage(backBuffer, 0, 0);
                    }


                    if (k)
                    {
                        PlaceCharacterRandomly(e.Graphics);
                        k = false;
                    }


                    app = new App(obstacles, treasures, ch, mapMatrix, e.Graphics, cellSize);

                    if (!k)
                    {

                        g.FillRectangle(new SolidBrush(Color.Blue), ch.getLocX() * cellSize, ch.getLocY() * cellSize, cellSize, cellSize);
                        using (Graphics cachedGraphics = Graphics.FromImage(cachedGrid))
                        {
                            cachedGraphics.FillRectangle(new SolidBrush(Color.Green), ch.getLocX() * cellSize, ch.getLocY() * cellSize, cellSize, cellSize);
                        }
                        smokeMap = cachedGrid;
                    }
                    if (smokeOn)
                    {

                        g.FillRectangle(new SolidBrush(Color.Blue), ch.getLocX() * cellSize, ch.getLocY() * cellSize, cellSize, cellSize);
                        using (Graphics sgr = Graphics.FromImage(smokeMap))
                        {
                            sgr.FillRectangle(new SolidBrush(Color.Green), ch.getLocX() * cellSize, ch.getLocY() * cellSize, cellSize, cellSize);
                        }
                    }



                }

            }
        }

        Bitmap cachedGrid;
        private bool isCached = true;
        private void DrawGrid(Graphics g, Pen pen)
        {

            if (!isCached || cachedGrid == null)
            {
                cachedGrid = new Bitmap(x * cellSize, y * cellSize);


                using (Graphics cachedGraphics = Graphics.FromImage(cachedGrid))
                {


                    for (int i = 0; i < this.x / 2; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            int locX = i * cellSize;
                            int locY = j * cellSize;
                            cachedGraphics.FillRectangle(new SolidBrush(Color.LimeGreen), locX, locY, cellSize, cellSize);
                        }
                    }
                    for (int i = this.x / 2; i < this.x; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            int locX = i * cellSize;
                            int locY = j * cellSize;
                            cachedGraphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), locX, locY, cellSize, cellSize);
                        }
                    }
                    for (int j = 0; j < y; j++)
                    {
                        for (int i = 0; i < x; i++)
                        {
                            int locX = i * cellSize;
                            int locY = j * cellSize;
                            cachedGraphics.DrawRectangle(pen, locX, locY, cellSize, cellSize);
                        }
                    }
                    PutObstacle(g);
                    foreach (Obstacle obstacle in obstacles)
                    {
                        obstacle.Draw(cachedGraphics, cellSize);
                    }
                    foreach (Treasure treasure in treasures)
                    {
                        treasure.Draw(cachedGraphics, cellSize);
                    }


                }
                isCached = true;
            }

            smokeMap = cachedGrid;


            if (!smokeOn)
            {
                g.DrawImage(cachedGrid, 0, 0);
            }
            else
            {
                g.DrawImage(smokeMap, 0, 0);
            }

            int startX = panel1.Location.X;
            int startY = panel1.Location.Y;
            int panelHeight = panel1.Height; // panel1'in yüksekliğini al


        }


        private void PutObstacle(Graphics g)
        {
            int minObstacleCount = 20;
            int maxObstacleCount = 30;
            Random random = new Random();
            if (this.x * this.y > 9999)
            {
                minObstacleCount = 20;
                maxObstacleCount = 30;
            }
            else if (this.x * this.y > 6399 && this.x * this.y < 10000)
            {
                minObstacleCount = 15;
                maxObstacleCount = 20;
            }
            else if (this.x * this.y < 6400)
            {
                minObstacleCount = 10;
                maxObstacleCount = 15;
            }
            int minObstacleTypeCount = minObstacleCount / 10;
            int maxObstacleTypeCount = maxObstacleCount / 10;

            List<Type> obstacleTypes = new List<Type> {
         typeof(Tree1), typeof(Tree2), typeof(Tree3), typeof(Tree4),
         typeof(Rock1), typeof(Rock2), typeof(Wall), typeof(Mount), typeof(Bird), typeof(Bee)
     };



            foreach (Type obstacleType in obstacleTypes)
            {
                int obstacleTypeCount = random.Next(minObstacleTypeCount, maxObstacleTypeCount + 1);

                for (int i = 0; i < obstacleTypeCount; i++)
                {
                    int obstacleWidth, obstacleHeight;


                    if (obstacleType == typeof(Tree1))
                    {
                        obstacleWidth = obstacleHeight = 2;
                    }
                    else if (obstacleType == typeof(Tree2))
                    {
                        obstacleWidth = obstacleHeight = 3;
                    }
                    else if (obstacleType == typeof(Tree3))
                    {
                        obstacleWidth = obstacleHeight = 4;
                    }
                    else if (obstacleType == typeof(Tree4))
                    {
                        obstacleWidth = obstacleHeight = 5;
                    }
                    else if (obstacleType == typeof(Rock1))
                    {
                        obstacleWidth = obstacleHeight = 2;
                    }
                    else if (obstacleType == typeof(Rock2))
                    {
                        obstacleWidth = obstacleHeight = 3;
                    }
                    else if (obstacleType == typeof(Wall))
                    {
                        obstacleWidth = 10;
                        obstacleHeight = 1;
                    }
                    else if (obstacleType == typeof(Mount))
                    {
                        obstacleWidth = obstacleHeight = 15;
                    }
                    else if (obstacleType == typeof(Bird))
                    {
                        obstacleWidth = 2;
                        obstacleHeight = 12;
                    }
                    else if (obstacleType == typeof(Bee))
                    {
                        obstacleWidth = 8;
                        obstacleHeight = 2;
                    }
                    else
                    {

                        obstacleWidth = obstacleHeight = 1;
                    }

                    int obstacleX, obstacleY;



                    do
                    {
                        obstacleX = random.Next(this.x);
                        obstacleY = random.Next(this.y);
                    } while (IsCollision(obstacleX, obstacleY, obstacleWidth, obstacleHeight, obstacles));


                    if (obstacleType == typeof(Bee))
                    {
                        Obstacle newObstacle = (Obstacle)Activator.CreateInstance(obstacleType, obstacleX, obstacleY, true);
                        obstacles.Add(newObstacle);

                    }
                    else if (obstacleType == typeof(Bird))
                    {
                        Obstacle newObstacle = (Obstacle)Activator.CreateInstance(obstacleType, obstacleX, obstacleY, true);
                        obstacles.Add(newObstacle);
                    }
                    else if (obstacleX < this.x / 2)
                    {
                        Obstacle newObstacle = (Obstacle)Activator.CreateInstance(obstacleType, obstacleX, obstacleY, true);
                        obstacles.Add(newObstacle);
                    }
                    else
                    {
                        Obstacle newObstacle = (Obstacle)Activator.CreateInstance(obstacleType, obstacleX, obstacleY, false);
                        obstacles.Add(newObstacle);
                    }
                }
            }


            foreach (Obstacle obstacle in obstacles)
                {
                    obstacle.Draw(g, cellSize);
                    for (int i = obstacle.x; i < obstacle.x + obstacle.width; i++)
                    {
                        for (int j = obstacle.y; j < obstacle.y + obstacle.height; j++)
                        {
                            mapMatrix[i, j] = 1;
                        }
                    }
                }
            

            PutTreasure(g);
        }


        private bool IsCollision(int startX, int startY, int width, int height, List<Obstacle> obstacles)
        {
            if (IsOutOfBounds(startX, startY, width, height))
            {
                return true; // Obstacle veya Treasure belirlenen alanın dışına çıkıyorsa true döndür
            }

            for (int i = 0; i < 8; i++)
            {
                foreach (Obstacle obstacle in obstacles)
                {
                    if (startX < obstacle.x + obstacle.width && startX + width > obstacle.x && startY < obstacle.y + obstacle.height && startY + height > obstacle.y)

                    {
                        return true;
                    }
                }
            }
            return false;
        }


        
        private void PutTreasure(Graphics g)
        {
            
            Random random = new Random();
            int minTreasureCount = 5;
            int maxTreasureCount = 10;

            List<Type> treasureTypes = new List<Type> {
    typeof(GoldChest), typeof(SilverChest), typeof(EmeraldChest), typeof(CopperChest)
};

            foreach (Type treasureType in treasureTypes)
            {
                int treasureCount = random.Next(minTreasureCount, maxTreasureCount + 1);

                for (int i = 0; i < treasureCount; i++)
                {
                    int treasureX, treasureY;
                    bool season = random.Next(2) == 0;


                    int width = 2;
                    int height = 2;


                    do
                    {
                        treasureX = random.Next(this.x);
                        treasureY = random.Next(this.y);
                    } while (IsCollision(treasureX, treasureY, width, height, obstacles, treasures));

                    Treasure newTreasure = (Treasure)Activator.CreateInstance(treasureType, treasureX, treasureY, season);
                    treasures.Add(newTreasure);
                }
            }


            foreach (Treasure treasure in treasures)
            {
                treasure.Draw(g, cellSize);
                for (int i = treasure.X; i < treasure.X + treasure.Width; i++)
                {
                    for (int j = treasure.Y; j < treasure.Y + treasure.Height; j++)
                    {
                        if(treasure is CopperChest)
                        {
                            mapMatrix[i, j] = 2;
                        }
                        else if(treasure is EmeraldChest)
                        {
                            mapMatrix[i,j] = 3;
                        }
                        else if(treasure is SilverChest)
                        {
                            mapMatrix[i,j]= 4;

                        }
                        else if(treasure is GoldChest)
                        {
                            mapMatrix[i, j] = 5;
                        }
                        
                    }

                }
            }
                
            
        }


        private bool IsCollision(int startX, int startY, int width, int height, List<Obstacle> obstacles, List<Treasure> treasures)
        {
            if (IsOutOfBounds(startX, startY, width, height))
            {
                return true; // Obstacle veya Treasure belirlenen alanın dışına çıkıyorsa true döndür
            }


            for (int i = 0; i < 8; i++)
            {
                foreach (Obstacle obstacle in obstacles)
                {
                    if (startX < obstacle.x + obstacle.width && startX + width > obstacle.x && startY < obstacle.y + obstacle.height && startY + height > obstacle.y)
                    {
                        return true;
                    }
                }


                foreach (Treasure treasure in treasures)
                {
                    if (startX < treasure.X + treasure.Width && startX + width > treasure.X && startY < treasure.Y + treasure.Height && startY + height > treasure.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsOutOfBounds(int startX, int startY, int width, int height)
        {
            if (startX < 0 || startY < 0 || startX + width > this.x || startY + height > this.y)
            {
                return true; // Belirlenen alanın dışına çıkıldıysa true döndür
            }
            return false;
        }

       
        private void PlaceCharacterRandomly(Graphics g)
        {
            
            Random random = new Random();
            int characterX, characterY;

            do
            {
                characterX = random.Next(this.x);
                characterY = random.Next(this.y);
            } while (IsCollision(characterX, characterY, 1, 1, obstacles, treasures));

            ch.setLoc(characterX, characterY);
            g.FillRectangle(solidbrush,characterX * cellSize,characterY * cellSize,cellSize,cellSize);// Karakterin konumunu ayarla
               
           
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.BackColor == Color.Green)
            {
                button3.Text = "S T O P  G A M E";
                button3.BackColor = Color.Red;
                timer1.Start();

            }
            else
            {
                button3.Text = "S T A R T  G A M E";
                button3.BackColor = Color.Green;
                timer1.Stop();
            }
        }
        int squareCount = 0;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            squareCount++;
            string a = app.startMove();
            if(a != null)
            {
                listBox1.Items.Add(a);
            }
            for (int i = 0; i < x / 7 + 1; i++)
            {
                for (int j = 0; j < y / 7 + 1; j++)
                {
                    foreach (Location lc in ch.locList)
                    {
                        if (lc.getX() / 7 == i && lc.getY() / 7 == j)
                        {
                            smokeClear[i, j] = 1;
                        }
                    }
                }
            }
            
            label4.Text =  squareCount.ToString();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true); // Çift tamponlama ve çizim stillerini ayarla
            UpdateStyles();
            
            panel1.Invalidate();
           
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            app.startShortPath();
            panel1.Invalidate();
        }
        bool smokeOn = false;
        private void button5_Click(object sender, EventArgs e)
        {
            if(button5.Text == "S M O K E   A D D ")
            {
                smokeOn = true;
                button5.Text = "C L E A R";
            }
            else
            {
                button5.Text = "S M O K E   A D D ";
                smokeOn = false;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int nX = random.Next(1000);
            int nY = random.Next(1000);
            ch.locList.Clear();
            Grid newRandomGame = new Grid(nX,nY,ch);
            this.Hide();
            newRandomGame.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button3.Text == "S T O P  G A M E")
            {
                if (timer1.Interval > 200)
                {
                    timer1.Interval -= 200;
                }
            }
            else if(button4.Text == "S T O P")
            {
                if (timer2.Interval > 200)
                {
                    timer2.Interval -= 200;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button3.Text == "S T O P  G A M E")
            {
                timer1.Interval += 200;
            }
            else if (button4.Text == "S T O P")
            {
                timer2.Interval += 200;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //shortWay
            if(button4.Text == "S H O R T  W A Y")
            {
                button4.Text = "S T O P";
                button5.Enabled = false;
                timer2.Start();
            }
            else
            {
                button5.Enabled = true;
                button4.Text = "S H O R T  W A Y";
                timer2.Stop();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            List<string> tempList = new List<string>();
            foreach (var item in listBox1.Items)
            {
                tempList.Add(item.ToString());
            }
            listBox1.Items.Clear();
            foreach (var item in tempList)
            {
                string typeChestStr = item.ToString();
                if (typeChestStr.Substring(typeChestStr.Length - 4) == "Gold")
                {
                    listBox1.Items.Add(item);
                }
            }
            foreach (var item in tempList)
            {
                string typeChestStr = item.ToString();
                if (typeChestStr.Substring(typeChestStr.Length - 6) == "silver")
                {
                    listBox1.Items.Add(item);
                }
            }
            foreach (var item in tempList)
            {
                string typeChestStr = item.ToString();
                if (typeChestStr.Substring(typeChestStr.Length - 6) == "emrald")
                {
                    listBox1.Items.Add(item);
                }
            }
            foreach (var item in tempList)
            {
                string typeChestStr = item.ToString();
                if (typeChestStr.Substring(typeChestStr.Length - 6) == "copper")
                {
                    listBox1.Items.Add(item);
                }
            }
        }



        // Form kapanırken eski formu göster
        private void Grid_FormClosed(object sender, FormClosedEventArgs e)
        {
            Result result = new Result(ch);
            result.Show();
        }
    }
}