using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class PlayerBlocker
    {
        private int mapWidth;

        public PlayerBlocker(int _mapWidth)
        {
            mapWidth = _mapWidth;
        }

        public void Update(Vector2 playerPosition)
        {
            if (playerPosition.X < 0)
                playerPosition.X = 0;
            if (playerPosition.X > mapWidth)
                playerPosition.X = mapWidth;
        }
    }
}
