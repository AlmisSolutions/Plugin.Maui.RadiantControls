using System.Globalization;

namespace Plugin.Maui.RadiantControls.Sample.Converters;

/// <summary>
/// Converts a string value to its boolean equivalent.
/// </summary>
public class StringToBooleanConverter : IValueConverter
{
    /// <summary>
    /// Converts a string to a boolean value.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <param name="targetType">The target conversion type.</param>
    /// <param name="parameter">An optional parameter.</param>
    /// <param name="culture">Culture info, if needed.</param>
    /// <returns>True if the string is "true", otherwise false.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string strValue)
        {
            return strValue?.ToLowerInvariant() == "true";
        }

        return false;
    }

    /// <summary>
    /// Converts a boolean value back to its string equivalent ("true" or "false").
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <param name="targetType">The target conversion type.</param>
    /// <param name="parameter">An optional parameter.</param>
    /// <param name="culture">Culture info, if needed.</param>
    /// <returns>"true" if value is true, "false" otherwise.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? "True" : "False";
        }
        return "False";
    }
}

