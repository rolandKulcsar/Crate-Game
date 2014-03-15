using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class Button 
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;
        private Rectangle mouseRectangle;
        private Color color;
        private bool down;
        private bool isClicked;
        private Vector2 size;

        public bool IsClicked
        {
            get { return isClicked; }
        }

        public Button(GraphicsDevice graphicsDevice)
        {
            size = new Vector2(160, 160);
            position = new Vector2(graphicsDevice.Viewport.Width / 2 - 80, graphicsDevice.Viewport.Height / 2 + 50);
            color = new Color(255, 255, 255, 255);
        }
        public void Load(Texture2D _texture)
        {
            texture = _texture;
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();

            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if(mouseRectangle.Intersects(rectangle))
            {
                if (color.A == 255) 
                    down = false;
                if (color.A == 0) 
                    down = true;

                if (down) 
                    color.A += 3;
                else 
                    color.A -= 3;

                if (mouse.LeftButton == ButtonState.Pressed)
                    isClicked = true;
            }
            else if(color.A < 255)
            {
                color.A += 3;
                isClicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
    }
}