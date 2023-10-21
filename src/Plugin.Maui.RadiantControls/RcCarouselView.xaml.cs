using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Plugin.Maui.RadiantControls;

public partial class RcCarouselView : ContentView
{
    #region Public static readonly bindable properties
    /// <summary>
    /// Bindable property for <see cref="Items"/>.
    /// </summary>
    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
        nameof(Items),
        typeof(ObservableCollection<View>),
        typeof(RcCarouselView),
        defaultValueCreator: (bindable) => new ObservableCollection<View>());

    /// <summary>
    /// Bindable property for <see cref="ItemsSource"/>.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(RcCarouselView),
            propertyChanged: OnItemSourceChanged);

    /// <summary>
    /// Bindable property for <see cref="ItemTemplate"/>.
    /// </summary>
    public static readonly BindableProperty ItemTemplateProperty =
        BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(RcCarouselView),
            propertyChanged: OnItemTemplateChanged);

    /// <summary>
    /// Bindable property for <see cref="IndicatorPosition"/>.
    /// </summary>
    public static readonly BindableProperty IndicatorPositionProperty = BindableProperty.Create(
        nameof(IndicatorPosition),
        typeof(IndicatorPosition),
        typeof(RcCarouselView),
        defaultValue: IndicatorPosition.Inside,
        propertyChanged: OnIndicatorPositionChanged);
    #endregion

    #region Public bindable properties
    /// <summary>
    /// The collection of views to be displayed in the carousel.
    /// </summary>
    public ObservableCollection<View> Items
    {
        get => (ObservableCollection<View>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    /// <summary>
    /// The source of data to be displayed in the carousel.
    /// </summary>
    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// The template used to display each item in the carousel.
    /// </summary>
    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    /// <summary>
    /// The position of the indicator view.
    /// </summary>
    public IndicatorPosition IndicatorPosition
    {
        get => (IndicatorPosition)GetValue(IndicatorPositionProperty);
        set => SetValue(IndicatorPositionProperty, value);
    }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the RcCarouselView class.
    /// </summary>
    public RcCarouselView()
    {
        InitializeComponent();
        Items.CollectionChanged += Items_CollectionChanged;
        CarouselListView.VisibilityChanged += CarouselListView_VisibilityChanged;

        CarouselIndicator.PositionChanged += CarouselIndicator_PositionChanged;
    }

    private void CarouselListView_VisibilityChanged(object sender, RcListViewVisibilityChangedEventArgs e)
    {
        if (e.VisibleIndexes.Count == 3)
        {
            CarouselIndicator.Position = CarouselListView.CurrentIndex;
        }
        else
        {
            CarouselIndicator.Position = CarouselListView.CurrentIndex;
        }
    }

    private async void CarouselIndicator_PositionChanged(object sender, SelectedPositionChangedEventArgs e)
    {
        if (e.SelectedPosition is int position)
        {
            await CarouselListView.ScrollToIndexAsync(position, ScrollToPosition.Start, true);
        }
    }
    #endregion

    #region Private static methods (bindable properties changed)
    /// <summary>
    /// Called when the <see cref="ItemsSource"/> property changes.
    /// Updates the ItemsSource with a new set of items.
    /// </summary>
    /// <param name="bindable">The bindable object that has changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcCarouselView carouselView && newValue is IEnumerable itemsSource)
        {
            carouselView.CarouselListView.ItemsSource = itemsSource;
        }
    }

    /// <summary>
    /// Called when the <see cref="ItemTemplate"/> property changes.
    /// Updates the ItemTemplate with a new template.
    /// </summary>
    /// <param name="bindable">The bindable object that has changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcCarouselView carouselView && newValue is DataTemplate dataTemplate)
        {
            carouselView.CarouselListView.ItemTemplate = dataTemplate;
        }
    }

    /// <summary>
    /// Called when <see cref="IndicatorPosition"/> property changes.
    /// Updates the row of IndicatorView, either stack on top or add in new row.
    /// </summary>
    /// <param name="bindable">The bindable object that has changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnIndicatorPositionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcCarouselView carouselView && newValue is IndicatorPosition position)
        {
            var carouselIndicatorRow = position == IndicatorPosition.Inside ? 0 : 1;

            Grid.SetRow(carouselView.CarouselIndicator, carouselIndicatorRow);
        }
    }
    #endregion

    #region Private event handlers
    /// <summary>
    /// Called when <see cref="Items"/> collection changes.
    /// Updates the carousel items with the new set of items.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        HandleNewCarouselItems(e.NewItems);
        HandleRemovedCarouselItems(e.OldItems);
        HandleCollectionReset(e.Action);
    }

    /// <summary>
    /// Called when item is added to carousel list view.
    /// Updates the height of each carousel item to be equal with carousel height.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void CarouselListView_ChildAdded(object sender, EventArgs e)
    {
        if (sender is View view)
        {
            var binding = new Binding(nameof(Height), source: CarouselListView);
            view.SetBinding(View.HeightRequestProperty, binding);
        }
    }
    #endregion

    #region Private methods
    private void HandleNewCarouselItems(IList newItems)
    {
        if (newItems == null)
        {
            return;
        }

        PopulateCarouselItems(newItems);
    }

    private void HandleRemovedCarouselItems(IList oldItems)
    {
        if (oldItems == null)
        {
            return;
        }

        foreach (View oldCarouselItem in oldItems)
        {
            RemoveCarouselItem(oldCarouselItem);
        }
    }

    private void HandleCollectionReset(NotifyCollectionChangedAction action)
    {
        if (action != NotifyCollectionChangedAction.Reset)
        {
            return;
        }

        CarouselListView.Items.Clear();

        foreach (var item in Items)
        {
            AddCarouselItem(item);
        }
    }

    private void AddCarouselItem(View item)
    {
        CarouselListView.Items.Add(item);
    }

    private void RemoveCarouselItem(View carouselItem)
    {
        CarouselListView.Items.Remove(carouselItem);
    }

    private void ClearCarouselItems()
    {
        CarouselListView.Items.Clear();
    }

    private void PopulateCarouselItems(IList carouselItems)
    {
        if (carouselItems == null)
        {
            return;
        }

        foreach (var carouselItem in carouselItems)
        {
            AddCarouselItem(carouselItem as View);
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is View dotView)
        {
            await CarouselListView.ScrollToItemAsync(dotView.BindingContext, ScrollToPosition.Start, true);

            CarouselListView.SelectedItem = dotView.BindingContext;
        }
    }

    /// <summary>
    /// Scroll to the previous item in the carousel.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private async void PreviousButton_Clicked(object sender, EventArgs e)
    {
        await CarouselListView.ScrollToPreviousItemAsync(ScrollToPosition.Start, true);
    }

    /// <summary>
    /// Scroll to the next item in the carousel.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private async void NextButton_Clicked(object sender, EventArgs e)
    {
        await CarouselListView.ScrollToNextItemAsync(ScrollToPosition.Start, true);
    }
    #endregion
}

/// <summary>
/// Enum representing the position of the indicator view.
/// </summary>
public enum IndicatorPosition
{
    Inside,
    Outside
}