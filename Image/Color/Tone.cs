﻿using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace Ion.Effects;

/// <summary>An effect that blends between partial desaturation and a two-color ramp.</summary>
[Group(ImageEffectGroup.Color)]
public class ToneEffect : BaseBlendEffect
{
    public static readonly DependencyProperty DesaturationProperty = DependencyProperty.Register("Desaturation", typeof(double), typeof(ToneEffect), new FrameworkPropertyMetadata(((double)(0.5D)), PixelShaderConstantCallback(0)));
    /// <summary>The amount of desaturation to apply.</summary>
    [Range(0.0, 1.0)]
    public double Desaturation
    {
        get => (double)GetValue(DesaturationProperty);
        set => SetValue(DesaturationProperty, value);
    }

    public static readonly DependencyProperty TonedProperty = DependencyProperty.Register("Toned", typeof(double), typeof(ToneEffect), new FrameworkPropertyMetadata(((double)(0.5D)), PixelShaderConstantCallback(1)));
    /// <summary>The amount of color toning to apply.</summary>
    [Range(0.0, 1.0)]
    public double Toned
    {
        get => (double)GetValue(TonedProperty);
        set => SetValue(TonedProperty, value);
    }

    public static readonly DependencyProperty LightColorProperty = DependencyProperty.Register("LightColor", typeof(Color), typeof(ToneEffect), new FrameworkPropertyMetadata(Color.FromArgb(255, 255, 255, 0), PixelShaderConstantCallback(2)));
    /// <summary>The first color to apply to input. This is usually a light tone.</summary>
    public Color LightColor
    {
        get => (Color)GetValue(LightColorProperty);
        set => SetValue(LightColorProperty, value);
    }

    public static readonly DependencyProperty DarkColorProperty = DependencyProperty.Register("DarkColor", typeof(Color), typeof(ToneEffect), new FrameworkPropertyMetadata(Color.FromArgb(255, 0, 0, 128), PixelShaderConstantCallback(3)));
    /// <summary>The second color to apply to the input. This is usuall a dark tone.</summary>
    public Color DarkColor
    {
        get => (Color)GetValue(DarkColorProperty);
        set => SetValue(DarkColorProperty, value);
    }

    public ToneEffect() : base()
    {
        UpdateShaderValue(DesaturationProperty);
        UpdateShaderValue(TonedProperty);
        UpdateShaderValue(LightColorProperty);
        UpdateShaderValue(DarkColorProperty);
    }

    public override Color Apply(Color color, double amount = 1) => color;
}