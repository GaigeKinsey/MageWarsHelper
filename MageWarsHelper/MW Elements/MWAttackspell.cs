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
                    if (value.ZoneAttack) Target = TargetType.ZONE;
                    else Target = TargetType.OBJECT;
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
    }
}
