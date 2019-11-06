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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MageWarsHelper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("home", typeof(Views.PlayerPage)),
        };

        public MainPage()
        {
            this.InitializeComponent();


        }

        private void navView_Loaded(object sender, RoutedEventArgs e)
        {
            navView.SelectedItem = navView.MenuItems[0];

            NavView_Navigate("home", new EntranceNavigationTransitionInfo());
        }

        private void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type _page = null;
            _page = typeof(Views.PlayerPage);
            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = Frame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (!(_page is null) && !Type.Equals(preNavPageType, _page))
            {
                contentFrame.Navigate(_page, null, transitionInfo);
            }
        }
    }
}
