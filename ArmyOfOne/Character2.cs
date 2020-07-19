using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyOfOne
{
    class Character2 : Player
    {
        //character type 2
        public Character2(int x, int y, int width, int height) : base(x, y, width, height)
        {
            health = 300;
        }
    }
}
