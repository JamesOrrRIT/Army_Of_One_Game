using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ArmyOfOne
{
    class Player : GameObject
    {
        //Attributes
        int health;
        int score;
        int power;
        private KeyboardState kState;

        private int prevX;
        private int prevY;

        public Player(int x, int y, int width, int height, int hp, int pow) : base(x, y, width, height)
        {
            health = hp;
            power = pow;
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
                return;
            }
            if (kState.IsKeyDown(Keys.Right))
            {
                hitbox.X += 5;
                return;
            }
            if (kState.IsKeyDown(Keys.Up))
            {
                hitbox.Y -= 5;
                return;
            }
            if (kState.IsKeyDown(Keys.Down))
            {
                hitbox.Y += 5;
                return;
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

    }
}
