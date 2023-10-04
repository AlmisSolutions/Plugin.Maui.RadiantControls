# RcListView

 The RcListView is a component that represents a custom list view control that provides enhanced features.

## Properties

| Property                | Description                            | Type                  |
|-------------------------|----------------------------------------|-----------------------|
| Orientation             | Layout orientation.                    | `StackOrientation`    |
| Items                   | Collection of items.                   | `ObservableCollection<IView>` |
| ItemsSource             | Data source.                           | `IEnumerable`         |
| ItemTemplate            | Item template.                         | `DataTemplate`        |
| ItemTemplateSelector    | Item template selector.                | `DataTemplateSelector`|
| SelectedItem            | Currently selected item.               | `object`              |
| SelectedView            | Currently selected view.               | `View`                |
| SelectedIndex           | Index of the selected item.            | `int`                 |
| ItemSpacing             | Spacing between items.                 | `int`                 |
| IsScrollingEnabled      | Whether scrolling is enabled.          | `bool`                |
| VisibleItems            | List of visible items.                 | `List<object>`        |
| VisibleViews            | List of visible views.                 | `List<IView>`         |
| VisibleIndexes          | List of visible indexes.               | `List<int>`           |
| CanScrollToPreviousItem | Indicates if scrolling to the previous item is possible. | `bool` |
| CanScrollToNextItem     | Indicates if scrolling to the next item is possible. | `bool`     |

## Methods

| Method                             | Description                                       |
|-----------------------------------|---------------------------------------------------|
| `ScrollToItemAsync<T>(item, scrollToPosition, animated)` | Scrolls to the specified item. |
| `ScrollToViewAsync(view, scrollToPosition, animated)` | Scrolls to the specified view. |
| `ScrollToIndexAsync(index, scrollToPosition, animated)` | Scrolls to the item at the specified index. |
| `ScrollToPreviousItemAsync(scrollToPosition, animated)` | Scrolls to the previous item if available. |
| `ScrollToNextItemAsync(scrollToPosition, animated)` | Scrolls to the next item if available. |

## Events

| Event                | Description                                           |
|----------------------|-------------------------------------------------------|
| VisibilityChanged    | Triggered when the visibility of items changes.       |
