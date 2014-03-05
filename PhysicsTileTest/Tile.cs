using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class Tile : DrawablePhysicsObject
    {
        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public Tile(World world, Vector2 size, float mass, bool isStatic, Vector2 _position, int i) : base(world, size, mass, isStatic)
        {
            texture = Content.Load<Texture2D>("Tile" + i);
            Position = _position;
        }
    }
}

