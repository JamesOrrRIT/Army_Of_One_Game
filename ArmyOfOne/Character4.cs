using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyOfOne
{
    class Character4 : Player
    {
        //character type 4
        public Character4(int x, int y, int width, int height) : base(x, y, width, height)
        {
            health = 250;
        }
    }
}
