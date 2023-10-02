using Plugin.Maui.RadiantControls.Sample.Pages.Playgrounds;
using Plugin.Maui.RadiantControls.Sample.Pages.Showcases;

namespace Plugin.Maui.RadiantControls.Sample;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AvatarShowcasePage), typeof(AvatarShowcasePage));
        Routing.RegisterRoute(nameof(AvatarPlaygroundPage), typeof(AvatarPlaygroundPage));
        Routing.RegisterRoute(nameof(AvatarGroupShowcasePage), typeof(AvatarGroupShowcasePage));
        Routing.RegisterRoute(nameof(AvatarGroupPlaygroundPage), typeof(AvatarGroupPlaygroundPage));
    }
}