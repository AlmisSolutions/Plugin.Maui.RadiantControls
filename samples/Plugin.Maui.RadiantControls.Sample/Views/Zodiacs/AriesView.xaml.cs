namespace Plugin.Maui.RadiantControls.Sample.Views.Zodiacs;

public partial class AriesView : ContentView
{
	public AriesView()
	{
		InitializeComponent();
    }

    public override string ToString()
    {
        return TitleLabel.Text;
    }
}
