using System.Collections;
using System.Globalization;
using Plugin.Maui.RadiantControls.Extensions;

namespace Plugin.Maui.RadiantControls.Converters;

public class IndicatorSelectedConverter : IMultiValueConverter
{
    public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.ElementAtOrDefault(0) is int position &&
            value.ElementAtOrDefault(1) is IEnumerable items &&
            parameter is View indicatorFrame)
        {
            var itemPosition = indicatorFrame.BindingContext is int pos ? pos
                : items.Cast<object>().IndexOf(indicatorFrame.BindingContext);

            return position == itemPosition;
        }

        return false;
    }

    public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

