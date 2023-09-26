namespace Plugin.Maui.RadiantControls.Sample.Views.Zodiacs;

public partial class TaurusView : ContentView
{
	public TaurusView()
	{
		InitializeComponent();
    }

    public override string ToString()
    {
        return TitleLabel.Text;
    }
}
