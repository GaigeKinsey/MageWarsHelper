using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper.MW_Elements
{ 
    class MWDice
    {
        private Random rng = new Random();

        public int Result { get; set; }

        public bool Crit { get; set; }

        public MWDice()
        {
            Result = 0;
            Crit = false;
        }

        public MWDice Roll()
        {
            int roll = rng.Next(0, 6);
            Crit = roll >= 3;
            if(Crit)
            {
                roll -= 3;
            }
            Result = roll;

            return this;
        }



    }
}
