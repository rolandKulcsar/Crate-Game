using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class LogoScreen
    {
        private ContentManager content;
        private TimeSpan duration;
        private Texture2D xnaLogo;
        private Texture2D farseerLogo;
        private Vector2 position;

        public LogoScreen(TimeSpan _duration)
        {
            duration = _duration;
            position = new Vector2(0, 0);
        }

        public void Load(ContentManager _content)
        {
            content = _content;
            xnaLogo = content.Load<Texture2D>("xna");
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
            if (duration <= TimeSpan.Zero)
                ExitScreen();
        }

        public void ExitScreen()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(xnaLogo, position, Color.White);
            spriteBatch.End();
        }



    }
}
