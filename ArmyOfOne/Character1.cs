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
        bool specialAvailable = true;
        public Character1(int x, int y, int width, int height) : base(x, y, width, height)
        {
            health = 200;
            damage = 5;
        }

        public override void special()
        {
            if (specialAvailable)
            {
                if (health > 100)
                {
                    health += 25;
                }
                else
                {
                    health += 50;
                }
                specialAvailable = false;
            }
        }
    }
}
