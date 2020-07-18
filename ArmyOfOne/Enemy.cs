using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArmyOfOne
{
    class Enemy : GameObject
    {
        //Attributes
        private int health;
        private bool active = true;
        private int prevX;
        private int prevY;

        private double chaseAngle;


        public Enemy(int x, int y, int width, int height) : base(x, y, width, height)
        {
            hitbox = new Rectangle(x, y, width, height);
            health = 100;
        }

        //UPDATING THE ENEMY
        public void update(Player player) //Passes in the player so it can interact with it
        {
            //Saving the previous coordinants
            prevX = hitbox.X;
            prevY = hitbox.Y;

            //CHASING THE PLAYER
            if(hitbox.X < player.hitbox.X)
            {
                hitbox.X += 1;
            } else if(hitbox.X > player.hitbox.X)
            {
                hitbox.X -= 1;
            }

            if(hitbox.Y < player.hitbox.Y)
            {
                hitbox.Y += 1;
            } else if(hitbox.Y > player.hitbox.Y)
            {
                hitbox.Y -= 1;
            }
        }

        //Drawing the health bar
        public void drawBar(SpriteBatch spriteBatch)
        {
            //Drawing the health bar
            if(health < 101)
            {
                spriteBatch.Draw(image, new Rectangle(hitbox.X, hitbox.Y - 15, health/2, 10), new Rectangle(0, 0, image.Width, image.Height), Color.White);
            }
        }

        public void wallCollision(GameObject g)
        {
            if (g.hitbox.Intersects(hitbox))
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
    }
}
