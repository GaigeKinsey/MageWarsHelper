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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MageWarsHelper.UserControls
{
    public sealed partial class MagePropDisplay : UserControl
    {
        public MWMage Mage { get; set; }
        public string Type
        {
            get { return (string)GetValue(TypeProperty); }
            set 
            {
                SetValue(TypeProperty, value); 
                
            }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(string), typeof(MagePropDisplay), null);

        public MagePropDisplay()
        {
            this.InitializeComponent();
            
        }

        private void TextBoxNumber_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //code provided from stack overflow: https://stackoverflow.com/questions/52624066/textbox-with-only-numbers
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void propAdd_Click(object sender, RoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustProp.Text, out value);

            if (Type == "Channeling")
            {
                Mage.Channeling += value;
                DataContext = Mage.Channeling;
            }
            else if (Type == "Life")
            {
                Mage.Life += value;
                DataContext = Mage.Life;
            }
            else if (Type == "Mana")
            {
                Mage.Mana += value;
                DataContext = Mage.Mana;
            }
            else if (Type == "Damage")
            {
                Mage.Damage += value;
                DataContext = Mage.Damage;
            }
            else if (Type == "Armor")
            {
                Mage.Armor += value;
                DataContext = Mage.Armor;
            }

        }

        private void propSubtract_Click(object sender, RoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustProp.Text, out value);
            if (Type == "Channeling")
            {
                Mage.Channeling -= value;
                DataContext = Mage.Channeling;
            }
            else if (Type == "Life")
            {
                Mage.Life -= value;
                DataContext = Mage.Life;
            }
            else if (Type == "Mana")
            {
                Mage.Mana -= value;
                DataContext = Mage.Mana;
            }
            else if (Type == "Damage")
            {
                Mage.Damage -= value;
                DataContext = Mage.Damage;
            }
            else if (Type == "Armor")
            {
                Mage.Armor -= value;
                DataContext = Mage.Armor;
            }
        }
    }
}
