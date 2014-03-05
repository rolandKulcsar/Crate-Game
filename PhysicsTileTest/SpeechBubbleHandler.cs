using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class SpeechBubbleHandler
    {
        Vector2 playerPosition;
        Vector2 offset;
        List<Texture2D> bubbles;
        List<float> positions;
        int bubbleNumber;
        bool drawed;

        public Vector2 PlayerPosition
        {
            set { playerPosition = value; }
        }
        public int BubbleNumber
        {
            set { bubbleNumber = value; }
        }
        public SpeechBubbleHandler(int size)
        {
            bubbles = new List<Texture2D>(size);
            positions = new List<float>(size);
            offset = new Vector2(-30, -100);
            bubbleNumber = 0;
            drawed = false;
        }

        public void InitPositions()
        {
            positions.Add(200);
            positions.Add(1000);
        }

        public void Load(ContentManager content)
        {
            for (int i = 0; i < bubbles.Count; i++)
            {
                bubbles[i] = content.Load<Texture2D>("Bubble" + i);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int i = 0;
            if (playerPosition.X > positions[i] && playerPosition.X < positions[i] + 50f)
            {
                spriteBatch.Draw(bubbles[bubbleNumber], playerPosition + offset, Color.White);
                bubbleNumber++;
                i++;
            }
        }
    }
}
