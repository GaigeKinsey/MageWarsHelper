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
            //StreamReader sr = new StreamReader("");

            //string line;
            //while ((line = sr.ReadLine()) != null)
            //{
            //    Console.WriteLine(line);
            //}
        }
    }
}
