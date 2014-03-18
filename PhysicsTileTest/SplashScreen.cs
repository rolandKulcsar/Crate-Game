using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class SplashScreen
    {
        private ContentManager content;
        private Texture2D farseerLogo;
        private Vector2 nullVector;
        private Color color;
        private TimeSpan duration;
        public bool End
        {
            get;
            set;
        }

        public SplashScreen()
        {
            nullVector = Vector2.Zero;
            color = new Color(255, 255, 255);
            duration = new TimeSpan(0, 0, 2);
            End = false;
        }

        public void Load(ContentManager _content)
        {
            content = _content;
            farseerLogo = content.Load<Texture2D>("farseer");
        }

        public void UnloadContent()
        {
            // játék közben erre már nincs szükség
            content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            duration -= gameTime.ElapsedGameTime;

            if(duration <= TimeSpan.Zero)
            {
                color.R--;
                color.G--;
                color.B--;
            }

            if (color.R == 0 && color.G == 0 && color.B == 0)
                End = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(farseerLogo, nullVector, color);
            spriteBatch.End();
        }
    }
}
