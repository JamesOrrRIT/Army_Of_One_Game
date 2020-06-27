using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ArmyOfOne
{
    class Tile : GameObject
    {
        public Tile(int x, int y, int width, int height) : base(x, y, width, height)
        {
            hitbox = new Rectangle(x, y, width, height);
        }
    }
}
