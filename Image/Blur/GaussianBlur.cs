using System.Windows;

namespace Ion.Effects;

[Group(ImageEffectGroup.Blur)]
public class GaussianBlurEffect : ImageEffect
{
    public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(nameof(Angle), typeof(double), typeof(GaussianBlurEffect), new FrameworkPropertyMetadata(0d, PixelShaderConstantCallback(1)));
    public double Angle
    {
        get => (double)GetValue(AngleProperty);
        set => SetValue(AngleProperty, value);
    }

    public GaussianBlurEffect() : base()
    {
        UpdateShaderValue(AngleProperty);
        UpdateShaderValue(AmountProperty);
    }
}