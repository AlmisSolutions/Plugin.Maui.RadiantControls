namespace Plugin.Maui.RadiantControls.Sample.Views.Zodiacs;

public partial class AquariusView : ContentView
{
	public AquariusView()
	{
		InitializeComponent();
    }

    public override string ToString()
    {
        return TitleLabel.Text;
    }
}
