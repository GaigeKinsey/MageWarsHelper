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
    public class SerialIDToImageConverter : IValueConverter
    {
        private Dictionary<String, BitmapImage> CardImageDictionary = new Dictionary<string, BitmapImage>();

        private static SerialIDToImageConverter instance = new SerialIDToImageConverter();

        public static SerialIDToImageConverter Instance
        {
            get { return instance; }
            set { instance = value; }
        }



        private SerialIDToImageConverter() 
        {}

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BitmapImage bitImg = new BitmapImage();
            if (!CardImageDictionary.ContainsKey((string)value))
            {
                bitImg = new BitmapImage(new Uri("http://forum.arcanewonders.com/cards/" + (string)value + ".jpg"));
                
                CardImageDictionary.Add((string)value, bitImg);
            }
            
            if(CardImageDictionary.TryGetValue((string)value, out bitImg))
            {
                Image img = new Image() { Source = bitImg };
                return img;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
