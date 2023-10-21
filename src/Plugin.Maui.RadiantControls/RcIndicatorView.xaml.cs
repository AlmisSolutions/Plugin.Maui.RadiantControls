using System;
using System.Collections;
using System.Collections.ObjectModel;
using Plugin.Maui.RadiantControls.Converters;
using Plugin.Maui.RadiantControls.Extensions;

namespace Plugin.Maui.RadiantControls;

/// <summary>
/// Represents a component that displays indicators that represent the number of items, and current position. 
/// </summary>
public partial class RcIndicatorView : ContentView
{
    #region Public static readonly bindable properties
    /// <summary>
    /// Bindable property for the <see cref="Orientation"/>.
    /// </summary>
    public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
        nameof(Orientation),
        typeof(StackOrientation),
        typeof(RcIndicatorView),
        StackOrientation.Horizontal);

    /// <summary>
    /// Bindable property for the <see cref="IndicatorSpacing"/>.
    /// </summary>
    public static readonly BindableProperty IndicatorSpacingProperty = BindableProperty.Create(
        nameof(IndicatorSpacing),
        typeof(double),
        typeof(RcIndicatorView),
        10.0);

    /// <summary>
    /// Bindable property for the <see cref="Count"/>.
    /// </summary>
    public static readonly BindableProperty CountProperty = BindableProperty.Create(
        nameof(Count),
        typeof(int),
        typeof(RcIndicatorView),
        -1,
        propertyChanged: OnCountChangend);

    /// <summary>
    /// Bindable property for the <see cref="HideSingle"/>.
    /// </summary>
    public static readonly BindableProperty HideSingleProperty = BindableProperty.Create(
        nameof(HideSingle),
        typeof(bool),
        typeof(RcIndicatorView),
        true);

    /// <summary>
    /// Bindable property for the <see cref="IndicatorColor"/>.
    /// </summary>
    public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(
        nameof(IndicatorColor),
        typeof(Color),
        typeof(RcIndicatorView),
        Colors.Grey);

    /// <summary>
    /// Bindable property for the <see cref="IndicatorSize"/>.
    /// </summary>
    public static readonly BindableProperty IndicatorSizeProperty = BindableProperty.Create(
        nameof(IndicatorSize),
        typeof(double),
        typeof(RcIndicatorView),
        8.0,
        propertyChanged: OnIndicatorSizeChanged);

    /// <summary>
    /// Bindable property for the <see cref="IndicatorTemplate"/>.
    /// </summary>
    public static readonly BindableProperty IndicatorTemplateProperty = BindableProperty.Create(
        nameof(IndicatorTemplate),
        typeof(DataTemplate),
        typeof(RcIndicatorView),
        defaultValueCreator: IndicatorTemplateDefaultValueCreator);

    /// <summary>
    /// Bindable property for the <see cref="IndicatorsShape"/>.
    /// </summary>
    public static readonly BindableProperty IndicatorsShapeProperty = BindableProperty.Create(
        nameof(IndicatorsShape),
        typeof(IndicatorShape),
        typeof(RcIndicatorView),
        IndicatorShape.Circle,
        propertyChanged: OnIndicatorsShapeChanged);

    /// <summary>
    /// Bindable property for the <see cref="ItemsSource"/>.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IEnumerable),
        typeof(RcIndicatorView),
        propertyChanged: OnItemsSourceChanged);

    /// <summary>
    /// Bindable property for the <see cref="MaximumVisible"/>.
    /// </summary>
    public static readonly BindableProperty MaximumVisibleProperty = BindableProperty.Create(
        nameof(MaximumVisible),
        typeof(int),
        typeof(RcIndicatorView),
        -1);

    /// <summary>
    /// Bindable property for the <see cref="Position"/>.
    /// </summary>
    public static readonly BindableProperty PositionProperty = BindableProperty.Create(
        nameof(Position),
        typeof(int),
        typeof(RcIndicatorView),
        -1,
        BindingMode.TwoWay);

    /// <summary>
    /// Bindable property for the <see cref="SelectedIndicatorColor"/>.
    /// </summary>
    public static readonly BindableProperty SelectedIndicatorColorProperty = BindableProperty.Create(
        nameof(SelectedIndicatorColor),
        typeof(Color),
        typeof(RcIndicatorView),
        defaultValueCreator: SelectedIndicatorColorDefaultValueCreator);

    /// <summary>
    /// Bindable property for the <see cref="IndicatorCornerRadius"/>.
    /// </summary>
    public static readonly BindableProperty IndicatorCornerRadiusProperty = BindableProperty.Create(
        nameof(IndicatorCornerRadius),
        typeof(float),
        typeof(RcIndicatorView),
        0f);
    #endregion

    #region Public bindable properties
    /// <summary>
    /// Orientation of indicators.
    /// </summary>
    public StackOrientation Orientation
    {
        get => (StackOrientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>
    /// Spacing between indicators.
    /// </summary>
    public double IndicatorSpacing
    {
        get => (double)GetValue(IndicatorSpacingProperty);
        set => SetValue(IndicatorSpacingProperty, value);
    }

    /// <summary>
    /// Number of indicators.
    /// </summary>
    public int Count
    {
        get => (int)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    /// <summary>
    /// Indicates whether the indicator should be hidden when only one exists.
    /// </summary>
    public bool HideSingle
    {
        get => (bool)GetValue(HideSingleProperty);
        set => SetValue(HideSingleProperty, value);
    }

    /// <summary>
    /// Color of indicators.
    /// </summary>
    public Color IndicatorColor
    {
        get => (Color)GetValue(IndicatorColorProperty);
        set => SetValue(IndicatorColorProperty, value);
    }

    /// <summary>
    /// The size of indicators.
    /// </summary>
    public double IndicatorSize
    {
        get => (double)GetValue(IndicatorSizeProperty);
        set => SetValue(IndicatorSizeProperty, value);
    }

    /// <summary>
    /// Appearance of the indicators.
    /// </summary>
    public DataTemplate IndicatorTemplate
    {
        get => (DataTemplate)GetValue(IndicatorTemplateProperty);
        set => SetValue(IndicatorTemplateProperty, value);
    }

    /// <summary>
    /// Shape of the indicators.
    /// </summary>
    public IndicatorShape IndicatorsShape
    {
        get => (IndicatorShape)GetValue(IndicatorsShapeProperty);
        set => SetValue(IndicatorsShapeProperty, value);
    }

    /// <summary>
    /// Collection to which the indicators will correspond.
    /// </summary>
    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Maximum number of visible indicators.
    /// </summary>
    public int MaximumVisible
    {
        get => (int)GetValue(MaximumVisibleProperty);
        set => SetValue(MaximumVisibleProperty, value);
    }

    /// <summary>
    /// The currently selected indicator index.
    /// </summary>
    public int Position
    {
        get => (int)GetValue(PositionProperty);
        set => SetValue(PositionProperty, value);
    }

    /// <summary>
    /// Color of the selected indicator.
    /// </summary>
    public Color SelectedIndicatorColor
    {
        get => (Color)GetValue(SelectedIndicatorColorProperty);
        set => SetValue(SelectedIndicatorColorProperty, value);
    }

    /// <summary>
    /// The corner radius of indicators.
    /// </summary>
    public float IndicatorCornerRadius
    {
        get => (float)GetValue(IndicatorCornerRadiusProperty);
        set => SetValue(IndicatorCornerRadiusProperty, value);
    }

    /// <summary>
    /// The actual corner radius used for indicators.
    /// </summary>
    internal float RealIndicatorCornerRadius
    {
        get
        {
            if (IndicatorsShape == IndicatorShape.Circle)
            {
                return (float)IndicatorSize / 2;
            }
            else if (IndicatorsShape == IndicatorShape.Square)
            {
                return 0;
            }
            else
            {
                return IndicatorCornerRadius;
            }
        }
    }

    public int ItemsSourceCount
    {
        get
        {
            return ItemsSource?.Cast<object>().Count() ?? 0;
        }
    }
    #endregion

    public event EventHandler<SelectedPositionChangedEventArgs> PositionChanged;

    #region Constructors
    public RcIndicatorView()
	{
		InitializeComponent();
	}
    #endregion

    #region Private static methods (bindable properties)
    /// <summary>
    /// Called when the <see cref="Count"/> property changes.
    /// Updates the ItemsSource with a new set of items reflecting the new count.
    /// </summary>
    /// <param name="bindable">The bindable object that has changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnCountChangend(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcIndicatorView indicatorView && newValue is int count)
        {
            indicatorView.ItemsSource = null;

            if (indicatorView.ItemsSourceCount == 0)
            {
                var items = new List<int>();

                for (var i = 0; i < count; i++)
                {
                    items.Add(i);
                }

                indicatorView.ItemsSource = new ObservableCollection<int>(items);

                indicatorView.OnPropertyChanged(nameof(ItemsSourceCount));

                indicatorView.Position = 0;
            }
        }
    }

    /// <summary>
    /// Called when the <see cref="ItemsSource"/> property changes.
    /// </summary>
    /// <param name="bindable">The bindable object that has changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcIndicatorView indicatorView)
        {
            indicatorView.OnPropertyChanged(nameof(ItemsSourceCount));

            if (indicatorView.Position == -1)
            {
                indicatorView.Position = 0;
            }
        }
    }

    /// <summary>
    /// Called when the <see cref="IndicatorSize"/> property changes.
    /// Triggers a change in the <see cref="RealIndicatorCornerRadius"/> property.
    /// </summary>
    /// <param name="bindable">The bindable object where the change occurred.</param>
    /// <param name="oldValue">The old value of the 'IndicatorSize' property.</param>
    /// <param name="newValue">The new value of the 'IndicatorSize' property.</param>
    private static void OnIndicatorSizeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcIndicatorView indicatorView && newValue is double size)
        {
            indicatorView.OnPropertyChanged(nameof(RealIndicatorCornerRadius));
        }
    }

    /// <summary>
    /// Called when the <see cref="IndicatorsShape"/> property changes.
    /// Triggers a change in the <see cref="RealIndicatorCornerRadius"/> property.
    /// </summary>
    /// <param name="bindable">The bindable object where the change occurred.</param>
    /// <param name="oldValue">The old value of the 'IndicatorSize' property.</param>
    /// <param name="newValue">The new value of the 'IndicatorSize' property.</param>
    private static void OnIndicatorsShapeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcIndicatorView indicatorView && newValue is IndicatorShape indicatorShape)
        {
            indicatorView.OnPropertyChanged(nameof(RealIndicatorCornerRadius));
        }
    }

    /// <summary>
    /// Set the default value creator for the <see cref="IndicatorTemplate"/> property.
    /// </summary>
    /// <param name="bindable">The bindable object for which to create the default value.</param>
    /// <returns>The default value for the <see cref="IndicatorTemplate"/> property.</returns>
    private static object IndicatorTemplateDefaultValueCreator(BindableObject bindable)
    {
        if (bindable is RcIndicatorView indicatorView)
        {
            return indicatorView.Resources["DefaultIndicatorTemplate"];
        }

        return null;
    }

    /// <summary>
    /// Set the default value creator for the <see cref="SelectedIndicatorColor"/> property based on <see cref="AppTheme"/>.
    /// </summary>
    /// <param name="bindable">The bindable object for which to create the default value.</param>
    /// <returns>The default color value for the <see cref="SelectedIndicatorColor"/> property.</returns>
    private static object SelectedIndicatorColorDefaultValueCreator(BindableObject bindable)
    {
        return AppInfo.RequestedTheme == AppTheme.Light ? Colors.Black : Colors.White;
    }
    #endregion

    #region Private event handlers
    /// <summary>
    /// Sets the <see cref="Position"/> property based on the tapped indicator.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void IndicatorFrame_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is ContentView contentView && contentView.Content is Frame indicatorFrame)
        {
            if (indicatorFrame.BindingContext is int position)
            {
                Position = position;
                PositionChanged?.Invoke(indicatorFrame, new SelectedPositionChangedEventArgs(Position));
            }
            else if (indicatorFrame.BindingContext is object item)
            {
                Position = ItemsSource.Cast<object>().IndexOf(item);
                PositionChanged?.Invoke(indicatorFrame, new SelectedPositionChangedEventArgs(Position));
            }
        }
    }

    /// <summary>
    /// Triggers a change in the <see cref="MaximumVisible"/> property for the
    /// <see cref="IndicatorVisibilityConverter"/>.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void IndicatorFrame_BindingContextChanged(object sender, EventArgs e)
    {
        if (sender is Frame)
        {
            OnPropertyChanged(nameof(MaximumVisible));
        }
    }
    #endregion
}

/// <summary>
/// Specifies the possible shapes for <see cref="IndicatorView"/>.
/// </summary>
public enum IndicatorShape
{
    Circle,
    RoundedSquare,
    Square,
}