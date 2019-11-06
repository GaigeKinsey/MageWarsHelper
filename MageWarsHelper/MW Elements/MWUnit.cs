using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    public class MWUnit : MWCard
    {
        public bool Incorporeal { get; set; }
        private int life = 1, damage = 0, mana = 0, channeling = 0, armor = 0;
        public int Life
        {
            get { return life; }
            set
            {
                if (value < 1) life = 1;
                else life = value;
            }
        }
        public int Damage
        {
            get { return damage; }
            set
            {
                if (value < 0) damage = 0;
                else damage = value;
            }
        }
        public int Mana
        {
            get { return mana; }
            set
            {
                if (value < 0) mana = 0;
                else mana = value;
            }
        }
        public int Channeling
        {
            get { return channeling; }
            set
            {
                if (value < 0) channeling = 0;
                else channeling = value;
            }
        }
        public int Armor
        {
            get { return armor; }
            set
            {
                if (Incorporeal || value < 0) armor = 0;
                else armor = value;
            }
        }
        public bool Alive
        {
            get { return life > damage; }
            set
            {
                if (!value && life > damage) damage = life;
                else if (value && damage >= life) damage = 0;
            }
        }
    }
}
