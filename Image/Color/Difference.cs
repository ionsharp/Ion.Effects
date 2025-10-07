using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace Ion.Effects;

[Group(ImageEffectGroup.Color)]
public class DifferenceEffect : BaseBlendEffect
{
    public static readonly DependencyProperty RedProperty = DependencyProperty.Register("Red", typeof(double), typeof(DifferenceEffect), new FrameworkPropertyMetadata(0d, PixelShaderConstantCallback(0)));
    [Range(.0, 255.0)]
    public double Red
    {
        get => (double)GetValue(RedProperty);
        set => SetValue(RedProperty, value);
    }

    public static readonly DependencyProperty GreenProperty = DependencyProperty.Register("Green", typeof(double), typeof(DifferenceEffect), new FrameworkPropertyMetadata(0d, PixelShaderConstantCallback(1)));
    [Range(.0, 255.0)]
    public double Green
    {
        get => (double)GetValue(GreenProperty);
        set => SetValue(GreenProperty, value);
    }

    public static readonly DependencyProperty BlueProperty = DependencyProperty.Register("Blue", typeof(double), typeof(DifferenceEffect), new FrameworkPropertyMetadata(0d, PixelShaderConstantCallback(2)));
    [Range(.0, 255.0)]
    public double Blue
    {
        get => (double)GetValue(BlueProperty);
        set => SetValue(BlueProperty, value);
    }

    public DifferenceEffect() : base()
    {
        UpdateShaderValue(RedProperty);
        UpdateShaderValue(GreenProperty);
        UpdateShaderValue(BlueProperty);
    }

    public override Color Apply(Color color, double amount = 1)
    {
        int ob = color.B, og = color.G, or = color.R;
        var nr = Red > or
    ? Convert.ToInt32(Red) - or
    : or - Convert.ToInt32(Red);
        var ng = Green > og
    ? Convert.ToInt32(Green) - og
    : og - Convert.ToInt32(Green);
        var nb = Blue > ob
    ? Convert.ToInt32(Blue) - ob
    : ob - Convert.ToInt32(Blue);
        return Color.FromArgb(color.A, Convert.ToByte(Math.Clamp(nr, 0, 255)), Convert.ToByte(Math.Clamp(ng, 0, 255)), Convert.ToByte(Math.Clamp(nb, 0, 255)));
    }
}