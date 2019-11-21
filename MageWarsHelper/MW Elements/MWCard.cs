using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    #region TargetType
    /// <summary>
    /// Targets that cards are played on. Might change or convert these to a logical class later,
    /// but for now it's just an enum for filtering.
    /// </summary>
    public enum TargetType
    {
        ARENA,
        CREATURE,
        ENCHANTMENT,
        MAGE,
        OBJECT,
        ZONE
    }
    #endregion
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
        /// What sort of target this card has. Doesn't factor in things like "friendly"
        /// or "living," just the general target type.
        /// </summary>
        public TargetType Target { get; set; }

        private int mana = 0, channeling = 0, manacost = 5, minrange = 0, maxrange = 0;
        private bool manax;
        private Dictionary<SpellSchool, int> levels = new Dictionary<SpellSchool, int>();
        private List<Subtype> subtypes = new List<Subtype>();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The base cost to cast this card.
        /// </summary>
        public int ManaCost {
            get
            {
                return manacost;
            }
            set
            {
                if (value < 1) value = 1;
                else manacost = value;
                FieldChanged("ManaCostString");
                FieldChanged();
            }
        }
        /// <summary>
        /// If the mana cost is going to be multiplied by something, this is true.
        /// </summary>
        public bool ManaCostX {
            get
            {
                return manax;
            }
            set
            {
                manax = value;
                FieldChanged("ManaCostString");
                FieldChanged();
            } 
        }
        /// <summary>
        /// The mana cost as a string. If you set this, it will parse the string into
        /// a valid mana cost for you (inluding the X, if there is one.)
        /// </summary>
        public string ManaCostString
        {
            get
            {
                if (ManaCost == 0) return "0";
                if (ManaCost == 1 && ManaCostX) return "X";
                return ManaCostX ? ManaCost + "X" : ManaCost.ToString();
            }
            set
            {
                if (value[value.Length - 1] == 'X')
                {
                    if (value == "X")
                    {
                        ManaCost = 1;
                        ManaCostX = true;
                    }
                    else if (int.TryParse(value.Substring(0, value.Length - 1), out manacost))
                    {
                        if (manacost < 1) manacost = 1;
                        ManaCostX = true;
                    }
                }
                else
                {
                    if (int.TryParse(value, out manacost))
                    {
                        if (manacost < 1) manacost = 1;
                        ManaCostX = false;
                    }
                }
                FieldChanged();
            }
        }
        /// <summary>
        /// The minimum range to cast this spell. Can only go as low as 0, which
        /// refers to the zone that the caster is in. MaxRange will always be greater
        /// than or equal to this.
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
        /// The maximum range to cast this spell. Can only go as low as 0, which
        /// refers to the zone that the caster is in. MinRange will always be less
        /// than or equal to this.
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
        /// A string of all of the schools in this spell, or "NOVICE" if there are none.
        /// </summary>
        public string Schools
        {
            get
            {
                if (levels.Count() == 0) return "NOVICE";
                var schools = new List<SpellSchool>();
                foreach(SpellSchool eachSchool in levels.Keys)
                {
                    schools.Add(eachSchool);
                }
                string schoolstr = schools[0].ToString();
                for (int i = 1; i < schools.Count; i++)
                {
                    schoolstr += ", " + schools[i];
                }
                return schoolstr;
            }
        }
        /// <summary>
        /// A string of all of the levels in this spell, or "1" if there are none.
        /// </summary>
        public string Levels
        {
            get
            {
                if (levels.Count() == 0) return "1";
                var levelist = new List<int>();
                foreach (int eachLv in levels.Values)
                {
                    levelist.Add(eachLv);
                }
                string lvstr = levelist[0].ToString();
                for (int i = 1; i < levelist.Count; i++)
                {
                    lvstr += (ChooseSchool ? " or " : " & ")  + levelist[i];
                }
                return lvstr;
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
        /// <summary>
        /// Finds a non-abstract subtype of MWCard based on a string.
        /// </summary>
        /// <param name="typestr">The string to get a type based on</param>
        /// <returns>a non-abstract subtype of MWCard</returns>
        public static Type StringTypeConverter(string typestr)
        {
            switch (typestr.Trim().ToLower())
            {
                case "creature":
                    return typeof(MWCreature);
                case "conjuration":
                    return typeof(MWConjuration);
                case "enchantment":
                    return typeof(MWEnchantment);
                case "equipment":
                    return typeof(MWEquipment);
                case "incantation":
                    return typeof(MWIncantation);
                default:
                    return typeof(MWAttackspell);
            }
        }
        /// <summary>
        /// Finds a non-abstract subtype of MWCard based on a char.
        /// </summary>
        /// <param name="typechar">The char to get a type based on</param>
        /// <returns>a non-abstract subtype of MWCard</returns>
        public static Type CharTypeConverter(char typechar)
        {
            switch (typechar)
            {
                case 'C':
                case 'c':
                    return typeof(MWCreature);
                case 'J':
                case 'j':
                    return typeof(MWConjuration);
                case 'E':
                case 'e':
                    return typeof(MWEnchantment);
                case 'Q':
                case 'q':
                    return typeof(MWEquipment);
                case 'I':
                case 'i':
                    return typeof(MWIncantation);
                default:
                    return typeof(MWAttackspell);
            }
        }
        /// <summary>
        /// Finds a non-abstract subtype of MWCard based on a serial number (the third-to-last character is key.)
        /// </summary>
        /// <param name="serialnum">The serial number to get a type based on</param>
        /// <returns>a non-abstract subtype of MWCard</returns>
        public static Type SerialConverter(string serialnum)
        {
            int index = serialnum.Length - 3;
            return CharTypeConverter(serialnum[index]);
        }
        /// <summary>
        /// Makes a default MWCard subtype based on a Type object.
        /// </summary>
        /// <param name="t">The Type object of the card type you want to make</param>
        /// <returns>An object of one of MWCard's subtypes</returns>
        public static MWCard Create(Type t)
        {
            if (t == null) throw new ArgumentNullException("Can't create a MWCard based on a null type.");
            try {
                MWCard card = (MWCard)(Activator.CreateInstance(t));
                return card;
            } catch(Exception e)
            {
                throw new ArgumentException("Type parameter isn't a subtype of MWCard.");
            }
            
        }
        /// <summary>
        /// Returns a string of what type of card this is.
        /// </summary>
        /// <returns></returns>
        public abstract string CardType { get; }
    }
}
