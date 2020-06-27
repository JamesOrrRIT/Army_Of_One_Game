using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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
