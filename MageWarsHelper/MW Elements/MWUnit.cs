using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    public class MWUnit : MWCard
    {
        private Dictionary<AttackElement, int> elementModifiers = new Dictionary<AttackElement, int>();
        private bool nonliving = false;
        private int life = 1, damage = 0, regen = 0, armor = 0;
        /// <summary>
        /// If true, only one copy can be in play at a time.
        /// </summary>
        public bool Legendary { get; set; }
        /// <summary>
        /// If true, can't be healed and can't regenerate, but can be repaired and have poison immunity.
        /// </summary>
        public bool Nonliving {
            get
            {
                return nonliving;
            }
            set
            {
                if (value)
                {
                    if (regen > 0)
                    {
                        regen = 0;
                        FieldChanged("Regenerate");
                    }
                    nonliving = true;
                }
                else nonliving = false;
                FieldChanged();
            }
        }
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
                FieldChanged("Alive");
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
                FieldChanged("Alive");
                FieldChanged();
            }
        }
        /// <summary>
        /// How much damage this unit heals during upkeep.
        /// </summary>
        public int Regenerate
        {
            get
            {
                return regen;
            }
            set
            {
                if (Nonliving || value < 0)
                {
                    regen = 0;
                }
                else regen = value;
            }
        }
        /// <summary>
        /// If false, armor will always be treated as 0.
        /// </summary>
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
                    FieldChanged("Incorporeal");
                } else if (!value && armor >= 0)
                {
                    armor = -1;
                }
                FieldChanged("Armor");
                FieldChanged();
            }
        }
        /// <summary>
        /// If true, can't have armor and 2s rolled on damage dice only count if the attack is ethereal.
        /// </summary>
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
                FieldChanged("HasArmor");
                FieldChanged("Armor");
                FieldChanged();
            }
        }
        /// <summary>
        /// The amount subtracted from incoming damage. Critical or direct damage ignores this.
        /// </summary>
        public int Armor
        {
            get
            {
                if (Incorporeal) return 0;
                return armor;
            }
            set
            {
                if (!HasArmor || value < 0) armor = 0;
                else armor = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// Whether this unit has any life remaining after damage. If it's switched
        /// from false to true, the unit revives with 0 damage.
        /// </summary>
        public bool Alive
        {
            get { return life > damage; }
            set
            {
                if (!value && life > damage) { 
                    damage = life;
                }
                else if (value && damage >= life) damage = 0;
                FieldChanged("Damage");
                FieldChanged();
            }
        }
        /// <summary>
        /// Figures out how many dice to add or remove from attacks of a given element that
        /// are made against this unit. If this unit is immune to that element, this returns
        /// null.
        /// </summary>
        /// <param name="element">The element to check</param>
        /// <returns>Null for immune, otherwise the number of dice to add or remove</returns>
        public int? GetElementModifier(AttackElement element)
        {
            if (elementModifiers.ContainsKey(element))
            {
                return elementModifiers[element];
            }
            else return null;
        }
        /// <summary>
        /// Increases or decreases the number of dice rolled by attacks of a given element against.
        /// this unit. Positive numbers increase, negative numbers decrease. Null makes this unit
        /// immune to that element.
        /// </summary>
        /// <param name="element">The element to modify</param>
        /// <param name="modifier">The number of dice that will be added. Negative to subtract, null to make immune.</param>
        public void SetElementModifier(AttackElement element, int? modifier)
        {
            if (modifier == null && elementModifiers.ContainsKey(element))
            {
                elementModifiers.Remove(element);
            } else if (modifier != null)
            {
                if (!elementModifiers.ContainsKey(element))
                {
                    elementModifiers.Add(element, (int)modifier);
                } else
                {
                    elementModifiers[element] = (int)modifier;
                }
            }
        }
        /// <summary>
        /// Checks if this unit is immune to an element.
        /// </summary>
        /// <param name="element">The element to check</param>
        /// <returns>True if immune, false otherwise</returns>
        public bool GetElementImmune(AttackElement element)
        {
            return !elementModifiers.ContainsKey(element);
        }
        /// <summary>
        /// Sets whether or not this unit is immune to an element.
        /// </summary>
        /// <param name="element">The element to set</param>
        /// <param name="immune">True to make immune, false to remove immunity</param>
        public void SetElementImmune(AttackElement element, bool immune)
        {
            if (immune && elementModifiers.ContainsKey(element))
            {
                elementModifiers.Remove(element);
            } else if (!immune && !elementModifiers.ContainsKey(element))
            {
                elementModifiers.Add(element, 0);
            }
        }
        /// <summary>
        /// The default constructor for a unit.
        /// </summary>
        public MWUnit()
        {
            foreach (AttackElement element in Enum.GetValues(typeof(AttackElement)))
            {
                elementModifiers.Add(element, 0);
            }
        }
    }
}
