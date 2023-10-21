using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Plugin.Maui.RadiantControls.Extensions;

namespace Plugin.Maui.RadiantControls;

/// <summary>
/// Represents a custom list view control that provides enhanced features.
/// </summary>
public partial class RcListView : ContentView
{
    #region Private properties
    private object _currentItem;
    private IView _currentView;
    private int _currentIndex;
    private bool _canScrollToPreviousItem = false;
    private bool _canScrollToNextItem = true;
    private List<object> _visibleItems;
    private List<IView> _visibleViews;
    private List<int> _visibleIndexes;
    #endregion

    #region Public bindable properties
    /// <summary>
    /// Bindable property for the orientation.
    /// </summary>
    public static readonly BindableProperty OrientationProperty =
        BindableProperty.Create(
            nameof(Orientation),
            typeof(StackOrientation),
            typeof(RcListView),
            StackOrientation.Vertical,
            propertyChanged: OnOrientationChanged);

    /// <summary>
    /// Bindable property for the collection of items (views) displayed in the RcListView.
    /// </summary>
    public static readonly BindableProperty ItemsProperty =
        BindableProperty.Create(
            nameof(Items),
            typeof(ObservableCollection<IView>),
            typeof(RcListView),
            defaultValueCreator: (binding) => new ObservableCollection<IView>(),
            propertyChanged: OnItemsChanged);


