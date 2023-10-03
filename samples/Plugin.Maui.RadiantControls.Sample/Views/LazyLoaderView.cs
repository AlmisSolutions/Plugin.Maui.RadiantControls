using CommunityToolkit.Maui.Views;

namespace Plugin.Maui.RadiantControls.Sample.Views;

public class LazyLoaderView<TView> : LazyView where TView : View, new()
{
    public override async ValueTask LoadViewAsync()
    {
        Content = new ActivityIndicator
        {
            IsRunning = true,
            HorizontalOptions = LayoutOptions.Start,
        };

        await Task.Delay(1);

        Content = new TView { BindingContext = BindingContext };

        SetHasLazyViewLoaded(true);
    }
}
