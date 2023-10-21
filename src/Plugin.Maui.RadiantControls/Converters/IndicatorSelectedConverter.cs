using System.Globalization;

namespace Plugin.Maui.RadiantControls.Converters;

public class IndicatorSelectedConverter : IValueConverter
	{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int position &&
            parameter is View indicatorFrame &&
            indicatorFrame.BindingContext is int itemPosition)
        {
            return position == itemPosition;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

