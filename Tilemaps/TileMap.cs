﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Managers.EntityRelated;
using Engine.Entities;
using Engine.Tilemaps;

namespace Engine
{
    public class TileMap
    {

        //TileMap to do

        /*
         * Add multiple layers for these tilemaps.
         * 
         * 
         */
        #region Variables
        //Variable to hold all tiles 
        private List<CollisionTile> collisionTiles = new List<CollisionTile>();


        private List<DrawTile> propsLayer = new List<DrawTile>();

       // private List<SpawnTile> spawnLayer = new List<SpawnTile>();

        private List<Tiles> Tiles = new List<Tiles>();
        //List to hold all tiles that dont contain collision(Static Tiles maybe scenery)
        private List<DrawTile> drawTiles = new List<DrawTile>();
        //List to hold all directional tiles
        private List<DirectionTile> directionTiles = new List<DirectionTile>();
        //Variables to hold the tiles width and height
        private int width, height;

        #endregion

        public int[,] Map;
        #region Properties

        //Property to give all tiles reference to another class (used to compare intersection)
        public List<CollisionTile> CollisionTiles
        {
            get { return collisionTiles; }
        }

        public List<DrawTile> DrawTiles
        {
            get { return drawTiles; }
        }

        public int[,] MapRef { get { return Map; } }
        public List<Tiles> getTiles
        {
            get { return Tiles; }
        }

        public List<DirectionTile> DirectionTiles
        {
            get { return directionTiles; }
        }


        public int Width
        {
            get { return Map.GetLength(1); }
        }

        public int Height
        {
            get { return Map.GetLength(0); }
        }

        #endregion

        #region Constructor & Map Generation method
        public TileMap()
        {

        }




        public void GenerateEmpty(int size)
        {
            Map = new int[size, size];


            for (int x = 0; x < Map.GetLength(1); x++)
            {
                for (int y = 0; y < Map.GetLength(0); y++)
                {
                    int number = Map[y, x];
                    {
                        Tiles.Add(new BlankTile(new Rectangle(x * size, y * size, size, size)));

                    }
                }
            }
        }

        public void GenerateLayer(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0 & number < 9)
                        drawTiles.Add(new DrawTile(number, new Rectangle(x * size, y * size, size, size)));
                }
            }
        }


        public void Generate(int[,] map, int size)
        {
            Map = map;
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0 & number < 9)
                        CollisionTiles.Add(new CollisionTile(number, new Rectangle(x * size, y * size, size, size)));
                    if (number == 10)
                    {
                        EntityManager.Instance.createEntity<pEntity>(new Vector2(x * size, y * size), "player");
                    }
                    if (number == 11)
                    {
                        EntityManager.Instance.createEntity<trapEntity>(new Vector2(x * size, y * size), "player");
                    }
                    if (number == 15)
                    {
                        EntityManager.Instance.createEntity<cEntity>(new Vector2(x * size, y * size), "player");
                    }



                    width = y;// * size;// (x + 1) * size;
                    height = x; //* size;//(y + 1) * size;

                }
            }



        }

        public void GenerateCollisionLayer(int[,] map, int size)
        {
            Map = map;
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    {








                    }
                }
            }
        }

        /// <summary>
        /// Concept
        /// 
        /// public void updateMap(tileMap map)
        /// {
        /// iterate through both maps and compare, adjust any changes
        /// draw the new map
        /// }
        /// 
        /// public void updateLayer(tileMap Layer, Layer)
        /// {
        /// Look through layer array until layer number is found
        /// iterate through both maps and compare, adjust any changes
        /// draw the new layer
        /// }
        /// </summary>
        /// <param name="spriteBatch"></param>
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < drawTiles.Count; i++ )
            {
                drawTiles[i].Draw(spriteBatch);
            }

            for (int i = 0; i < Tiles.Count; i++)
            {
                Tiles[i].Draw(spriteBatch);
            }


            for (int i = 0; i < collisionTiles.Count; i++)
            {
                collisionTiles[i].Draw(spriteBatch);
            }

            for (int i = 0; i < directionTiles.Count; i++)
            {
                directionTiles[i].Draw(spriteBatch);
            }

           
        }
        #endregion


   }
}
