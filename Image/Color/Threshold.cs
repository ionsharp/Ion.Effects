using Ion.Imaging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace Ion.Effects;

[Group(ImageEffectGroup.Color)]
public class ThresholdEffect : BaseBlendEffect
{
    public static readonly DependencyProperty Color1Property = DependencyProperty.Register("Color1", typeof(Color), typeof(ThresholdEffect), new FrameworkPropertyMetadata(Color.FromArgb(255, 0, 0, 0), PixelShaderConstantCallback(0)));
    public Color Color1
    {
        get => (Color)GetValue(Color1Property);
        set => SetValue(Color1Property, value);
    }

    public static readonly DependencyProperty Color2Property = DependencyProperty.Register("Color2", typeof(Color), typeof(ThresholdEffect), new FrameworkPropertyMetadata(Color.FromArgb(255, 1, 1, 1), PixelShaderConstantCallback(1)));
    public Color Color2
    {
        get => (Color)GetValue(Color2Property);
        set => SetValue(Color2Property, value);
    }

    public static readonly DependencyProperty LevelProperty = DependencyProperty.Register("Level", typeof(double), typeof(ThresholdEffect), new FrameworkPropertyMetadata(100.0, PixelShaderConstantCallback(2)));
    [Range(1.0, 255.0)]
    public double Level
    {
        get => (double)GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    public ThresholdEffect() : base()
    {
        UpdateShaderValue(Color1Property);
        UpdateShaderValue(Color2Property);
        UpdateShaderValue(LevelProperty);
    }

    public override Color Apply(Color i, double amount = 1)
    {
        i.Convert(out System.Drawing.Color color);

        var brightness = color.GetBrightness();
        return brightness > Convert.ToDouble(Level) / 255.0 ? Color1 : Color2;
    }
}