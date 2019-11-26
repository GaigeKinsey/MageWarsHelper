using MageWarsHelper.Database;
using MageWarsHelper.Models;
using MageWarsHelper.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MageWarsHelper.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayerPage : Page
    {

        private MWPlayer player = null;
        private SerialIDToImageConverter converter = SerialIDToImageConverter.Instance;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            player = (MWPlayer)e.Parameter;

            Binding bind = new Binding();
            bind.Source = player;
            bind.Path = new PropertyPath("Mage");
            bind.Mode = BindingMode.OneWay;
            MageCard.SetBinding(DataContextProperty, bind);

            BindPropDisplays();

            SpellBookGrid.ItemsSource = player.Spellbook;
            PreparedGrid.ItemsSource = player.Prepared;
            DiscardGrid.ItemsSource = player.Discard;
            PlayerName.Text = player.Name;

        }

        public PlayerPage()
        {
            this.InitializeComponent();

            playerGrid.DataContext = player;

        }

        private void BindPropDisplays()
        {

            Binding bind = new Binding();
            bind.Source = player;
            bind.Path = new PropertyPath("Mage.Channeling");
            bind.Mode = BindingMode.OneWay;
            ChannelingPropDisplay.SetBinding(DataContextProperty, bind);
            ChannelingPropDisplay.Mage = player.Mage;

            bind = new Binding();
            bind.Source = player;
            bind.Path = new PropertyPath("Mage.Mana");
            bind.Mode = BindingMode.OneWay;
            ManaPropDisplay.SetBinding(DataContextProperty, bind);
            ManaPropDisplay.Mage = player.Mage;

            bind = new Binding();
            bind.Source = player;
            bind.Path = new PropertyPath("Mage.Life");
            bind.Mode = BindingMode.OneWay;
            LifePropDisplay.SetBinding(DataContextProperty, bind);
            LifePropDisplay.Mage = player.Mage;

            bind = new Binding();
            bind.Source = player;
            bind.Path = new PropertyPath("Mage.Damage");
            bind.Mode = BindingMode.OneWay;
            DamagePropDisplay.SetBinding(DataContextProperty, bind);
            DamagePropDisplay.Mage = player.Mage;

            bind = new Binding();
            bind.Source = player;
            bind.Path = new PropertyPath("Mage.Armor");
            bind.Mode = BindingMode.OneWay;
            ArmorPropDisplay.SetBinding(DataContextProperty, bind);
            ArmorPropDisplay.Mage = player.Mage;
        }

        private void TextBoxNumber_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //code provided from stack overflow: https://stackoverflow.com/questions/52624066/textbox-with-only-numbers
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void AddCardButton_Click(object sender, RoutedEventArgs e)
        {

            MWCard c = CardDatabase.Instance.Cards.Where(m => m.SerialNumber == CardID.Text).FirstOrDefault();
            if(c != null)
            {
                c.SerialNumber = CardID.Text;
                player.Spellbook.Add(c);
            }

        }

        private void PlayerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            player.Name = PlayerName.Text;

        }

        private void mageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string mageName = e.AddedItems[0].ToString();
            switch (mageName)
            {
                case "Beastmaster Straywood":
                    player.Mage.Type = MWMage.MageType.BEASTMASTER_STRAYWOOD;
                    break;
                case "Beastmaster Johktari":
                    player.Mage.Type = MWMage.MageType.BEASTMASTER_JOHKTARI;
                    break;
                case "Druid":
                    player.Mage.Type = MWMage.MageType.DRUID;
                    break;
                case "Warlock Arraxian":
                    player.Mage.Type = MWMage.MageType.WARLOCK_ARRAXIAN;
                    break;
                case "Warlock Adramelech":
                    player.Mage.Type = MWMage.MageType.WARLOCK_ADRAMELECH;
                    break;
                case "Priestess Westlock":
                    player.Mage.Type = MWMage.MageType.PRIESTESS_WESTLOCK;
                    break;
                case "Priest Malakai":
                    player.Mage.Type = MWMage.MageType.PRIEST_MALAKAI;
                    break;
                case "Wizard":
                    player.Mage.Type = MWMage.MageType.WIZARD;
                    break;
                case "Forcemaster":
                    player.Mage.Type = MWMage.MageType.FORCEMASTER;
                    break;
                case "Warlord Bloodwave":
                    player.Mage.Type = MWMage.MageType.WARLORD_BLOODWAVE;
                    break;
                case "Warlord Anvil":
                    player.Mage.Type = MWMage.MageType.WARLORD_ANVIL;
                    break;
                case "Necromancer":
                    player.Mage.Type = MWMage.MageType.NECROMANCER;
                    break;

            }
            
        }

        private void PreparedCardButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MWCard card = (MWCard)((CardButton)sender).DataContext;
            player.Discard.Add(card);
            player.Prepared.Remove(card);
        }
        private void SpellbookCardButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MWCard card = (MWCard)((CardButton)sender).DataContext;
            player.Prepared.Add(card);
            player.Spellbook.Remove(card);
        }
        private void DiscardCardButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MWCard card = (MWCard)((CardButton)sender).DataContext;
            player.Prepared.Add(card);
            player.Discard.Remove(card);
        }
    }
}
