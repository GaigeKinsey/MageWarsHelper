using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    public enum EquipmentLocation
    {
        HELMET,
        CLOAK,
        CHEST,
        GLOVES,
        BOOTS,
        AMULET,
        RING,
        BELT,
        SHIELD,
        WEAPON,
        ONE_HAND,
        BOTH_HANDS
    }
    public class MWEquipment : MWCard
    {
        private EquipmentLocation slot = EquipmentLocation.RING;
        /// <summary>
        /// The slot that this Equipment goes in. Note that ONE_HAND
        /// and BOTH_HANDS aren't true slots, but rather overlap with
        /// either SHIELD, WEAPON, or both.
        /// </summary>
        public EquipmentLocation Slot { 
            get {
                return slot;
            }
            set
            {
                slot = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// The minumum range to cast this spell. All equipment has a min range of 0.
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
        /// The maximum range to cast this spell. All equipment has a max range of 2.
        /// </summary>
        public new int MaxRange
        {
            get
            {
                return 2;
            }
            set { }
        }
        public override string CardType => "Equipment";
    }
}
