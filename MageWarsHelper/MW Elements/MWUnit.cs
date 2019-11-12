using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    public class MWUnit : MWCard
    {
        /// <summary>
        /// If true, can't have armor and 2s rolled on damage dice only count if the attack is ethereal.
        /// </summary>
        /// <summary>
        /// If true, only one copy can be in play at a time.
        /// </summary>
        public bool Legendary { get; set; }
        /// <summary>
        /// If true, can't be healed and can't regenerate, but can be repaired.
        /// </summary>
        public bool Nonliving { get; set; }
        private int life = 1, damage = 0, mana = 0, channeling = 0, armor = 0;
        /// <summary>
        /// The maximum damage that this unit can take.
        /// </summary>
        public int Life
        {
            get { return life; }
            set
            {
                if (value < 1) life = 1;
                else life = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// How much damage this unit already has.
        /// </summary>
        public int Damage
        {
            get { return damage; }
            set
            {
                if (value < 0) damage = 0;
                else damage = value;
                FieldChanged();
            }
        }
        public bool HasMana
        {
            get
            {
                return mana + channeling > 0;
            }
            set
            {
                if (!value)
                {
                    channeling = 0;
                    mana = 0;
                } else if (value && channeling == 0)
                {
                    channeling = 1;
                }
            }
        }
        public int Mana
        {
            get { return mana; }
            set
            {
                if (value < 0) mana = 0;
                else mana = value;
                FieldChanged();
            }
        }
        public int Channeling
        {
            get { return channeling; }
            set
            {
                if (value < 0) channeling = 0;
                else channeling = value;
                FieldChanged();
            }
        }
        public bool HasArmor
        {
            get
            {
                return armor > 0;
            }
            set
            {
                if (value && armor < 0)
                {
                    armor = 0;
                    Incorporeal = false;
                } else if (!value && armor >= 0)
                {
                    armor = -1;
                }
            }
        }
        public bool Incorporeal {
            get 
            {
                return armor > -1;
            }
            set
            {
                if (value)
                {
                    armor = -2;
                } else
                {
                    if (armor < 0)
                    {
                        armor = -1;
                    }
                }
            }
        }
        public int Armor
        {
            get 
            {
                if (Incorporeal) return 0;
                return armor; 
            }
            set
            {
                if (Incorporeal || value < 0) armor = 0;
                else armor = value;
                FieldChanged();
            }
        }
        public bool Alive
        {
            get { return life > damage; }
            set
            {
                if (!value && life > damage) damage = life;
                else if (value && damage >= life) damage = 0;
                FieldChanged();
            }
        }
    }
}
