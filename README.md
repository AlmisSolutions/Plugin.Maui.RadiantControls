# Plugin.Maui.RadiantControls

This project aims to bring components from popular web and mobile frameworks to the MAUI framework.

## Table of Contents

- [Getting Started](#getting-started)
- [Components](#components)
- [License](#license)

## Getting Started

* Available on NuGet: <http://www.nuget.org/packages/Plugin.Maui.RadiantControls> [![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.RadiantControls.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Maui.RadiantControls/)

## Components
- [RcAvatar](#rcavatar)
- [RcAvatarGroup](#rcavatargroup)

### RcAvatar

RcAvatar is a versatile control that simplifies the creation of avatars in your Xamarin.Maui applications. Whether you need to display user profile pictures, placeholders, or badges, RcAvatar provides an easy-to-use and highly customizable solution.

- Display avatars with initials and customize the appearance.
- Support for displaying images as avatars.
- Badge functionality to indicate status or notifications.
- Configurable avatar shape (circle, rounded square, square).
- Easily set the avatar size and customize padding.
- Badge positioning options for flexible placement.

#### Usage

Here's a basic example of how to use RcAvatar in XAML. For more examples check the sample.

```xml
<ContentView 
  xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
  xmlns:rc="clr-namespace:Plugin.Maui.RadiantControls;assembly=Plugin.Maui.RadiantControls">
    <rc:RcAvatar
        ImageSource="avatar.png"
        Initials="AB"
        Size="64"
        BadgeText="3"
        BadgePosition="TopRight"
        ContainerBackgroundColor="LightGray"
        BadgeBackgroundColor="Red" />
</ContentView>
```

### RcAvatarGroup

RcAvatarGroup simplifies the task of displaying a collection of avatars in your Xamarin.Maui application. You can stack avatars either from the bottom to the top or from the top to the bottom using the `StackDirection` property.

- Display a group of avatars.
- Choose between stacking avatars from the bottom to the top or from the top to the bottom.

Here's a basic example of how to use RcAvatarGroup in XAML. For more examples check the sample.

```xml
<ContentView 
  xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
  xmlns:rc="clr-namespace:Plugin.Maui.RadiantControls;assembly=Plugin.Maui.RadiantControls">
  <rc:RcAvatarGroup>
      <rc:RcAvatarGroup.Avatars>
          <rc:RcAvatar
              ImageSource="avatar.jpeg"
              Size="32"
              VerticalOptions="End"/>
          <rc:RcAvatar
              ImageSource="avatar.jpeg"
              Size="32"
              VerticalOptions="End"/>
          <rc:RcAvatar
              ImageSource="avatar.jpeg"
              Size="32"
              VerticalOptions="End"/>
      </rc:RcAvatarGroup.Avatars>
  </rc:RcAvatarGroup>
</ContentView>
```

## License

Plugin.Maui.RadiantControls is licensed under the MIT License. See the [LICENSE](LICENSE.md) file for more details.