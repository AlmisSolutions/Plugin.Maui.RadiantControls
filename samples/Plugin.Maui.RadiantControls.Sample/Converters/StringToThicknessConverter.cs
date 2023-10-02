using System.Globalization;

namespace Plugin.Maui.RadiantControls.Sample.Converters;

/// <summary>
/// Converts a string to a <see cref="Thickness"/> struct.
/// </summary>
public class StringToThicknessConverter : IValueConverter
{
    /// <summary>
    /// Converts a string to a <see cref="Thickness"/> struct.
    /// </summary>
    /// <param name="value">The string value to convert.</param>
    /// <param name="targetType">The target type of the conversion.</param>
    /// <param name="parameter">Additional parameter for the conversion.</param>
    /// <param name="culture">The culture info.</param>
    /// <returns>A Thickness object or default value.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string thickness)
        {
            var sizes = thickness.Split(',');

            if (sizes.Length == 1 && double.TryParse(sizes[0], out double uniformSize))
            {
                return new Thickness(uniformSize);
            }
            else if (sizes.Length == 2 &&
                double.TryParse(sizes[0], out double horizontalSize) &&
                double.TryParse(sizes[1], out double verticalSize))
            {
                return new Thickness(horizontalSize, verticalSize);
            }
            else if (sizes.Length == 4 &&
                double.TryParse(sizes[0], out double left) &&
                double.TryParse(sizes[1], out double top) &&
                double.TryParse(sizes[2], out double right) &&
                double.TryParse(sizes[3], out double bottom))
            {
                return new Thickness(left, top, right, bottom);
            }
        }

        return default(Thickness);
    }

    /// <summary>
    /// Converts a <see cref="Thickness"/> struct back to a string.
    /// </summary>
    /// <param name="value">The Thickness object to convert.</param>
    /// <param name="targetType">The target type of the conversion.</param>
    /// <param name="parameter">Additional parameter for the conversion.</param>
    /// <param name="culture">The culture info.</param>
    /// <returns>A string representation of the Thickness object.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Thickness thickness)
        {
            return $"{thickness.Left},{thickness.Top},{thickness.Right},{thickness.Bottom}";
        }

        return null;
    }
}

