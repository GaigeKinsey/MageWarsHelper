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

        private MWPlayer p = new MWPlayer();
        private SerialIDToImageConverter converter = SerialIDToImageConverter.Instance;

        public PlayerPage()
        {
            this.InitializeComponent();
            subStackPannel.DataContext = p;
            //Image img = new Image();
            //img.Source = new BitmapImage(new Uri("http://forum.arcanewonders.com/cards/BEASTMASTERABILITYOUTLINE.jpg"));
            //test.Content = img;

            p.Mage.SerialNumber = "BEASTMASTERABILITYOUTLINE";
            p.Mage.HasArmor = true;

            Binding bind = new Binding();
            bind.Source = p;
            bind.Path = new PropertyPath("Mage.SerialNumber");
            bind.Mode = BindingMode.OneWay;
            bind.Converter = converter;
            
            MageCard.SetBinding(ContentProperty, bind);


            bind = new Binding();
            bind.Source = p;
            bind.Path = new PropertyPath("Mage.Channeling");
            bind.Mode = BindingMode.OneWay;
            ChannelingPropDisplay.SetBinding(DataContextProperty, bind);
            ChannelingPropDisplay.Mage = p.Mage;

            bind = new Binding();
            bind.Source = p;
            bind.Path = new PropertyPath("Mage.Mana");
            bind.Mode = BindingMode.OneWay;
            ManaPropDisplay.SetBinding(DataContextProperty, bind);
            ManaPropDisplay.Mage = p.Mage;

            bind = new Binding();
            bind.Source = p;
            bind.Path = new PropertyPath("Mage.Life");
            bind.Mode = BindingMode.OneWay;
            LifePropDisplay.SetBinding(DataContextProperty , bind);
            LifePropDisplay.Mage = p.Mage;

            bind = new Binding();
            bind.Source = p;
            bind.Path = new PropertyPath("Mage.Damage");
            bind.Mode = BindingMode.OneWay;
            DamagePropDisplay.SetBinding(DataContextProperty, bind);
            DamagePropDisplay.Mage = p.Mage;

            bind = new Binding();
            bind.Source = p;
            bind.Path = new PropertyPath("Mage.Armor");
            bind.Mode = BindingMode.OneWay;
            ArmorPropDisplay.SetBinding(DataContextProperty, bind);
            ArmorPropDisplay.Mage = p.Mage;

            CardsGrid.ItemsSource = p.Cards;
            MWCard c = MWCard.Create(MWCard.SerialConverter("MW1C01"));
            c.SerialNumber = "MW1C01";
            p.Cards.Add(c);
            p.Cards.Add(c);
            p.Cards.Add(c);
        }

        //private void channelAdd_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    int value = 0;
        //    int.TryParse(adjustChanel.Text, out value);
        //    p.Mage.Channeling += value;
        //}

        //private void channelSubtract_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    int value = 0;
        //    int.TryParse(adjustChanel.Text, out value);
        //    p.Mage.Channeling -= value;
        //}

        //private void lifeAdd_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    int value = 0;
        //    int.TryParse(adjustLife.Text, out value);
        //    p.Mage.Life += value;
        //}

        //private void lifeSubtract_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    int value = 0;
        //    int.TryParse(adjustLife.Text, out value);
        //    p.Mage.Life -= value;
        //}

        //private void manaAdd_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    int value = 0;
        //    int.TryParse(adjustMana.Text, out value);
        //    p.Mage.Mana += value;
        //}

        //private void manaSubtract_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    int value = 0;
        //    int.TryParse(adjustMana.Text, out value);
        //    p.Mage.Mana -= value;
        //}

        //private void DamageAdd_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    int value = 0;
        //    int.TryParse(adjustDamage.Text, out value);
        //    p.Mage.Damage += value;
        //}

        //private void DamageSubtract_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    int value = 0;
        //    int.TryParse(adjustDamage.Text, out value);
        //    p.Mage.Damage -= value;
        //}

        //private void ArmorAdd_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    int value = 0;
        //    int.TryParse(adjustArmor.Text, out value);
        //    p.Mage.Armor += value;
        //}

        //private void ArmorSubtract_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    int value = 0;
        //    int.TryParse(adjustArmor.Text, out value);
        //    p.Mage.Armor -= value;
        //}

        private void TextBoxNumber_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //code provided from stack overflow: https://stackoverflow.com/questions/52624066/textbox-with-only-numbers
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void AddCardButton_Click(object sender, RoutedEventArgs e)
        {

            MWCard c = MWCard.Create(MWCard.SerialConverter(CardID.Text));
            c.SerialNumber = CardID.Text;
            p.Cards.Add(c);


        }
    }
}
