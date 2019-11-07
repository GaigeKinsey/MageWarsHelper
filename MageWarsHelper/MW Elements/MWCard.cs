using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
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
    public class MWCard
    {
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public int ManaCost { get; set; }
        private int minrange = 0, maxrange = 0;
        private Dictionary<SpellSchool, int> levels = new Dictionary<SpellSchool, int>();
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
            }
        }
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
            }
        }
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
        public bool Quick { get; set; } = true;

        public bool Novice { get; set; } = false;

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
        }
    }
}
