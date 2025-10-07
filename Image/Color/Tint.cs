using Ion.Numeral;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace Ion.Effects;

[Group(ImageEffectGroup.Color)]
public class TintEffect : BaseBlendEffect
{
    public static readonly DependencyProperty RedProperty = DependencyProperty.Register("Red", typeof(double), typeof(TintEffect), new FrameworkPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(0)));
    [Range(0.0, 100.0)]
    public double Red
    {
        get => (double)GetValue(RedProperty);
        set => SetValue(RedProperty, value);
    }

    public static readonly DependencyProperty GreenProperty = DependencyProperty.Register("Green", typeof(double), typeof(TintEffect), new FrameworkPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(1)));
    [Range(0.0, 100.0)]
    public double Green
    {
        get => (double)GetValue(GreenProperty);
        set => SetValue(GreenProperty, value);
    }

    public static readonly DependencyProperty BlueProperty = DependencyProperty.Register("Blue", typeof(double), typeof(TintEffect), new FrameworkPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(2)));
    [Range(0.0, 100.0)]
    public double Blue
    {
        get => (double)GetValue(BlueProperty);
        set => SetValue(BlueProperty, value);
    }

    public TintEffect() : base()
    {
        UpdateShaderValue(RedProperty);
        UpdateShaderValue(GreenProperty);
        UpdateShaderValue(BlueProperty);
    }

    public TintEffect(double red, double green, double blue) : this()
    {
        SetCurrentValue(RedProperty, red);
        SetCurrentValue(GreenProperty, green);
        SetCurrentValue(BlueProperty, blue);
    }

    public override Color Apply(Color color, double amount = 1)
    {
        int b = color.B, g = color.G, r = color.R, a = color.A;
        return Color.FromArgb(Convert.ToByte(a),
            Convert.ToByte(Math.Clamp(Convert.ToInt32(r + (255 - r) * (Red / 100.0).Round()), 0, 255)),
            Convert.ToByte(Math.Clamp(Convert.ToInt32(g + (255 - g) * (Green / 100.0).Round()), 0, 255)),
            Convert.ToByte(Math.Clamp(Convert.ToInt32(b + (255 - b) * (Blue / 100.0).Round()), 0, 255)));
    }
}