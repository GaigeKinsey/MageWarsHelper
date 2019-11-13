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

        public List<MWCard> Cards
        {
            get { return cards; }
            set { cards = value; }
        }


        public MWMage Mage { get; set; }

        public MWPlayer()
        {
            Mage = new MWMage();
        }
    }
}
