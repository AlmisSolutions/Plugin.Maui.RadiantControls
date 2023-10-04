using System.Collections.ObjectModel;

namespace Plugin.Maui.RadiantControls.Sample.ViewModels;

public class ZodiacsViewModel : BindableObject
{
    public ObservableCollection<ZodiacViewModel> Zodiacs { get; set; } =
        new ObservableCollection<ZodiacViewModel>(new List<ZodiacViewModel>
        {
            new ZodiacViewModel("Aries", "aries.png", "Mar 21 - Apr 19", "Energetic, adventurous, and competitive. They love challenges and taking action."),
            new ZodiacViewModel("Taurus", "taurus.png", "Apr 20 - May 20", "Practical, reliable, and patient. They value stability and enjoy the comforts of life."),
            new ZodiacViewModel("Gemini", "gemini.png", "May 21 - Jun 20", "Curious, adaptable, and communicative. They enjoy socializing and learning new things."),
            new ZodiacViewModel("Cancer", "cancer.png", "Jun 21 - Jul 22", "Emotional, nurturing, and empathetic. They value family and home life."),
            new ZodiacViewModel("Leo", "leo.png", "Jul 23 - Aug 22", "Confident, charismatic, and creative. They love attention and being in the spotlight."),
            new ZodiacViewModel("Virgo", "virgo.png", "Aug 23 - Sep 22", "Analytical, detail-oriented, and practical. They value precision and are often perfectionists."),
            new ZodiacViewModel("Libra", "libra.png", "Sep 23 - Oct 22", "Balanced, diplomatic, and social. They seek harmony and fairness in relationships."),
            new ZodiacViewModel("Scorpio", "scorpio.png", "Oct 23 - Nov 21", "Intense, passionate, and mysterious. They are driven by deep emotions and desires."),
            new ZodiacViewModel("Sagittarius", "sagittarius.png", "Nov 22 - Dec 21", "Optimistic, adventurous, and philosophical. They love exploring and seeking the truth."),
            new ZodiacViewModel("Capricorn", "capricorn.png", "Dec 22 - Jan 19", "Disciplined, ambitious, and responsible. They are goal-oriented and work hard to achieve success."),
            new ZodiacViewModel("Aquarius", "aquarius.png", "Jan 20 - Feb 18", "Innovative, independent, and humanitarian. They value originality and strive to make a difference."),
            new ZodiacViewModel("Pisces", "pisces.png", "Feb 19 - Mar 20", "Compassionate, intuitive, and artistic. They are dreamers who often have a strong spiritual connection."),
        });
}

public class ZodiacViewModel : BindableObject
{
    private string _name;
    private string _imagePath;
    private string _dateRange;
    private string _description;

    public ZodiacViewModel(string name, string imagePath, string dateRange, string description)
    {
        Name = name;
        ImagePath = imagePath;
        DateRange = dateRange;
        Description = description;
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public string ImagePath
    {
        get => _imagePath;
        set
        {
            _imagePath = value;
            OnPropertyChanged();
        }
    }

    public string DateRange
    {
        get => _dateRange;
        set
        {
            _dateRange = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }
}