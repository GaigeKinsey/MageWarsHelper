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
        private List<MWCard> cardDatabase = CardDatabase.Instance.Cards;
        private List<MWCard> displayedCards;

        public CardsPage()
        {
            this.InitializeComponent();

            Search();
            mainGrid.SizeChanged += MainGrid_SizeChanged;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            cardPopup.IsOpen = false;
        }

        private void MainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            cardListView.ItemsSource = null;
            
            Search();
        }

        private void Search()
        {
            string name = nameSearch.Text;
            string type = "";
            if (typeSearch.SelectedItem != null)
            {
                type = typeSearch.SelectedItem.ToString();
            }
            string subtype = subtypeSearch.Text;
            string school = "";
            if (schoolSearch.SelectedItem != null)
            {
                school = schoolSearch.SelectedItem.ToString();
            }
            string level = levelSearch.Text;
            string cost = costSearch.Text;
            string reveal = revealSearch.Text;

            displayedCards = cardDatabase;

            if (name != "")
            {
                displayedCards = displayedCards.Where(c => c.Name.ToUpper().Contains(name.ToUpper())).ToList();
            }
            if (type != "" && type != "All")
            {
                displayedCards = displayedCards.Where(c => c.CardType.ToUpper().Contains(type.ToUpper())).ToList();
            }
            if (subtype != "")
            {
                displayedCards = displayedCards.Where(c => c.Subtypes.Where(s => s.ToString().ToUpper().Contains(subtype.ToUpper())).ToList().Count() > 0).ToList();
            }
            if (school != "" && school != "All")
            {
                displayedCards = displayedCards.Where(c => c.Schools.ToUpper().Contains(school.ToUpper())).ToList();
            }
            if (level != "")
            {
                displayedCards = displayedCards.Where(c => c.Levels.ToUpper().Contains(level.ToUpper())).ToList();
            }
            if (cost != "")
            {
                displayedCards = displayedCards.Where(c => c.ManaCostString.ToUpper().Contains(cost.ToUpper())).ToList();
            }

            if (reveal != "")
            {
                List<MWEnchantment> enchantments = new List<MWEnchantment>();
                foreach (MWCard card in displayedCards)
                {
                    if (card.GetType() == typeof(MWEnchantment))
                    {
                        enchantments.Add((MWEnchantment)card);
                    }
                }

                enchantments = enchantments.Where(c => c.RevealCostString.ToUpper().Contains(reveal.ToUpper())).ToList();

                cardListView.ItemsSource = enchantments;
            }
            else
            {
                cardListView.ItemsSource = displayedCards;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            player = (MWPlayer)e.Parameter;
        }

        private void DisplayImage(MWCard card)
        {
            CardButton button = new CardButton();

            Binding bind = new Binding();
            bind.Source = card;
            bind.Mode = BindingMode.OneWay;

            button.SetBinding(DataContextProperty, bind);
            button.Tapped += Button_Tapped;

            //cardPopup.Margin = new Thickness(100);
            cardPopup.VerticalOffset = (this.Frame.ActualHeight / 2) - (255);
            cardPopup.HorizontalOffset = (this.Frame.ActualWidth / 2) - (183);

            cardPopup.Child = button;
            cardPopup.IsOpen = true;
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            cardPopup.IsOpen = false;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void CardRowControl_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            MWCard card = (MWCard)element.DataContext;

            player.Spellbook.Add(card);
        }

        private void CardRowControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            MWCard card = (MWCard)element.DataContext;
            DisplayImage(card);
        }
    }
}
