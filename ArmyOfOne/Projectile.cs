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
    class Projectile : GameObject
    {
        //Fields
        private int speed = 5;
        private int damage;

        private bool fromPlayer;
        private int states = 0;

        //properties
        public int States
        {
            get { return states; }
            set { states = value; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public bool FromPlayer
        {
            get { return fromPlayer; }
        }

        public Projectile(int x, int y, int s, int d) : base(x, y, s, d)
        {
            hitbox = new Rectangle(x, y, 5, 5);
            speed = s;
            damage = d;

        }

        //if the states meet then move the projectile
        public void update()
        {
            switch (states)
            {
                case (1):
                    //Up
                    hitbox.Y -= speed;
                    break;
                case (2):
                    //Down
                    hitbox.Y += speed;
                    break;
                case (3):
                    //Right
                    hitbox.X += speed;
                    break;
                case (4):
                    //Left
                    hitbox.X -= speed;
                    break;
            }
        }
    }
}
