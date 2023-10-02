namespace Plugin.Maui.RadiantControls.Sample.Pages.Playgrounds;

public partial class AvatarPlaygroundPage : ContentPage
{
    #region Private Fields
    #region Avatar
    private string _imageUrl;
    private string _placeholderImageUrl;
    private string _avatarPadding;
    private string _containerBackgroundColor = "Grey";
    private string _initialsTextColor = "White";
    private string _initials = "RC";
    private double _size = 100;
    private double _initialsFontSize = -1;
    private float _cornerRadius;
    private AvatarShape _shape;
    #endregion
    #region Badge
    private string _badgePadding = "5";
    private string _badgeBackgroundColor = "Green";
    private string _badgeBorderColor = "Transparent";
    private string _badgeTextColor = "#FFFFFF";
    private string _badgeText = "4";
    private double _badgeSize = -1;
    private double _badgeFontSize = -1;
    private bool _hasBadge = true;
    private BadgePosition _badgePosition;
    #endregion
    #endregion

    #region Public Properties
    #region Avatar
    public string ImageUrl
    {
        get => _imageUrl;
        set
        {
            _imageUrl = value;
            OnPropertyChanged();
        }
    }

    public string PlaceholderImageUrl
    {
        get => _placeholderImageUrl;
        set
        {
            _placeholderImageUrl = value;
            OnPropertyChanged();
        }
    }

    public string AvatarPadding
    {
        get => _avatarPadding;
        set
        {
            _avatarPadding = value;
            OnPropertyChanged();
        }
    }

    public string ContainerBackgroundColor
    {
        get => _containerBackgroundColor;
        set
        {
            _containerBackgroundColor = value;
            OnPropertyChanged();
        }
    }

    public string InitialsTextColor
    {
        get => _initialsTextColor;
        set
        {
            _initialsTextColor = value;
            OnPropertyChanged();
        }
    }

    public string Initials
    {
        get => _initials;
        set
        {
            _initials = value;
            OnPropertyChanged();
        }
    }

    public double Size
    {
        get => _size;
        set
        {
            _size = value;
            OnPropertyChanged();
        }
    }

    public double InitialsFontSize
    {
        get => _initialsFontSize;
        set
        {
            _initialsFontSize = value;
            OnPropertyChanged();
        }
    }

    public float CornerRadius
    {
        get => _cornerRadius;
        set
        {
            _cornerRadius = value;
            OnPropertyChanged();
        }
    }

    public AvatarShape Shape
    {
        get => _shape;
        set
        {
            _shape = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Badge
    public string BadgePadding
    {
        get => _badgePadding;
        set
        {
            _badgePadding = value;
            OnPropertyChanged();
        }
    }

    public string BadgeBackgroundColor
    {
        get => _badgeBackgroundColor;
        set
        {
            _badgeBackgroundColor = value;
            OnPropertyChanged();
        }
    }

    public string BadgeBorderColor
    {
        get => _badgeBorderColor;
        set
        {
            _badgeBorderColor = value;
            OnPropertyChanged();
        }
    }

    public string BadgeTextColor
    {
        get => _badgeTextColor;
        set
        {
            _badgeTextColor = value;
            OnPropertyChanged();
        }
    }

    public string BadgeText
    {
        get => _badgeText;
        set
        {
            _badgeText = value;
            OnPropertyChanged();
        }
    }

    public double BadgeSize
    {
        get => _badgeSize;
        set
        {
            _badgeSize = value;
            OnPropertyChanged();
        }
    }

    public double BadgeFontSize
    {
        get => _badgeFontSize;
        set
        {
            _badgeFontSize = value;
            OnPropertyChanged();
        }
    }

    public bool HasBadge
    {
        get => _hasBadge;
        set
        {
            _hasBadge = value;
            OnPropertyChanged();
        }
    }

    public BadgePosition BadgePosition
    {
        get => _badgePosition;
        set
        {
            _badgePosition = value;
            OnPropertyChanged();
        }
    }
    #endregion
    #endregion

    #region Constructors
    public AvatarPlaygroundPage()
	{
		InitializeComponent();

        BindingContext = this;
	}
    #endregion

    #region Private methods
    private void RandomImageButton_Clicked(object sender, EventArgs e)
    {
        ImageUrl = $"https://cataas.com/cat?{Guid.NewGuid()}";
    }

    private void PlaceholderImageButton_Clicked(object sender, EventArgs e)
    {
        PlaceholderImageUrl = "placeholder.png";
    }
    #endregion
}
