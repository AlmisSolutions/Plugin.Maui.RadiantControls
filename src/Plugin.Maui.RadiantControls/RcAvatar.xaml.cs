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
        defaultValue: default(float));

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
        },
        propertyChanged: OnInitialsLabelStyleChanged);

    /// <summary>
    /// Bindable property for indicating whether the RcAvatar has a badge.
    /// </summary>
    public static readonly BindableProperty HasBadgeProperty = BindableProperty.Create(
        propertyName: nameof(HasBadge),
        returnType: typeof(bool),
        declaringType: typeof(RcAvatar));

    /// <summary>
    /// Bindable property for the style of the badge label.
    /// </summary>
    public static readonly BindableProperty BadgeLabelStyleProperty = BindableProperty.Create(
        propertyName: nameof(BadgeLabelStyle),
        returnType: typeof(RcLabelStyle),
        declaringType: typeof(RcAvatar),
        defaultValueCreator: (bindable) => new RcLabelStyle(),
        propertyChanged: OnBadgeLabelStyleChanged);

    /// <summary>
    /// Bindable property for the padding of the inner badge.
    /// </summary>
    public static readonly BindableProperty BadgePaddingProperty = BindableProperty.Create(
        propertyName: nameof(BadgePadding),
        returnType: typeof(Thickness),
        declaringType: typeof(RcAvatar),
        defaultValueCreator: (bindable) => default(Thickness),
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
    /// Margin around the avatar.
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
    /// Shape of the avatar, e.g., circle or square.
    /// </summary>
    public AvatarShape Shape
    {
        get => (AvatarShape)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    /// <summary>
    /// Size of the avatar.
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
    /// Gets or sets a value indicating whether the RcAvatar has a badge.
    /// </summary>
    public bool HasBadge
    {
        get => (bool)GetValue(HasBadgeProperty);
        set => SetValue(HasBadgeProperty, value);
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
    /// Gets or sets the text displayed on the avatar's badge.
    /// </summary>
    public string BadgeText
    {
        get => (string)GetValue(BadgeTextProperty);
        set => SetValue(BadgeTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the background color of the avatar's badge.
    /// </summary>
    public Color BadgeBackgroundColor
    {
        get => (Color)GetValue(BadgeBackgroundColorProperty);
        set => SetValue(BadgeBackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the border color of the avatar's badge.
    /// </summary>
    public Color BadgeBorderColor
    {
        get => (Color)GetValue(BadgeBorderColorProperty);
        set => SetValue(BadgeBorderColorProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the RcAvatar class.
    /// </summary>
    public RcAvatar()
    {
        InitializeComponent();

        BindingContext = this;

        SetOuterBadgePadding();
        SetBadgePosition(BadgePosition);
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
            avatar.SetInitialBadgePadding(size);
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
    private static void OnBadgePositionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatar avatar && newValue is BadgePosition badgePosition)
        {
            avatar.SetBadgePosition(badgePosition);
        }
    }

    /// <summary>
    /// Adjusts the font size of the initials label when its style changes. If the label's font size isn't set (i.e., it's 0 or negative), it defaults to a quarter of the avatar's size.
    /// </summary>
    /// <param name="bindable">The bindable object that has changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnInitialsLabelStyleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatar avatar && newValue is RcLabelStyle labelStyle)
        {
            avatar.SetInitialsSize(labelStyle);
        }
    }

    /// <summary>
    /// Adjusts the font size of the badge label when its style changes. If the label's font size isn't set (i.e., it's 0 or negative), it defaults to a quarter of the avatar's size.
    /// </summary>
    /// <param name="bindable">The bindable object that has changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    private static void OnBadgeLabelStyleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatar avatar && newValue is RcLabelStyle labelStyle)
        {
            labelStyle.FontSize = labelStyle.FontSize > 0 ? labelStyle.FontSize : avatar.Size / 6;
        }
    }

    /// <summary>
    /// Handles changes to the Shape bindable property. Updates the corner radius based on the new shape.
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
    /// Adjusts the outer badge radius size when the frame's size changes.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event arguments.</param>
    private async void OuterBadgeFrame_SizeChanged(object sender, EventArgs e)
    {
        OuterBadgeFrame.CornerRadius = (float)OuterBadgeFrame.Width / 2;
        var divider = 6;

        if (BadgePosition == BadgePosition.TopLeft)
        {
            await OuterBadgeFrame.TranslateTo(-OuterBadgeFrame.Width / divider, -OuterBadgeFrame.Height / divider);
        }
        else if (BadgePosition == BadgePosition.TopCenter)
        {
            await OuterBadgeFrame.TranslateTo(0, -OuterBadgeFrame.Height / divider);
        }
        else if (BadgePosition == BadgePosition.TopRight)
        {
            await OuterBadgeFrame.TranslateTo(OuterBadgeFrame.Width / divider, -OuterBadgeFrame.Height / divider);
        }
        else if (BadgePosition == BadgePosition.CenterLeft)
        {
            await OuterBadgeFrame.TranslateTo(-OuterBadgeFrame.Width / divider, 0);
        }
        else if (BadgePosition == BadgePosition.CenterRight)
        {
            await OuterBadgeFrame.TranslateTo(OuterBadgeFrame.Width / divider, 0);
        }
        else if (BadgePosition == BadgePosition.BottomLeft)
        {
            await OuterBadgeFrame.TranslateTo(-OuterBadgeFrame.Width / divider, OuterBadgeFrame.Height / divider);
        }
        else if (BadgePosition == BadgePosition.BottomCenter)
        {
            await OuterBadgeFrame.TranslateTo(0, -OuterBadgeFrame.Height / divider);
        }
        else if (BadgePosition == BadgePosition.BottomRight)
        {
            await OuterBadgeFrame.TranslateTo(OuterBadgeFrame.Width / divider, OuterBadgeFrame.Height / divider);
        }
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
        var padding = GetBadgePaddingBasedOnSize() / 3f;

        OuterBadgeFrame.Padding = new Thickness(padding);
    }

    /// <summary>
    /// Sets the badge's position within the parent container based on the specified badge position.
    /// </summary>
    /// <param name="badgePosition">The desired position of the badge within its parent container.</param>
    private async void SetBadgePosition(BadgePosition badgePosition)
    {
        await _viewLoadedTcs.Task;

        if (badgePosition == BadgePosition.TopLeft)
        {
            OuterBadgeFrame.VerticalOptions = LayoutOptions.Start;
            OuterBadgeFrame.HorizontalOptions = LayoutOptions.Start;
            //await OuterBadgeFrame.TranslateTo(-BadgePadding.Left, -BadgePadding.Top);
        }
        else if (badgePosition == BadgePosition.TopCenter)
        {
            OuterBadgeFrame.VerticalOptions = LayoutOptions.Start;
            OuterBadgeFrame.HorizontalOptions = LayoutOptions.Center;
            //await OuterBadgeFrame.TranslateTo(0, -BadgePadding.Top);
        }
        else if (badgePosition == BadgePosition.TopRight)
        {
            OuterBadgeFrame.VerticalOptions = LayoutOptions.Start;
            OuterBadgeFrame.HorizontalOptions = LayoutOptions.End;
            //await OuterBadgeFrame.TranslateTo(BadgePadding.Right, -BadgePadding.Top);
        }
        else if (badgePosition == BadgePosition.CenterLeft)
        {
            OuterBadgeFrame.VerticalOptions = LayoutOptions.Center;
            OuterBadgeFrame.HorizontalOptions = LayoutOptions.Start;
            //await OuterBadgeFrame.TranslateTo(-BadgePadding.Left, 0);
        }
        else if (badgePosition == BadgePosition.CenterRight)
        {
            OuterBadgeFrame.VerticalOptions = LayoutOptions.Center;
            OuterBadgeFrame.HorizontalOptions = LayoutOptions.End;
            //await OuterBadgeFrame.TranslateTo(BadgePadding.Right, 0);
        }
        else if (badgePosition == BadgePosition.BottomLeft)
        {
            OuterBadgeFrame.VerticalOptions = LayoutOptions.End;
            OuterBadgeFrame.HorizontalOptions = LayoutOptions.Start;
            //await OuterBadgeFrame.TranslateTo(-BadgePadding.Left, BadgePadding.Bottom);
        }
        else if (badgePosition == BadgePosition.BottomCenter)
        {
            OuterBadgeFrame.VerticalOptions = LayoutOptions.End;
            OuterBadgeFrame.HorizontalOptions = LayoutOptions.Center;
            //await OuterBadgeFrame.TranslateTo(0, -BadgePadding.Bottom);
        }
        else if (badgePosition == BadgePosition.BottomRight)
        {
            OuterBadgeFrame.VerticalOptions = LayoutOptions.End;
            OuterBadgeFrame.HorizontalOptions = LayoutOptions.End;
            //await OuterBadgeFrame.TranslateTo(BadgePadding.Right, BadgePadding.Bottom);
        }
    }

    /// <summary>
    /// Sets the corner radius of the avatar's container based on its shape and size.
    /// </summary>
    /// <param name="size">The size of the avatar.</param>
    private void SetCornerRadius(double size)
    {
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

    private void SetInitialBadgePadding(double size)
    {
        if (BadgePadding.IsEmpty)
        {
            if (string.IsNullOrWhiteSpace(BadgeText))
            {
                BadgePadding = GetBadgePaddingBasedOnSize();
            }
            else
            {
                BadgePadding = (float)size / 12f;
            }
        }
    }

    /// <summary>
    /// Sets the font size for the initials label based on the provided size.
    /// If the font size is already set (greater than 0), it retains the current value.
    /// Otherwise, it sets the font size to a quarter of the provided size.
    /// </summary>
    private void SetInitialsSize(RcLabelStyle labelStyle)
    {
        labelStyle.FontSize = labelStyle.FontSize > 0
            ? labelStyle.FontSize : Size / 4;
    }

    /// <summary>
    /// Calculates the padding for the badge based on the size.
    /// </summary>
    /// <returns>The calculated padding for the badge.</returns>
    private float GetBadgePaddingBasedOnSize()
    {
        return (float)Size / 8f;
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