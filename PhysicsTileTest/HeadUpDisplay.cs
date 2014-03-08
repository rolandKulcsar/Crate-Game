using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class HeadUpDisplay
    {
        private Texture2D healthBar;
        private Texture2D manaBar;
        private Vector2 healthBarPosition;
        private Vector2 manaBarPosition;
        private Rectangle health;
        private Rectangle mana;

        public void Load(ContentManager content)
        {
            healthBar = content.Load<Texture2D>("Hp");
            //manaBar = content.Load<Texture2D>("Mana");
            healthBarPosition = new Vector2(50, 30);
            manaBarPosition = new Vector2(50, 50);
            health = new Rectangle(0, 0, healthBar.Width, healthBar.Height);
            //mana = new Rectangle(0, 0, manaBar.Width, healthBar.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBar, healthBarPosition, health, Color.White);
            //spriteBatch.Draw(manaBar, manaBarPosition, mana, Color.White);
        }

        public void DecreaseHealth(int amount)
        {
            health.Width -= amount;
        }
    }
}
