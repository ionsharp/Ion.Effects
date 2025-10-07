using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace Ion.Effects;

[Group(ImageEffectGroup.Blur)]
public class DirectionalBlurEffect : ImageEffect
{
    /// <summary>The scale of the blur (as a fraction of the input size).</summary>
    [Range(0.0, 0.01)]
    public override double Amount { get => base.Amount; set => base.Amount = value; }

    public static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(double), typeof(DirectionalBlurEffect), new FrameworkPropertyMetadata(.0, PixelShaderConstantCallback(1)));
    /// <summary>The direction of the blur (in degrees).</summary>
    [Range(0.0, 359.0)]
    public double Angle
    {
        get => (double)GetValue(AngleProperty);
        set => SetValue(AngleProperty, value);
    }

    public DirectionalBlurEffect() : base()
    {
        UpdateShaderValue(AngleProperty);
    }
}