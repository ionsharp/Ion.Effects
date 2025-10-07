using Ion;
using Ion.Colors;
using Ion.Imaging;
using Ion.Numeral;
using Ion.Reflect;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace Ion.Effects;

/// <summary>An effect that can be applied to an image.</summary>
public abstract class ImageEffect() : BaseEffect()
{
    public string Category => GetType().GetAttribute<GroupAttribute>()?.Name?.ToString();

    public override string FilePath => $"-Image/{Category}/{Name}.ps";

    public static readonly DependencyProperty AmountProperty = DependencyProperty.Register(nameof(Amount), typeof(double), typeof(ImageEffect), new FrameworkPropertyMetadata(1.0, PixelShaderConstantCallback(0)));
    [Range(0.0, 1.0)]
    public virtual double Amount
    {
        get => (double)GetValue(AmountProperty);
        set => SetValue(AmountProperty, value);
    }

    public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(nameof(IsVisible), typeof(bool), typeof(ImageEffect), new FrameworkPropertyMetadata(true));
    public bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }

    public string Name => GetType().Name.Replace(nameof(Effect), "");

    public virtual Color Apply(Color color, double amount = 1) => color;

    public virtual ColorMatrix4 Apply(ColorMatrix4 input)
    {
        var result = new ByteVector4[input.Rows, input.Columns];
        input.ForEach((y, x, oldColor) =>
        {
            var newColor = Apply(Color.FromArgb(oldColor.X, oldColor.Y, oldColor.Z, oldColor.W), Amount);
            result[y, x] = new(newColor.R, newColor.G, newColor.B, newColor.A);
        });
        return new ColorMatrix4(result.As());
    }

    public virtual void Apply(WriteableBitmap bitmap)
    {
        bitmap.ForEach((x, y, color) =>
        {
            Apply(color, Amount);
            return color;
        });
    }
}