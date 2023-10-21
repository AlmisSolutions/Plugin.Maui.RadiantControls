using System.Globalization;

namespace Plugin.Maui.RadiantControls.Converters;

public class IndicatorVisibilityConverter : IMultiValueConverter
{
    public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.ElementAtOrDefault(0) is int maxVisible &&
            value.ElementAtOrDefault(1) is bool hideSingle &&
            value.ElementAtOrDefault(2) is int count &&
            parameter is View indicatorFrame &&
            indicatorFrame.BindingContext is int itemPosition)
        {
            if (count == 1)
            {
                return !hideSingle;
            }
            else if (maxVisible > -1)
            {
                return maxVisible > itemPosition;
            }
        }

        return true;
    }

    public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

