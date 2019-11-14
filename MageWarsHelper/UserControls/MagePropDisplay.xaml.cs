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
        public MagePropDisplay()
        {
            this.InitializeComponent();
        }

        private void TextBoxNumber_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //code provided from stack overflow: https://stackoverflow.com/questions/52624066/textbox-with-only-numbers
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void propSubtract_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int value = 0;
            int.TryParse(adjustProp.Text, out value);
            //mainPropStack.DataContext = mainPropStack.DataContext - value;
        }

        private void propAdd_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
