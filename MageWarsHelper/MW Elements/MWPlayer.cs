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
        private ObservableCollection<MWCard> prepared = new ObservableCollection<MWCard>();
        private ObservableCollection<MWCard> discard = new ObservableCollection<MWCard>();

        public ObservableCollection<MWCard> Spellbook
        {
            get { return spellbook; }
            set { spellbook = value; }
        }

        public ObservableCollection<MWCard> Prepared
        {
            get { return prepared; }
            set { prepared = value; }
        }

        public ObservableCollection<MWCard> Discard
        {
            get { return discard; }
            set { discard = value; }
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
