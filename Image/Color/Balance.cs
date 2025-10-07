using Ion.Colors;
using Ion.Imaging;
using Ion.Numeral;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace Ion.Effects;

[Group(ImageEffectGroup.Color)]
public class BalanceEffect : BaseBlendEffect, IColorEffect
{
    public override string FilePath => $"{DefaultFolder}/ColorModelEffect.ps";

    private static readonly DependencyProperty ComponentProperty = DependencyProperty.Register(nameof(Component), typeof(Component3), typeof(BalanceEffect), new FrameworkPropertyMetadata(Component3.X, OnComponentChanged));
    private Component3 Component
    {
        get => (Component3)GetValue(ComponentProperty);
        set => SetValue(ComponentProperty, value);
    }
    private static void OnComponentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        => (sender as BalanceEffect).SetCurrentValue(ActualComponentProperty, (int)e.NewValue);

    private static readonly DependencyProperty ModeProperty = DependencyProperty.Register(nameof(Mode), typeof(ColorModelEffect.Modes), typeof(BalanceEffect), new FrameworkPropertyMetadata(ColorModelEffect.Modes.XYZ, OnModeChanged, OnModeCoerced));
    private ColorModelEffect.Modes Mode
    {
        get => (ColorModelEffect.Modes)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }
    private static void OnModeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        => sender.If<BalanceEffect>(i => i.SetCurrentValue(ActualModelProperty, (int)(ColorModelEffect.Modes)e.NewValue));
    private static object OnModeCoerced(DependencyObject sender, object input) => ColorModelEffect.Modes.XYZ;

    public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(nameof(Model), typeof(Type), typeof(BalanceEffect), new FrameworkPropertyMetadata(typeof(RGB), OnModelChanged));
    public Type Model
    {
        get => (Type)GetValue(ModelProperty);
        set => SetValue(ModelProperty, value);
    }
    private static void OnModelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        => sender.If<BalanceEffect>(i => i.SetCurrentValue(ActualModelProperty, i.GetModelIndex((Type)e.NewValue)));

    ///(C0)

    public static readonly DependencyProperty ActualModelProperty = DependencyProperty.Register(nameof(ActualModel), typeof(double), typeof(BalanceEffect), new FrameworkPropertyMetadata(0.0, PixelShaderConstantCallback(0)));
    public double ActualModel
    {
        get => (double)GetValue(ActualModelProperty);
        set => SetValue(ActualModelProperty, value);
    }

    ///(C1)

    private static readonly DependencyProperty ActualComponentProperty = DependencyProperty.Register(nameof(ActualComponent), typeof(double), typeof(BalanceEffect), new FrameworkPropertyMetadata(0.0, PixelShaderConstantCallback(1)));

    private double ActualComponent
    {
        get => (double)GetValue(ActualComponentProperty);
        set => SetValue(ActualComponentProperty, value);
    }

    ///(C2)

    private static readonly DependencyProperty ActualModeProperty = DependencyProperty.Register(nameof(ActualMode), typeof(double), typeof(BalanceEffect), new FrameworkPropertyMetadata((double)(int)ColorModelEffect.Modes.XYZ, PixelShaderConstantCallback(2)));

    private double ActualMode
    {
        get => (double)GetValue(ActualModeProperty);
        set => SetValue(ActualModeProperty, value);
    }

    ///(C3)

    public static readonly DependencyProperty XProperty = DependencyProperty.Register(nameof(X), typeof(double), typeof(BalanceEffect), new FrameworkPropertyMetadata(0d, PixelShaderConstantCallback(3)));
    [Range(-100.0, 100.0)]
    public double X
    {
        get => (double)GetValue(XProperty);
        set => SetValue(XProperty, value);
    }

    ///(C4)

    public static readonly DependencyProperty YProperty = DependencyProperty.Register(nameof(Y), typeof(double), typeof(BalanceEffect), new FrameworkPropertyMetadata(0d, PixelShaderConstantCallback(4)));
    [Range(-100.0, 100.0)]
    public double Y
    {
        get => (double)GetValue(YProperty);
        set => SetValue(YProperty, value);
    }

    ///(C5)

    public static readonly DependencyProperty ZProperty = DependencyProperty.Register(nameof(Z), typeof(double), typeof(BalanceEffect), new FrameworkPropertyMetadata(0d, PixelShaderConstantCallback(5)));
    [Range(-100.0, 100.0)]
    public double Z
    {
        get => (double)GetValue(ZProperty);
        set => SetValue(ZProperty, value);
    }

    ///(C6-7)

    public static readonly DependencyProperty HighlightAmountProperty = DependencyProperty.Register(nameof(HighlightAmount), typeof(double), typeof(BalanceEffect), new FrameworkPropertyMetadata(1.0, PixelShaderConstantCallback(6)));
    [Range(-1.0, 1.0)]
    public double HighlightAmount
    {
        get => (double)GetValue(HighlightAmountProperty);
        set => SetValue(HighlightAmountProperty, value);
    }

