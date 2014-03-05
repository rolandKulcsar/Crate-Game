using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class Animation
    {
        Texture2D texture;
        float frameTime;
        public int frameWidth;
        public int frameHeight
        {
            get { return texture.Height; }
        }
        public Texture2D Texture
        {
            get { return texture; }
        }
        public float FrameTime
        {
            get { return frameTime; }
        }
        public int frameCount;
        bool isLooping;
        public bool IsLooping
        {
            get { return isLooping; }
        }
        public Animation(Texture2D _texture, int _frameWidth, float _frameTime, bool _isLooping)
        {
            texture = _texture;
            frameWidth = _frameWidth;
            frameTime = _frameTime;
            isLooping = _isLooping;
            frameCount = texture.Width / frameWidth;
        }
    }
}
