﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Engine.Managers;
using Engine;
using Engine.Managers.EntityRelated;
using Engine.Entities;

namespace ADS.Tilemaps
{
    //Cellular Automata generation class
    public class CA
    {
        //Empty 2D map
        int[,] map;
        //Probability Factor 
        int fillPercent ;

        int mapWidth;
        int mapHeight;
        //String to be converted to a hashcode
        string seed = "JamieVerner";

        bool useRandomSeed = true;

       public  void Start(int fill, int x, int y)
        {
            fillPercent = fill;
            mapWidth = x;
            mapHeight = y;
            GenerateMap();
            stupidTestMethod();
        }

      public  void GenerateMap()
        {
            map = new int[mapWidth, mapHeight];
            RandomFill();

            for (int i = 0; i<5; i++)
            {
                SmoothMap();
               
            }
           // checkForSpot();

            Console.WriteLine("GENERATED");
        }


        public void stupidTestMethod()
        {
            bool entityAdded = false;
            Console.WriteLine("YOLO");


            
                for (int x = 0; x < mapWidth; x++)
                {
                    for (int y = 0; y < mapHeight; y++)
                    {
                        if (map[x, y] == 0 && entityAdded == false)
                        {
                            EntityManager.Instance.createEntityCamDrawable<pEntity>(new Vector2(x * 64, y * 64), "player");
                            entityAdded = true;
                            continue;
                        }

                    }
                
            }
        }
        void checkForSpot()
        {

            Random random = new Random();

            int refy = random.Next(0, mapWidth);
            for (int x = refy; x < mapWidth; x++)
            {
                for (int y = refy; y < mapHeight; y++)
                {
                    int n = GetSurroundingWallCount(x, y);

                    if (n ==1)
                        map[x, y] = 3;
                    break;
                
                }
            }
        }

       public void RandomFill()
        {
            if (useRandomSeed)
                seed = DateTime.Now.TimeOfDay.ToString();

            System.Random random = new System.Random(seed.GetHashCode());
            Console.WriteLine(seed.GetHashCode());

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (x == 0 || x == mapWidth - 1 || y == 0 || y == mapHeight - 1)
                    {
                        map[x, y] = 1;
                    }
                    else
                    {
                        map[x, y] = (random.Next(0, 100) < fillPercent) ? 1 : 0;
                    }
                }
            }
        }

        void SmoothMap()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    int neighbourWallTiles = GetSurroundingWallCount(x, y);
                    if (neighbourWallTiles > 4)
                        map[x, y] = 1;
                    else if (neighbourWallTiles < 4)
                        map[x, y] = 0;
                   

                }

            }
        }

 


        //Checks each tile for the amount of neighbours they contain which are walls
        int GetSurroundingWallCount(int x, int y)
        {
            //number of neighbours which are walls
            int wallCount = 0;
            for (int neighbourX = x - 1; neighbourX <= x + 1; neighbourX++){
                for (int neighbourY = y - 1; neighbourY <= y + 1; neighbourY++){
                    //Make sure in bounds of index && Check for walls
                    if (neighbourX >= 0 && neighbourX < mapWidth && neighbourY >= 0 && neighbourY < mapHeight) {
                        if (neighbourX != x || neighbourY != y) {
                            wallCount += map[neighbourX, neighbourY];
                        }
                    }
                    else { wallCount++;
                    }

                }

            }
            return wallCount;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (map != null)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    for (int y = 0; y < mapHeight; y++)
                    {
                        if(map[x,y] == 1)
                        spriteBatch.Draw(ResourceLoader.Instance.GetTex("Tile6"), new Rectangle(x*64, y*64, 64, 64), Color.Maroon);
                        if(map[x,y] == 0)
                            spriteBatch.Draw(ResourceLoader.Instance.GetTex("Tile6"), new Rectangle(x*64, y*64, 64, 64), Color.Red);
                        if (map[x, y] == 3)
                            spriteBatch.Draw(ResourceLoader.Instance.GetTex("Tile6"), new Rectangle(x * 64, y * 64, 64, 64), Color.Yellow);


                    }

                }
            }

        }
    }
}
