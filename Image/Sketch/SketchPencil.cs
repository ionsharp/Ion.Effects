﻿using System.Windows;

namespace Ion.Effects;

[Group(nameof(ImageEffectGroup.Sketch))]
public class SketchPencilEffect : ImageEffect
{
    public static readonly DependencyProperty BrushSizeProperty = DependencyProperty.Register(nameof(BrushSize), typeof(double), typeof(SketchPencilEffect), new FrameworkPropertyMetadata(0.005, PixelShaderConstantCallback(0)));
    public double BrushSize
    {
        get => (double)GetValue(BrushSizeProperty);
        set => SetValue(BrushSizeProperty, value);
    }

    public SketchPencilEffect() : base()
    {
        UpdateShaderValue(BrushSizeProperty);
    }
}