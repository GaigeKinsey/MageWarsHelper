using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    public class MWMage : MWCreature
    {
        public enum MageType
        {
            BEASTMASTER_STRAYWOOD,
            BEASTMASTER_JOHKTARI,
            WARLOCK_ARRAXIAN,
            WARLOCK_ADRAMELECH,
            PRIESTESS_WESTLOCK,
            PRIEST_MALAKAI,
            WIZARD,
            FORCEMASTER,
            WARLORD_BLOODWAVE,
            WARLORD_ANVIL,
            DRUID,
            NECROMANCER
        }
        private MageType type;
        private IDictionary<SpellSchool, int?> trained = new Dictionary<SpellSchool, int?>();
        private ISet<SpellSchool> restricted = new SortedSet<SpellSchool>();
        public MWMage()
        {
            Type = MageType.BEASTMASTER_STRAYWOOD;
        }
        /// <summary>
        /// The base cost to cast this card. A Mage's ManaCost is -1, to indicate that it can't be cast.
        /// </summary>
        public new int ManaCost
        {
            get
            {
                return -1;
            }
        }
        /// <summary>
        /// The minumum range to cast this spell. A Mage's MinRange is -1, to indicate that it can't be cast.
        /// </summary>
        public new int MinRange
        {
            get
            {
                return -1;
            }
        }
        /// <summary>
        /// The maximum range to cast this spell. A Mage's MaxRange is -1, to indicate that it can't be cast.
        /// </summary>
        public new int MaxRange
        {
            get
            {
                return -1;
            }
        }
        /// <summary>
        /// The level of this spell for card effects. A Mage's level is always treated as 6.
        /// </summary>
        public new int Level
        {
            get
            {
                return 6;
            }
        }
        /// <summary>
        /// A map of the schools that the Mage is trained in. Usually each key
        /// maps to null, but if it maps to a number, that number is the maximum
        /// level that the Mage is trained in that school.
        /// This value can't be modified directly, but it changes when you change
        /// the Mage's type. If you get this value, it actually gives you a copy.
        /// </summary>
        public IDictionary<SpellSchool, int?> Trained
        {
            get
            {
                Dictionary<SpellSchool, int?> schools = new Dictionary<SpellSchool, int?>();
                foreach (var pair in trained)
                {
                    schools.Add(pair.Key, pair.Value);
                }
                return schools;
            }
        }
        /// <summary>
        /// A set of the schools that are restricted for the Mage. These schools
        /// cost triple.
        /// This value can't be modified directly, but it changes when you change
        /// the Mage's type. If you get this value, it actually gives you a copy.
        /// </summary>
        public ISet<SpellSchool> Restricted
        {
            get
            {
                ISet<SpellSchool> schools = new SortedSet<SpellSchool>();
                foreach (SpellSchool school in restricted)
                {
                    schools.Add(school);
                }
                return schools;
            }
        }
        /// <summary>
        /// If true, creature cards that the Mage isn't trained with count as restricted.
        /// </summary>
        public bool RestrictCreatures { get; set; }
        public override string CardType => "Mage";
        public int CostForCard(MWCard card)
        {
            return card.CostToLearn(Trained, Restricted, RestrictCreatures);
        }
        /// <summary>
        /// The Mage's type. Changing this automatically sets all of its relevant properties.
        /// </summary>
        public MageType Type
        {
            get
            {
                return type;
            }
            set
            {
                switch (value)
                {
                    case MageType.BEASTMASTER_STRAYWOOD:
                        SerialNumber = "BEASTMASTERABILITYOUTLINE";
                        Name = "Beastmaster";
                        Subtypes.Add(Subtype.WOOD_ELF);
                        Life = 36;
                        Armor = 0;
                        Channeling = 9;
                        Mana = 10;
                        trained.Clear();
                        trained.Add(SpellSchool.NATURE, null);
                        restricted.Clear();
                        restricted.Add(SpellSchool.FIRE);
                        RestrictCreatures = false;
                        break;
                    case MageType.BEASTMASTER_JOHKTARI:
                        SerialNumber = "JOHKTARIABILITYOUTLINE";
                        Name = "Beastmaster";
                        Subtypes.Add(Subtype.HUMAN);
                        Life = 34;
                        Armor = 0;
                        Channeling = 9;
                        Mana = 10;
                        trained.Clear();
                        trained.Add(SpellSchool.NATURE, null);
                        restricted.Clear();
                        restricted.Add(SpellSchool.FIRE);
                        RestrictCreatures = false;
                        break;
                    case MageType.WARLOCK_ARRAXIAN:
                        SerialNumber = "WARLOCKABILITYOUTLINE";
                        Name = "Beastmaster";
                        Subtypes.Add(Subtype.HUMAN);
                        Life = 38;
                        Armor = 0;
                        Channeling = 9;
                        Mana = 10;
                        trained.Clear();
                        trained.Add(SpellSchool.DARK, null);
                        trained.Add(SpellSchool.FIRE, null);
                        restricted.Clear();
                        restricted.Add(SpellSchool.HOLY);
                        RestrictCreatures = false;
                        break;
                    case MageType.WARLOCK_ADRAMELECH:
                        SerialNumber = "ADRAMELECH WARLOCKABILITYOUTLINE";
                        Name = "Beastmaster";
                        Subtypes.Add(Subtype.HUMAN);
                        Life = 33;
                        Armor = 0;
                        Channeling = 9;
                        Mana = 10;
                        trained.Clear();
                        trained.Add(SpellSchool.DARK, null);
                        trained.Add(SpellSchool.FIRE, null);
                        restricted.Clear();
                        restricted.Add(SpellSchool.HOLY);
                        RestrictCreatures = false;
                        break;
                    case MageType.PRIESTESS_WESTLOCK:
                        SerialNumber = "PRIESTESSABILITYOUTLINE";
                        Name = "Beastmaster";
                        Subtypes.Add(Subtype.HIGH_ELF);
                        Life = 32;
                        Armor = 0;
                        Channeling = 10;
                        Mana = 10;
                        trained.Clear();
                        trained.Add(SpellSchool.HOLY, null);
                        restricted.Clear();
                        restricted.Add(SpellSchool.DARK);
                        RestrictCreatures = false;
                        break;
                    case MageType.PRIEST_MALAKAI:
                        SerialNumber = "PRIESTABILITYOUTLINE";
                        Name = "Beastmaster";
                        Subtypes.Add(Subtype.HIGH_ELF);
                        Life = 34;
                        Armor = 0;
                        Channeling = 10;
                        Mana = 9;
                        trained.Clear();
                        trained.Add(SpellSchool.HOLY, null);
                        restricted.Clear();
                        restricted.Add(SpellSchool.DARK);
                        RestrictCreatures = false;
                        break;
                    case MageType.WIZARD:
                        SerialNumber = "WIZARDABILITYOUTLINE";
                        Name = "Beastmaster";
                        Subtypes.Add(Subtype.HUMAN);
                        Life = 32;
                        Armor = 0;
                        Channeling = 10;
                        Mana = 10;
                        trained.Clear();
                        trained.Add(SpellSchool.ARCANE, null);
                        trained.Add(SpellSchool.AIR, null);
                        restricted.Clear();
                        RestrictCreatures = false;
                        break;
                    case MageType.FORCEMASTER:
                        SerialNumber = "FORCEMASTERABILITYOUTLINE";
                        Name = "Beastmaster";
                        Subtypes.Add(Subtype.HUMAN);
                        Life = 32;
                        Armor = 0;
                        Channeling = 10;
                        Mana = 10;
                        trained.Clear();
                        trained.Add(SpellSchool.MIND, null);
                        restricted.Clear();
                        RestrictCreatures = true;
                        break;
                    case MageType.WARLORD_BLOODWAVE:
                        SerialNumber = "WARLORDABILITYOUTLINE";
                        Name = "Beastmaster";
                        Subtypes.Add(Subtype.ORC);
                        Life = 36;
                        Armor = 0;
                        Channeling = 9;
                        Mana = 10;
                        trained.Clear();
                        trained.Add(SpellSchool.WAR, null);
                        trained.Add(SpellSchool.EARTH, null);
                        restricted.Clear();
                        restricted.Add(SpellSchool.ARCANE);
                        RestrictCreatures = false;
                        break;
                    case MageType.WARLORD_ANVIL:
                        SerialNumber = "ANVIL THRONE WARLORDABILITYOUTLINE";
                        Name = "Beastmaster";
                        Subtypes.Add(Subtype.DWARF);
                        Life = 34;
                        Armor = 0;
                        Channeling = 9;
                        Mana = 10;
                        trained.Clear();
                        trained.Add(SpellSchool.WAR, null);
                        trained.Add(SpellSchool.EARTH, null);
                        restricted.Clear();
                        restricted.Add(SpellSchool.ARCANE);
                        RestrictCreatures = false;
                        break;
                    case MageType.DRUID:
                        SerialNumber = "DRUIDABILITYOUTLINE";
                        Name = "Beastmaster";
                        Subtypes.Add(Subtype.WOOD_ELF);
                        Life = 30;
                        Armor = 0;
                        Channeling = 9;
                        Mana = 10;
                        trained.Clear();
                        trained.Add(SpellSchool.WATER, 1);
                        trained.Add(SpellSchool.NATURE, null);
                        restricted.Clear();
                        restricted.Add(SpellSchool.FIRE);
                        restricted.Add(SpellSchool.WAR);
                        RestrictCreatures = false;
                        break;
                    case MageType.NECROMANCER:
                        SerialNumber = "NECROMANCERABILITYOUTLINE";
                        Name = "Beastmaster";
                        Subtypes.Add(Subtype.HUMAN);
                        Life = 32;
                        Armor = 0;
                        Channeling = 10;
                        Mana = 10;
                        trained.Clear();
                        trained.Add(SpellSchool.DARK, null);
                        restricted.Clear();
                        restricted.Add(SpellSchool.HOLY);
                        RestrictCreatures = false;
                        break;
                    default:
                        return;
                }
                type = value;
                FieldChanged();
            }
        }
    }
}
