namespace Plugin.Maui.RadiantControls;

/// <summary>
/// Represents a customizable avatar view.
/// </summary>
public partial class RcAvatar : ContentView
{
    #region Private fields
    private bool _hasBadgeBindingSet = false;
    private TaskCompletionSource<bool> _viewLoadedTcs = new TaskCompletionSource<bool>();
    #endregion

    #region Public bindable properties
    /// <summary>
    /// Bindable property for the margin around the avatar.
    /// </summary>
    public static readonly BindableProperty AvatarPaddingProperty = BindableProperty.Create(
        propertyName: nameof(AvatarPadding),
        returnType: typeof(Thickness),
        declaringType: typeof(RcAvatar),
        defaultValue: default(Thickness),
        propertyChanged: OnAvatarMarginChanged);

    /// <summary>
    /// Bindable property for the background color of the avatar's container.
    /// </summary>
    public static readonly BindableProperty ContainerBackgroundColorProperty = BindableProperty.Create(
        propertyName: nameof(ContainerBackgroundColor),
        returnType: typeof(Color),
        declaringType: typeof(RcAvatar),
        defaultValue: default(Color));

    /// <summary>
    /// Bindable property for the corner radius of the avatar, determining its roundness.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        propertyName: nameof(CornerRadius),
        returnType: typeof(float),
        declaringType: typeof(RcAvatar),
        defaultValue: default(float),
        propertyChanged: OnCornerRadiusChanged);

    /// <summary>
    /// Bindable property for the shape of the avatar, e.g., circle or square.
    /// </summary>
    public static readonly BindableProperty ShapeProperty = BindableProperty.Create(
        propertyName: nameof(Shape),
        returnType: typeof(AvatarShape),
        declaringType: typeof(RcAvatar),
        defaultValue: AvatarShape.Circle,
        propertyChanged: OnShapeChanged);

    /// <summary>
    /// Bindable property for the size of the avatar.
    /// </summary>
    public static readonly BindableProperty SizeProperty = BindableProperty.Create(
        propertyName: nameof(Size),
        returnType: typeof(double),
        declaringType: typeof(RcAvatar),
        defaultValue: -1.0,
        propertyChanged: OnSizeChanged);

    /// <summary>
    /// Bindable property for the image source of the avatar.
    /// </summary>
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
        propertyName: nameof(ImageSource),
        returnType: typeof(ImageSource),
        declaringType: typeof(RcAvatar),
        defaultValue: default(ImageSource),
        propertyChanged: OnImageSourceChanged);

    /// <summary>
    /// Bindable property for a placeholder image to be shown when the main image isn't available.
    /// </summary>
    public static readonly BindableProperty PlaceholderImageSourceProperty = BindableProperty.Create(
        propertyName: nameof(PlaceholderImageSource),
        returnType: typeof(ImageSource),
        declaringType: typeof(RcAvatar),
        defaultValue: default(ImageSource),
        propertyChanged: OnPlaceholderImageSourceChanged);

    /// <summary>
    /// Bindable property for the initials to be displayed on the avatar.
    /// </summary>
    public static readonly BindableProperty InitialsProperty = BindableProperty.Create(
        propertyName: nameof(Initials),
        returnType: typeof(string),
        declaringType: typeof(RcAvatar),
        defaultValue: null);

    /// <summary>
    /// Bindable property for the style of the initials label.
    /// </summary>
    public static readonly BindableProperty InitialsLabelStyleProperty = BindableProperty.Create(
        propertyName: nameof(InitialsLabelStyle),
        returnType: typeof(RcLabelStyle),
        declaringType: typeof(RcAvatar),
        defaultValueCreator: (bindable) => new RcLabelStyle
        {
            Padding = 10
        });

    /// <summary>
    /// Bindable property for indicating whether the RcAvatar has a badge.
    /// </summary>
    public static readonly BindableProperty HasBadgeProperty = BindableProperty.Create(
        propertyName: nameof(HasBadge),
        returnType: typeof(bool),
        declaringType: typeof(RcAvatar));

    /// <summary>
    /// Bindable property for the size of the badge.
    /// </summary>
    public static readonly BindableProperty BadgeSizeProperty = BindableProperty.Create(
        propertyName: nameof(BadgeSize),
        returnType: typeof(double),
        declaringType: typeof(RcAvatar),
        defaultValue: -1.0);

    /// <summary>
    /// Bindable property for the style of the badge label.
    /// </summary>
    public static readonly BindableProperty BadgeLabelStyleProperty = BindableProperty.Create(
        propertyName: nameof(BadgeLabelStyle),
        returnType: typeof(RcLabelStyle),
        declaringType: typeof(RcAvatar),
        defaultValueCreator: (bindable) => new RcLabelStyle());

    /// <summary>
    /// Bindable property for the padding of the inner badge.
    /// </summary>
    public static readonly BindableProperty BadgePaddingProperty = BindableProperty.Create(
        propertyName: nameof(BadgePadding),
        returnType: typeof(Thickness),
        declaringType: typeof(RcAvatar),
        defaultValue:   default(Thickness),
        propertyChanged: OnBadgePaddingChanged);

    /// <summary>
    /// Bindable property for the position of the badge on the avatar.
    /// </summary>
    public static readonly BindableProperty BadgePositionProperty = BindableProperty.Create(
        propertyName: nameof(BadgePosition),
        returnType: typeof(BadgePosition),
        declaringType: typeof(RcAvatar),
        defaultValue: BadgePosition.BottomRight,
        propertyChanged: OnBadgePositionChanged);

    /// <summary>
    /// Bindable property for the text displayed on the avatar's badge.
    /// </summary>
    public static readonly BindableProperty BadgeTextProperty = BindableProperty.Create(
        propertyName: nameof(BadgeText),
        returnType: typeof(string),
        declaringType: typeof(RcAvatar));

    /// <summary>
    /// Bindable property for the background color of the avatar's badge.
    /// </summary>
    public static readonly BindableProperty BadgeBackgroundColorProperty = BindableProperty.Create(
        propertyName: nameof(BadgeBackgroundColor),
        returnType: typeof(Color),
        declaringType: typeof(RcAvatar),
        defaultValue: default(Color));

    /// <summary>
    /// Bindable property for the border color of the avatar's badge.
    /// </summary>
    public static readonly BindableProperty BadgeBorderColorProperty = BindableProperty.Create(
        propertyName: nameof(BadgeBorderColor),
        returnType: typeof(Color),
        declaringType: typeof(RcAvatar),
        defaultValue: Colors.Transparent);
    #endregion

    #region Public properties
    /// <summary>
    /// Padding around the avatar.
    /// </summary>
    public Thickness AvatarPadding
    {
        get => (Thickness)GetValue(AvatarPaddingProperty);
        set => SetValue(AvatarPaddingProperty, value);
    }

    /// <summary>
    /// Background color of the avatar's container.
    /// </summary>
    public Color ContainerBackgroundColor
    {
        get => (Color)GetValue(ContainerBackgroundColorProperty);
        set => SetValue(ContainerBackgroundColorProperty, value);
    }

    /// <summary>
    /// Shape of the avatar.
    /// </summary>
    public AvatarShape Shape
    {
        get => (AvatarShape)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    /// <summary>
    /// Corner radius of the avatar (ignored for <see cref="AvatarShape.Circle"/> or <see cref="AvatarShape.Square"/> shapes).
    /// </summary>
    public float CornerRadius
    {
        get => (float)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Size of the avatar.
    /// </summary>
    public double Size
    {
        get => (double)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Image source for the avatar.
    /// </summary>
    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    /// <summary>
    /// Placeholder image to be shown when the main image isn't available.
    /// </summary>
    public ImageSource PlaceholderImageSource
    {
        get => (ImageSource)GetValue(PlaceholderImageSourceProperty);
        set => SetValue(PlaceholderImageSourceProperty, value);
    }

    /// <summary>
    /// Initials to be displayed on the avatar.
    /// </summary>
    public string Initials
    {
        get => (string)GetValue(InitialsProperty);
        set => SetValue(InitialsProperty, value);
    }

    /// <summary>
    /// Style for the initials label.
    /// </summary>
    public RcLabelStyle InitialsLabelStyle
    {
        get => (RcLabelStyle)GetValue(InitialsLabelStyleProperty);
        set => SetValue(InitialsLabelStyleProperty, value);
    }

    /// <summary>
    /// Style for the badge label.
    /// </summary>
    public RcLabelStyle BadgeLabelStyle
    {
        get => (RcLabelStyle)GetValue(BadgeLabelStyleProperty);
        set => SetValue(BadgeLabelStyleProperty, value);
    }

    /// <summary>
    /// Indicates if the avatar has a badge.
    /// </summary>
    public bool HasBadge
    {
        get => (bool)GetValue(HasBadgeProperty);
        set => SetValue(HasBadgeProperty, value);
    }

    /// <summary>
    /// Size of the badge.
    /// </summary>
    public double BadgeSize
    {
        get => (double)GetValue(BadgeSizeProperty);
        set => SetValue(BadgeSizeProperty, value);
    }

    /// <summary>
    /// Padding for the badge.
    /// </summary>
    public Thickness BadgePadding
    {
        get => (Thickness)GetValue(BadgePaddingProperty);
        set => SetValue(BadgePaddingProperty, value);
    }

    /// <summary>
    /// Position of the badge on the avatar.
    /// </summary>
    public BadgePosition BadgePosition
    {
        get => (BadgePosition)GetValue(BadgePositionProperty);
        set => SetValue(BadgePositionProperty, value);
    }

    /// <summary>
    /// Text displayed on the avatar's badge.
    /// </summary>
    public string BadgeText
    {
        get => (string)GetValue(BadgeTextProperty);
        set => SetValue(BadgeTextProperty, value);
    }

    /// <summary>
    /// Background color of the avatar's badge.
    /// </summary>
    public Color BadgeBackgroundColor
    {
        get => (Color)GetValue(BadgeBackgroundColorProperty);
        set => SetValue(BadgeBackgroundColorProperty, value);
    }

    /// <summary>
    /// Border color of the avatar's badge.
    /// </summary>
    public Color BadgeBorderColor
    {
        get => (Color)GetValue(BadgeBorderColorProperty);
        set => SetValue(BadgeBorderColorProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="RcAvatar"/> class.
    /// </summary>
    public RcAvatar()
    {
        InitializeComponent();
    }
    #endregion

    #region Private static methods (bindable properties changed)
    /// <summary>
    /// Handles changes to the avatar's margin. Adjusts the avatar's image size based on
    /// the new margin value, reducing the image size by the sum of the left and right margins.
    /// </summary>
    /// <param name="bindable">The bindable object whose avatar margin has changed.</param>
    /// <param name="oldValue">The previous avatar margin value.</param>
    /// <param name="newValue">The updated avatar margin value.</param>
    private static void OnAvatarMarginChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatar avatar)
        {
            if (newValue is Thickness margin)
            {
                if (avatar.Size > -1)
                {
                    avatar.SetImageSize(avatar.Size - margin.Left - margin.Right);
                }
            }
        }
    }

    /// <summary>
    /// Handles changes to the avatar's image source. Sets the avatar's image to the new value. 
    /// If both the new image source and the placeholder image source are null, initials are displayed. 
    /// </summary>
    /// <param name="bindable">The bindable object whose image source has changed.</param>
    /// <param name="oldValue">The previous image source value.</param>
    /// <param name="newValue">The updated image source value.</param>
    private static void OnImageSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatar avatar)
        {
            if (newValue is ImageSource imageSource)
            {
                avatar.AvatarImage.Source = imageSource;
                avatar.InitialsLabel.IsVisible = avatar.PlaceholderImageSource == null && imageSource == null;
            }
            else
            {
                avatar.InitialsLabel.IsVisible = avatar.PlaceholderImageSource == null;
            }
        }
    }

    /// <summary>
    /// Updates the avatar's image source to the new placeholder image source, if an original image source isn't already set.
    /// If the new placeholder image source is null, initials are displayed.
    /// </summary>
    /// <param name="bindable">The bindable object whose placeholder image source has changed.</param>
    /// <param name="oldValue">The previous placeholder image source value.</param>
    /// <param name="newValue">The updated placeholder image source value.</param>
    private static void OnPlaceholderImageSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatar avatar)
        {
            if (newValue is ImageSource imageSource)
            {
                avatar.AvatarImage.Source = avatar.ImageSource ?? imageSource;
                avatar.InitialsLabel.IsVisible = imageSource == null;
            }
            else
            {
                avatar.InitialsLabel.IsVisible = false;
            }
        }
    }

    /// <summary>
    /// Updates the avatar's image size and container radius when the size changes. 
    /// The container radius is set to half of the new size.
    /// </summary>
    /// <param name="bindable">The bindable object whose size has changed.</param>
    /// <param name="oldValue">The previous size value.</param>
    /// <param name="newValue">The updated size value.</param>
    private static void OnSizeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatar avatar && newValue is double size)
        {
            avatar.SetImageSize(size);
            avatar.SetCornerRadius(size);
            //avatar.SetInitialBadgePadding(size);
        }
    }

    /// <summary>
    /// Handles the change in badge padding. Adjusts the outer badge padding based on the new padding value.
    /// </summary>
    /// <param name="bindable">The bindable object whose badge padding has changed.</param>
    /// <param name="oldValue">The previous badge padding value.</param>
    /// <param name="newValue">The updated badge padding value.</param>
    private static void OnBadgePaddingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatar avatar && newValue is Thickness padding)
        {
            avatar.SetOuterBadgePadding();
        }
    }

    /// <summary>
    /// Handles changes to the BadgePosition bindable property. Updates the badge's position on the avatar.
    /// </summary>
    /// <param name="bindable">The bindable object whose badge padding has changed.</param>
    /// <param name="oldValue">The previous badge padding value.</param>
    /// <param name="newValue">The updated badge padding value.</param>
    private static async void OnBadgePositionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatar avatar && newValue is BadgePosition badgePosition)
        {
            await avatar.SetBadgePosition(badgePosition);
        }
    }

    /// <summary>
    /// Handles changes to the Shape bindable property.
    /// Updates the corner radius based on the new shape.
    /// </summary>
    /// <param name="bindable">The bindable object that has changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnShapeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatar avatar)
        {
            avatar.SetCornerRadius(avatar.Size);
        }
    }

    /// <summary>
    /// Handles changes to the CornerRadius bindable property.
    /// Updates the corner radius if the shape is supported.
    /// </summary>
    /// <param name="bindable">The bindable object that has changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnCornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatar avatar)
        {
            avatar.SetCornerRadius(avatar.Size);
        }
    }
    #endregion

    #region Private events handlers
    /// <summary>
    /// Event handler for the 'Loaded' event of the RcContentView.
    /// Signals that the view has completed loading by setting the result of the TaskCompletionSource.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void RcContentView_Loaded(object sender, EventArgs e)
    {
        _viewLoadedTcs.SetResult(true);
    }

    /// <summary>
    /// Adjusts the BadgeLabel's dimensions to ensure it maintains a square aspect ratio when its size changes.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void BadgeLabel_SizeChanged(object sender, EventArgs e)
    {
        if (!_hasBadgeBindingSet)
        {
            _hasBadgeBindingSet = true;

            if (BadgeLabel.Width > BadgeLabel.Height)
            {
                var binding = new Binding(nameof(BadgeLabel.Width), source: BadgeLabel);
                BadgeLabel.SetBinding(Label.HeightRequestProperty, binding);
            }
            else
            {
                var binding = new Binding(nameof(BadgeLabel.Height), source: BadgeLabel);
                BadgeLabel.SetBinding(Label.WidthRequestProperty, binding);
            }
        }
    }

    /// <summary>
    /// Adjusts the inner badge radius size when the frame's size changes.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void InnerBadgeFrame_SizeChanged(object sender, EventArgs e)
    {
        InnerBadgeFrame.CornerRadius = (float)InnerBadgeFrame.Width / 2f;
    }

    /// <summary>
    /// Adjusts the outer badge radius size and position when the frame's size changes.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event arguments.</param>
    private async void OuterBadgeFrame_SizeChanged(object sender, EventArgs e)
    {
        OuterBadgeFrame.CornerRadius = (float)OuterBadgeFrame.Width / 2f;

        SetOuterBadgePadding();
        await SetBadgePosition(BadgePosition);
    }

    /// <summary>
    /// Passes the BindingContext to child elements when it changes.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">Event arguments.</param>
    private void Self_BindingContextChanged(object sender, EventArgs e)
    {
        InitialsLabelStyle.BindingContext = BindingContext;
        BadgeLabelStyle.BindingContext = BindingContext;
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Sets the width and height of the AvatarImage based on the provided size.
    /// </summary>
    /// <param name="size">The desired size for the AvatarImage's width and height.</param>
    private void SetImageSize(double size)
    {
        AvatarImage.WidthRequest = AvatarImage.HeightRequest = size;
    }

    /// <summary>
    /// Sets the outer badge padding by halving the values of the provided inner badge padding.
    /// </summary>
    /// <param name="innerBadgePadding">The padding values for the inner badge.</param>
    private void SetOuterBadgePadding()
    {
        var padding = InnerBadgeFrame.Width / 8f;

        OuterBadgeFrame.Padding = new Thickness(padding);
    }

    /// <summary>
    /// Sets the badge's position within the parent container based on the specified <see cref="BadgePosition"/>.
    /// </summary>
    /// <param name="badgePosition">The desired position of the badge within its parent container.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private async Task SetBadgePosition(BadgePosition badgePosition)
    {
        (var x, var y) = CalculateBadgePosition(badgePosition);

        await OuterBadgeFrame.TranslateTo(x, y);
    }

    /// <summary>
    /// Determines the (x, y) coordinates for the badge based on its position.
    /// </summary>
    private (double, double) CalculateBadgePosition(BadgePosition badgePosition)
    {
        return IsCornerPosition(badgePosition)
            ? CalculateCornerPosition(badgePosition)
            : CalculateCenterPosition(badgePosition);
    }

    /// <summary>
    /// Checks if the badge position is one of the corners.
    /// </summary>
    private bool IsCornerPosition(BadgePosition badgePosition)
    {
        return badgePosition == BadgePosition.TopLeft ||
               badgePosition == BadgePosition.BottomLeft ||
               badgePosition == BadgePosition.TopRight ||
               badgePosition == BadgePosition.BottomRight;
    }

    /// <summary>
    /// Calculates the (x, y) coordinates for corner badge positions.
    /// </summary>
    private (double, double) CalculateCornerPosition(BadgePosition badgePosition)
    {
        var distance = CalculateDistance();
        var radiusToMove = Math.Sqrt(distance * distance / 2) + OuterbadgeRadius() / 2;
        return GetXYForCornerPosition(radiusToMove, badgePosition);
    }

    /// <summary>
    /// Calculates the (x, y) coordinates for center badge positions.
    /// </summary>
    private (double, double) CalculateCenterPosition(BadgePosition badgePosition)
    {
        var radiusToMove = CalculateDistance() + OuterbadgeRadius() / 1.5;
        return GetXYForCenterPosition(radiusToMove, badgePosition);
    }

    /// <summary>
    /// Calculates the distance between the big circle and small circle centers.
    /// </summary>
    private double CalculateDistance()
    {
        return AvatarRadius() - OuterbadgeRadius();
    }

    /// <summary>
    /// Gets the avatar radius.
    /// </summary>
    private double AvatarRadius()
    {
        return Size / 2;
    }

    /// <summary>
    /// Gets the outer badge radius.
    /// </summary>
    private double OuterbadgeRadius()
    {
        return OuterBadgeFrame.Width / 2;
    }

    /// <summary>
    /// Gets the (x, y) coordinates for the badge for corner positions.
    /// </summary>
    private (double, double) GetXYForCornerPosition(double radiusToMove, BadgePosition badgePosition)
    {
        double x = badgePosition == BadgePosition.TopLeft || badgePosition == BadgePosition.BottomLeft ? -radiusToMove : radiusToMove;
        double y = badgePosition == BadgePosition.TopLeft || badgePosition == BadgePosition.TopRight ? -radiusToMove : radiusToMove;
        return (x, y);
    }

    /// <summary>
    /// Gets the (x, y) coordinates for the badge for center positions.
    /// </summary>
    private (double, double) GetXYForCenterPosition(double radiusToMove, BadgePosition badgePosition)
    {
        double x = 0, y = 0;
        if (badgePosition == BadgePosition.TopCenter || badgePosition == BadgePosition.BottomCenter)
        {
            y = badgePosition == BadgePosition.TopCenter ? -radiusToMove : radiusToMove;
        }
        else if (badgePosition == BadgePosition.CenterLeft || badgePosition == BadgePosition.CenterRight)
        {
            x = badgePosition == BadgePosition.CenterLeft ? -radiusToMove : radiusToMove;
        }
        return (x, y);
    }

    /// <summary>
    /// Sets the corner radius of the avatar's container based on its shape and size.
    /// </summary>
    /// <param name="size">The size of the avatar.</param>
    private void SetCornerRadius(double size)
    {
        if (size < 1)
        {
            return;
        }

        if (Shape == AvatarShape.Circle)
        {
            ContainerFrame.CornerRadius = (float)size / 2.0f;
        }
        else if (Shape == AvatarShape.RoundedSquare)
        {
            ContainerFrame.CornerRadius = CornerRadius > 0 ? CornerRadius : (float)size / 8.0f;
        }
        else if (Shape == AvatarShape.Square)
        {
            ContainerFrame.CornerRadius = 0f;
        }
    }
    #endregion
}

/// <summary>
/// Specifies the possible shapes for avatars.
/// </summary>
public enum AvatarShape
{
    Circle,
    RoundedSquare,
    Square,
}

/// <summary>
/// Specifies the possible positions for badges on an element.
/// </summary>
public enum BadgePosition
{
    TopLeft,
    TopCenter,
    TopRight,
    CenterLeft,
    CenterRight,
    BottomLeft,
    BottomCenter,
    BottomRight,
}