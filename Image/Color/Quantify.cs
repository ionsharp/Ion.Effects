using Ion.Numeral;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace Ion.Effects;

[Group(ImageEffectGroup.Color)]
public class QuantifyEffect : BaseBlendEffect
{
    private enum Categories { Size }

    public static readonly DependencyProperty SizeXProperty = DependencyProperty.Register(nameof(SizeX), typeof(double), typeof(QuantifyEffect), new FrameworkPropertyMetadata(10d, PixelShaderConstantCallback(0)));
    [Range(0.0, 1000.0)]
    public double SizeX
    {
        get => (double)GetValue(SizeXProperty);
        set => SetValue(SizeXProperty, value);
    }

    public static readonly DependencyProperty SizeYProperty = DependencyProperty.Register(nameof(SizeY), typeof(double), typeof(QuantifyEffect), new FrameworkPropertyMetadata(10d, PixelShaderConstantCallback(1)));
    [Range(0.0, 1000.0)]
    public double SizeY
    {
        get => (double)GetValue(SizeYProperty);
        set => SetValue(SizeYProperty, value);
    }

    public static readonly DependencyProperty SizeZProperty = DependencyProperty.Register(nameof(SizeZ), typeof(double), typeof(QuantifyEffect), new FrameworkPropertyMetadata(10d, PixelShaderConstantCallback(2)));
    [Range(0.0, 1000.0)]
    public double SizeZ
    {
        get => (double)GetValue(SizeZProperty);
        set => SetValue(SizeZProperty, value);
    }

    public QuantifyEffect() : base()
    {
        UpdateShaderValue(SizeXProperty);
        UpdateShaderValue(SizeYProperty);
        UpdateShaderValue(SizeZProperty);
    }

    public override Color Apply(Color color, double amount = 1)
    {
        double r = color.R.Normalize(), g = color.G.Normalize(), b = color.B.Normalize(); byte a = color.A;
        r *= SizeX; r = r.Round(); r /= SizeX;
        g *= SizeY; g = g.Round(); g /= SizeY;
        b *= SizeZ; b = b.Round(); b /= SizeZ;
        return Color.FromArgb(a, r.Denormalize<byte>(), g.Denormalize<byte>(), b.Denormalize<byte>());
    }
}