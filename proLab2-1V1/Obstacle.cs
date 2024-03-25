using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proLab2_1V1
{
    public abstract class Obstacle
    {
        public int x {  get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        
        public bool seasson { get; set; }
        public string icon_path = AppDomain.CurrentDomain.BaseDirectory + "iconsAssets";
        public Obstacle(int x, int y, int w, int h, bool s)
        { 
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
            this.seasson = s;
        }
        public Obstacle() { }
        public abstract void Draw(Graphics g, int cellSize);
    }
}    

