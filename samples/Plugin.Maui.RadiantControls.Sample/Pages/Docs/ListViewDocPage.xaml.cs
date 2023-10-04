using Plugin.Maui.RadiantControls.Sample.Pages.Playgrounds;
using Plugin.Maui.RadiantControls.Sample.Pages.Showcases;

namespace Plugin.Maui.RadiantControls.Sample.Pages.Docs;

public partial class ListViewDocPage : ContentPage
{
	public ListViewDocPage()
	{
		InitializeComponent();
    }

    private async void ShowcaseButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ListViewShowcasePage));
    }

    private async void PlaygroundButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ListViewPlaygroundPage));
    }
}
