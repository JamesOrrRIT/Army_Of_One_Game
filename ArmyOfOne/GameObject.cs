using System;
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
    }
}
