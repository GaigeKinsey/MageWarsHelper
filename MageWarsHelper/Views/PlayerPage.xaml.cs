﻿using MageWarsHelper.Models;
using System;
using System.Collections.Generic;
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
        private SerialIDToImageConverter converter = new SerialIDToImageConverter();

        public PlayerPage()
        {
            this.InitializeComponent();
            mainStackPannel.DataContext = p;
            //Image img = new Image();
            //img.Source = new BitmapImage(new Uri("http://forum.arcanewonders.com/cards/BEASTMASTERABILITYOUTLINE.jpg"));
            //test.Content = img;

            p.Mage.SerialNumber = "BEASTMASTERABILITYOUTLINE";

            Binding b = new Binding();
            b.Source = p;
            b.Path = new PropertyPath("Mage.SerialNumber");
            b.Mode = BindingMode.OneWay;
            b.Converter = converter;

            test.SetBinding(ContentProperty, b);

        }

        private void channelAdd_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustChanel.Text, out value);
            p.Mage.Channeling += value;
        }

        private void channelSubtract_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustChanel.Text, out value);
            p.Mage.Channeling -= value;
        }

        private void lifeAdd_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustLife.Text, out value);
            p.Mage.Life += value;
        }

        private void lifeSubtract_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustLife.Text, out value);
            p.Mage.Life -= value;
        }

        private void manaAdd_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustMana.Text, out value);
            p.Mage.Mana += value;
        }

        private void manaSubtract_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustMana.Text, out value);
            p.Mage.Mana -= value;
        }

        private void DamageAdd_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustDamage.Text, out value);
            p.Mage.Damage += value;
        }

        private void DamageSubtract_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustDamage.Text, out value);
            p.Mage.Damage -= value;
        }

        private void ArmorAdd_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustArmor.Text, out value);
            p.Mage.Armor += value;
        }

        private void ArmorSubtract_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustArmor.Text, out value);
            p.Mage.Armor -= value;
        }

        private void TextBoxNumber_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //code provided from stack overflow: https://stackoverflow.com/questions/52624066/textbox-with-only-numbers
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
    }
}
