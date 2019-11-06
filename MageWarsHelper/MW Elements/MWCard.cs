using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    public class MWCard
    {
        public int ManaCost { get; set; }
        private int minrange = 0, maxrange = 0, level = 1;
        public int MinRange
        {
            get { return minrange; }
            set
            {
                if (value < 0) minrange = 0;
                else if (value > maxrange)
                {
                    minrange = value;
                    maxrange = value;
                }
                else minrange = value;
            }
        }
        public int MaxRange
        {
            get { return maxrange; }
            set
            {
                if (value < minrange)
                {
                    if (value < 0)
                    {
                        minrange = 0;
                        maxrange = 0;
                    }
                    else
                    {
                        minrange = value;
                        maxrange = value;
                    }
                }
                else maxrange = value;
            }
        }
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                if (value < 1) level = 1;
                else level = value;
            }
        }
        public bool Quick { get; set; }

        public MWCard()
        {

        }
    }
}
