namespace Plugin.Maui.RadiantControls.Sample.Views.Zodiacs;

public partial class CancerView : ContentView
{
	public CancerView()
	{
		InitializeComponent();
    }

    public override string ToString()
    {
        return TitleLabel.Text;
    }
}
