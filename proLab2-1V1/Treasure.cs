using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proLab2_1V1
{
    public abstract class Treasure
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Select { get; set; }
        public string IconPath { get; set; }

        public Treasure(int x, int y, int width, int height, bool s)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Select = s;
            IconPath = AppDomain.CurrentDomain.BaseDirectory + "iconsAssets";
        }

        public abstract void Draw(Graphics g, int cellSize);
    }

    public class GoldChest : Treasure
    {
        public GoldChest(int x, int y, bool s) : base(x, y, 2, 2, s)
        {
        }

        public override void Draw(Graphics g, int cellSize)
        {
            Bitmap image = new Bitmap(IconPath + @"\gold.png");
            g.DrawImage(image, X * cellSize, Y * cellSize, Width * cellSize, Height * cellSize);
        }
    }

    public class SilverChest : Treasure
    {
        public SilverChest(int x, int y, bool s) : base(x, y, 2, 2, s)
        {

        }

        public override void Draw(Graphics g, int cellSize)
        {
            Bitmap image = new Bitmap(IconPath + @"\silver.png");
            g.DrawImage(image, X * cellSize, Y * cellSize, Width * cellSize, Height * cellSize);
        }
    }

    public class EmeraldChest : Treasure
    {
        public EmeraldChest(int x, int y, bool s) : base(x, y, 2, 2, s)
        {

        }

        public override void Draw(Graphics g, int cellSize)
        {
            Bitmap image = new Bitmap(IconPath + @"\emerald.png");
            g.DrawImage(image, X * cellSize, Y * cellSize, Width * cellSize, Height * cellSize);
        }
    }
        
    public class CopperChest : Treasure
    {
        public CopperChest(int x, int y, bool s) : base(x, y, 2, 2, s)
        {

        }

        public override void Draw(Graphics g, int cellSize)
        {
            Bitmap image = new Bitmap(IconPath + @"\copper.png");
            g.DrawImage(image, X * cellSize, Y * cellSize, Width * cellSize, Height * cellSize);
        }
    }
}
