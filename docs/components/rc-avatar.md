# RcAvatar

The RcAvatar is a customizable avatar component that provides a visually appealing way to display user avatars.

## Properties
| Property Name             | Description                                                                         | Type                |
|---------------------------|-------------------------------------------------------------------------------------|---------------------|
| AvatarPadding           | Padding around the avatar.                                                           | Thickness           |
| ContainerBackgroundColor| Background color of the avatar's container.                                          | Color               |
| `Shape`                   | Shape of the avatar.                  | [AvatarShape](#avatarshape)         |
| CornerRadius            | Corner radius of the avatar (ignored for Circle or Square shapes).                   | float               |
| Size                    | Size of the avatar.                                                                 | double              |
| ImageSource             | Image source for the avatar.                                                        | ImageSource         |
| PlaceholderImageSource  | Placeholder image to be shown when the main image isn't available.                   | ImageSource         |
| Initials                | Initials to be displayed on the avatar.                                             | string              |
| InitialsLabelStyle      | Style for the initials label.                                                       | [RcLabelStyle](../styles/rc-label-style.md#rclabelstyle)        |
| BadgeLabelStyle         | Style for the badge label.                                                          | [RcLabelStyle](../styles/rc-label-style.md#rclabelstyle)      |
| HasBadge                | Indicates if the avatar has a badge.                                                | bool                |
| BadgePadding            | Padding for the badge.                                                              | Thickness           |
| BadgePosition           | Position of the badge on the avatar.| [BadgePosition](#badgeposition)       |
| BadgeText               | Text displayed on the avatar's badge.                                               | string              |
| BadgeBackgroundColor    | Background color of the avatar's badge.                                             | Color               |
| BadgeBorderColor        | Border color of the avatar's badge.                                                 | Color               |


### AvatarShape
Specifies the possible shapes for avatars:
- `AvatarShape.Circle`
- `AvatarShape.RoundedSquare`
- `AvatarShape.Square`

### BadgePosition
Specifies the possible positions for badges on an element:
- `BadgePosition.TopLeft`
- `BadgePosition.TopCenter`
- `BadgePosition.TopRight`
- `BadgePosition.CenterLeft`
- `BadgePosition.CenterRight`
- `BadgePosition.BottomLeft`
- `BadgePosition.BottomCenter`
- `BadgePosition.BottomRight`
