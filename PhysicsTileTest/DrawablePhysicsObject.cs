using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class DrawablePhysicsObject
    {
        // Because Farseer uses 1 unit = 1 meter we need to convert
        // between pixel coordinates and physics coordinates.
        // I've chosen to use the rule that 100 pixels is one meter.
        // We have to take care to convert between these two 
        // coordinate-sets wherever we mix them!
        
        protected const float unitToPixel = 100.0f;
        protected const float pixelToUnit = 1 / unitToPixel;

        protected Body body;
        public Vector2 Position
        {
            get { return body.Position * unitToPixel; }
            set { body.Position = value * pixelToUnit; }
        }

        protected Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return rectangle; }
        }

        protected Texture2D texture;

        protected Vector2 size;
        public Vector2 Size
        {
            get { return size * unitToPixel; }
            set { size = value * pixelToUnit; }
        }
        /// <param name="world">The farseer simulation this object should be part of</param>
        /// <param name="size">The size in pixels</param>
        /// <param name="mass">The mass in kilograms</param> 
        public DrawablePhysicsObject(World world, Vector2 size, float mass, bool isStatic)
        {
            body = BodyFactory.CreateRectangle(world, size.X * pixelToUnit, size.Y * pixelToUnit, 1);
            body.IsStatic = isStatic;

            this.Size = size;
        }
        public void Load(Texture2D _texture)
        {
            texture = _texture;
        }
        public virtual void Update()
        {
            // "Bounding Box"
            rectangle = new Rectangle
            (
                (int)Position.X,
                (int)Position.Y,
                (int)Size.X,
                (int)Size.Y
            );
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 scale = new Vector2(Size.X / (float)texture.Width, Size.Y / (float)texture.Height);
            spriteBatch.Draw(texture, Position, null, Color.White, body.Rotation, new Vector2(texture.Width / 2.0f, texture.Height / 2.0f), scale, SpriteEffects.None, 0);
        }
    }
}

