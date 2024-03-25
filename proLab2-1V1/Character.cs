using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proLab2_1V1
{
    public class Character
    {
        private string id;
        private string name;
        public List<Location> locList = new List<Location>(); // keep visited raod loaction
        Location loc;
        byte[,] visibleMatrix;

        // default constractor
        public Character(int gridX, int gridY) 
        {
            setName("player");
            setID("0000");
            visibleMatrix = new byte[gridX, gridY];
        }

        //structured constractor
        public Character(string n, int gridX, int gridY)
        {
            setName((string)n);

            Random random = new Random();
            string randomID = random.Next(1000, 9999).ToString("0000");
            setID(randomID);
            visibleMatrix = new byte[gridX, gridY];
        }
        public void setLoc(int lx, int ly)
        {
            loc = new Location(lx, ly);
            locList.Add(loc);
        }

        public void setName(string name)
        {
            this.name = name;
        }
        public string getName() 
        {
            return name;
        }

        public void setID(string v)
        {
            id = v;
        }
        
        public string getID()
        {
            return id;
        }
        public int getLocX()
        {
            return this.loc.getX();
        }
        public int getLocY()
        {
            return this.loc.getY();
        }
        public void MoveRight()
        {
      
            loc.setLoc(loc.getX() + 1, loc.getY());
            locList.Add(loc);
        }

        public void MoveLeft()
        {
           
            loc.setLoc(loc.getX() - 1, loc.getY());
            locList.Add(loc);
        }

        public void MoveUp()
        {
            loc.setLoc(loc.getX(), loc.getY() - 1);
            locList.Add(loc);
        }

        public void MoveDown()
        {
           
            loc.setLoc(loc.getX(), loc.getY() + 1);
            locList.Add(loc);
        }

    }
}
