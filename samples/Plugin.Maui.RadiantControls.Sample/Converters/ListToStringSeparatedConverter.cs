using System.Collections;
using System.Globalization;

namespace Plugin.Maui.RadiantControls.Sample.Converters;

/// <summary>
/// Converts a list of objects to a single string, with items separated by a comma and a space.
/// </summary>
public class ListToStringSeparatedConverter : IValueConverter
{
    /// <summary>
    /// Converts the list to a comma-separated string.
    /// </summary>
    /// <param name="value">The list to convert.</param>
    /// <param name="targetType">The target type for the binding.</param>
    /// <param name="parameter">An optional parameter.</param>
    /// <param name="culture">The culture information.</param>
    /// <returns>A comma-separated string representation of the list, or null if the list is empty or null.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable list)
        {
            return string.Join(", ", list.Cast<object>()
                .Where(item => !string.IsNullOrWhiteSpace(item?.ToString()))
                .Select(item => item.ToString()));
        }

        return null;
    }

    [Obsolete("ConvertBack is not implemented.", true)]
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
