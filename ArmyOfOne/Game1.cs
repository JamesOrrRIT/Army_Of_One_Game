using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        //Texture2D projectileImg;
        //Attributes
        private enum State { title, mainMenu, levelSelect, level, gameOver };
        private State gameState = State.mainMenu;
        private SpriteFont mainFont;

        //Integers
        private int round;
        private int spawnTimer;

        //The player class
        private Player player = new Player(150, 150, 50, 50);
        private Projectile projectile = new Projectile(150, 150, 5, 5);

        //Attributes for zombies
        private List<Enemy> enemies = new List<Enemy>();
        private Texture2D enemyImg;
        Random rand = new Random();

        //Attributes for projectiles
        private List<Projectile> projectiles = new List<Projectile>();

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

            //Making the level
            try
            {
                StreamReader sr = new StreamReader("customLevelGrid.txt");
                String line = sr.ReadLine();
                int row = 0;

                //Going through each line
                while (line != null)
                {
                    char[] letters = line.ToCharArray();

                    for(int i = 0; i < line.Length; i+= 2)
                    {
                        if(letters[i] == '1')
                        {
                            tiles.Add(new Tile(50*i, 100*row, 100, 100));
                            tiles[tiles.Count - 1].image = enemyImg;
                        }
                    }

                    //Getting the next line
                    line = sr.ReadLine();
                    row += 1;
                }

                sr.Close();
            }
            catch
            {

            }
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

            //Getting key states
            prevState = kState;
            kState = Keyboard.GetState();

            //Updating parts of the game depending on the state
            switch (gameState)
            {

                //UPDATING THE GAMEPLAY
                case State.level:
                    //ENDING A ROUND
                    if (enemies.Count == 0)
                    {
                        //Starting the next round
                        for (int i = 0; i < (round + 1) * 5; i++)
                        {
                            int val = rand.Next(2);
                            int valY = rand.Next(650);
                            Console.WriteLine(valY);
                            if (val == 0)
                            {
                                enemies.Add(new Enemy(0, valY, 50, 50));
                                enemies[enemies.Count - 1].image = enemyImg;
                            }
                            else
                            {
                                enemies.Add(new Enemy(950, valY, 50, 50));
                                enemies[enemies.Count - 1].image = enemyImg;
                            }
                        }
                        round += 1;
                    }

                    //Updating the player
                    player.update();

                    //Updating the zombies
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].update(player);
                    }

                    //Seeing if the player collides with the walls
                    for (int i = 0; i < tiles.Count; i++)
                    {
                        player.wallCollision(tiles[i]);
                    }

                    //Seeing if the enemies collides with the walls
                    for (int i = 0; i < tiles.Count; i++)
                    {
                        for (int j = 0; j < enemies.Count; j++)
                        {
                            enemies[j].wallCollision(tiles[i]);
                        }
                    }

                    //Seeing if the player is hit by any zombies
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].intersects(player))
                        {
                            player.hurt(1);
                        }
                    }

                    //Shooting a projectile
                    if (isPressed(Keys.Space))
                    {
                        projectiles.Add(new Projectile(player.hitbox.X + 22, player.hitbox.Y + 22, 15, 25));
                        projectiles[projectiles.Count - 1].States = player.getDirection();
                        projectiles[projectiles.Count - 1].image = enemyImg;
                    }

                    //Updating the projectiles
                    for (int i = 0; i < projectiles.Count; i++)
                    {
                        projectiles[i].update();
                        for (int j = 0; j < enemies.Count; j++)
                        {
                            //Injuring enemies
                            if (enemies[j].intersects(projectiles[i]))
                            {
                                enemies[j].damage(projectiles[i].Damage);
                                projectiles[i].Damage = 0;
                            }
                        }
                    }

                    //Removing dead zombies and projectiles
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].Die())
                        {
                            enemies.RemoveAt(i);
                            player.Score += 1;
                        }
                    }
                    for (int i = 0; i < projectiles.Count; i++)
                    {
                        if (projectiles[i].Damage == 0)
                        {
                            projectiles.RemoveAt(i);
                        }
                    }

                    //Knowing if the game is over
                    if (player.Die())
                    {
                        gameState = State.gameOver;
                    }

                    break;

                //IF THE GAME IS ON THE MAIN MENU
                case State.mainMenu:
                    if (isPressed(Keys.Enter))
                    {
                        gameState = State.level;
                        round = 0;
                        enemies.Clear();
                        projectiles.Clear();


                        //Resetting the game
                        player.reset();
                    }
                    break;

                case State.gameOver:
                    if (isPressed(Keys.Enter))
                    {
                        gameState = State.mainMenu;
                    }
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Beginning the spritebatch
            spriteBatch.Begin();

            switch (gameState)
            {
                //Drawing the title screen
                case State.title:
                    spriteBatch.DrawString(mainFont, "ARMY OF ONE", new Vector2(400, 100), Color.Red);
                    break;

                //Drawing the main menu
                case State.mainMenu:
                    spriteBatch.DrawString(mainFont, "ARMY OF ONE", new Vector2(400, 100), Color.Red);
                    spriteBatch.DrawString(mainFont, "Press Enter", new Vector2(400, 500), Color.Red);
                    break;

                //Drawing the level select menu
                case State.levelSelect:

                //Drawing the gameplay
                case State.level:
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

                    //Drawing the zombies's health bars
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].drawBar(spriteBatch);
                    }

                    //Drawing the projectiles
                    for (int i = 0; i < projectiles.Count; i++)
                    {
                        projectiles[i].draw(spriteBatch);
                    }

                    //Drawing the player
                    player.draw(spriteBatch);

                    //Drawing the player's hud
                    player.drawHUD(spriteBatch, mainFont);

                    //Drawing the round counter
                    spriteBatch.DrawString(mainFont, "ROUND " + round, new Vector2(400, 100), Color.Red);
                    projectile.draw(spriteBatch);

                    break;

                //projectile


                //Drawing the game over menu
                case State.gameOver:
                    spriteBatch.DrawString(mainFont, "GAME OVER", new Vector2(400, 100), Color.Red);
                    spriteBatch.DrawString(mainFont, "FINAL SCORE: " + player.Score, new Vector2(400, 300), Color.Red);
                    spriteBatch.DrawString(mainFont, "Press enter to play again...", new Vector2(400, 500), Color.Red);
                    break;
            }

            //Ending the spriteBatch
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool isPressed(Keys k)
        {
            if (!prevState.IsKeyDown(k) && kState.IsKeyDown(k))
            {
                return true;
            }

            return false;
        }
    }
}