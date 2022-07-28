using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ObserableCollection
{
    public static class Convertors
    {
        public static Brush HexColorToBrush(string hexColor)
        {
            if (string.IsNullOrWhiteSpace(hexColor))
            {
                return new SolidColorBrush(Colors.Red);
            }

            Color color;

            try
            {
                color = (Color)ColorConverter.ConvertFromString(hexColor);
            }
            catch (FormatException)
            {
                color = Colors.Red;
            }

            return new SolidColorBrush(color);
        }
    }
}
