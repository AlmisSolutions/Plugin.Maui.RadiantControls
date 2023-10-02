using System.Globalization;

namespace Plugin.Maui.RadiantControls.Sample.Converters;

/// <summary>
/// Converts a string to a Color object.
/// </summary>
public class StringToColorConverter : IValueConverter
{
    /// <summary>
    /// Converts a string to a Color object.
    /// </summary>
    /// <param name="value">The string value to convert.</param>
    /// <param name="targetType">The target type of the conversion.</param>
    /// <param name="parameter">Additional parameter for the conversion.</param>
    /// <param name="culture">The culture info.</param>
    /// <returns>A Color object or default value.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        
        if (value is string colorStr)
        {
            return Color.TryParse(colorStr, out Color color) ? color : default;
        }

        return default(Color);
    }

    /// <summary>
    /// Converts a Color object back to a string.
    /// </summary>
    /// <param name="value">The Color object to convert.</param>
    /// <param name="targetType">The target type of the conversion.</param>
    /// <param name="parameter">Additional parameter for the conversion.</param>
    /// <param name="culture">The culture info.</param>
    /// <returns>A string representation of the Color object.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Color color)
        {
            return color.ToHex();
        }

        return null;
    }
}

