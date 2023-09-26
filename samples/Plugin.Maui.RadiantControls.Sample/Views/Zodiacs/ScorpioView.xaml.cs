namespace Plugin.Maui.RadiantControls.Sample.Views.Zodiacs;

public partial class ScorpioView : ContentView
{
	public ScorpioView()
	{
		InitializeComponent();
    }

    public override string ToString()
    {
        return TitleLabel.Text;
    }
}
