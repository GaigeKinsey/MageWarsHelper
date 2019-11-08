using MageWarsHelper.MW_Elements;
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
    public sealed partial class DicePage : Page
    {
        public DicePage()
        {
            this.InitializeComponent();
        }

        private void TextBoxNumber_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //code provided from stack overflow: https://stackoverflow.com/questions/52624066/textbox-with-only-numbers
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
            
        }

        private void rollButton_Click(object sender, RoutedEventArgs e)
        {
            
            if((bool)effectDieCheck.IsChecked)
            {
                Random rng = new Random();
                effectResultNum.Text = rng.Next(1, 13).ToString();
            }

            if((bool)damageDiceCheck.IsChecked)
            {
                int numOfDie = 0;
                int crit = 0;
                int normal = 0;
                MWDice die = new MWDice();
                int.TryParse(numOfDamageDice.Text, out numOfDie);

                for(int i = 0; i < numOfDie; i++)
                {
                    die.Roll();
                    if(die.Crit)
                    {
                        crit += die.Result;
                    }
                    else
                    {
                        normal += die.Result;
                    }
                }
                CritDamageNum.Text = crit.ToString();
                NormalDamageNum.Text = normal.ToString();
                
            }


        }
    }
}
