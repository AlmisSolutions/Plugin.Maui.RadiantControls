namespace Plugin.Maui.RadiantControls.Sample.Views.Zodiacs;

public partial class PiscesView : ContentView
{
	public PiscesView()
	{
		InitializeComponent();
    }

    public override string ToString()
    {
        return TitleLabel.Text;
    }
}
