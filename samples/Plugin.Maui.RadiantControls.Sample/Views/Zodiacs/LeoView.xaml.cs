namespace Plugin.Maui.RadiantControls.Sample.Views.Zodiacs;

public partial class LeoView : ContentView
{
	public LeoView()
	{
		InitializeComponent();
    }

    public override string ToString()
    {
        return TitleLabel.Text;
    }
}
