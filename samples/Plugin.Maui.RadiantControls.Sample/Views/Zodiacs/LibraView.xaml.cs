namespace Plugin.Maui.RadiantControls.Sample.Views.Zodiacs;

public partial class LibraView : ContentView
{
	public LibraView()
	{
		InitializeComponent();
    }

    public override string ToString()
    {
        return TitleLabel.Text;
    }
}
