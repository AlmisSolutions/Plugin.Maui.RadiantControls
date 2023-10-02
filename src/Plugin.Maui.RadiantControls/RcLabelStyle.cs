using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Plugin.Maui.RadiantControls;

/// <summary>
/// Represents a style configuration for labels in Radiant Controls.
/// </summary>
public class RcLabelStyle : BindableObject
{
    #region Public Bindable Properties
    public static readonly BindableProperty TextTransformProperty =
        BindableProperty.Create(
            nameof(TextTransform),
            typeof(TextTransform),
            typeof(RcLabelStyle));

    public static readonly BindableProperty LineBreakModeProperty =
        BindableProperty.Create(
            nameof(LineBreakMode),
            typeof(LineBreakMode),
            typeof(RcLabelStyle));

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(RcLabelStyle));

    public static readonly BindableProperty CharacterSpacingProperty =
        BindableProperty.Create(
            nameof(CharacterSpacing),
            typeof(double),
            typeof(RcLabelStyle));

    public static readonly BindableProperty FontAttributesProperty =
        BindableProperty.Create(
            nameof(FontAttributes),
            typeof(FontAttributes),
            typeof(RcLabelStyle));

    public static readonly BindableProperty TextDecorationsProperty =
        BindableProperty.Create(
            nameof(TextDecorations),
            typeof(TextDecorations),
            typeof(RcLabelStyle));

    public static readonly BindableProperty FontFamilyProperty =
        BindableProperty.Create(
            nameof(FontFamily),
            typeof(string),
            typeof(RcLabelStyle));

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(
            nameof(FontSize),
            typeof(double),
            typeof(RcLabelStyle));

    public static readonly BindableProperty FontAutoScalingEnabledProperty =
        BindableProperty.Create(
            nameof(FontAutoScalingEnabled),
            typeof(bool),
            typeof(RcLabelStyle));

    public static readonly BindableProperty LineHeightProperty =
        BindableProperty.Create(
            nameof(LineHeight),
            typeof(double),
            typeof(RcLabelStyle));

    public static readonly BindableProperty MaxLinesProperty =
        BindableProperty.Create(
            nameof(MaxLines),
            typeof(int),
            typeof(RcLabelStyle));

    public static readonly BindableProperty PaddingProperty =
        BindableProperty.Create(
            nameof(Padding),
            typeof(Thickness),
            typeof(RcLabelStyle));

    public static readonly BindableProperty TextTypeProperty =
        BindableProperty.Create(
            nameof(TextType),
            typeof(TextType),
            typeof(RcLabelStyle));
    #endregion

    #region Public properties
    /// <summary>
    /// Text transform style for the label.
    /// </summary>
    public TextTransform TextTransform
    {
        get => (TextTransform)GetValue(TextTransformProperty);
        set => SetValue(TextTransformProperty, value);
    }

    /// <summary>
    /// Line break mode for the label's text.
    /// </summary>
    public LineBreakMode LineBreakMode
    {
        get => (LineBreakMode)GetValue(LineBreakModeProperty);
        set => SetValue(LineBreakModeProperty, value);
    }

    /// <summary>
    /// Text color for the label.
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Character spacing for the label's text.
    /// </summary>
    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    /// <summary>
    /// Font attributes for the label's text.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    /// <summary>
    /// Text decorations for the label's text.
    /// </summary>
    public TextDecorations TextDecorations
    {
        get => (TextDecorations)GetValue(TextDecorationsProperty);
        set => SetValue(TextDecorationsProperty, value);
    }

    /// <summary>
    /// Font family for the label's text.
    /// </summary>
    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    /// <summary>
    /// Font size for the label's text.
    /// </summary>
    [TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    /// <summary>
    /// Indicates whether font auto-scaling is enabled for the label.
    /// </summary>
    public bool FontAutoScalingEnabled
    {
        get => (bool)GetValue(FontAutoScalingEnabledProperty);
        set => SetValue(FontAutoScalingEnabledProperty, value);
    }

    /// <summary>
    /// Line height for the label's text.
    /// </summary>
    public double LineHeight
    {
        get => (double)GetValue(LineHeightProperty);
        set => SetValue(LineHeightProperty, value);
    }

    /// <summary>
    /// Maximum number of lines for the label.
    /// </summary>
    public int MaxLines
    {
        get => (int)GetValue(MaxLinesProperty);
        set => SetValue(MaxLinesProperty, value);
    }

    /// <summary>
    /// Padding for the label.
    /// </summary>
    public Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }

    /// <summary>
    /// Text type for the label.
    /// </summary>
    public TextType TextType
    {
        get => (TextType)GetValue(TextTypeProperty);
        set => SetValue(TextTypeProperty, value);
    }
    #endregion
}