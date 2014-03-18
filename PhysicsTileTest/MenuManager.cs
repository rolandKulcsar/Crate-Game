using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class MenuManager
    {
        Texture2D pause;
        Texture2D gameOver;
        Texture2D menu;
        Vector2 nullVector;
        Button play;
        Button back;

        public MenuManager(GraphicsDevice graphicsDevice)
        {
            nullVector = Vector2.Zero;
            play = new Button(graphicsDevice);
            back = new Button(graphicsDevice);
        }
        
        public void Load(ContentManager Content)
        {
            menu = Content.Load<Texture2D>("Menu");
            pause = Content.Load<Texture2D>("Pause");
            //gameOver = Content.Load<Texture2D>("GameOver");
            play.Load(Content.Load<Texture2D>("Play"));
            back.Load(Content.Load<Texture2D>("Back"));
        }

        public void UpdateMenu()
        {
            play.Update();
        }

        public void UpdatePause()
        {
            back.Update();
        }

        public bool PlayIsClicked()
        {
            return play.IsClicked;
        }

        public bool BackIsClicked()
        {
            return back.IsClicked;
        }

        public void DrawMenuScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(menu, nullVector, Color.White);
            play.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void DrawGameOverScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(gameOver, nullVector, Color.White);
            spriteBatch.End();
        }

        public void DrawPauseScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(pause, nullVector, Color.White);
            back.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
