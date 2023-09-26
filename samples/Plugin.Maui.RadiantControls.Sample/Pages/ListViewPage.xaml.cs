using Microsoft.Maui.Controls;

namespace Plugin.Maui.RadiantControls.Sample.Pages;

public partial class ListViewPage : ContentPage
{
    private int _spacing = 15;
    private StackOrientation _orientation = StackOrientation.Horizontal;
    private bool _animated;
    private ScrollToPosition _scrollToPosition;

    public ListViewPage()
	{
		InitializeComponent();

		BindingContext = this;
	}

    public int Spacing
    {
        get => _spacing;
        set
        {
            _spacing = value;
            OnPropertyChanged();
        }
    }

    public StackOrientation Orientation
    {
        get => _orientation;
        set
        {
            _orientation = value;
            OnPropertyChanged();
        }
    }

    public ScrollToPosition ScrollToPosition
    {
        get => _scrollToPosition;
        set
        {
            _scrollToPosition = value;
            OnPropertyChanged();
        }
    }

    public bool Animated
    {
        get => _animated;
        set
        {
            _animated = value;
            OnPropertyChanged();
        }
    }

    private async void ScrollToPreviousButton_Clicked(object sender, EventArgs e)
    {
        await ZodiacsList.ScrollToPreviousItemAsync(ScrollToPosition, Animated);
    }

    private async void ScrollToNextButton_Clicked(object sender, EventArgs e)
    {
        await ZodiacsList.ScrollToNextItemAsync(ScrollToPosition, Animated);
    }
}
