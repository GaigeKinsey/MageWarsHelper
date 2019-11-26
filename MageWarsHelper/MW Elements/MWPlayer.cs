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
        private ObservableCollection<MWCard> spellbook = new ObservableCollection<MWCard>();

        public ObservableCollection<MWCard> Spellbook
        {
            get { return spellbook; }
            set { spellbook = value; }
        }



        public MWMage Mage { get; set; }

        public MWPlayer()
        {
            Mage = new MWMage();
        }

        private string name = "";

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
