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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MageWarsHelper.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DiceRoller : Page
    {
        public DiceRoller()
        {
            this.InitializeComponent();
        }

        private void TextBoxNumber_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //code provided from stack overflow: https://stackoverflow.com/questions/52624066/textbox-with-only-numbers
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
            
        }

        private void rollButton_Tapped(object sender, TappedRoutedEventArgs e)
        {

            int sides = 0;
            int.TryParse(numberOfSides.Text, out sides);
            if(sides > 1)
            {

                Random rng = new Random();
                numberRolled.Text = rng.Next(1, sides + 1).ToString();
            }
            else
            {
                numberRolled.Text = "invalid number of sides";
            }

        }
    }
}
