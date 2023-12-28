using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RoboticPaintingSimulator.Converters;

public class BooleanToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is bool))
            return Brushes.Transparent;

        var isPainted = (bool)value;
        if (!isPainted)
            return Brushes.Transparent;

        // Assuming the parameter is the color name
        var colorName = parameter as string;
        var color = (Color)ColorConverter.ConvertFromString(colorName);

        return new SolidColorBrush(color);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