    /// <summary>
    /// Bindable property for the data source of the RcListView, which provides the data for the items.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(RcListView),
            propertyChanged: OnItemsSourceChanged);

    /// <summary>
    /// Bindable property for the template used to display each item in the RcListView.
    /// </summary>
    public static readonly BindableProperty ItemTemplateProperty =
        BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(RcListView),
            propertyChanged: OnItemTemplateChanged);


    /// <summary>
    /// Bindable property for the template selector used to choose a template for each item in the RcListView.
    /// </summary>
    public static readonly BindableProperty ItemTemplateSelectorProperty =
        BindableProperty.Create(
            nameof(ItemTemplateSelector),
            typeof(DataTemplateSelector),
            typeof(RcListView),
            propertyChanged: OnItemTemplateSelectorChanged);

    /// <summary>
    /// Bindable property for the currently selected item.
    /// </summary>
    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(RcListView),
            propertyChanged: OnSelectedItemChanged);

    /// <summary>
    /// Bindable property for the currently selected view.
    /// </summary>
    public static readonly BindableProperty SelectedViewProperty =
        BindableProperty.Create(
            nameof(SelectedView),
            typeof(View),
            typeof(RcListView),
            propertyChanged: OnSelectedViewChanged);

    /// <summary>
    /// Bindable property for the currently selected index.
    /// </summary>
    public static readonly BindableProperty SelectedIndexProperty =
        BindableProperty.Create(
            nameof(SelectedIndex),
            typeof(int),
            typeof(RcListView),
            -1,
            propertyChanged: OnSelectedIndexChanged);

    /// <summary>
    /// Bindable property for the spacing between items.
    /// </summary>
    public static readonly BindableProperty ItemSpacingProperty = BindableProperty.Create(
        nameof(ItemSpacing),
        typeof(int),
        typeof(RcListView),
        default(int));


    /// <summary>
    /// Bindable property for the spacing between items.
    /// </summary>
    public static readonly BindableProperty IsScrollingEnabledProperty = BindableProperty.Create(
        nameof(IsScrollingEnabled),
        typeof(bool),
        typeof(RcListView),
        true,
        propertyChanged: OnIsScrollingEnabledChanged);
    #endregion

    #region Public properties
    /// <summary>
    /// Gets or sets the <see cref="StackOrientation"/> of the list view.
    /// </summary>
    public StackOrientation Orientation
    {
        get => (StackOrientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>
    /// Gets or sets the collection of views to be displayed in the list view.
    /// </summary>
    public ObservableCollection<IView> Items
    {
        get => (ObservableCollection<IView>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    /// <summary>
    /// Gets or sets the source of data to be displayed in the list view.
    /// </summary>
    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the template used to display each item in the list view.
    /// </summary>
    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    /// <summary>
    /// Gets or sets the template selector used to choose a template for each item in the list view.
    /// </summary>
    public DataTemplateSelector ItemTemplateSelector
    {
        get => (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty);
        set => SetValue(ItemTemplateSelectorProperty, value);
    }

    /// <summary>
    /// Gets or sets the currently selected item in the list view.
    /// </summary>
    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    /// <summary>
    /// Gets or sets the currently selected view in the list view.
    /// </summary>
    public IView SelectedView
    {
        get => (IView)GetValue(SelectedViewProperty);
        set => SetValue(SelectedViewProperty, value);
    }

    /// <summary>
    /// Gets or sets the currently selected index in the list view.
    /// </summary>
    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between items in the list view.
    /// </summary>
    public int ItemSpacing
    {
        get => (int)GetValue(ItemSpacingProperty);
        set => SetValue(ItemSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the content of the list view.
    /// </summary>
    public new View Content
    {
        get => base.Content;
        private set => base.Content = value;
    }

    /// <summary>
    /// Gets the list of currently visible items in the list view.
    /// </summary>
    public List<object> VisibleItems
    {
        get => _visibleItems;
        private set
        {
            _visibleItems = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets the list of currently visible views in the list view.
    /// </summary>
    public List<IView> VisibleViews
    {
        get => _visibleViews;
        private set
        {
            _visibleViews = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets the list of indexes for currently visible items in the list view.
    /// </summary>
    public List<int> VisibleIndexes
    {
        get => _visibleIndexes;
        private set
        {
            _visibleIndexes = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the list view can scroll to the previous item.
    /// </summary>
    public bool CanScrollToPreviousItem
    {
        get => _canScrollToPreviousItem;
        set
        {
            _canScrollToPreviousItem = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the list view can scroll to the next item.
    /// </summary>
    public bool CanScrollToNextItem
    {
        get => _canScrollToNextItem;
        set
        {
            _canScrollToNextItem = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the list view is currently being scrolled programmatically.
    /// When set to true, it signifies that the scrolling action was initiated by the code rather than by user interaction.
    /// </summary>
    public bool IsProgrammaticallyScrolling { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether scrolling is enabled.
    /// </summary>
    public bool IsScrollingEnabled
    {
        get => (bool)GetValue(IsScrollingEnabledProperty);
        set => SetValue(IsScrollingEnabledProperty, value);
    }
    #endregion

    #region Public event handlers
    public event EventHandler<RcListViewVisibilityChangedEventArgs> VisibilityChanged;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the RcListView class.
    /// </summary>
    public RcListView()
    {
        InitializeComponent();

        Items.CollectionChanged += Items_CollectionChanged;
    }
    #endregion

    #region Public methods
    /// <summary>
    /// Scrolls to the specified item in the list view.
    /// </summary>
    /// <param name="item">The item to scroll to.</param>
    /// <param name="scrollToPosition">The position to which to scroll within the viewport of the list view.</param>
    /// <param name="animated">Whether the scrolling should be animated.</param>
    public async Task ScrollToItemAsync<T>(T item, ScrollToPosition scrollToPosition, bool animated)
    {
        IsProgrammaticallyScrolling = true;

        var itemIndex = ItemsSource.Cast<object>().IndexOf(item);

         await ScrollToIndexAsync(itemIndex, scrollToPosition, animated);

        IsProgrammaticallyScrolling = false;
    }

    /// <summary>
    /// Scrolls to the specified view in the list view.
    /// </summary>
    /// <param name="view">The view to scroll to.</param>
    /// <param name="scrollToPosition">The position to which to scroll within the viewport of the list view.</param>
    /// <param name="animated">Whether the scrolling should be animated.</param>
    public async Task ScrollToViewAsync(IView view, ScrollToPosition scrollToPosition, bool animated)
    {
        if (view == null)
        {
            return;
        }

        UpdateScrollToItemAvailability();

        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await ScrollListView.ScrollToAsync(view as Element, scrollToPosition, animated);
        });
    }

    /// <summary>
    /// Scrolls to the item at the specified index in the list view.
    /// </summary>
    /// <param name="index">The index of the item to scroll to.</param>
    /// <param name="scrollToPosition">The position to which to scroll within the viewport of the list view.</param>
    /// <param name="animated">Whether the scrolling should be animated.</param>
    public async Task ScrollToIndexAsync(int index, ScrollToPosition scrollToPosition, bool animated)
    {
        IsProgrammaticallyScrolling = true;

        await ScrollToViewAsync(StackListView.Children.ElementAtOrDefault(index), scrollToPosition, animated);

        IsProgrammaticallyScrolling = false;
    }

    /// <summary>
    /// Scrolls to the previous item in the list view.
    /// </summary>
    /// <param name="scrollToPosition">The position to which to scroll within the viewport of the list view.</param>
    /// <param name="animated">Whether the scrolling should be animated.</param>
    public async Task ScrollToPreviousItemAsync(ScrollToPosition scrollToPosition, bool animated)
    {
        if (CanScrollToPreviousItem)
        {
            IsProgrammaticallyScrolling = true;

            _currentIndex--;
            _currentView = GetViewByIndex(_currentIndex);
            _currentItem = GetItemByIndex(_currentIndex);

            await ScrollToViewAsync(_currentView, scrollToPosition, animated);

            SetVisibleItems(ScrollListView.ScrollX, ScrollListView.ScrollY);

            UpdateScrollToItemAvailability();

            IsProgrammaticallyScrolling = false;
        }
    }

    /// <summary>
    /// Scrolls to the next item in the list view.
    /// </summary>
    /// <param name="scrollToPosition">The position to which to scroll within the viewport of the list view.</param>
    /// <param name="animated">Whether the scrolling should be animated.</param>
    public async Task ScrollToNextItemAsync(ScrollToPosition scrollToPosition, bool animated)
    {
        if (CanScrollToNextItem)
        {
            IsProgrammaticallyScrolling = true;

            _currentIndex++;
            _currentView = GetViewByIndex(_currentIndex);
            _currentItem = GetItemByIndex(_currentIndex);

            await ScrollToViewAsync(_currentView, scrollToPosition, animated);

            SetVisibleItems(ScrollListView.ScrollX, ScrollListView.ScrollY);

            UpdateScrollToItemAvailability();

            IsProgrammaticallyScrolling = false;
        }
    }
    #endregion

    #region Private static methods (bindable properties changed)
    /// <summary>
    /// Handles changes to the Items property.
    /// </summary>
    /// <param name="bindable">The object that the property has changed on.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is ObservableCollection<View> items &&
            bindable is RcListView listView)
        {
            listView.StackListView.Children.Clear();

            foreach (var view in items)
            {
                listView.AddItem(view);
            }

            listView.InitializeCurrentItem();
        }
    }

    /// <summary>
    /// Handles changes to the <see cref="SelectedItem"/> property.
    /// </summary>
    /// <param name="bindable">The object that the property has changed on.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcListView listView)
        {
            if (listView.ItemsSource == null && newValue is View newView)
            {
                listView.SelectedIndex = listView.Items.IndexOf(newView);
                listView.SelectedView = newView;
                //listView.SelectedItem = newView;
            }
            else
            {
                var items = listView.ItemsSource.Cast<object>().ToList();

                listView.SelectedIndex = items.IndexOf(newValue);
                listView.SelectedView = listView.StackListView.Children.ElementAtOrDefault(listView.SelectedIndex);
                //listView.SelectedItem = newValue;
            }
        }
    }

    /// <summary>
    /// Handles changes to the <see cref="SelectedView"/> property.
    /// </summary>
    /// <param name="bindable">The object that the property has changed on.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnSelectedViewChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcListView listView && newValue is IView newView)
        {
            if (listView.ItemsSource == null)
            {
                listView.SelectedIndex = listView.Items.IndexOf(newView);
                //listView.SelectedView = newView;
                listView.SelectedItem = newView;
            }
            else
            {
                var items = listView.ItemsSource.Cast<object>().ToList();

                listView.SelectedIndex = listView.StackListView.Children.IndexOf(newView);
                //listView.SelectedView = newView;
                listView.SelectedItem = items.ElementAtOrDefault(listView.SelectedIndex);
            }
        }
    }

    /// <summary>
    /// Handles changes to the <see cref="SelectedIndex"/> property.
    /// </summary>
    /// <param name="bindable">The object that the property has changed on.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcListView listView && newValue is int newIndex)
        {
            if (listView.ItemsSource == null)
            {
                //listView.SelectedIndex = newIndex;
                listView.SelectedView = listView.Items.ElementAtOrDefault(newIndex);
                listView.SelectedItem = listView.SelectedView;
            }
            else
            {
                var items = listView.ItemsSource.Cast<object>().ToList();

                //listView.SelectedIndex = newIndex;
                listView.SelectedView = listView.StackListView.ElementAtOrDefault(newIndex);
                listView.SelectedItem = items.ElementAtOrDefault(newIndex);
            }
        }
    }

    /// <summary>
    /// Handles changes to the <see cref="Orientation"/> property.
    /// </summary>
    /// <param name="bindable">The object that the property has changed on.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnOrientationChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcListView listView &&
            newValue is StackOrientation orientation)
        {
            listView.ScrollListView.Orientation = StackToScrollOrientation(orientation);
        }
    }

    /// <summary>
    /// Handles the change of the <see cref="IsScrollingEnabled"/> property for the <see cref="RcListView"/>.
    /// </summary>
    /// <param name="bindable">The bindable object where the property changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnIsScrollingEnabledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcListView listView && newValue is bool isScrollingEnabled)
        {
            var newVisibility = isScrollingEnabled ? ScrollBarVisibility.Default : ScrollBarVisibility.Never;

            if (listView.Orientation == StackOrientation.Horizontal)
            {
                listView.ScrollListView.HorizontalScrollBarVisibility = newVisibility;
            }
            else
            {
                listView.ScrollListView.VerticalScrollBarVisibility = newVisibility;
            }
        }
    }

    /// <summary>
    /// Handles changes to the <see cref="ItemsSource"/> property of the <see cref="RcListView"/>.
    /// </summary>
    /// <param name="bindable">The bindable object where the property changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcListView listView && newValue is IEnumerable items)
        {
            BindableLayout.SetItemsSource(listView.StackListView, items);
        }
    }

    /// <summary>
    /// Handles changes to the <see cref="ItemTemplate"/> property of the <see cref="RcListView"/>.
    /// </summary>
    /// <param name="bindable">The bindable object where the property changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcListView listView && newValue is DataTemplate dataTemplate)
        {
            BindableLayout.SetItemTemplate(listView.StackListView, dataTemplate);
        }
    }

    /// <summary>
    /// Handles changes to the <see cref="ItemTemplateSelector"/> property of the <see cref="RcListView"/>.
    /// </summary>
    /// <param name="bindable">The bindable object where the property changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnItemTemplateSelectorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcListView listView && newValue is DataTemplateSelector dataTemplateSelector)
        {
            BindableLayout.SetItemTemplateSelector(listView.StackListView, dataTemplateSelector);
        }
    }
    #endregion

    #region Private static methods
    /// <summary>
    /// Converts a <see cref="StackOrientation"/> value to its corresponding <see cref="ScrollOrientation"/> value.
    /// </summary>
    /// <param name="orientation">The <see cref="StackOrientation"/> value to be converted.</param>
    /// <returns>Returns the corrsponding <see cref="ScrollOrientation"/>.</returns>
    private static ScrollOrientation StackToScrollOrientation(StackOrientation orientation)
    {
        return orientation == StackOrientation.Horizontal
            ? ScrollOrientation.Horizontal : ScrollOrientation.Vertical;
    }
    #endregion

    #region Private event handlers
    /// <summary>
    /// Handles the CollectionChanged event of the Items collection.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        RemoveOldItems(e);
        AddNewItems(e);

        InitializeCurrentItem();
    }

    /// <summary>
    /// Handles the Scrolled event for the ScrollListView.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data that contains information about the scrolling action.</param>
    private void ScrollListView_Scrolled(object sender, ScrolledEventArgs e)
    {
        if (IsProgrammaticallyScrolling)
        {
            return;
        }

        SetVisibleItems(e.ScrollX, e.ScrollY);
    }

    /// <summary>
    /// Handles the ChildAdded event for StackListView.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data that contains the newly added child element.</param>
    private void StackListView_ChildAdded(object sender, ElementEventArgs e)
    {
        if (e.Element is View view)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();

            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;

            view.GestureRecognizers.Add(tapGestureRecognizer);

            ExecuteInvalidateLayout();
        }
    }

    /// <summary>
    /// Handles the ChildRemoved event for StackListView.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data that contains the removed child element.</param>
    private void StackListView_ChildRemoved(object sender, ElementEventArgs e)
    {
        if (e.Element is View view)
        {
            var tapGestureRecognizer = view.GestureRecognizers
                .FirstOrDefault(x => x is TapGestureRecognizer) as TapGestureRecognizer;

            if (tapGestureRecognizer != null)
            {
                tapGestureRecognizer.Tapped -= TapGestureRecognizer_Tapped;
            }

            view.GestureRecognizers.Remove(tapGestureRecognizer);
        }
    }

    /// <summary>
    /// Handles tap events for the child views in StackListView.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data that contains information about the tapped view.</param>
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is View view)
        {
            SelectedView = view;
            SelectedIndex = StackListView.Children.IndexOf(view);
            SelectedItem = GetItemByIndex(SelectedIndex);
        }
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Determines and sets the visible items based on the scroll position and viewport size.
    /// </summary>
    /// <param name="scrollX">The current horizontal scroll position.</param>
    /// <param name="scrollY">The current vertical scroll position.</param>
    private void SetVisibleItems(double scrollX, double scrollY)
    {
        double scrollPosition, viewportSize, contentSize;

        if (Orientation == StackOrientation.Horizontal)
        {
            scrollPosition = scrollX;
            viewportSize = ScrollListView.Width;
            contentSize = StackListView.Width;
        }
        else
        {
            scrollPosition = scrollY;
            viewportSize = ScrollListView.Height;
            contentSize = StackListView.Height;
        }

        var viewEndPosition = 0.0;

        var visibleIndexes = new List<int>();
        var visibleItems = new List<object>();
        var visibleViews = new List<IView>();

        for (var i = 0; i < StackListView.Children.Count; i++)
        {
            var child = StackListView.Children[i] as View;
            viewEndPosition = viewEndPosition + ItemSpacing + (Orientation == StackOrientation.Horizontal ? child.Width : child.Height);

            if (scrollPosition > viewEndPosition)
            {
                continue;
            }

            var visibleIndex = i;
            var visibleItem = GetItemByIndex(visibleIndex);
            var visibleView = visibleItem is IView view ? view :
                StackListView.Children.ElementAtOrDefault(visibleIndex);

            visibleIndexes.Add(visibleIndex);
            visibleItems.Add(visibleItem);
            visibleViews.Add(visibleView);

            if (viewEndPosition >= scrollPosition + viewportSize)
            {
                break;
            }
        }

        VisibleItems = visibleItems;
        VisibleViews = visibleViews;
        VisibleIndexes = visibleIndexes;

        if (!_visibleIndexes.Contains(_currentIndex))
        {
            _currentIndex = visibleIndexes.FirstOrDefault();
            _currentItem = visibleItems.FirstOrDefault();
            _currentView = visibleViews.FirstOrDefault();
        }

        VisibilityChanged?.Invoke(this, new RcListViewVisibilityChangedEventArgs(
            visibleItems, visibleViews, visibleIndexes));
    }

    /// <summary>
    /// Updates the flags that indicate if scrolling to the next or previous item in the list is possible.
    /// </summary>
    private void UpdateScrollToItemAvailability()
    {
        CanScrollToPreviousItem = _currentIndex > 0;
        CanScrollToNextItem = _currentIndex < StackListView.Count - 1;
    }

    /// <summary>
    /// Removes old items from the list view.
    /// </summary>
    /// <param name="e">The event data containing the old items.</param>
    private void RemoveOldItems(NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems != null && e.OldItems.Count > 0)
        {
            foreach (var view in e.OldItems.OfType<View>())
            {
                RemoveItem(view);
            }
        }
    }

    /// <summary>
    /// Adds new items to the list view.
    /// </summary>
    /// <param name="e">The event data containing the new items.</param>
    private void AddNewItems(NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null && e.NewItems.Count > 0)
        {
            foreach (var view in e.NewItems.OfType<View>())
            {
                AddItem(view);
            }
        }
    }

    /// <summary>
    /// Removes the specified item from the list view.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    private void RemoveItem(View item)
    {
        StackListView.Children.Remove(item);
    }

    /// <summary>
    /// Adds the specified item to the list view.
    /// </summary>
    /// <param name="item">The item to add.</param>
    private void AddItem(View item)
    {
        StackListView.Children.Add(item);
    }

    /// <summary>
    /// Initializes the CurrentItem and CurrentIndex properties.
    /// </summary>
    private void InitializeCurrentItem()
    {
        SetVisibleItems(ScrollListView.ScrollX, ScrollListView.ScrollY);
    }

    /// <summary>
    /// Retrieves an item from the collection by its index.
    /// </summary>
    /// <param name="index">The index of the item to retrieve.</param>
    /// <returns>The item at the specified index, or null if the index is out of range.</returns>
    private object GetItemByIndex(int index)
    {
        if (index < 0)
        {
            return null;
        }

        if (ItemsSource == null)
        {
            return Items.ElementAtOrDefault(index);
        }
        else
        {
            return ItemsSource.Cast<object>().ElementAtOrDefault(index);
        }
    }

    /// <summary>
    /// Retrieves a view from the collection by its index.
    /// </summary>
    /// <param name="index">The index of the view to retrieve.</param>
    /// <returns>The view at the specified index, or null if the index is out of range.</returns>
    private IView GetViewByIndex(int index)
    {
        if (index < 0)
        {
            return null;
        }

        if (ItemsSource == null)
        {
            return Items.ElementAtOrDefault(index);
        }
        else
        {
            return StackListView.Children.ElementAtOrDefault(index);
        }
    }

    /// <summary>
    /// Invalidates the layout.
    /// </summary>
    private async void ExecuteInvalidateLayout()
    {
        // TODO: Check the other platforms.
        if (DeviceInfo.Platform == DevicePlatform.macOS || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
        {
            await Task.Delay(1);

            InvalidateLayout();
        }
    }
    #endregion
}

/// <summary>
/// Event arguments for changes in the visibility of items, views, and indexes in an <see cref="RcListView"/>.
/// </summary>
public class RcListViewVisibilityChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the list of currently visible items.
    /// </summary>
    public List<object> VisibleItems { get; }

    /// <summary>
    /// Gets the list of currently visible views.
    /// </summary>
    public List<IView> VisibleViews { get; }

    /// <summary>
    /// Gets the list of indexes for currently visible items.
    /// </summary>
    public List<int> VisibleIndexes { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RcListViewVisibilityChangedEventArgs"/> class.
    /// </summary>
    /// <param name="visibleItems">List of currently visible items.</param>
    /// <param name="visibleViews">List of currently visible views.</param>
    /// <param name="visibleIndexes">List of indexes for currently visible items.</param>
    public RcListViewVisibilityChangedEventArgs(
        List<object> visibleItems,
        List<IView> visibleViews,
        List<int> visibleIndexes)
    {
        VisibleItems = visibleItems;
        VisibleViews = visibleViews;
        VisibleIndexes = visibleIndexes;
    }
}