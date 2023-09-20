using System.ComponentModel;

namespace Plugin.Maui.RadiantControls;

public class RcLabelStyle : BindableObject
{
    private TextTransform _textTransform;
    private LineBreakMode _lineBreakMode;
    private Color _textColor;
    private double _characterSpacing;
    private FontAttributes _fontAttributes;
    private TextDecorations _textDecorations;
    private string _fontFamily;
    private double _fontSize;
    private bool _fontAutoScalingEnabled;
    private double _lineHeight;
    private int _maxLines;
    private Thickness _padding;
    private TextType _textType;

    public TextTransform TextTransform
    {
        get => _textTransform;
        set
        {
            _textTransform = value;
            OnPropertyChanged();
        }
    }

    public LineBreakMode LineBreakMode
    {
        get => _lineBreakMode;
        set
        {
            _lineBreakMode = value;
            OnPropertyChanged();
        }
    }

    public Color TextColor
    {
        get => _textColor;
        set
        {
            _textColor = value;
            OnPropertyChanged();
        }
    }

    public double CharacterSpacing
    {
        get => _characterSpacing;
        set
        {
            _characterSpacing = value;
            OnPropertyChanged();
        }
    }

    public FontAttributes FontAttributes
    {
        get => _fontAttributes;
        set
        {
            _fontAttributes = value;
            OnPropertyChanged();
        }
    }

    public TextDecorations TextDecorations
    {
        get => _textDecorations;
        set
        {
            _textDecorations = value;
            OnPropertyChanged();
        }
    }

    public string FontFamily
    {
        get => _fontFamily;
        set
        {
            _fontFamily = value;
            OnPropertyChanged();
        }
    }

    [TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get => _fontSize;
        set
        {
            _fontSize = value;
            OnPropertyChanged();
        }
    }

    public bool FontAutoScalingEnabled
    {
        get => _fontAutoScalingEnabled;
        set
        {
            _fontAutoScalingEnabled = value;
            OnPropertyChanged();
        }
    }

    public double LineHeight
    {
        get => _lineHeight;
        set
        {
            _lineHeight = value;
            OnPropertyChanged();
        }
    }

    public int MaxLines
    {
        get => _maxLines;
        set
        {
            _maxLines = value;
            OnPropertyChanged();
        }
    }

    public Thickness Padding
    {
        get => _padding;
        set
        {
            _padding = value;
            OnPropertyChanged();
        }
    }

    public TextType TextType
    {
        get => _textType;
        set
        {
            _textType = value;
            OnPropertyChanged();
        }
    }
}