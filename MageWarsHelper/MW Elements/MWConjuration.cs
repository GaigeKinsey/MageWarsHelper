using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    class MWConjuration : MWUnit
    {
        private bool zoneexclusive = false, wall = false, losblock = false, pasblock = false, pasattack = false;
        /// <summary>
        /// If true, no other conjurations can be in its zone.
        /// </summary>
        public bool ZoneExclusive {
            get
            {
                return zoneexclusive;
            }
            set
            {
                zoneexclusive = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// Walls are placed on zone borders.
        /// </summary>
        public bool Wall
        {
            get
            {
                return wall;
            }
            set
            {
                wall = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// If true, this wall blocks Line of Sight. This can't be true while Wall is false.
        /// </summary>
        public bool LineOfSightBlocked
        {
            get
            {
                return losblock;
            }
            set
            {
                if (Wall && value) losblock = true;
                else losblock = false;
                FieldChanged();
            }
        }
        /// <summary>
        /// If true, creatures need Flying or Climbing to cross the passage that this wall is in.
        /// This can't be true while Wall is false. Setting it to true also sets PassageAttacks to false;
        /// </summary>
        public bool PassageBlocked
        {
            get
            {
                return pasblock;
            }
            set
            {
                if (Wall && value) 
                { 
                    pasblock = true;
                    PassageAttacks = false;
                }
                else pasblock = false;
                FieldChanged();
            }
        }
        /// <summary>
        /// If true, creatures need Flying or Climbing to cross the passage that this wall is in.
        /// This can't be true while Wall is false or PassageBlocked is false.
        /// </summary>
        public bool PassageAttacks
        {
            get
            {
                return pasattack;
            }
            set
            {
                if (Wall && value && !PassageBlocked) pasattack = true;
                else pasblock = false;
                FieldChanged();
            }
        }
        /// <summary>
        /// The default constructor for a Conjuration. Sets the range to 0-1 and makes it Quick.
        /// </summary>
        public MWConjuration()
        {
            MaxRange = 1;
            Quick = true;
        }

        public override string CardType()
        {
            return "Conjuration";
        }
    }
}
