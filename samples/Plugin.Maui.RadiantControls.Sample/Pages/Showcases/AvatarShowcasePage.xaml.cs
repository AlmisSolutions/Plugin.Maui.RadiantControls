namespace Plugin.Maui.RadiantControls.Sample.Pages.Showcases;

public partial class AvatarShowcasePage : ContentPage
{
	public AvatarShowcasePage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(1);

        _ = AvatarCircular.LoadViewAsync();
        _ = AvatarRounded.LoadViewAsync();
        _ = AvatarCircularWithBadge.LoadViewAsync();
        _ = AvatarCircularWithTextBadge.LoadViewAsync();
        _ = AvatarCircularWithBadgeBottomLeft.LoadViewAsync();
        _ = AvatarCircularPlaceholder.LoadViewAsync();
        _ = AvatarCircularPlaceholderInitials.LoadViewAsync();
    }
}
