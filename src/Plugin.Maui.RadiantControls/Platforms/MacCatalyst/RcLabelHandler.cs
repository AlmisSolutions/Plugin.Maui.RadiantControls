using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace Plugin.Maui.RadiantControls.Platforms.MacCatalyst;

/// <summary>
/// Handler for the RcLabel custom control on Mac Catalyst platform.
/// </summary>
public class RcLabelHandler : LabelHandler
{
    /// <summary>
    /// Connects the handler to the MauiLabel, and hooks up any necessary events.
    /// </summary>
    /// <param name="platformView">The native view.</param>
    protected override void ConnectHandler(MauiLabel platformView)
    {
        base.ConnectHandler(platformView);

        if (VirtualView is RcLabel label)
        {
            label.PropertyChanged += Label_PropertyChanged;

            if (!string.IsNullOrWhiteSpace(label.Text))
            {
                UpdateLabelHeight(label);
            }
        }

    }

    /// <summary>
    /// Disconnects the handler from the MauiLabel, and unhooks any previously hooked events.
    /// </summary>
    /// <param name="platformView">The native view.</param>
    protected override void DisconnectHandler(MauiLabel platformView)
    {
        base.DisconnectHandler(platformView);

        if (VirtualView is RcLabel label)
        {
            label.PropertyChanged -= Label_PropertyChanged;
        }
    }

    /// <summary>
    /// Event handler for when properties on the RcLabel change.
    /// </summary>
    /// <param name="sender">The sender (expected to be an RcLabel).</param>
    /// <param name="e">The event arguments.</param>
    private async void Label_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (sender is RcLabel label)
        {
            if (e.PropertyName == Label.TextProperty.PropertyName)
            {
                await WaitForTextUpdate(label);
                UpdateLabelHeight(label);
            }
        }
    }

    /// <summary>
    /// Resize to fit and then update the height of label.
    /// </summary>
    /// <param name="label">The RcLabel to be resized.</param>
    private async void UpdateLabelHeight(RcLabel label)
    {
        await WaitForTextUpdate(label);

        PlatformView.SizeToFit();
        label.HeightRequest = PlatformView.Frame.Height;
    }

    /// <summary>
    /// Waits for the PlatformView's text to match the RcLabel's text.
    /// </summary>
    /// <param name="label">The RcLabel whose text needs to be updated.</param>
    /// <returns>An awaitable Task.</returns>
    private async Task WaitForTextUpdate(RcLabel label)
    {
        // TODO: Seems like it always delays once, report to microsoft
        while (label.Text != PlatformView.Text)
        {
            await Task.Delay(1);
        }
    }
}

