namespace Plugin.Maui.RadiantControls.Sample.Views.Zodiacs;

public partial class GeminiView : ContentView
{
	public GeminiView()
	{
		InitializeComponent();
    }

    public override string ToString()
    {
        return TitleLabel.Text;
    }
}
