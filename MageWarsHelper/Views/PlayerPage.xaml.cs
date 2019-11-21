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

            CardsGrid.ItemsSource = player.Cards;
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
                player.Cards.Add(c);
            }

        }

        private void PlayerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            player.Name = PlayerName.Text;

        }
    }
}
