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
        private Player player;

        public PlayerBlocker(Player _player, int _mapWidth)
        {
            player = _player;
            mapWidth = _mapWidth;
        }

        public void Update()
        {
            if (player.Position.X < 0)
                player.Position = new Vector2(0, player.Position.Y);
            if (player.Position.X > mapWidth)
                player.Position = new Vector2(mapWidth, player.Position.Y);
        }
    }
}
