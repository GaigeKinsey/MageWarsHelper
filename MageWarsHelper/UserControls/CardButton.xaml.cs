using MageWarsHelper.Models;
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
    public sealed partial class CardButton : UserControl
    {
        public CardButton()
        {
            this.InitializeComponent();

            SerialIDToImageConverter converter = SerialIDToImageConverter.Instance;

            Binding bind = new Binding();
            bind.Source = mainControl.DataContext;
            bind.Path = new PropertyPath("SerialNumber");
            bind.Mode = BindingMode.OneWay;
            bind.Converter = converter;

            Card.SetBinding(ContentProperty, bind);


        }
    }
}
