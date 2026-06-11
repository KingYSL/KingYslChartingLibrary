using ChartingLibrary1._4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartingLibrary1._4.Utilities
{
    public class ScaleTypeConverter : TypeConverter
    {


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
            {
                switch (s.Trim().ToLowerInvariant())
                {
                    case "datetime":
                        return new DateTimeScale();
                    case "numeric":
                        return new NumericScale();
                    default:
                        throw new NotSupportedException($"Cannot convert '{s}' to a Scale.");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        // Optional: allow converting back to string (useful for serialization)
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            => destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is DateTimeScale) return "DateTime";
                if (value is NumericScale) return "Numeric";
                return base.ConvertTo(context, culture, value, destinationType);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
