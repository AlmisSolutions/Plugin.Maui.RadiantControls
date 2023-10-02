using System.Collections.ObjectModel;

namespace Plugin.Maui.RadiantControls.Sample.Pages.Playgrounds;

public partial class AvatarGroupPlaygroundPage : ContentPage
{
    #region Private Fields
    private int _avatarsCount;
    private ObservableCollection<RcAvatar> _avatars;
    private StackDirection _stackDirection;
    #endregion

    #region Public Properties
    public int AvatarsCount
    {
        get => _avatarsCount;
        set
        {
            _avatarsCount = value;
            OnPropertyChanged();
        }
    }
    public ObservableCollection<RcAvatar> Avatars
    {
        get => _avatars;
        set
        {
            _avatars = value;
            OnPropertyChanged();
        }
    }
    public StackDirection StackDirection
    {
        get => _stackDirection;
        set
        {
            _stackDirection = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Constructors
    public AvatarGroupPlaygroundPage()
	{
		InitializeComponent();

        BindingContext = this;
    }
    #endregion

    #region Private methods
    private void GenerateAvatars_Clicked(object sender, EventArgs e)
    {
        var avatars = new List<RcAvatar>();

        for (var i = 0; i < AvatarsCount; i++)
        {
            avatars.Add(new RcAvatar
            {
                ImageSource = $"https://cataas.com/cat?{Guid.NewGuid()}",
                Size = 100,
            });
        }

        Avatars = new ObservableCollection<RcAvatar>(avatars);
    }
    #endregion
}
