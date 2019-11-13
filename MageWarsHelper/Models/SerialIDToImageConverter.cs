using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace MageWarsHelper.Models
{
    class SerialIDToImageConverter : IValueConverter
    {

        Dictionary<String, Image> CardImageDictionary = new Dictionary<string, Image>();

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!CardImageDictionary.ContainsKey(value.ToString()))
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri("http://forum.arcanewonders.com/cards/" + value.ToString() + ".jpg"));
                
                CardImageDictionary.Add(value.ToString(), img);
            }
            return CardImageDictionary.GetValueOrDefault(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
