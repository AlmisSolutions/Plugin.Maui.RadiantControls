using System.Globalization;

namespace Plugin.Maui.RadiantControls.Converters;

/// <summary>
/// Converts the selected index in a Carousel to a boolean value
/// indicating whether it's currently selected or not.
/// </summary>
public class CarouselIndexSelectedConverter : IValueConverter
{
    /// <summary>
    /// Converts the value to a boolean indicating selection status.
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is List<object> valueObjects && parameter is View parameterObject)
        {
            return valueObjects.Any(x => x == parameterObject.BindingContext);
        }
        return false;
    }

    /// <summary>
    /// ConvertBack is not implemented.
    /// </summary>
    [Obsolete("ConvertBack is not implemented.", true)]
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

