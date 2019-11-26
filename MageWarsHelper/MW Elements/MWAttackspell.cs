using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    public class MWAttackspell : MWCard
    {
        public override string CardType => "Attack";

        private MWAttackAction attack;

        /// <summary>
        /// A list of all the different attacks this spell has. Since this
        /// is an Attack spell, it will always have exactly 1 attack, and
        /// that attack will have the same range and speed as the spell.
        /// </summary>
        public new List<MWAttackAction> Attacks
        {
            get
            {
                List<MWAttackAction> atks = new List<MWAttackAction>();
                atks.Add(attack);
                return atks;
            }
            set
            {
                if (value != null && value.Count() > 0)
                {
                    Attack = value[0];
                    FieldChanged();
                }
            }
        }
        /// <summary>
        /// The attack that happens when this spell is cast. The caster
        /// is treated as the attacker, and the attack will always have
        /// the same range and speed as the spell. The target of the spell
        /// and the target of the attack also match.
        /// </summary>
        public MWAttackAction Attack
        {
            get
            {
                return attack;
            }
            set
            {
                if (value.Ranged)
                {
                    attack = value;
                    Quick = value.Quick;
                    MinRange = value.MinRange;
                    MaxRange = value.MaxRange;
                    FieldChanged();
                    FieldChanged("Attacks");
                }
            }
        }
        /// <summary>
        /// If true, casting this spell and making its attack is a quick action.
        /// </summary>
        public new bool Quick
        {
            get
            {
                return attack.Quick;
            }
            set
            {
                attack.Quick = value;
                FieldChanged("Attack");
                FieldChanged("Attacks");
                FieldChanged();
            }
        }
        /// <summary>
        /// The minimum range to cast this spell. Can only go as low as 0, which refers 
        /// to the zone that the attacking card is in. MaxRange will always be greater
        /// than or equal to this. The range of this spell's attack is always the same
        /// as the spell itself.
        /// </summary>
        public new int MinRange
        {
            get { return attack.MinRange; }
            set
            {
                if (value > attack.MaxRange)
                {
                    attack.MaxRange = value;
                    FieldChanged("MaxRange");
                    attack.MinRange = value;
                }
                else attack.MinRange = value;
                FieldChanged();
                FieldChanged("Attack");
                FieldChanged("Attacks");
            }
        }
        /// <summary>
        /// The maximum range to cast this spell. Can only go as low as 0, which refers 
        /// to the zone that the attacking card is in. MinRange will always be less
        /// than or equal to this.  The range of this spell's attack is always the same
        /// as the spell itself.
        /// </summary>
        public new int MaxRange
        {
            get { return attack.MaxRange; }
            set
            {
                if (value < attack.MaxRange)
                {
                    attack.MinRange = value;
                    FieldChanged("MinRange");
                    attack.MaxRange = value;
                }
                else attack.MaxRange = value;
                FieldChanged();
                FieldChanged("Attack");
                FieldChanged("Attacks");
            }
        }

        /// <summary>
        /// What sort of target this card has. Doesn't factor in things like "friendly"
        /// or "living," just the general target type. For Attack spells, this will be
        /// Zone for a Zone Attack, or Object for a regular Creature/Conjuration attack.
        /// </summary>
        public new TargetType Target
        {
            get
            {
                if (attack.ZoneAttack) return TargetType.ZONE;
                else return TargetType.OBJECT;
            }
            set
            {
                if (value == TargetType.ZONE)
                {
                    attack.ZoneAttack = true;
                    FieldChanged();
                    FieldChanged("Attack");
                    FieldChanged("Attacks");
                } else if (value == TargetType.OBJECT || value == TargetType.CREATURE)
                {
                    attack.ZoneAttack = false;
                    FieldChanged();
                    FieldChanged("Attack");
                    FieldChanged("Attacks");
                }
            }
        }
    }
}
