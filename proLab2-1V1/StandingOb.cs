using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace proLab2_1V1
{
    public abstract class StandingOb : Obstacle
    {
        public StandingOb(int x, int y, int width, int height, bool s) : base(x, y, width, height, s)
        {

        }
        public StandingOb() { }

    }

    public class Tree1 : StandingOb
    {
        public Tree1() { }
        public Tree1(int x, int y, bool s) : base(x, y, 2, 2, s)
        {
            this.seasson = s;

        }
        
        public override void Draw(Graphics g, int cellSize)
        {
            if(seasson)
            {
                Bitmap tree1s = new Bitmap(icon_path + @"\pine-tree.png");
                g.DrawImage(tree1s, x * cellSize, y * cellSize, width * cellSize, height * cellSize);
            }
            else
            {
                Bitmap tree1w = new Bitmap(icon_path + @"\pine-treeWint.png");
                g.DrawImage(tree1w, x * cellSize, y * cellSize, width * cellSize, height * cellSize);
            }
        }
    }

    public class Tree2 : StandingOb
    {
        public Tree2() { }

        public Tree2(int x, int y, bool s) : base(x, y, 3, 3, s)
        {
            this.seasson = s;

        }

        public override void Draw(Graphics g, int cellSize)
        {
            if (seasson)
            {
                Bitmap tree2s = new Bitmap(icon_path + @"\pine-tree.png");
                g.DrawImage(tree2s, x * cellSize, y * cellSize, width * cellSize, height * cellSize);
            }
            else
            {
                Bitmap tree2w = new Bitmap(icon_path + @"\pine-treeWint.png");
                g.DrawImage(tree2w, x * cellSize, y * cellSize, width * cellSize, height * cellSize);
            }
        }
    }

    public class Tree3 : StandingOb
    {
        public Tree3() { }
        public Tree3(int x, int y, bool s) : base(x, y, 4, 4, s)
        {
            this.seasson = s;
        }

        public override void Draw(Graphics g, int cellSize)
        {
            if (seasson)
            {
                Bitmap tree3s = new Bitmap(icon_path + @"\pine-tree.png");
                g.DrawImage(tree3s, x * cellSize, y * cellSize, width * cellSize, height * cellSize);
            }
            else
            {
                Bitmap tree3w = new Bitmap(icon_path + @"\pine-treeWint.png");
                g.DrawImage(tree3w, x * cellSize, y * cellSize, width * cellSize, height * cellSize);
            }
        }
    }
    public class Tree4 : StandingOb
    {
  
        public Tree4() { }
        public Tree4(int x, int y, bool s) : base(x, y, 5, 5, s)
        {
            this.seasson = s;

        }

        public override void Draw(Graphics g, int cellSize)
        {
            if (seasson)
            {
                Bitmap tree4s = new Bitmap(icon_path + @"\pine-tree.png");
                g.DrawImage(tree4s, x * cellSize, y * cellSize, width * cellSize, height * cellSize);
            }
            else
            {
                Bitmap tree4w = new Bitmap(icon_path + @"\pine-treeWint.png");
                g.DrawImage(tree4w, x * cellSize, y * cellSize, width * cellSize, height * cellSize);
            }
        }
    }
    public class Rock1 : StandingOb
    {
        public Rock1() { }
        public Rock1(int x, int y, bool s) : base(x, y, 2, 2, s)
        {
 
        }
        public override void Draw(Graphics g, int cellSize)
        {
            Bitmap rock1 = new Bitmap(icon_path + @"\rock.png");
            g.DrawImage(rock1, x * cellSize, y * cellSize, width * cellSize, height * cellSize);
        }
    }

    public class Rock2 : StandingOb
    {
        public Rock2() { }
        public Rock2(int x, int y, bool s) : base(x, y, 3, 3, s)
        {

        }

        public override void Draw(Graphics g, int cellSize)
        {
            Bitmap rock2 = new Bitmap(icon_path + @"\rock.png");
            g.DrawImage(rock2, x * cellSize, y * cellSize, width * cellSize, height * cellSize);
        }
    }

    public class Wall : StandingOb
    {
        public Wall() { }
        public Wall(int x, int y, bool s) : base(x, y, 10, 1, s)
        {

        }

        public override void Draw(Graphics g, int cellSize)
        {
            Bitmap wall = new Bitmap(icon_path+ @"\wall.png");
            g.DrawImage(wall, x * cellSize, y * cellSize, width * cellSize, height * cellSize);
        }
    }

    public class Mount : StandingOb
    {
       public Mount() { }
        public Mount(int x, int y, bool s) : base(x, y, 15, 15, s)
        {
            this.seasson = s; // if s = 1 it's summer, if s=0  it's winter
 
        }


        public override void Draw(Graphics g, int cellSize)
        {
            if(seasson)   
            {
                Bitmap mountSum = new Bitmap(icon_path + @"\mount_sum.png");
                g.DrawImage(mountSum, x * cellSize, y * cellSize, width * cellSize, height * cellSize);

            }
            else 
            {
                Bitmap mountWint = new Bitmap(icon_path + @"\mount_winter.png");
                g.DrawImage(mountWint, x * cellSize, y * cellSize, width * cellSize, height * cellSize);
            }

           
        }
    }

}
