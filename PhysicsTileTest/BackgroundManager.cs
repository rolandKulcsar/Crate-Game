using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class BackgroundManager
    {
        Texture2D background;
        Texture2D cloud;
        Vector2 nullVector;
        Vector2 cloudPosition;
        Vector2 cloudSpeed;
        Random random;
        int mapHeight;

        public BackgroundManager(int _mapHeight)
        {
            nullVector = Vector2.Zero;
            cloudSpeed = new Vector2(0.2f, 0f);
            random = new Random();
            mapHeight = _mapHeight;
            cloudPosition = new Vector2(100, random.Next(mapHeight / 4));
        }

        public void Load(ContentManager Content)
        {
            background = Content.Load<Texture2D>("Background");
            cloud = Content.Load<Texture2D>("Cloud");
        }

        public void Update()
        {
            cloudPosition += cloudSpeed;
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, nullVector, Color.White);
            spriteBatch.End();
        }

        public void DrawClouds(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cloud, cloudPosition, Color.White);
        }
    }
}
