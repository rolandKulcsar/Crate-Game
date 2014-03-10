using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class Map
    {
        private List<Tile> tiles = new List<Tile>();
        private int width;
        private int height;
        private World world;

        public List<Tile> Tiles
        {
            get { return tiles; }
        }
        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }
        public World World
        {
            set { world = value; }
        }
        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0)
                    {
                       tiles.Add(new Tile(world, new Vector2(63.0f, 63.0f), true, new Vector2(x * size + 32, y * size + 32), number)); 
                    }

                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}