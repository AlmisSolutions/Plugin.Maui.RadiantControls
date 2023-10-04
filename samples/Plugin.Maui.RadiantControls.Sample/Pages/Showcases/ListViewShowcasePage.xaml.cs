namespace Plugin.Maui.RadiantControls.Sample.Pages.Showcases;

public partial class ListViewShowcasePage : ContentPage
{
	public ListViewShowcasePage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(1);

        _ = ListViewWithGrowingContent.LoadViewAsync();
    }
}
