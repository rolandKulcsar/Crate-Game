#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using FarseerPhysics.Dynamics;
#endregion

// TODO
// kör menüből lehet kiválasztani hogy milyen dobozt akarsz
// eldöntéssel kell továbbjuttni
// célba lőni a dobozokkal
// paddle libikóka
// dobozok esnek és előlük kell rohanni
// ha doboz rádesik akkor sebződsz
// low gravity
// sebződésnél pixel shader

// Bubbles
// what now?
// cant use crates here :(
// uhh ahh
// wrong way
// bad boy
// out of crates
namespace PhysicsTileTest
{
    enum GameState { Splash, Menu, Playing, Pause, GameOver };
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        World world;
        Map map;
        Camera camera;
        Player player;
        CrateManager crateManager;
        HeadUpDisplay hud;
        PlayerBlocker playerBlocker;
        SplashScreen splashScreen;

        // Ezek a csúnyaságok csak teszt jelleggel vannak itt
        Texture2D back;
        Texture2D speechBubble;
        Texture2D cloud;
        Texture2D menu;
        Button play;
        Vector2 cloudpos = new Vector2(50, 100);
        Vector2 nullPosition = new Vector2(0, 0);
        Texture2D pauseTexture;
        Button backButton;

        GameState currentState;

        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            //graphics.PreferMultiSampling = true;        //2x Anti Aliasing
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.ApplyChanges();
            this.IsMouseVisible = true;
            this.Window.Title = "Crate Game";

            currentState = GameState.Splash;

            world = new World(new Vector2(0, 9.81f));
            map = new Map();
            camera = new Camera(GraphicsDevice.Viewport);
            hud = new HeadUpDisplay();
            play = new Button(graphics.GraphicsDevice);
            backButton = new Button(graphics.GraphicsDevice);
            splashScreen = new SplashScreen();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            map.World = world;
            Tile.Content = Content;

            player = new Player(world, new Vector2(60f, 84f), false);
            player.Load(Content.Load<Texture2D>("Player1"));
            player.Position = new Vector2(100, 50);

            hud.Load(Content);

            back = Content.Load<Texture2D>("Background");
            speechBubble = Content.Load<Texture2D>("Speech");
            cloud = Content.Load<Texture2D>("Cloud0");
            menu = Content.Load<Texture2D>("Menu");
            play.Load(Content.Load<Texture2D>("Play"));
            backButton.Load(Content.Load<Texture2D>("Back"));
            pauseTexture = Content.Load<Texture2D>("Pause");
            splashScreen.Load(Content);

            map.Generate(new int[,]{
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 2, 2, 2, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 5, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7},
                {2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6},
            }, 64);

            // csak a map felépítése után lehet létrehozni
            crateManager = new CrateManager(world, map.Width, hud);
            crateManager.Load(Content);
            playerBlocker = new PlayerBlocker(player, map.Width);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            switch(currentState)
            {
                case GameState.Splash:
                    splashScreen.Update(gameTime);
                    if (splashScreen.end)
                        currentState = GameState.Menu;
                    break;

                case GameState.Menu:
                    if (play.IsClicked)
                        currentState = GameState.Playing;
                    play.Update();
                    break;

                case GameState.Playing:
                    player.Update();
                    camera.Update(player.Position, map.Width, map.Height);
                    crateManager.Update(camera);
                    playerBlocker.Update();
                    cloudpos += new Vector2(0.2f, 0f);
                    world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
                    foreach (Tile tile in map.Tiles)
                    {
                        if (tile.ID == 7)
                        {
                            tile.Update();
                            if (player.Rectangle.Intersects(tile.Rectangle))
                                currentState = GameState.GameOver;
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        currentState = GameState.Pause;
                    break;

                case GameState.Pause:
                    if (backButton.IsClicked)
                        currentState = GameState.Playing;
                    backButton.Update();
                    break;

                case GameState.GameOver:
                    Console.WriteLine("Game over");
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            switch (currentState)
            {
                case GameState.Splash:
                    splashScreen.Draw(spriteBatch);
                    break;

                case GameState.Menu:
                    spriteBatch.Begin();
                    spriteBatch.Draw(menu, nullPosition, Color.White);
                    play.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.Playing:
                    // Background
                    spriteBatch.Begin();
                    spriteBatch.Draw(back, nullPosition, Color.White);
                    spriteBatch.End();

                    // Player,Crates etc.
                    spriteBatch.Begin(SpriteSortMode.Deferred,
                                      BlendState.AlphaBlend,
                                      null, null, null, null,
                                      camera.Transform);
                    crateManager.Draw(spriteBatch);
                    map.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    if (player.Position.X > 200 && player.Position.X < 250)
                        spriteBatch.Draw(speechBubble, player.Position + new Vector2(-30, -100), Color.White);
                    spriteBatch.Draw(cloud, cloudpos, Color.White);
                    spriteBatch.End();

                    // HUD
                    spriteBatch.Begin();
                    hud.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.Pause:
                    spriteBatch.Begin();
                    spriteBatch.Draw(pauseTexture, nullPosition, Color.White);
                    backButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
