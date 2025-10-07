using Ion.Numeral;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ion.Effects;

[Group(ImageEffectGroup.Blend)]
public class PatternOverlayEffect : BaseBlendEffect
{
    public static readonly DependencyProperty PatternProperty = RegisterPixelShaderSamplerProperty(nameof(Pattern), typeof(PatternOverlayEffect), 1);
    public Brush Pattern
    {
        get => (Brush)GetValue(PatternProperty);
        set => SetValue(PatternProperty, value);
    }

    public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(nameof(Scale), typeof(double), typeof(PatternOverlayEffect), new FrameworkPropertyMetadata(1d, PixelShaderConstantCallback(2)));
    [Range(0.0, 10.0)]
    public double Scale
    {
        get => (double)GetValue(ScaleProperty);
        set => SetValue(ScaleProperty, value);
    }

    public PatternOverlayEffect() : base()
    {
        UpdateShaderValue(PatternProperty);
        UpdateShaderValue(ScaleProperty);
        //Pattern = new ImageBrush(new ImageSourceConverter().ConvertFromString(Resource.GetImageUri("Pattern1.png").OriginalString).As<ImageSource>().Bitmap(ImageExtensions.Png).WriteableBitmap());
    }

    protected static Matrix<Color> Render(WriteableBitmap input)
    {
        return default;
        /*
        Pattern = new(new ImageSourceConverter().ConvertFromString(Resource.GetImageUri("Pattern1.png").OriginalString).As<ImageSource>().Bitmap(ImageExtensions.Png).WriteableBitmap().Resize((Pattern.Rows * Scale).ToInt32(), (Pattern.Columns * Scale).ToInt32(), Interpolations.NearestNeighbor));
        var newPattern = Pattern;

        //Top left of bounds = non transparent pixel with smallest x and y
        System.Drawing.Point? topLeft = null;
        //Bottom right of bounds = non transparent pixel with largest x and y
        System.Drawing.Point? bottomRight = null;

        var sx = int.MaxValue;
        var sy = int.MaxValue;

        var lx = int.MinValue;
        var ly = int.MinValue;

        input.ForEach((xa, ya, a) =>
        {
            if (a.A > 0)
            {
                if (xa < sx)
                    sx = xa;

                if (ya < sy)
                    sy = ya;

                if (xa > lx)
                    lx = xa;

                if (ya > ly)
                    ly = ya;

            }
            return a;
        });

        topLeft = new(sx, sy);
        bottomRight = new(lx, ly);

        var bounds = new Int32Region(topLeft.Value.X, topLeft.Value.Y, bottomRight.Value.X - topLeft.Value.X, bottomRight.Value.Y - topLeft.Value.Y);

        var overlay = new ColorMatrix4((uint)bounds.Height, (uint)bounds.Width);
        for (uint x = 0; x < overlay.Columns; x++)
        {
            for (uint y = 0; y < overlay.Rows; y++)
                overlay[y, x] = newPattern[y % newPattern.Rows, x % newPattern.Columns];
        }

        input.ForEach((xa, ya, a) =>
        {
            if (xa >= topLeft.Value.X && xa < topLeft.Value.X + bounds.Width)
            {
                if (ya >= topLeft.Value.Y && ya < topLeft.Value.Y + bounds.Height)
                {
                    if (a.A > 0)
                        return a.Blend(overlay[(uint)(ya - topLeft.Value.Y), (uint)(xa - topLeft.Value.X)], BlendMode, Opacity);
                }
            }
            return a;
        });
        */
    }
}