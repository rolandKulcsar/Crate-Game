using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    struct AnimationPlayer
    {
        Animation animation;
        public Animation Animation
        {
            get { return animation; }
        }
        int frameIndex;
        public int FrameIndex
        {
            get { return frameIndex; }
            set { frameIndex = value; }
        }
        public float timer;
        public Vector2 origin
        {
            get { return new Vector2(animation.frameWidth / 2, animation.frameHeight); }
        }
        public void PlayAnimation(Animation _animation)
        {
            if (animation == _animation)                                                        // LOL ???????
                return;

            animation = _animation;
            frameIndex = 0;
            timer = 0;
        }

        public void Draw(GameTime gameTime,SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects, float rotation, Vector2 scale, float depth)
        {
            if (Animation == null)
                throw new NotSupportedException("There is no animation!");

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            while(timer >= animation.FrameTime)
            {
                timer -= animation.FrameTime;

                if (animation.IsLooping)
                    frameIndex = (frameIndex + 1) % animation.frameCount;
                else
                    frameIndex = Math.Min(frameIndex + 1, animation.frameCount - 1);
            }

            Rectangle rectangle = new Rectangle(frameIndex * Animation.frameWidth, 0, animation.frameWidth, animation.frameHeight);

            spriteBatch.Draw(Animation.Texture, position, rectangle, Color.White, rotation, new Vector2(animation.Texture.Width / 2,animation.Texture.Height / 2), scale, spriteEffects, depth);
        }
    }
}
