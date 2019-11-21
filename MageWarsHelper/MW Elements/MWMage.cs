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
            Name = "Mage";
            Subtypes.Add(Subtype.HUMAN);
            Life = 36;
            Channeling = 9;
            Mana = 10;
            Damage = 0;
        }
        /// <summary>
        /// The base cost to cast this card. A Mage's ManaCost is -1, to indicate that it can't be cast.
        /// </summary>
        public new int ManaCost
        {
            get
            {
                return -1;
            }
        }
        /// <summary>
        /// The minumum range to cast this spell. A Mage's MinRange is -1, to indicate that it can't be cast.
        /// </summary>
        public new int MinRange
        {
            get
            {
                return -1;
            }
        }
        /// <summary>
        /// The maximum range to cast this spell. A Mage's MaxRange is -1, to indicate that it can't be cast.
        /// </summary>
        public new int MaxRange
        {
            get
            {
                return -1;
            }
        }
        /// <summary>
        /// The level of this spell for card effects. A Mage's level is always treated as 6.
        /// </summary>
        public new int Level
        {
            get
            {
                return 6;
            }
        }
        public override string CardType => "Mage";
    }
}
