using Ion.Numeral;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace Ion.Effects;

[Group(ImageEffectGroup.Color)]
public class GammaEffect : BaseBlendEffect
{
    private int[] r, g, b;

    public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(nameof(Scale), typeof(double), typeof(GammaEffect), new FrameworkPropertyMetadata(1d, PixelShaderConstantCallback(1)));
    [Range(0.0, 1.0)]
    public double Scale
    {
        get => (double)GetValue(ScaleProperty);
        set => SetValue(ScaleProperty, value);
    }

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(GammaEffect), new FrameworkPropertyMetadata(1d, PixelShaderConstantCallback(0)));
    [Range(0.2, 5.0)]
    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set
        {
            SetValue(ValueProperty, value);
            Ramps(Convert.ToSingle(Value), out r, out g, out b);
        }
    }

    public GammaEffect() : base()
    {
        UpdateShaderValue(ScaleProperty);
        UpdateShaderValue(ValueProperty);
        Ramps(Convert.ToSingle(Value), out r, out g, out b);
    }

    public GammaEffect(double value) : this()
    {
        SetCurrentValue(ValueProperty, value);
    }

    private static void Ramps(float input, out int[] r, out int[] g, out int[] b)
    {
        r = new int[256];
        g = new int[256];
        b = new int[256];
        for (int x = 0; x < 256; ++x)
        {
            r[x] = Math.Clamp(Convert.ToInt32(((255.0 * Math.Pow(x / 255.0, 1.0 / input)) + 0.5).Round()), 0, 255);
            g[x] = Math.Clamp(Convert.ToInt32(((255.0 * Math.Pow(x / 255.0, 1.0 / input)) + 0.5).Round()), 0, 255);
            b[x] = Math.Clamp(Convert.ToInt32(((255.0 * Math.Pow(x / 255.0, 1.0 / input)) + 0.5).Round()), 0, 255);
        }
    }

    public override Color Apply(Color color, double amount = 1) => Color.FromArgb(color.A, Convert.ToByte(r[color.R]), Convert.ToByte(g[color.G]), Convert.ToByte(b[color.B]));
}