using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper
{
    public class MWPlayer
    {
        private ObservableCollection<MWCard> cards = new ObservableCollection<MWCard>();

        public ObservableCollection<MWCard> Cards
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
