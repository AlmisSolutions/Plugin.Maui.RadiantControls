namespace Plugin.Maui.RadiantControls.Sample.Views.Zodiacs;

public partial class CapricornView : ContentView
{
	public CapricornView()
	{
		InitializeComponent();
    }

    public override string ToString()
    {
        return TitleLabel.Text;
    }
}
