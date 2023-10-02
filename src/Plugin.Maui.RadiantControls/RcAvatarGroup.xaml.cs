using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Plugin.Maui.RadiantControls;

/// <summary>
/// Represents a group of avatars displayed in a stack.
/// </summary>
public partial class RcAvatarGroup : ContentView
{
    #region Public static readonly bindable properties
    /// <summary>
    /// Bindable property for the avatars collection.
    /// </summary>
    public static readonly BindableProperty AvatarsProperty = BindableProperty.Create(
        propertyName: nameof(Avatars),
        returnType: typeof(IList<RcAvatar>),
        declaringType: typeof(RcAvatarGroup),
        defaultValueCreator: (bindable) => new ObservableCollection<RcAvatar>(),
        propertyChanged: OnAvatarsChanged);

    /// <summary>
    /// Bindable property for the stack direction.
    /// </summary>
    public static readonly BindableProperty StackDirectionProperty = BindableProperty.Create(
        propertyName: nameof(StackDirection),
        returnType: typeof(StackDirection),
        declaringType: typeof(RcAvatarGroup),
        defaultValue: StackDirection.BottomToTop,
        propertyChanged: OnStackDirectionChangedAsync);
    #endregion

    #region Public bindable properties
    /// <summary>
    /// Avatars collection.
    /// </summary>
    public ObservableCollection<RcAvatar> Avatars
    {
        get { return (ObservableCollection<RcAvatar>)GetValue(AvatarsProperty); }
        set { SetValue(AvatarsProperty, value); }
    }

    /// <summary>
    /// Stack direction of the avatar group.
    /// </summary>
    public StackDirection StackDirection
    {
        get { return (StackDirection)GetValue(StackDirectionProperty); }
        set { SetValue(StackDirectionProperty, value); }
    }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the RcAvatarGroup class.
    /// </summary>
    public RcAvatarGroup()
    {
        InitializeComponent();

        Avatars.CollectionChanged += Avatars_CollectionChanged;
    }
    #endregion

    #region Private static methods (bindable properties changed)
    /// <summary>
    /// Updates the avatars when the Avatars property changes.
    /// </summary>
    private static void OnAvatarsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatarGroup avatarGroup && newValue is ObservableCollection<RcAvatar> avatars)
        {
            avatarGroup.UpdateAvatars(avatars);
        }
    }

    /// <summary>
    /// Updates the avatars when the StackDirection property changes.
    /// </summary>
    private static void OnStackDirectionChangedAsync(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RcAvatarGroup avatarGroup)
        {
            avatarGroup.UpdateAvatars(avatarGroup.Avatars);
        }
    }
    #endregion

    #region Private event handlers
    /// <summary>
    /// Handles changes in the Avatars collection.
    /// </summary>
    private void Avatars_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        HandleNewItems(e.NewItems);
        HandleRemovedItems(e.OldItems);
        HandleCollectionReset(e.Action);
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Processes newly added avatars.
    /// </summary>
    /// <param name="newItems">The list of new avatars to process.</param>
    private void HandleNewItems(IList newItems)
    {
        if (newItems == null)
        {
            return;
        }

        PopulateAvatars(newItems);
    }

    /// <summary>
    /// Processes removed avatars.
    /// </summary>
    /// <param name="oldItems">The list of avatars that were removed.</param>
    private void HandleRemovedItems(IList oldItems)
    {
        if (oldItems == null)
        {
            return;
        }

        foreach (RcAvatar oldAvatar in oldItems)
        {
            RemoveAvatar(oldAvatar);
        }
    }

    /// <summary>
    /// Handles the reset action for the Avatars collection.
    /// </summary>
    /// <param name="action">The action that triggered the collection change.</param>
    private void HandleCollectionReset(NotifyCollectionChangedAction action)
    {
        if (action != NotifyCollectionChangedAction.Reset)
        {
            return;
        }

        AvatarGroup.Children.Clear();

        foreach (var avatar in Avatars)
        {
            AddAvatar(avatar);
        }
    }

    /// <summary>
    /// Adds an avatar to the group.
    /// </summary>
    /// <param name="avatar">The avatar to be added to the group.</param>
    private void AddAvatar(RcAvatar avatar)
    {
        SetAvatarSpacing(avatar);

        AvatarGroup.Children.Add(avatar);
    }

    /// <summary>
    /// Removes an avatar from the group.
    /// </summary>
    /// <param name="avatar">The avatar to be removed from the group.</param>
    private void RemoveAvatar(RcAvatar avatar)
    {
        AvatarGroup.Children.Remove(avatar);
    }

    /// <summary>
    /// Sets the spacing for an avatar.
    /// </summary>
    /// <param name="avatar">The avatar for which the spacing is to be set.</param>
    private void SetAvatarSpacing(RcAvatar avatar)
    {
        AvatarGroup.Spacing = -(avatar.Size / 2);
    }

    /// <summary>
    /// Updates the avatars in the group based on the provided collection.
    /// </summary>
    /// <param name="avatars">The collection of avatars to update the group with.</param>
    private void UpdateAvatars(ObservableCollection<RcAvatar> avatars)
    {
        ClearAvatarGroup();
        PopulateAvatars(avatars);
    }

    /// <summary>
    /// Clears all avatars from the group.
    /// </summary>
    private void ClearAvatarGroup()
    {
        AvatarGroup.Children.Clear();
    }

    /// <summary>
    /// Populates the group with the provided list of avatars.
    /// </summary>
    /// <param name="avatars">The list of avatars to be added to the group.</param>
    private void PopulateAvatars(IList avatars)
    {
        if (avatars == null)
        {
            return;
        }

        foreach (var avatar in avatars)
        {
            AddAvatar(avatar as RcAvatar);
        }
    }

    /// <summary>
    /// Rotates the avatars and the group for stack direction from top to bottom.
    /// </summary>
    /// <param name="avatars">The list of avatars to be rotated.</param>
    private void RotateAvatarsAndGroupFromTopToBottom(IList avatars)
    {
        foreach (RcAvatar avatar in avatars)
        {
            avatar.RotateYTo(180);
        }

        AvatarGroup.RotateYTo(180);
    }

    private void AvatarGroup_Loaded(object sender, EventArgs e)
    {
        if (StackDirection == StackDirection.BottomToTop)
        {
        }
        else if (StackDirection == StackDirection.TopToBottom)
        {
            RotateAvatarsAndGroupFromTopToBottom(Avatars);
        }
    }
    #endregion
}

/// <summary>
/// Represents the direction in which avatars are stacked.
/// </summary>
public enum StackDirection
{
    BottomToTop,
    TopToBottom
}