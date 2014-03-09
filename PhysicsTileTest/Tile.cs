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

        private int Id;

        public int ID
        {
            get { return Id; }
        }

        public Tile(World world, Vector2 size, bool isStatic, Vector2 _position, int i) : base(world, size, isStatic)
        {
            texture = Content.Load<Texture2D>("Tile" + i);
            Position = _position;
            Id = i;
        }
    }
}

