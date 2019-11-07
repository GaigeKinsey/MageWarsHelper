using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    public class MWMage : MWCreature
    {
        public MWMage()
        {
            Life = 36;
            Channeling = 9;
            Mana = 10;
            Damage = 0;
        }
    }
}
