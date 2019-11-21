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
        private CardDatabase() { }

        private static CardDatabase instance = new CardDatabase();

        public static CardDatabase Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        private List<MWCard> cards = new List<MWCard>();

        public List<MWCard> Cards
        {
            get { return cards; }
            set { cards = value; }
        }

        public void LoadDataBase()
        {
            StreamReader sr = new StreamReader("Database/cardDatabase.txt");

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] elements = line.Split("| ");
                MWCard card = (MWCard)Activator.CreateInstance(MWCard.StringTypeConverter(elements[2]));

                //Serial Number
                card.SerialNumber = elements[0];

                //Name
                card.Name = elements[1];

                //SubType
                string[] subTypes = elements[3].Split(", ");
                List<Subtype> subTypesEnum = new List<Subtype>();
                foreach (string subType in subTypes)
                {
                    Subtype type;
                    if (Enum.TryParse(subType.Replace(" ", "_").ToUpper(), out type))
                    {
                        subTypesEnum.Add(type);
                    }
                }
                card.Subtypes = subTypesEnum;

                //Levels and Schools
                if (elements[4].Contains(","))
                {
                    string[] levels;
                    string[] schools = elements[4].Split(", ");
                    if (elements[5].Contains(" or "))
                    {
                        card.ChooseSchool = true;
                        levels = elements[5].Split(" or ");
                    }
                    else
                    {
                        levels = elements[5].Split(" & ");
                    }

                    for (int i = 0; i < schools.Length; i++)
                    {
                        AddSchool(schools[i], levels[i], card);
                    }
                }
                else
                {
                    AddSchool(elements[4], elements[5], card);
                }

                //Mana Cost
                card.ManaCostString = elements[6];

                if (card.GetType() == typeof(MWEnchantment))
                {
                    //Reveal Cost
                    string revealCost = elements[7];
                    MWEnchantment enchantCard = (MWEnchantment)card;
                    enchantCard.RevealCostString = revealCost;

                    Cards.Add(enchantCard);
                }
                else
                {
                    Cards.Add(card);
                }
            }
        }

        private void AddSchool(string schoolstr, string levelstr, MWCard card)
        {
            SpellSchool school;
            int level = 0;
            if (Enum.TryParse(schoolstr.ToUpper(), out school) && int.TryParse(levelstr, out level))
            {
                card.SetSchoolLevel(school, level);
            }
        }
    }
}
