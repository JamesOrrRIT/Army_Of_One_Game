using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArmyOfOne
{
    class Player : GameObject
    {
        //Attributes
        int health = 200;
        int score = 0;
        int direction = 3;
        private KeyboardState kState;

        private int prevX;
        private int prevY;

        public Player(int x, int y, int width, int height) : base(x, y, width, height)
        {
            hitbox = new Rectangle(x, y, width, height);
        }

        //Updating the player
        public void update()
        {
            //Saving the previous coordinants
            prevX = hitbox.X;
            prevY = hitbox.Y;

            //Moving the image with the arrow keys
            kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Left))
            {
                hitbox.X -= 5;
                direction = 4;
                return;
            }
            if (kState.IsKeyDown(Keys.Right))
            {
                hitbox.X += 5;
                direction = 3;
                return;
            }
            if (kState.IsKeyDown(Keys.Up))
            {
                hitbox.Y -= 5;
                direction = 1;
                return;
            }
            if (kState.IsKeyDown(Keys.Down))
            {
                hitbox.Y += 5;
                direction = 2;
                return;
            }

        }

        public void drawHUD(SpriteBatch spriteBatch, SpriteFont font)
        {
            //Drawing information such as health and armor

            //Health bar
            if (health >= 0 && health <= 100)
            {
                spriteBatch.Draw(image, new Rectangle(50, 600, health * 2, 50), new Rectangle(0, 0, image.Width, image.Height), Color.White);
            } else if(health > 100)
            {
                spriteBatch.Draw(image, new Rectangle(50, 600, 200, 50), new Rectangle(0, 0, image.Width, image.Height), Color.White);
                spriteBatch.Draw(image, new Rectangle(50, 540, (health-100) * 2, 50), new Rectangle(0, 0, image.Width, image.Height), Color.White);
            }
        }

        public void hurt(int damage)
        {
            //Damaging armor
            if(health > 100)
            {
                health -= damage;
            } else
            {
                health -= damage * 2;
            }
        }

        public void wallCollision(GameObject g)
        {
            if(g.hitbox.Intersects(hitbox))
            {
                hitbox.X = prevX;
                hitbox.Y = prevY;
            }
        }

        //if the player's health is less than 0 then return the state to true
        public bool Die()
        {
            if (this.health <= 0)
            {
                return true;
            }
            return false;
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public void reset()
        {
            health = 200;
            hitbox.X = 150;
            hitbox.Y = 150;
            score = 0;
        }

        public int getDirection()
        {
            return direction;
        }
    }
}
