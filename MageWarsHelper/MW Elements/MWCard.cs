using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    #region AttackElement
    /// <summary>
    /// Elements of attacks, used for calculating weakness, resistance, and bonuses.
    /// An attack can only have 1 element.
    /// </summary>
    public enum AttackElement
    {
        ACID,
        HYDRO,
        FLAME,
        FROST,
        LIGHT,
        LIGHTNING,
        POISON,
        PSYCHIC,
        WIND
    }
    #endregion
    #region SpellSchool
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
    #endregion
    #region Subtype
    /// <summary>
    /// Subtypes, relevent for searching and some card effects. A MWCard
    /// won't always have one, but it can have several.
    /// </summary>
    public enum Subtype
    {
        ACID,
        AEGIS,
        ALTAR,
        ANGEL,
        ANIMAL,
        ANTARIAN,
        APE,
        ARTIFACT,
        BARRIER,
        BAT,
        BEAR,
        BIRD,
        CANINE,
        CAT,
        CERVINE,
        CHARM,
        CLERIC,
        CLOUD,
        COMMAND,
        CONTROL,
        CURSE,
        DARK_ELF,
        DEFENSE,
        DEMON,
        DRAGON,
        DWARF,
        ELEMENTAL,
        FAERIE,
        FLAME,
        FLOWER,
        FORCE,
        GARGOYLE,
        GOBLIN,
        GOLEM,
        GREMLIN,
        HARPY,
        HEALING,
        HIGH_ELF,
        HORSE,
        HUMAN,
        HYDRO,
        ILLUSION,
        INSECT,
        KNIGHT,
        LIGHT,
        LIGHTNING,
        LIZARD,
        LYCANTHROPE,
        MANA,
        METAMAGIC,
        MINOTAUR,
        NECRO,
        OBELISK,
        OOZE,
        ORC,
        OUTPOST,
        PLANT,
        POISON,
        PORTAL,
        PROTECTION,
        PSYCHIC,
        PSYOCULUS,
        REPTILE,
        RUNE,
        SEISMIC,
        SEQUOIAN,
        SERPENT,
        SHADOW,
        SKELETON,
        SOLDIER,
        SPIDER,
        SPIRIT,
        STATUE,
        STONE,
        STRUCTURE,
        TELEPORT,
        TEMPLE,
        TOME,
        TOTEM,
        TOWER,
        TRANSFORM,
        TRAP,
        TREE,
        TROLL,
        UNDEAD,
        VAMPIRE,
        VAMPIRIC,
        VINE,
        V_TAR,
        WAR_MACHINE,
        WEATHER,
        WIND,
        WOOD_ELF,
        WORM,
        ZOMBIE
    }
    #endregion
    /// <summary>
    /// Cards in Mage Wars. Aside from the Mage, they start in the player's spellbook.
    /// </summary>
    public abstract class MWCard : INotifyPropertyChanged
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
        private int mana = 0, channeling = 0, manacost = 5, minrange = 0, maxrange = 0;
        private Dictionary<SpellSchool, int> levels = new Dictionary<SpellSchool, int>();
        private List<Subtype> subtypes = new List<Subtype>();

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
                    if (ChooseSchool)
                    {
                        if (level == 0 || bySchool.Value < level) level = bySchool.Value;
                    } else
                    {
                        level += bySchool.Value;
                    }
                }
                return level;
            }
        }
        private bool chooseschool = false, quick = true, novice = false, epic = false, spawnpoint = false, familiar = false, spellbind = false;
        /// <summary>
        /// If true, this spell's school/level is chooseable from the options, instead of a combination of them.
        /// </summary>
        public bool ChooseSchool
        {
            get
            {
                return chooseschool;
            }
            set
            {
                chooseschool = value;
                FieldChanged();
                FieldChanged("Level");
            }
        }
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
        /// If true, the spell only costs 1 to put in the spellbook.
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
        /// <summary>
        /// If true, you can only have 1 copy of this spell in your spellbook.
        /// </summary>
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
        /// <summary>
        /// If true, only one copy can be in play at a time.
        /// </summary>
        public bool Legendary { get; set; }
        /// <summary>
        /// If true, only one copy can be in play for each player.
        /// </summary>
        public bool Unique { get; set; }
        /// <summary>
        /// Can cast a spell during the Deployment phase. Can only be true for cards that have mana.
        /// </summary>
        public bool Spawnpoint
        {
            get
            {
                return spawnpoint;
            }
            set
            {
                if (value && HasMana) spawnpoint = true;
                else spawnpoint = false;
                FieldChanged();
            }
        }
        /// <summary>
        /// Can cast a spell during its turn (or a friendly action if it's not a creature.) Can only be true for cards that have mana.
        /// </summary>
        public bool Familiar
        {
            get
            {
                return familiar;
            }
            set
            {
                if (value && HasMana) familiar = true;
                else familiar = false;
                FieldChanged();
            }
        }
        /// <summary>
        /// Can have an incantation or attack spell bound, which it can cast repeatedly.
        /// </summary>
        public bool Spellbind
        {
            get
            {
                return spellbind;
            }
            set
            {
                spellbind = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// A list of all the different subtypes this spell has. It can be empty.
        /// </summary>
        public List<Subtype> Subtypes
        {
            get
            {
                return subtypes;
            }
            set
            {
                subtypes = value;
                FieldChanged();
            }
        }
        /// <summary>
        /// All Mages have mana. In general, the only other things with mana are
        /// spawnpoints and familiars. In certain expansions, non-unit cards can
        /// be spawnpoints or familiars.
        /// </summary>
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
                    FieldChanged("Channeling");
                    mana = 0;
                    FieldChanged("Mana");
                }
                else if (value && channeling == 0)
                {
                    channeling = 1;
                    FieldChanged("Channeling");
                }
            }
        }
        /// <summary>
        /// Current amount of mana, which is never below 0. If you give something
        /// mana when it didn't have any before, it counts as having mana.
        /// </summary>
        public int Mana
        {
            get { return mana; }
            set
            {
                if (value < 0) mana = 0;
                else mana = value;
                FieldChanged("HasMana");
                FieldChanged();
            }
        }
        /// <summary>
        /// How much mana this gains during upkeep. If it's above 0, this will
        /// always be treated as having mana, even when its curren mana is 0.
        /// </summary>
        public int Channeling
        {
            get { return channeling; }
            set
            {
                if (value < 0) channeling = 0;
                else channeling = value;
                FieldChanged("HasMana");
                FieldChanged();
            }
        }
        /// <summary>
        /// Default constructor for a card.
        /// </summary>
        public MWCard()
        {

        }
        /// <summary>
        /// Sets the level of a particular school for this spell. If ChooseSchool is true,
        /// the lowest level will be treated as the level count. Otherwise, all levels
        /// will be added up to get the count. 
        /// </summary>
        /// <param name="school">The school for this level</param>
        /// <param name="level">The level</param>
        public void SetSchoolLevel(SpellSchool school, int level)
        {
            if (levels.ContainsKey(school))
            {
                levels[school] = level;
                if (levels[school] <= 0) levels.Remove(school);
            } else if (level > 0)
            {
                levels.Add(school, level);
            }
            FieldChanged("Level");
        }
        /// <summary>
        /// The cost to add this to your spell book. This is based on the spell's levels and
        /// which schools your Mage is trained in and are restricted to it. Being untrained
        /// doubles the cost per level. Having the school restricted triples it instead.
        /// </summary>
        /// <param name="trained">Your Mage's trained schools</param>
        /// <param name="restricted">Your Mage's restricted schools</param>
        /// <returns>The total cost for your Mage to put this in a spellbook.</returns>
        public int CostToLearn(ICollection<SpellSchool> trained, ICollection<SpellSchool> restricted)
        {
            if (Novice || levels.Count > 0) return 1;
            int cost;
            if (ChooseSchool)
            {
                cost = 10;
                foreach (var bySchool in levels)
                {
                    if (trained.Contains(bySchool.Key))
                    {
                        cost = Math.Min(bySchool.Value, cost);
                    }
                    else if (restricted.Contains(bySchool.Key))
                    {
                        cost = Math.Min(bySchool.Value * 3, cost);
                    }
                    else
                    {
                        cost = Math.Min(bySchool.Value * 2, cost);
                    }
                }
            }
            else
            {
                cost = 0;
                foreach (var bySchool in levels)
                {
                    if (trained.Contains(bySchool.Key))
                    {
                        cost += bySchool.Value;
                    }
                    else if (restricted.Contains(bySchool.Key))
                    {
                        cost += bySchool.Value * 3;
                    }
                    else
                    {
                        cost += bySchool.Value * 2;
                    }
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
