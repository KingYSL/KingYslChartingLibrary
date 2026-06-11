using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChartingLibrary1._4.Utilities
{
    public static class ColorHelper
    {
        public static SolidColorBrush FromHex(string hexCode)
        {
            // Remove the '#' if present, and ensure it's a valid format (6 or 8 characters).
            if (!hexCode.StartsWith("#") && !hexCode.StartsWith("#FF"))
            {
                hexCode = "#" + hexCode; //Add leading hash tag in case it was omitted

            }


            try
            {
                // Check for an Alpha component (8-digit hex code)
                if (hexCode.Length == 7)
                {
                    Color color = (Color)ColorConverter.ConvertFromString(hexCode);
                    return new SolidColorBrush(color);

                }
                else if (hexCode.Length == 9)
                {
                    Color color = (Color)ColorConverter.ConvertFromString(hexCode);
                    return new SolidColorBrush(color);
                }
                else
                {
                    throw new ArgumentException("Invalid hex code format. Must be #RRGGBB or #AARRGGBB");

                }


            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting hex code: {ex.Message}"); // Handle conversion errors gracefully
                return Brushes.Black; // Or any default brush you prefer.
            }
        }
    }
}

