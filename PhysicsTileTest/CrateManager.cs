using FarseerPhysics.Dynamics;
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
    class CrateManager
    {
        private List<DrawablePhysicsObject> cratesAndBricks;
        private World world;
        private Random random;
        private int mapWidth;
        private Texture2D crateTexture;
        private Texture2D brickTexture;
        private MouseState prevMouseState;
        private MouseState currentMouseState;
        private HeadUpDisplay hud;

        public CrateManager(World _world, int _mapWidth, HeadUpDisplay _hud)
        {
            cratesAndBricks = new List<DrawablePhysicsObject>();
            world = _world;
            random = new Random();
            mapWidth = _mapWidth;
            hud = _hud;
        }

        public void Load(ContentManager Content)
        {
            crateTexture = Content.Load<Texture2D>("Crate");
            brickTexture = Content.Load<Texture2D>("Brick");
        }

        public void Update(Camera camera)
        {
            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if (currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                // Transfrom to World coords
                Vector2 worldPosition = Vector2.Transform(new Vector2(currentMouseState.X, currentMouseState.Y), Matrix.Invert(camera.Transform));
                SpawnCrate(worldPosition.X);
                hud.DecreaseMana(20);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                SpawnBrick();
            }
        }

        private void SpawnBrick()
        {
            DrawablePhysicsObject brick;
            brick = new DrawablePhysicsObject(world, new Vector2(19.0f, 14.0f), false);
            brick.Position = new Vector2(random.Next(mapWidth), 1);
            brick.Load(brickTexture);

            cratesAndBricks.Add(brick);
        }

        private void SpawnCrate(float x)
        {
            DrawablePhysicsObject crate;
            crate = new DrawablePhysicsObject(world, new Vector2(50.0f, 50.0f), false);
            crate.Position = new Vector2(x, 1);
            crate.Load(crateTexture);

            cratesAndBricks.Add(crate);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(DrawablePhysicsObject item in cratesAndBricks)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
