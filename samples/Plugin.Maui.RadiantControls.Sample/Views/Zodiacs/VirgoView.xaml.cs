namespace Plugin.Maui.RadiantControls.Sample.Views.Zodiacs;

public partial class VirgoView : ContentView
{
	public VirgoView()
	{
		InitializeComponent();
    }

    public override string ToString()
    {
        return TitleLabel.Text;
    }
}
