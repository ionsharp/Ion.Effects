using Ion.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace Ion.Effects;

public abstract class BaseBlendEffect : ImageEffect
{
    public static readonly DependencyProperty ActualBlendModeProperty = DependencyProperty.Register(nameof(ActualBlendMode), typeof(BlendModes), typeof(BaseBlendEffect), new FrameworkPropertyMetadata(BlendModes.Normal, OnActualBlendModeChanged));
    public BlendModes ActualBlendMode
    {
        get => (BlendModes)GetValue(ActualBlendModeProperty);
        set => SetValue(ActualBlendModeProperty, value);
    }
    protected static void OnActualBlendModeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        => (sender as BaseBlendEffect).BlendMode = (int)(BlendModes)e.NewValue;

    public static readonly DependencyProperty BlendModeProperty = DependencyProperty.Register(nameof(BlendMode), typeof(double), typeof(BaseBlendEffect), new FrameworkPropertyMetadata((double)(int)BlendModes.Normal, PixelShaderConstantCallback(0)));
    public double BlendMode
    {
        get => (double)GetValue(BlendModeProperty);
        set => SetValue(BlendModeProperty, value);
    }

    public static readonly DependencyProperty OpacityProperty = DependencyProperty.Register(nameof(Opacity), typeof(double), typeof(BaseBlendEffect), new FrameworkPropertyMetadata(1d, PixelShaderConstantCallback(1)));
    [Range(0.0, 1.0)]
    public double Opacity
    {
        get => (double)GetValue(OpacityProperty);
        set => SetValue(OpacityProperty, value);
    }

    protected BaseBlendEffect() : base()
    {
        UpdateShaderValue
            (BlendModeProperty);
        UpdateShaderValue
            (OpacityProperty);
    }
}