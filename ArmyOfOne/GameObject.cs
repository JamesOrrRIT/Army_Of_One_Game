﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArmyOfOne
{
    public abstract class GameObject
    {
        //Attributes
        public Rectangle hitbox;
        public Texture2D image;

        public GameObject(int x, int y, int width, int height)
        {
            hitbox = new Rectangle(x, y, width, height);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            //Drawing the image in the Rectangle hitbox
            if (image != null) //This if statement makes sure the image properly loaded
            {
                spriteBatch.Draw(image, hitbox, new Rectangle(0, 0, image.Width, image.Height), Color.White);
            }
        }

        //check the bullet collision with enemy
        public virtual bool checkCollision(GameObject obj1)
        {
            return false;
        }

        //Checks to see if the object intersects with another game object
        public bool intersects(GameObject obj)
        {
            if(obj.hitbox.Intersects(this.hitbox))
            {
                return true;
            }

            return false;
        }
    }
}
