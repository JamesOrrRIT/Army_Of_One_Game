using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyOfOne
{
    class Character1 : Player
    {
        //character type 1
        public Character1(int x, int y, int width, int height) : base(x, y, width, height)
        {
            health = 200;
        }
    }
}
