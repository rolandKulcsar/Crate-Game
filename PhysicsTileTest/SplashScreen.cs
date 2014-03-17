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
        private Vector2 position;
        private Color color;
        private TimeSpan duration;
        public bool end
        {
            get;
            set;
        }

        public SplashScreen()
        {
            position = new Vector2(0, 0);
            color = new Color(255, 255, 255);
            duration = new TimeSpan(0, 0, 2);
            end = false;
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
                end = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(farseerLogo, position, color);
            spriteBatch.End();
        }
    }
}
