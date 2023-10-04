namespace Plugin.Maui.RadiantControls;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UserRadiantControls(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers(handlers =>
        {
#if MACCATALYST
            handlers.TryAddHandler<RcLabel, Platforms.MacCatalyst.RcLabelHandler>();
#endif
        });

        return builder;
    }
}

