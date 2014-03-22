using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class Player : DrawablePhysicsObject
    {
        Vector2 movingImpulse;
        Vector2 jumpingImpulse;
        Vector2 movingVector;
        const float jumpInterval = 1f;
        DateTime previousJump;
        KeyboardState prevKeyboardState;
        SoundEffect jumpEffect;

        public Player(World world, Vector2 size, bool isStatic) : base(world, size, isStatic)
        {
            // próbálgatással beállított értékek
            movingImpulse = new Vector2(0.01f, 0f);
            jumpingImpulse = new Vector2(0f, -2.4f);
            movingVector = new Vector2(3.2f, 0f);
            previousJump = DateTime.Now;
            body.FixedRotation = true;
        }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Player1");
            jumpEffect = Content.Load<SoundEffect>("Jump");
        }

        public override void Update()
        {
            // Bounding Box
            rectangle = new Rectangle
            (
                (int)Position.X,
                (int)Position.Y,
                (int)Size.X,
                (int)Size.Y
            );

            KeyboardState keyboardState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position += movingVector;
                body.ApplyLinearImpulse(movingImpulse);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position -= movingVector;
                body.ApplyLinearImpulse(-movingImpulse);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !prevKeyboardState.IsKeyDown(Keys.W))
            {
                if ((DateTime.Now - previousJump).TotalSeconds >= jumpInterval)
                {
                    jumpEffect.Play();
                    body.ApplyLinearImpulse(jumpingImpulse);
                    previousJump = DateTime.Now;
                }
            }

            prevKeyboardState = keyboardState;
        }
    }
}
