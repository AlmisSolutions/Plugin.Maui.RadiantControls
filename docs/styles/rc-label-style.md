# RcLabelStyle

Represents a style configuration for labels in Radiant Controls.

## Properties
| Property Name             | Description                                       | Type               |
|---------------------------|---------------------------------------------------|--------------------|
| TextTransform             | Text transform style for the label.               | TextTransform       |
| LineBreakMode             | Line break mode for the label's text.             | LineBreakMode       |
| TextColor                 | Text color for the label.                         | Color              |
| CharacterSpacing          | Character spacing for the label's text.           | double             |
| FontAttributes            | Font attributes for the label's text.             | FontAttributes     |
| TextDecorations           | Text decorations for the label's text.            | TextDecorations    |
| FontFamily                | Font family for the label's text.                 | string             |
| FontSize                  | Font size for the label's text. (TypeConverter: FontSizeConverter) | double |
| FontAutoScalingEnabled    | Indicates whether font auto-scaling is enabled for the label. | bool        |
| LineHeight                | Line height for the label's text.                 | double             |
| MaxLines                  | Maximum number of lines for the label.           | int                |
| Padding                   | Padding for the label.                            | Thickness          |
| TextType                  | Text type for the label.                          | TextType           |


## Example

### C#
```csharp
var labelStyle = new RcLabelStyle
{
    TextTransform = TextTransform.Uppercase,
    LineBreakMode = LineBreakMode.WordWrap,
    TextColor = Color.Black,
    CharacterSpacing = 1.2,
    FontAttributes = FontAttributes.Bold,
    TextDecorations = TextDecorations.Underline,
    FontFamily = "Arial",
    FontSize = 16.0,
    FontAutoScalingEnabled = true,
    LineHeight = 1.5,
    MaxLines = 2,
    Padding = new Thickness(10, 5, 10, 5),
    TextType = TextType.Normal
};
```

### XAML
```xml
<rc:RcLabelStyle 
    TextTransform="Uppercase"
    LineBreakMode="WordWrap"
    TextColor="Black"
    CharacterSpacing="1.2"
    FontAttributes="Bold"
    TextDecorations="Underline"
    FontFamily="Arial"
    FontSize="16.0"
    FontAutoScalingEnabled="True"
    LineHeight="1.5"
    MaxLines="2"
    Padding="10,5,10,5"
    TextType="Normal" />
```