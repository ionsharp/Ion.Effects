﻿using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace Ion.Effects;

/// <summary>An effect that replaces pixels of a color with another color.</summary>
[Group(ImageEffectGroup.Color)]
public class ReplaceEffect : BaseBlendEffect
{
    public static readonly DependencyProperty Color1Property = DependencyProperty.Register("Color1", typeof(Color), typeof(ReplaceEffect), new FrameworkPropertyMetadata(Color.FromArgb(255, 0, 128, 0), PixelShaderConstantCallback(0)));
    public Color Color1
    {
        get => (Color)GetValue(Color1Property);
        set => SetValue(Color1Property, value);
    }

    public static readonly DependencyProperty Color2Property = DependencyProperty.Register("Color2", typeof(Color), typeof(ReplaceEffect), new FrameworkPropertyMetadata(Color.FromArgb(255, 0, 0, 0), PixelShaderConstantCallback(1)));
    public Color Color2
    {
        get => (Color)GetValue(Color2Property);
        set => SetValue(Color2Property, value);
    }

    public static readonly DependencyProperty ToleranceProperty = DependencyProperty.Register("Tolerance", typeof(double), typeof(ReplaceEffect), new FrameworkPropertyMetadata(((double)(0.9D)), PixelShaderConstantCallback(2)));
    /// <summary>The tolerance in color differences.</summary>
    [Range(0.0, 1.0)]
    public double Tolerance
    {
        get => (double)GetValue(ToleranceProperty);
        set => SetValue(ToleranceProperty, value);
    }

    public ReplaceEffect() : base()
    {
        UpdateShaderValue
            (Color1Property);
        UpdateShaderValue
            (Color2Property);
        UpdateShaderValue
            (ToleranceProperty);
    }

    public override Color Apply(Color color, double amount = 1) => color == Color1 ? Color2 : color;
}