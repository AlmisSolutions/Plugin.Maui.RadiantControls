using System.Collections.ObjectModel;

namespace Plugin.Maui.RadiantControls.Sample.Pages.Showcases.Views;

public partial class ListViewWithGrowingContentView : Frame
{
    public ObservableCollection<string> Texts { get; set; } = new ObservableCollection<string>();

	public ListViewWithGrowingContentView()
	{
		InitializeComponent();

        BindingContext = this;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var lorem = new Bogus.DataSets.Lorem(locale: "en");
        int numOfSentences = new Random().Next(2, 6);
        string fullText = lorem.Sentences(numOfSentences);

        Texts.Add(fullText);
    }
}
