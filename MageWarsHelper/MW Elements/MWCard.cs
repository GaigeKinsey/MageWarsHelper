using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    /// <summary>
    /// Schools of magic, which determine how much extra a Mage has to pay
    /// to put spells in their spellbooks. A MWCard may have more than one
    /// SpellSchool, and each has its own level paired to it.
    /// </summary>
    public enum SpellSchool
    {
        ARCANE,
        NATURE,
        MIND,
        WAR,
        LIGHT,
        DARK,
        FIRE,
        WATER,
        EARTH,
        WIND
    }
    /// <summary>
    /// Cards in Mage Wars. Aside from the Mage, they start in the player's spellbook.
    /// </summary>
    public class MWCard : INotifyPropertyChanged
    {
        private string serialnum, name;
        /// <summary>
        /// Every different card's Serial Number is unique. Duplicate cards have the same serial number.
        /// </summary>
        public string SerialNumber {
            get
            {
                return serialnum;
            }
            set
            {
                serialnum = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// The card's name, seen at the top.
        /// </summary>
        /// 
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// The base cost to cast this card.
        /// </summary>
        private int manacost = 5, minrange = 0, maxrange = 0;
        private Dictionary<SpellSchool, int> levels = new Dictionary<SpellSchool, int>();

        public event PropertyChangedEventHandler PropertyChanged;
        public int ManaCost {
            get
            {
                return manacost;
            }
            set
            {
                manacost = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// The minimum range to cast this spell. Can only go as low as 0.
        /// MaxRange will always be greater than or equal to this.
        /// </summary>
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
                FieldChanged();
            }
        }
        /// <summary>
        /// The maximum range to cast this spell. Can only go as low as 0.
        /// MinRange will always be less than or equal to this.
        /// </summary>
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
                FieldChanged();
            }
        }
        /// <summary>
        /// The level of this spell for card effects. Use CostToLearn() for
        /// putting this into a spellbook.
        /// </summary>
        public int Level
        {
            get
            {
                int level = 0;
                foreach (var bySchool in levels)
                {
                    level += bySchool.Value;
                }
                return level;
            }
        }
        private bool quick = true, novice = false, epic = false;
        /// <summary>
        /// If true, casting this spell is a quick action.
        /// </summary>
        public bool Quick {
            get
            {
                return quick;
            }
            set
            {
                quick = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// If
        /// </summary>
        public bool Novice
        {
            get
            {
                return novice;
            }
            set
            {
                novice = value;
                FieldChanged();
            }
        }
        public bool Epic
        {
            get
            {
                return epic;
            }
            set
            {
                epic = value;
                FieldChanged();
            }
        }
        public MWCard()
        {

        }

        public void ApplySchoolLevel(SpellSchool school, int level)
        {
            if (levels.ContainsKey(school))
            {
                levels[school] += level;
                if (levels[school] <= 0) levels.Remove(school);
            } else if (level > 0)
            {
                levels.Add(school, level);
            }
            PropertyChanged(this, new PropertyChangedEventArgs("Level"));
        }

        public int CostToLearn(ICollection<SpellSchool> trained, ICollection<SpellSchool> restricted)
        {
            if (Novice) return 1;
            int cost = 0;
            foreach (var bySchool in levels)
            {
                if (trained.Contains(bySchool.Key))
                {
                    cost += bySchool.Value;
                } else if (restricted.Contains(bySchool.Key))
                {
                    cost += bySchool.Value * 3;
                } else
                {
                    cost += bySchool.Value * 2;
                }
            }
            return cost;
        }
        protected void FieldChanged([CallerMemberName] string field = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(field));
            }
        }
    }
}
