using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Ion.Effects;

/// <inheritdoc/>
public abstract class BaseEffect : ShaderEffect
{
    protected const string DefaultFolder = $"Default";

    protected string DefaultFilePath => $"{DefaultFolder}/{GetType().Name}.ps";

    public virtual string FilePath => DefaultFilePath;

    public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(BaseEffect), 0);
    public Brush Input
    {
        get => (Brush)GetValue(InputProperty);
        set => SetValue(InputProperty, value);
    }

    protected BaseEffect() : base()
    {
        PixelShader = new() { UriSource = new Uri($"/{AssemblyData.Name};component/{FilePath}", UriKind.Relative) };
        UpdateShaderValue(InputProperty);
    }
}