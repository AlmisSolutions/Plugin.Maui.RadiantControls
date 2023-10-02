using Plugin.Maui.RadiantControls.Sample.Pages.Playgrounds;
using Plugin.Maui.RadiantControls.Sample.Pages.Showcases;

namespace Plugin.Maui.RadiantControls.Sample.Pages.Docs;

public partial class AvatarDocPage : ContentPage
{
	public AvatarDocPage()
	{
		InitializeComponent();
	}

    private async void ShowcaseButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AvatarShowcasePage));
    }

    private async void PlaygroundButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AvatarPlaygroundPage));
    }
}
