using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace MageWarsHelper.Converters
{
    public class SubtypesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            List<Subtype> subtypes = (List<Subtype>)value;
            string finalString = "";
            foreach (Subtype subtype in subtypes)
            {
                finalString += $"{subtype.ToString()}, ";
            }
            if (finalString.Length > 2)
            {
                finalString = finalString.Remove(finalString.Length - 2);
            }
            else
            {
                finalString = "NONE";
            }

            return finalString.Replace("_", " ");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
