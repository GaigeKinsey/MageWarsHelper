using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    public class MWPlayer
    {
        private List<MWCard> cards = new List<MWCard>();
        public MWMage Mage
        {
            get
            {
                if (cards.First().GetType() == typeof(MWMage))
                {
                    return (MWMage)cards.First();
                }
                else return null;
            }
            private set
            {
                if (value == null) cards.RemoveAt(0);
                else if (cards.First().GetType() == typeof(MWMage))
                {
                    cards.RemoveAt(0);
                    cards.Insert(0, value);
                }
                else cards.Insert(0, value);
            }
        }
    }
}
