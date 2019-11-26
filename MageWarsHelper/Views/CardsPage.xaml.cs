using MageWarsHelper.Database;
using MageWarsHelper.UserControls;
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
    public sealed partial class CardsPage : Page
    {
        private MWPlayer player;
        private Popup cardPopup = new Popup();

        public CardsPage()
        {
            this.InitializeComponent();

            cardListView.ItemsSource = CardDatabase.Instance.Cards;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            player = (MWPlayer)e.Parameter;
        }

        private void cardListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MWCard card = CardDatabase.Instance.Cards.ElementAt(cardListView.SelectedIndex);

            DisplayImage(card);
        }

        private void DisplayImage(MWCard card)
        {
            CardButton button = new CardButton();

            Binding bind = new Binding();
            bind.Source = card;
            bind.Mode = BindingMode.OneWay;

            button.SetBinding(DataContextProperty, bind);
            button.Tapped += Button_Tapped;
            button.RightTapped += Button_RightTapped;

            cardPopup.Child = button;
            cardPopup.IsOpen = true;
        }

        private void Button_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            player.Spellbook.Add(CardDatabase.Instance.Cards.ElementAt(cardListView.SelectedIndex));
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            cardPopup.IsOpen = false;
        }

    }
}
