using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class AnimatedPlayer : Player
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        private int currentFrame;
        private int totalFrame;

        public AnimatedPlayer(World world, Vector2 size, bool isStatic,int rows, int columns) : base(world, size, isStatic)
        {
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrame = Rows * Columns;
        }

        public override void Update()
        {
            if (currentFrame == totalFrame)
                currentFrame = 0;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position += new Vector2(3.2f, 0f);
                body.ApplyLinearImpulse(new Vector2(0.01f, 0f));
                currentFrame++;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position -= new Vector2(3.2f, 0f);
                body.ApplyLinearImpulse(new Vector2(0.01f, 0f));
            }
        }

        public void Draw2(SpriteBatch spriteBatch)
        {
            int width = texture.Width / Columns;
            int height = texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, body.Rotation, new Vector2(texture.Width / 2.0f, texture.Height / 2.0f), SpriteEffects.None, 0);
        }

    }
}
