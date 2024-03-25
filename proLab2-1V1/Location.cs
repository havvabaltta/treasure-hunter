using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static proLab2_1V1.App;

namespace proLab2_1V1
{
    public class Location
    {
        private int x { get; set; }
        private int y { get; set; }

        public double F { get; set; }
        public double G { get; set; }
        public double H { get; set; }
        public Location parent { get; set; }
        public Location() { }
        public Location(int x, int y)  
        {
            setLoc(x, y);
        }

        public void setLoc(int a, int b)
        {
            this.x = a;
            this.y = b;
        }

        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;
        }
    }
}
