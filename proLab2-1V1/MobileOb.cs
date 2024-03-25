using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proLab2_1V1
{
    public abstract class MobileOb : Obstacle
    {

        int x { get; set; }
        int y { get; set; }
        int width { get; set; }
        int height { get; set; }
        public bool seasson { get; set; }
        public MobileOb() { }
        public MobileOb(int x, int y, int width, int height, bool s) : base(x, y, width, height, s)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public virtual void PaintSurroundingArea(Graphics g, int cellSize, Color color) { }
    }


    public class Bird : MobileOb
    {

        private const int MaxRoadLength = 5;
        public Bird()
        {
        }
        public Bird(int x, int y, bool s) : base(x, y, 2, 2, s)
        {

        }


        public override void Draw(Graphics g, int cellSize)
        {
            PaintSurroundingArea(g, cellSize, Color.Red);
            Bitmap birdPng = new Bitmap(icon_path + @"\bird.png");
            g.DrawImage(birdPng, x * cellSize, (y + 5) * cellSize, width * cellSize, height * cellSize);

        }

        public override void PaintSurroundingArea(Graphics g, int cellSize, Color color)
        {
            // Paint the area 5*2 units below the bird
            g.FillRectangle(new SolidBrush(color), x * cellSize, y * cellSize, width * cellSize, 5 * cellSize);

            // Paint the area below the bird with specific dimensions
            g.FillRectangle(new SolidBrush(color), x * cellSize, (y + 7) * cellSize, width * cellSize, 5 * cellSize); ;
        }
    }


    public class Bee : MobileOb
    {

        private const int MaxRoadLength = 3;

        public Bee() { }
        public Bee(int x, int y, bool s) : base(x, y, 2, 2, s)
        {

        }

        public override void Draw(Graphics g, int cellSize)
        {
            PaintSurroundingArea(g, cellSize, Color.Red);

            Bitmap beePng = new Bitmap(icon_path + @"\bee.png");
            g.DrawImage(beePng, (x + 3) * cellSize, y * cellSize, width * cellSize, height * cellSize);

        }

        public override void PaintSurroundingArea(Graphics g, int cellSize, Color color)
        {
            // Paint the area 3*2 units to the right of the bee
            g.FillRectangle(new SolidBrush(color), x * cellSize, y * cellSize, 3 * cellSize, height * cellSize);

            // Paint the area to the left of the bee with specific dimensions
            g.FillRectangle(new SolidBrush(color), (x + 5) * cellSize, y * cellSize, 3 * cellSize, height * cellSize);
        }
    }


}