using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArmyOfOne
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Attributes
        private enum State { title, mainMenu, levelSelect, level, gameOver};
        private State gameState = State.mainMenu;
        private SpriteFont mainFont;

        //Integers
        private int round;
        private int spawnTimer;

        //The player class
        private Player player = new Player(150, 150, 50, 50);

        //Attributes for zombies
        private List<Enemy> enemies = new List<Enemy>();
        private Texture2D enemyImg;

        //Attributes for tiles
        private List<Tile> tiles = new List<Tile>();
        private Texture2D tileImg;

        //Keystates
        private KeyboardState kState;
        private KeyboardState prevState;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

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

            //Loading in the player's image
            player.image = Content.Load<Texture2D>("Player");

            //Loading in the image for all the enemies
            enemyImg = Content.Load<Texture2D>("Enemy");


            //Fonts
            mainFont = Content.Load<SpriteFont>("mainFont");


            //ADDING 2 TEST ZOMBIES REMOVE THIS CODE LATER
            enemies.Add(new Enemy(600, 50, 50, 50));
            enemies.Add(new Enemy(300, 500, 50, 50));
            enemies[0].image = enemyImg;
            enemies[1].image = enemyImg;

            //ADDING TEST TILES REMOVE THIS CODE LATER
            tiles.Add(new Tile(0, 0, 100, 100));
            tiles.Add(new Tile(100, 0, 100, 100));
            tiles.Add(new Tile(200, 0, 100, 100));
            tiles.Add(new Tile(300, 400, 100, 100));
            tiles[0].image = enemyImg;
            tiles[1].image = enemyImg;
            tiles[2].image = enemyImg;
            tiles[3].image = enemyImg;

            //Boundries
            tiles.Add(new Tile(-10, 0, 10, 700));
            tiles.Add(new Tile(1000, 0, 10, 700));
            tiles.Add(new Tile(0, -10, 1000, 10));
            tiles.Add(new Tile(0, 700, 1000, 10));

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            prevState = kState;
            kState = Keyboard.GetState();

            //Updating parts of the game depending on the state
            //IF THE GAME IS ACTIVE
            if (gameState == State.level)
            {
                //Updating the player
                player.update();

                //Updating the zombies
                for(int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].update(player);
                }

                //Seeing if the player collides with the walls
                for(int i = 0; i < tiles.Count; i++)
                {
                    player.wallCollision(tiles[i]);
                }

                //Seeing if the enemies collides with the walls
                for (int i = 0; i < tiles.Count; i++)
                {
                    for (int j = 0; j < enemies.Count; j++) {
                        enemies[j].wallCollision(tiles[i]);
                    }
                }
            }
            //IF THE GAME IS ON THE MAIN MENU
            else if(gameState == State.mainMenu)
            {
                if(isPressed(Keys.Enter))
                {
                    gameState = State.level;
                }
            }



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Beginning the spritebatch
            spriteBatch.Begin();

            //DRAWING THE GAMEPLAY
            if (gameState == State.level)
            {
                //Drawing the tiles
                for (int i = 0; i < tiles.Count; i++)
                {
                    tiles[i].draw(spriteBatch);
                }

                //Drawing the zombies
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].draw(spriteBatch);
                }

                //Drawing the player
                player.draw(spriteBatch);
            }

            //DRAWING THE MAIN MENU
            else if(gameState == State.mainMenu)
            {
                spriteBatch.DrawString(mainFont, "ARMY OF ONE", new Vector2(400, 100), Color.Red);
                spriteBatch.DrawString(mainFont, "Press Enter", new Vector2(400, 500), Color.Red);
            }

            //Ending the spriteBatch
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool isPressed(Keys k)
        {
            if(!prevState.IsKeyDown(k) && kState.IsKeyDown(k))
            {
                return true;
            }

            return false;
        }
    }
}
