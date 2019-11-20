using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    /// <summary>
    /// Attacks in Mage Wars. If a creature has an attack, it can use it during its turn.
    /// If an equipment has an attack, the equipped Mage can use it. Some attacks will be
    /// produced by Attack spells and other card effects.
    /// </summary>
    public class MWAttackAction
    {
        private int dice, piercing, minrange, maxrange;
        private bool ranged, zone, reach, unavoidable;
        /// <summary>
        /// If true, making this attack is a quick action.
        /// </summary>
        public bool Quick { get; set; } = true;
        /// <summary>
        /// The number of attack dice rolled when making this attack. This is the
        /// default, and may be increased or decreased by card effects. It can
        /// never be below 0.
        /// </summary>
        public int AttackDiceCount
        {
            get
            {
                return dice;
            }
            set
            {
                if (value < 0) dice = 0;
                else dice = value;
            }
        }
        /// <summary>
        /// The amount of the target's armor that doesn't work against this attack.
        /// It can never be below 0.
        /// </summary>
        public int Piercing
        {
            get
            {
                return piercing;
            }
            set
            {
                if (value < 0) piercing = 0;
                else piercing = value;
            }
        }
        /// <summary>
        /// The element of this attack. Not all attacks have an element.
        /// </summary>
        public AttackElement Element { get; set; }
        /// <summary>
        /// Whether or not this attack is ranged. Ranged attacks don't trigger
        /// damage barriers, can hit flying targets, and can potentially attack
        /// enemies in other zones, depending on their minimum and maximum range.
        /// </summary>
        public bool Ranged {
            get
            {
                return ranged;
            }
            set
            {
                ranged = value;
                if (ranged)
                {
                    reach = false;
                }
                else
                {
                    minrange = 0;
                    maxrange = 0;
                }
            } 
        }
        /// <summary>
        /// The minimum range of this attack. Can only go as low as 0, which refers 
        /// to the zone that the attacking card is in. MaxRange will always be greater
        /// than or equal to this. Only ranged attacks can have a MinRange above 0.
        /// </summary>
        public int MinRange
        {
            get { return minrange; }
            set
            {
                if (ranged && value > 0)
                {
                    if (value > maxrange)
                    {
                        maxrange = value;
                        minrange = value;
                    }
                    else minrange = value;
                }
            }
        }
        /// <summary>
        /// The maximum range to cast this spell. Can only go as low as 0, which refers 
        /// to the zone that the attacking card is in. MinRange will always be less
        /// than or equal to this. Only ranged attacks can have a MaxRange above 0.
        /// </summary>
        public int MaxRange
        {
            get { return maxrange; }
            set
            {
                if (ranged && value > 0)
                {
                    if (value < minrange)
                    {
                        minrange = value;
                        maxrange = value;
                    }
                    else maxrange = value;
                }
            }
        }
        /// <summary>
        /// If true, the attack hits everything in the target zone with a separate roll
        /// of the dice. Zone attacks are always Unavoidable. Only ranged attacks
        /// can be zone attacks.
        /// </summary>
        public bool ZoneAttack
        {
            get
            {
                return zone;
            }
            set
            {
                zone = (ranged && value);
                if (zone) unavoidable = true;
            }
        }
        /// <summary>
        /// If true, this attack can hit flying units even if the attacker is not
        /// flying. Ranged attacks can't have Reach.
        /// </summary>
        public bool Reach
        {
            get
            {
                return reach;
            }
            set
            {
                reach = (!ranged && value);
            }
        }
        /// <summary>
        /// If true, this attack works normally against incorporeal targets. Otherwise,
        /// 2s count as 0s when the attack dice are rolled.
        /// </summary>
        public bool Ethereal { get; set; }
        /// <summary>
        /// If true, defenses and other forms of attack negation don't work against this
        /// attack. Zone attacks are always Unavoidable.
        /// </summary>
        public bool Unavoidable
        {
            get
            {
                return unavoidable;
            }
            set
            {
                unavoidable = (zone || value);
            }
        }
    }
}
