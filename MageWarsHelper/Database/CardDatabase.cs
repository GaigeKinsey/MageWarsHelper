using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MageWarsHelper.Database
{
    public class CardDatabase
    {
        private CardDatabase() {}

        private static CardDatabase instance = new CardDatabase();

        public static CardDatabase Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        public void LoadDataBase()
        {
            List<MWCard> cards = new List<MWCard>();
            string[] lines = System.IO.File.ReadAllLines("cardDatabase.txt");

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