    public static readonly DependencyProperty HighlightRangeProperty = DependencyProperty.Register(nameof(HighlightRange), typeof(double), typeof(BalanceEffect), new FrameworkPropertyMetadata(1.0, PixelShaderConstantCallback(7)));
    [Range(0.0, 1.0)]
    public double HighlightRange
    {
        get => (double)GetValue(HighlightRangeProperty);
        set => SetValue(HighlightRangeProperty, value);
    }

    ///(C8-9)

    public static readonly DependencyProperty MidtoneAmountProperty = DependencyProperty.Register(nameof(MidtoneAmount), typeof(double), typeof(BalanceEffect), new FrameworkPropertyMetadata(1.0, PixelShaderConstantCallback(8)));
    [Range(-1.0, 1.0)]
    public double MidtoneAmount
    {
        get => (double)GetValue(MidtoneAmountProperty);
        set => SetValue(MidtoneAmountProperty, value);
    }

    public static readonly DependencyProperty MidtoneRangeProperty = DependencyProperty.Register(nameof(MidtoneRange), typeof(double), typeof(BalanceEffect), new FrameworkPropertyMetadata(0.66, PixelShaderConstantCallback(9)));
    [Range(0.0, 1.0)]
    public double MidtoneRange
    {
        get => (double)GetValue(MidtoneRangeProperty);
        set => SetValue(MidtoneRangeProperty, value);
    }

    ///(C10-11)

    public static readonly DependencyProperty ShadowAmountProperty = DependencyProperty.Register(nameof(ShadowAmount), typeof(double), typeof(BalanceEffect), new FrameworkPropertyMetadata(1.0, PixelShaderConstantCallback(10)));
    [Range(-1.0, 1.0)]
    public double ShadowAmount
    {
        get => (double)GetValue(ShadowAmountProperty);
        set => SetValue(ShadowAmountProperty, value);
    }

    public static readonly DependencyProperty ShadowRangeProperty = DependencyProperty.Register(nameof(ShadowRange), typeof(double), typeof(BalanceEffect), new FrameworkPropertyMetadata(0.33, PixelShaderConstantCallback(11)));
    [Range(0.0, 1.0)]
    public double ShadowRange
    {
        get => (double)GetValue(ShadowRangeProperty);
        set => SetValue(ShadowRangeProperty, value);
    }

    ///

    public BalanceEffect() : base()
    {

        UpdateShaderValue(ActualModelProperty);
        UpdateShaderValue(ActualComponentProperty);

        UpdateShaderValue(ActualModeProperty);

        UpdateShaderValue(XProperty);
        UpdateShaderValue(YProperty);
        UpdateShaderValue(ZProperty);

        UpdateShaderValue
            (HighlightAmountProperty);
        UpdateShaderValue
            (HighlightRangeProperty);

        UpdateShaderValue
            (MidtoneAmountProperty);
        UpdateShaderValue
            (MidtoneRangeProperty);

        UpdateShaderValue
            (ShadowAmountProperty);
        UpdateShaderValue
            (ShadowRangeProperty);
    }

    public BalanceEffect(double x, double y, double z, Type model) : this()
    {
        SetCurrentValue(ModelProperty, model);
        SetCurrentValue(XProperty, x);
        SetCurrentValue(YProperty, y);
        SetCurrentValue(ZProperty, z);
    }

    ///

    public override Color Apply(Color color, double amount = 1)
    {
        int b = color.B, g = color.G, r = color.R;

        color.Convert(out System.Drawing.Color d);
        var l = d.GetBrightness();

        Color result(int r0, int g0, int b0) => Color.FromArgb(color.A, Convert.ToByte(Math.Clamp(r + r0, 0, 255)), Convert.ToByte(Math.Clamp(g + g0, 0, 255)), Convert.ToByte(Math.Clamp(b + b0, 0, 255)));
        /*
        //Highlight
        if (Range == ColorRanges.Highlights && l > 0.66)
            return result(X.ToInt32(), Y.ToInt32(), Z.ToInt32());

        //Midtone
        else if (Range == ColorRanges.Midtones && l > 0.33)
            return result(X.ToInt32(), Y.ToInt32(), Z.ToInt32());

        //Shadow
        else if (Range == ColorRanges.Shadows && l <= 0.33)
            return result(X.ToInt32(), Y.ToInt32(), Z.ToInt32());
        */
        return result(Convert.ToInt32((Convert.ToDouble(X) * l).Round()), Convert.ToInt32((Convert.ToDouble(Y) * l).Round()), Convert.ToInt32((Convert.ToDouble(Z) * l).Round()));
    }
}