using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace Ion.Effects;

[Group(ImageEffectGroup.Blur)]
public class ZoomBlurEffect : ImageEffect
{
    public static readonly DependencyProperty CenterProperty = DependencyProperty.Register("Center", typeof(Point), typeof(ZoomBlurEffect), new FrameworkPropertyMetadata(new Point(0.9D, 0.6D), PixelShaderConstantCallback(0)));
    /// <summary>The center of the blur.</summary>
    public Point Center
    {
        get => (Point)GetValue(CenterProperty);
        set => SetValue(CenterProperty, value);
    }

    /// <summary>The amount of blur.</summary>
    [Range(0.0, 0.2)]
    public override double Amount { get => base.Amount; set => base.Amount = value; }

    public ZoomBlurEffect() : base()
    {
        UpdateShaderValue(CenterProperty);
    }
}