using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    public class MWEnchantment : MWCard
    {
        private bool mandatory = false, revealed = false;
        private int revealcost = 0, magebind = 0;
        /// <summary>
        /// The base cost to cast this card. All enchantments have a mana cost of 2.
        /// </summary>
        public new int ManaCost
        {
            get
            {
                return 2;
            }
            set { }
        }
        /// <summary>
        /// The minumum range to cast this spell. All enchantments have a min range of 0.
        /// </summary>
        public new int MinRange
        {
            get
            {
                return 0;
            }
            set { }
        }
        /// <summary>
        /// The maximum range to cast this spell. All enchantments have a max range of 2.
        /// </summary>
        public new int MaxRange
        {
            get
            {
                return 2;
            }
            set { }
        }
        /// <summary>
        /// The secondary mana cost to reveal the enchantment and start its effect.
        /// </summary>
        public int RevealCost
        {
            get
            {
                return revealcost;
            }
            set
            {
                if (value < 0) revealcost = 0;
                else revealcost = value;
                FieldChanged();
            }
        }

        /// <summary>
        /// The amount of extra mana paid to reveal this enchantment if the target is a Mage.
        /// </summary>
        public int Magebind
        {
            get
            {
                return magebind;
            }
            set
            {
                if (value < 0) magebind = 0;
                else magebind = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// If true, the enchantment has to be revealed when its conditions are met.
        /// If a mandatory enchantment isn't paid for when it's revealed, it's destroyed.
        /// </summary>
        public bool Mandatory
        {
            get
            {
                return mandatory;
            }
            set
            {
                mandatory = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// If true, the enchantment has already been revealed. Revealed enchantments are
        /// face-up and their effects are on.
        /// </summary>
        public bool Revealed
        {
            get
            {
                return revealed;
            }
            set
            {
                revealed = value;
                FieldChanged();
            }
        }
        public MWEnchantment()
        {
        }
    }
}
