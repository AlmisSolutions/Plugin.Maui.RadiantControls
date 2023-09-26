namespace Plugin.Maui.RadiantControls.Sample.Views.Zodiacs;

public partial class SagittariusView : ContentView
{
	public SagittariusView()
	{
		InitializeComponent();
    }

    public override string ToString()
    {
        return TitleLabel.Text;
    }
}
