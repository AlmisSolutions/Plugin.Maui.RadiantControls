namespace Plugin.Maui.RadiantControls.Sample.Pages.Showcases;

public partial class AvatarGroupShowcasePage : ContentPage
{
	public AvatarGroupShowcasePage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(1);

        _ = AvatarGroupBottomToTop.LoadViewAsync();
        _ = AvatarGroupTopToBottom.LoadViewAsync();
    }
}
