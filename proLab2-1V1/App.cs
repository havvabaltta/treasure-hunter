    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    namespace proLab2_1V1
    {
        public class App
        {
            int cellSize;
            Graphics g;
            byte[,] mapMatrix;
            List<Obstacle> obstacles;
            List<Treasure> treasures;
            Character c;
            int lx, ly;
            List<Location> nearbyTreasures = new List<Location>();
            public List<Location> visitedTreasures = new List<Location>();
            Random random = new Random();
            //for heat map
            int[,] heatMap;
            int heatThreshold = 7;
            int regionSize; // parsellenen alan 
            public App(List<Obstacle> obstacles, List<Treasure> treasures, Character c, byte[,] mapMatrix, Graphics g, int cellSize)
            {
                this.g = g;
                this.mapMatrix = mapMatrix;
                this.obstacles = obstacles;
                this.treasures = treasures;
                this.c = c;
                this.cellSize = cellSize;
                heatMap = new int[mapMatrix.GetLength(0), mapMatrix.GetLength(1)];
                regionSize = mapMatrix.GetLength(1) / 3;
                FindNearbyTreasures();
            }

            private void UpdateHeatMap(int x, int y)
            {
                heatMap[x, y]++;

                int regionX = x / regionSize;
                int regionY = y / regionSize;
                heatMap[regionX, regionY]++;
                if (heatMap[regionX, regionY] > heatThreshold)
                {
                    MoveToNewRegion(regionX, regionY);
                }
            }
            public void UpdateCharacterMovement(int newX, int newY)
            {
                UpdateHeatMap(newX, newY);
            }
            private void MoveToNewRegion(int currentRegionX, int currentRegionY)
            {
                int charX = c.getLocX();
                int charY = c.getLocY();
                int newX;
                int newY;

                while (true)
                {
                    newX = random.Next((currentRegionX - 1) * regionSize, (currentRegionX + 1) * regionSize);
                    newY = random.Next((currentRegionY - 1) * regionSize, (currentRegionY + 1) * regionSize);
                    newX = Math.Max(0, Math.Min(newX, mapMatrix.GetLength(0) - 1));
                    newY = Math.Max(0, Math.Min(newY, mapMatrix.GetLength(1) - 1));

                    if (mapMatrix[newX, newY] == 1)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                if (newX > charX && !isWall(c.getLocX() + 1, c.getLocY()))
                {
                    move_right();
                    nearbyTreasures.Clear();
                    FindNearbyTreasures();

                }
                else if (newX < charX && !isWall(c.getLocX() - 1, c.getLocY()))
                {
                    move_left();
                    nearbyTreasures.Clear();
                    FindNearbyTreasures();

                }

                if (newY > charY && !isWall(c.getLocX(), c.getLocY() + 1))
                {
                    move_down();
                    nearbyTreasures.Clear();
                    FindNearbyTreasures();

                }
                else if (newY < charY && !isWall(c.getLocX(), c.getLocY() - 1))
                {
                    move_up();
                    nearbyTreasures.Clear();
                    FindNearbyTreasures();
                }
            
            }

            public void move_up()
            {
                if (c.getLocY() > 0 && !isWall(c.getLocX(), c.getLocY() - 1))
                {
                    c.MoveUp();
                    if (nearbyTreasures.Count == 0)
                    {
                        UpdateHeatMap(c.getLocX(), c.getLocY());
                    }
                }
            }
            public void move_down()
            {
                if (c.getLocY() < mapMatrix.GetLength(1) && !isWall(c.getLocX(), c.getLocY() + 1))
                {
                    c.MoveDown();
                    if (nearbyTreasures.Count == 0)
                    {
                        UpdateHeatMap(c.getLocX(), c.getLocY());
                    }
                }
            }
            public void move_left()
            {
                if (c.getLocX() > 0 && !isWall(c.getLocX() - 1, c.getLocY()))
                {
                    c.MoveLeft();
                    if (nearbyTreasures.Count == 0)
                    {
                        UpdateHeatMap(c.getLocX(), c.getLocY());
                    }
                }
            }
            public void move_right()
            {
                if (c.getLocX() < mapMatrix.GetLength(0) && !isWall(c.getLocX() + 1, c.getLocY()))
                {
                    c.MoveRight();
                    if (nearbyTreasures.Count == 0)
                    {
                        UpdateHeatMap(c.getLocX(), c.getLocY());
                    }
                }
            }
            private bool isWall(int x, int y)
            {
                if (mapMatrix[x, y] == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public void FindNearbyTreasures()
            {

                int characterX = c.getLocX(); int characterY = c.getLocY();
                int startX = characterX - 3; int startY = characterY - 3;

                for (int i = startX; i < startX + 7; i++)
                {
                    for (int j = startY; j < startY + 7; j++)
                    {

                        if (i >= 0 && i < mapMatrix.GetLength(0) && j >= 0 && j < mapMatrix.GetLength(1)
                                && mapMatrix[i, j] != 0)
                        {
                            if (mapMatrix[i, j] > 1)
                            {
                                nearbyTreasures.Add(new Location(i, j));
                            }
                        }
                    }
                }
            }
            public string startMove()
            {
                if (nearbyTreasures.Count == 0)
                {
                    int targetX = c.getLocX();
                    int targetY = c.getLocY();

                    int offsetX = random.Next(-50, 51);  // Genişletilmiş rastgele aralık
                    int offsetY = random.Next(-50, 51);  // Genişletilmiş rastgele aralık

                    targetX += offsetX;
                    targetY += offsetY;

                    targetX = Math.Max(0, Math.Min(targetX, mapMatrix.GetLength(0) - 1));
                    targetY = Math.Max(0, Math.Min(targetY, mapMatrix.GetLength(1) - 1));

                    UpdateHeatMap(targetX, targetY);

                
                        switch (random.Next(2))
                        {
                            case 0:
                                if (targetX > c.getLocX() && !isWall(c.getLocX() + 1, c.getLocY()))
                                {
                                    move_right();
                                }
                                else if (targetX < c.getLocX() && !isWall(c.getLocX() - 1, c.getLocY()))
                                {
                                    move_left();
                                }
                                break;
                            case 1:
                            if (targetY > c.getLocY() && !isWall(c.getLocX(), c.getLocY() + 1))
                            {
                                move_down();
                            }
                            else if (targetY < c.getLocY() && !isWall(c.getLocX(), c.getLocY() - 1))
                            {
                                move_up();
                            }
                            break;
                        }
                    

                    nearbyTreasures.Clear();
                    FindNearbyTreasures();

                    return null;
                }
                else
                {

                    string a;
                    while (true)
                    {
                        a = toTarget();
                        if (a != null)
                        {
                            break;
                        }
                    }

                    nearbyTreasures.Clear();
                    FindNearbyTreasures();
                    return a;
                }
            }


            public string toTarget()
            {
                Location nearest = null;
                int shortestDistance = int.MaxValue;

                int charX = c.getLocX();
                int charY = c.getLocY();

                foreach (Location treasureLoc in nearbyTreasures)
                {
                    int distance = (int)Math.Sqrt((int)Math.Pow(treasureLoc.getX() - charX, 2) + (int)Math.Pow(treasureLoc.getY() - charY, 2));
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearest = treasureLoc;
                    }
                }

                if (nearest != null)
                {
                    int targetX = nearest.getX();
                    int targetY = nearest.getY();

                    if (charX == targetX && charY == targetY)
                    {

                        //g.FillRectangle(Brushes.Yellow, targetX * cellSize, targetY * cellSize, cellSize, cellSize);


                        byte v = mapMatrix[charX, charY];
                        string chestType = "";
                        switch (v)
                        {
                            case 2:
                                chestType = "copper";
                                break;
                            case 3:
                                chestType = "emrald";
                                break;
                            case 4:
                                chestType = "silver";
                                break;
                            case 5:
                                chestType = "Gold";
                                break;
                        }
                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                if (charX > 1 && charY > 1 && charX < mapMatrix.GetLength(0) - 1 && charY < mapMatrix.GetLength(1) - 1)
                                {
                                    mapMatrix[(charX - 1) + i, (charY - 1) + j] = 0;
                                }
                            }
                        }

                        string findLoc = $"x:{charX} y:{charY}  type: {chestType}";
                        return findLoc;
                    }
                    else
                    {
                        // Hedefe doğru hareket et
                            switch (random.Next(2))
                            {

                                case 0:
                                    if (targetX > charX && !isWall(c.getLocX() + 1, c.getLocY()))
                                    {
                                        move_right();
                                    }
                                    else if (targetX < charX && !isWall(c.getLocX() - 1, c.getLocY()))
                                    {
                                        move_left();
                                    }
                                    break;
                                case 1:
                                    if (targetY > charY && !isWall(c.getLocX(), c.getLocY() + 1))
                                    {
                                        move_down();
                                    }
                                    else if (targetY < charY && !isWall(c.getLocX(), c.getLocY() - 1))
                                    {
                                        move_up();
                                    }
                                    break;
                            }         
                    }
                }

                return null; // Hedefe ulaşılmadıysa null döndür
            }

            //SmallWay

            public string startShortPath()
            {

                Location closestTreasure = FindClosestTreasure();
                if (closestTreasure != null)
                {
                    MoveToTarget(closestTreasure);
                    CheckTreasureCollection();
                    return "Hareket başarılı.";

                    if (treasures.Count == 0)
                    {
                        MessageBox.Show("Oyun Tamamlandı! Tebrikler, kazandınız!", "Oyun Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return "Game Win";
                    }
                }
                else
                {
                    return "En yakın hazine bulunamadı.";
                }


            }

            private Location FindClosestTreasure()
            {
                int characterX = c.getLocX();
                int characterY = c.getLocY();
                double minDistance = double.MaxValue;
                Location closestTreasure = null;

                foreach (Treasure treasure in treasures)
                {
                    int treasureX = treasure.X;
                    int treasureY = treasure.Y;
                    double distance = Math.Sqrt(Math.Pow(characterX - treasureX, 2) + Math.Pow(characterY - treasureY, 2));
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestTreasure = new Location(treasureX, treasureY);
                    }
                }

                return closestTreasure;
            }

            private void MoveToTarget(Location target)
            {
                int characterX = c.getLocX();
                int characterY = c.getLocY();
                int targetX = target.getX();
                int targetY = target.getY();

                bool canMove = false;

                // X ekseninde hareket
                if (characterX < targetX)
                {
                    if (!IsCollision(characterX + 1, characterY))
                    {
                        c.MoveRight();
                        canMove = true;
                    }
                    else if (characterY < targetY && !IsCollision(characterX, characterY + 1))
                    {
                        c.MoveDown();
                        canMove = true;
                    }
                    else if (characterY > targetY && !IsCollision(characterX, characterY - 1))
                    {
                        c.MoveUp();
                        canMove = true;
                    }
                }
                else if (characterX > targetX)
                {
                    if (!IsCollision(characterX - 1, characterY))
                    {
                        c.MoveLeft();
                        canMove = true;
                    }
                    else if (characterY < targetY && !IsCollision(characterX, characterY + 1))
                    {
                        c.MoveDown();
                        canMove = true;
                    }
                    else if (characterY > targetY && !IsCollision(characterX, characterY - 1))
                    {
                        c.MoveUp();
                        canMove = true;
                    }
                }

                // Y ekseninde hareket
                if (!canMove && characterY < targetY)
                {
                    if (!IsCollision(characterX, characterY + 1))
                    {
                        c.MoveDown();
                        canMove = true;
                    }
                    else if (characterX < targetX && !IsCollision(characterX + 1, characterY))
                    {
                        c.MoveRight();
                        canMove = true;
                    }
                    else if (characterX > targetX && !IsCollision(characterX - 1, characterY))
                    {
                        c.MoveLeft();
                        canMove = true;
                    }
                }
                else if (!canMove && characterY > targetY)
                {
                    if (!IsCollision(characterX, characterY - 1))
                    {
                        c.MoveUp();
                        canMove = true;
                    }
                    else if (characterX < targetX && !IsCollision(characterX + 1, characterY))
                    {
                        c.MoveRight();
                        canMove = true;
                    }
                    else if (characterX > targetX && !IsCollision(characterX - 1, characterY))
                    {
                        c.MoveLeft();
                        canMove = true;
                    }
                }
            }


            private bool IsCollision(int x, int y)
            {
                if (x < 0 || y < 0 || x >= mapMatrix.GetLength(0) || y >= mapMatrix.GetLength(1))
                {
                    return true;
                }


                foreach (Obstacle obstacle in obstacles)
                {
                    if (x >= obstacle.x && x < obstacle.x + obstacle.width &&
                         y >= obstacle.y && y < obstacle.y + obstacle.height)
                    {
                        return true;
                    }
                }

                return false; // Engelle çakışma yoksa geçilebilir kabul edilir.
            }


            private void CheckTreasureCollection()
            {
                int characterX = c.getLocX();
                int characterY = c.getLocY();

                for (int i = 0; i < treasures.Count; i++)
                {
                    if (characterX == treasures[i].X && characterY == treasures[i].Y)
                    {
                        treasures.RemoveAt(i);
                        mapMatrix[characterX, characterY] = 0;


                        foreach (Obstacle obstacle in obstacles)
                        {
                            if (obstacle.x <= characterX && characterX < obstacle.x + obstacle.width &&
                                obstacle.y <= characterY && characterY < obstacle.y + obstacle.height)
                            {

                                //graphics.FillRectangle(Brushes.Yellow, obstacle.x * cellSize, obstacle.y * cellSize, obstacle.width * cellSize, obstacle.height * cellSize);
                                break;
                            }
                        }
                        break;
                    }
                }
            }




        }
    }