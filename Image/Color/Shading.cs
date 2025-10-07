using Ion.Numeral;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace Ion.Effects;

[Group(ImageEffectGroup.Color)]
public class ShadingEffect : BaseBlendEffect
{
    public static readonly DependencyProperty RedProperty = DependencyProperty.Register("Red", typeof(double), typeof(ShadingEffect), new FrameworkPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(0)));
    [Range(0.0, 100.0)]
    public double Red
    {
        get => (double)GetValue(RedProperty);
        set => SetValue(RedProperty, value);
    }

    public static readonly DependencyProperty GreenProperty = DependencyProperty.Register("Green", typeof(double), typeof(ShadingEffect), new FrameworkPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(1)));
    [Range(0.0, 100.0)]
    public double Green
    {
        get => (double)GetValue(GreenProperty);
        set => SetValue(GreenProperty, value);
    }

    public static readonly DependencyProperty BlueProperty = DependencyProperty.Register("Blue", typeof(double), typeof(ShadingEffect), new FrameworkPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(2)));
    [Range(0.0, 100.0)]
    public double Blue
    {
        get => (double)GetValue(BlueProperty);
        set => SetValue(BlueProperty, value);
    }

    public ShadingEffect() : base()
    {
        UpdateShaderValue(RedProperty);
        UpdateShaderValue(GreenProperty);
        UpdateShaderValue(BlueProperty);
    }

    public ShadingEffect(double red, double green, double blue) : this()
    {
        SetCurrentValue(RedProperty, red);
        SetCurrentValue(GreenProperty, green);
        SetCurrentValue(BlueProperty, blue);
    }

    public override Color Apply(Color color, double amount = 1)
    {
        var r = Math.Clamp(Convert.ToInt32((Convert.ToDouble(color.R) * (Convert.ToDouble(Red) / 100.0)).Round()), 0, 255);
        var g = Math.Clamp(Convert.ToInt32((Convert.ToDouble(color.G) * (Convert.ToDouble(Green) / 100.0)).Round()), 0, 255);
        var b = Math.Clamp(Convert.ToInt32((Convert.ToDouble(color.B) * (Convert.ToDouble(Blue) / 100.0)).Round()), 0, 255);
        return Color.FromArgb(Convert.ToByte(color.A), Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));
    }
}