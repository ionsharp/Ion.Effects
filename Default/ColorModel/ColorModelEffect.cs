﻿using Ion;
using Ion.Colors;
using Ion.Numeral;
using Ion.Reflect;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace Ion.Effects;

public class ColorModelEffect : BaseEffect, IColorEffect
{
    #region (enum) Modes

    public enum Modes
    {
        /// <summary>
        /// Given <see cref="Component4.X"/>, <see cref="Component4.Y"/>, and <see cref="Component4.Z"/>, displays original color with corresponding components adjusted.
        /// </summary>
        XYZ,
        /// <summary>
        /// Given <see cref="Component4.Z"/>, displays <see cref="Component4.X"/> and <see cref="Component4.Y"/> (in any order).
        /// </summary>
        XY,
        /// <summary>
        /// Given <see cref="Component4.X"/> and <see cref="Component4.Y"/>, displays <see cref="Component4.Z"/> (in any order).
        /// </summary>
        Z
    }

    #endregion

    #region Properties

    #region Profile

    public static readonly DependencyProperty ProfileProperty = DependencyProperty.Register(nameof(Profile), typeof(ColorProfile), typeof(ColorModelEffect), new FrameworkPropertyMetadata(ColorProfile.Default, OnProfileChanged));
    public ColorProfile Profile
    {
        get => (ColorProfile)GetValue(ProfileProperty);
        set => SetValue(ProfileProperty, value);
    }
    private static void OnProfileChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        => sender.As<ColorModelEffect>().OnProfileChanged(new((ColorProfile)e.OldValue, (ColorProfile)e.NewValue));

    #endregion

    #region (C0) Model

    internal static readonly DependencyProperty ActualModelProperty = DependencyProperty.Register(nameof(ActualModel), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(0.0, PixelShaderConstantCallback(0)));
    internal double ActualModel
    {
        get => (double)GetValue(ActualModelProperty);
        set => SetValue(ActualModelProperty, value);
    }

    public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(nameof(Model), typeof(Type), typeof(ColorModelEffect), new FrameworkPropertyMetadata(typeof(HSB), OnModelChanged));
    public Type Model
    {
        get => (Type)GetValue(ModelProperty);
        set => SetValue(ModelProperty, value);
    }
    private static void OnModelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        => sender.If<ColorModelEffect>(i => i.SetCurrentValue(ActualModelProperty, (double)i.GetModelIndex((Type)e.NewValue)));

    #endregion

    #region (C1) XComponent

    public static readonly DependencyProperty XComponentProperty = DependencyProperty.Register(nameof(XComponent), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(0.0, PixelShaderConstantCallback(1)));
    public double XComponent
    {
        get => (double)GetValue(XComponentProperty);
        set => SetValue(XComponentProperty, value);
    }

    #endregion

    #region (C2) YComponent

    public static readonly DependencyProperty YComponentProperty = DependencyProperty.Register(nameof(YComponent), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(1.0, PixelShaderConstantCallback(2)));
    public double YComponent
    {
        get => (double)GetValue(YComponentProperty);
        set => SetValue(YComponentProperty, value);
    }

    #endregion

    #region (C3) Mode

    internal static readonly DependencyProperty ActualModeProperty = DependencyProperty.Register(nameof(ActualMode), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata((double)(int)Modes.XY, PixelShaderConstantCallback(3)));
    internal double ActualMode
    {
        get => (double)GetValue(ActualModeProperty);
        set => SetValue(ActualModeProperty, value);
    }

    public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(nameof(Mode), typeof(Modes), typeof(ColorModelEffect), new FrameworkPropertyMetadata(Modes.XY, OnModeChanged));
    public Modes Mode
    {
        get => (Modes)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }
    private static void OnModeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        => sender.If<ColorModelEffect>(i => i.SetCurrentValue(ActualModeProperty, (double)(int)(Modes)e.NewValue));

    #endregion

    #region (C4) Shape

    internal static readonly DependencyProperty ActualShapeProperty = DependencyProperty.Register(nameof(ActualShape), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata((double)(int)Polygon2D.Square, PixelShaderConstantCallback(4)));
    internal double ActualShape
    {
        get => (double)GetValue(ActualShapeProperty);
        set => SetValue(ActualShapeProperty, value);
    }

    public static readonly DependencyProperty ViewProperty = DependencyProperty.Register(nameof(Shape), typeof(Polygon2D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(Polygon2D.Square, OnShapeChanged));
    public Polygon2D Shape
    {
        get => (Polygon2D)GetValue(ViewProperty);
        set => SetValue(ViewProperty, value);
    }
    private static void OnShapeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        => sender.If<ColorModelEffect>(i => i.SetCurrentValue(ActualShapeProperty, (double)(int)(Polygon2D)e.NewValue));

    #endregion

    #region (C5) Depth

    public static readonly DependencyProperty DepthProperty = DependencyProperty.Register(nameof(Depth), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(double), PixelShaderConstantCallback(5)));
    public double Depth
    {
        get => (double)GetValue(DepthProperty);
        set => SetValue(DepthProperty, value);
    }

    #endregion

    #region (C6-9) X|Y|Z|W

    //(C6)

    public static readonly DependencyProperty XProperty = DependencyProperty.Register(nameof(X), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(0.0, PixelShaderConstantCallback(6)));
    public double X
    {
        get => (double)GetValue(XProperty);
        set => SetValue(XProperty, value);
    }

    //(C7)

    public static readonly DependencyProperty YProperty = DependencyProperty.Register(nameof(Y), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(0.0, PixelShaderConstantCallback(7)));
    public double Y
    {
        get => (double)GetValue(YProperty);
        set => SetValue(YProperty, value);
    }

    //(C8)

    public static readonly DependencyProperty ZProperty = DependencyProperty.Register(nameof(Z), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(0.0, PixelShaderConstantCallback(8)));
    public double Z
    {
        get => (double)GetValue(ZProperty);
        set => SetValue(ZProperty, value);
    }

    //(C9)

    public static readonly DependencyProperty WProperty = DependencyProperty.Register(nameof(W), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(0.0, PixelShaderConstantCallback(9)));
    public double W
    {
        get => (double)GetValue(WProperty);
        set => SetValue(WProperty, value);
    }

    #endregion

    #region (C10) Companding

    public static readonly DependencyProperty CompandingProperty = DependencyProperty.Register(nameof(Companding), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(4.0, PixelShaderConstantCallback(10)));
    /// <summary>The default is <see cref="ColorProfile.Default"/>.</summary>
    public double Companding
    {
        get => (double)GetValue(CompandingProperty);
        set => SetValue(CompandingProperty, value);
    }

    #endregion

    #region (C45-49) Compression_(A-E)

    public static readonly DependencyProperty Compression_AProperty = DependencyProperty.Register(nameof(Compression_A), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(double), PixelShaderConstantCallback(45)));
    /// <summary>γ</summary>
    public double Compression_A
    {
        get => (double)GetValue(Compression_AProperty);
        set => SetValue(Compression_AProperty, value);
    }

    public static readonly DependencyProperty Compression_BProperty = DependencyProperty.Register(nameof(Compression_B), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(double), PixelShaderConstantCallback(46)));
    /// <summary>α</summary>
    public double Compression_B
    {
        get => (double)GetValue(Compression_BProperty);
        set => SetValue(Compression_BProperty, value);
    }

    public static readonly DependencyProperty Compression_CProperty = DependencyProperty.Register(nameof(Compression_C), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(double), PixelShaderConstantCallback(47)));
    /// <summary>β</summary>
    public double Compression_C
    {
        get => (double)GetValue(Compression_CProperty);
        set => SetValue(Compression_CProperty, value);
    }

    public static readonly DependencyProperty Compression_DProperty = DependencyProperty.Register(nameof(Compression_D), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(double), PixelShaderConstantCallback(48)));
    /// <summary>δ</summary>
    public double Compression_D
    {
        get => (double)GetValue(Compression_DProperty);
        set => SetValue(Compression_DProperty, value);
    }

    public static readonly DependencyProperty Compression_EProperty = DependencyProperty.Register(nameof(Compression_E), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(double), PixelShaderConstantCallback(49)));
    /// <summary>βδ</summary>
    public double Compression_E
    {
        get => (double)GetValue(Compression_EProperty);
        set => SetValue(Compression_EProperty, value);
    }

    #endregion

    #region (C12-13) White(X|Y)

    //(C12)

    public static readonly DependencyProperty WhiteXProperty = DependencyProperty.Register(nameof(WhiteX), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(Illuminant2.D65.X, PixelShaderConstantCallback(12)));
    /// <summary>The default is <see cref="Illuminant2.D65"/>.</summary>
    public double WhiteX
    {
        get => (double)GetValue(WhiteXProperty);
        set => SetValue(WhiteXProperty, value);
    }

    //(C13)

    public static readonly DependencyProperty WhiteYProperty = DependencyProperty.Register(nameof(WhiteY), typeof(double), typeof(ColorModelEffect), new FrameworkPropertyMetadata(Illuminant2.D65.Y, PixelShaderConstantCallback(13)));
    /// <summary>The default is <see cref="Illuminant2.D65"/>.</summary>
    public double WhiteY
    {
        get => (double)GetValue(WhiteYProperty);
        set => SetValue(WhiteYProperty, value);
    }

    #endregion

    ///

    #region (C14-16) LMS_XYZ_(x|y|z)

    //(C14)

    public static readonly DependencyProperty LMS_XYZ_xProperty = DependencyProperty.Register(nameof(LMS_XYZ_x), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(14)));
    public System.Windows.Media.Media3D.Point3D LMS_XYZ_x
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(LMS_XYZ_xProperty);
        set => SetValue(LMS_XYZ_xProperty, value);
    }

    //(C15)

    public static readonly DependencyProperty LMS_XYZ_yProperty = DependencyProperty.Register(nameof(LMS_XYZ_y), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(15)));
    public System.Windows.Media.Media3D.Point3D LMS_XYZ_y
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(LMS_XYZ_yProperty);
        set => SetValue(LMS_XYZ_yProperty, value);
    }

    //(C16)

    public static readonly DependencyProperty LMS_XYZ_zProperty = DependencyProperty.Register(nameof(LMS_XYZ_z), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(16)));
    public System.Windows.Media.Media3D.Point3D LMS_XYZ_z
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(LMS_XYZ_zProperty);
        set => SetValue(LMS_XYZ_zProperty, value);
    }

    #endregion

    #region (C17-19) RGB_XYZ_(x|y|z)

    //(C17)

    public static readonly DependencyProperty RGB_XYZ_xProperty = DependencyProperty.Register(nameof(RGB_XYZ_x), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(17)));
    public System.Windows.Media.Media3D.Point3D RGB_XYZ_x
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(RGB_XYZ_xProperty);
        set => SetValue(RGB_XYZ_xProperty, value);
    }

    //(C18)

    public static readonly DependencyProperty RGB_XYZ_yProperty = DependencyProperty.Register(nameof(RGB_XYZ_y), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(18)));
    public System.Windows.Media.Media3D.Point3D RGB_XYZ_y
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(RGB_XYZ_yProperty);
        set => SetValue(RGB_XYZ_yProperty, value);
    }

    //(C19)

    public static readonly DependencyProperty RGB_XYZ_zProperty = DependencyProperty.Register(nameof(RGB_XYZ_z), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(19)));
    public System.Windows.Media.Media3D.Point3D RGB_XYZ_z
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(RGB_XYZ_zProperty);
        set => SetValue(RGB_XYZ_zProperty, value);
    }

    #endregion

    #region (C20-22) XYZ_LMS_(x|y|z)

    //(C20)

    public static readonly DependencyProperty XYZ_LMS_xProperty = DependencyProperty.Register(nameof(XYZ_LMS_x), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(20)));
    public System.Windows.Media.Media3D.Point3D XYZ_LMS_x
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(XYZ_LMS_xProperty);
        set => SetValue(XYZ_LMS_xProperty, value);
    }

    //(C21)

    public static readonly DependencyProperty XYZ_LMS_yProperty = DependencyProperty.Register(nameof(XYZ_LMS_y), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(21)));
    public System.Windows.Media.Media3D.Point3D XYZ_LMS_y
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(XYZ_LMS_yProperty);
        set => SetValue(XYZ_LMS_yProperty, value);
    }

    //(C22)

    public static readonly DependencyProperty XYZ_LMS_zProperty = DependencyProperty.Register(nameof(XYZ_LMS_z), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(22)));
    public System.Windows.Media.Media3D.Point3D XYZ_LMS_z
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(XYZ_LMS_zProperty);
        set => SetValue(XYZ_LMS_zProperty, value);
    }

    #endregion

    #region (C23-25) XYZ_RGB_(x|y|z)

    //(C23)

    public static readonly DependencyProperty XYZ_RGB_xProperty = DependencyProperty.Register(nameof(XYZ_RGB_x), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(23)));
    public System.Windows.Media.Media3D.Point3D XYZ_RGB_x
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(XYZ_RGB_xProperty);
        set => SetValue(XYZ_RGB_xProperty, value);
    }

    //(C24)

    public static readonly DependencyProperty XYZ_RGB_yProperty = DependencyProperty.Register(nameof(XYZ_RGB_y), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(24)));
    public System.Windows.Media.Media3D.Point3D XYZ_RGB_y
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(XYZ_RGB_yProperty);
        set => SetValue(XYZ_RGB_yProperty, value);
    }

    //(C25)

    public static readonly DependencyProperty XYZ_RGB_zProperty = DependencyProperty.Register(nameof(XYZ_RGB_z), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(25)));
    public System.Windows.Media.Media3D.Point3D XYZ_RGB_z
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(XYZ_RGB_zProperty);
        set => SetValue(XYZ_RGB_zProperty, value);
    }

    #endregion

    ///

    #region (C26-28) LABk_LMSk_(x|y|z)

    //(26)

    public static readonly DependencyProperty LABk_LMSk_xProperty = DependencyProperty.Register(nameof(LABk_LMSk_x), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(26)));
    public System.Windows.Media.Media3D.Point3D LABk_LMSk_x
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(LABk_LMSk_xProperty);
        set => SetValue(LABk_LMSk_xProperty, value);
    }

    //(27)

    public static readonly DependencyProperty LABk_LMSk_yProperty = DependencyProperty.Register(nameof(LABk_LMSk_y), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(27)));
    public System.Windows.Media.Media3D.Point3D LABk_LMSk_y
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(LABk_LMSk_yProperty);
        set => SetValue(LABk_LMSk_yProperty, value);
    }

    //(28)

    public static readonly DependencyProperty LABk_LMSk_zProperty = DependencyProperty.Register(nameof(LABk_LMSk_z), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(28)));
    public System.Windows.Media.Media3D.Point3D LABk_LMSk_z
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(LABk_LMSk_zProperty);
        set => SetValue(LABk_LMSk_zProperty, value);
    }

    #endregion

    #region (C29-31) LMSk_LABk_(x|y|z)

    //(29)

    public static readonly DependencyProperty LMSk_LABk_xProperty = DependencyProperty.Register(nameof(LMSk_LABk_x), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(29)));
    public System.Windows.Media.Media3D.Point3D LMSk_LABk_x
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(LMSk_LABk_xProperty);
        set => SetValue(LMSk_LABk_xProperty, value);
    }

    //(30)

    public static readonly DependencyProperty LMSk_LABk_yProperty = DependencyProperty.Register(nameof(LMSk_LABk_y), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(30)));
    public System.Windows.Media.Media3D.Point3D LMSk_LABk_y
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(LMSk_LABk_yProperty);
        set => SetValue(LMSk_LABk_yProperty, value);
    }

    //(31)

    public static readonly DependencyProperty LMSk_LABk_zProperty = DependencyProperty.Register(nameof(LMSk_LABk_z), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(31)));
    public System.Windows.Media.Media3D.Point3D LMSk_LABk_z
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(LMSk_LABk_zProperty);
        set => SetValue(LMSk_LABk_zProperty, value);
    }

    #endregion

    #region (C32-34) LMSk_XYZk_(x|y|z)

    //(32)

    public static readonly DependencyProperty LMSk_XYZk_xProperty = DependencyProperty.Register(nameof(LMSk_XYZk_x), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(32)));
    public System.Windows.Media.Media3D.Point3D LMSk_XYZk_x
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(LMSk_XYZk_xProperty);
        set => SetValue(LMSk_XYZk_xProperty, value);
    }

    //(33)

    public static readonly DependencyProperty LMSk_XYZk_yProperty = DependencyProperty.Register(nameof(LMSk_XYZk_y), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(33)));
    public System.Windows.Media.Media3D.Point3D LMSk_XYZk_y
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(LMSk_XYZk_yProperty);
        set => SetValue(LMSk_XYZk_yProperty, value);
    }

    //(34)

    public static readonly DependencyProperty LMSk_XYZk_zProperty = DependencyProperty.Register(nameof(LMSk_XYZk_z), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(34)));
    public System.Windows.Media.Media3D.Point3D LMSk_XYZk_z
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(LMSk_XYZk_zProperty);
        set => SetValue(LMSk_XYZk_zProperty, value);
    }

    #endregion

    #region (C35-37) XYZk_LMSk_(x|y|z)

    //(35)

    public static readonly DependencyProperty XYZk_LMSk_xProperty = DependencyProperty.Register(nameof(XYZk_LMSk_x), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(35)));
    public System.Windows.Media.Media3D.Point3D XYZk_LMSk_x
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(XYZk_LMSk_xProperty);
        set => SetValue(XYZk_LMSk_xProperty, value);
    }

    //(36)

    public static readonly DependencyProperty XYZk_LMSk_yProperty = DependencyProperty.Register(nameof(XYZk_LMSk_y), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(36)));
    public System.Windows.Media.Media3D.Point3D XYZk_LMSk_y
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(XYZk_LMSk_yProperty);
        set => SetValue(XYZk_LMSk_yProperty, value);
    }

    //(37)

    public static readonly DependencyProperty XYZk_LMSk_zProperty = DependencyProperty.Register(nameof(XYZk_LMSk_z), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(37)));
    public System.Windows.Media.Media3D.Point3D XYZk_LMSk_z
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(XYZk_LMSk_zProperty);
        set => SetValue(XYZk_LMSk_zProperty, value);
    }

    #endregion

    ///

    #region (C38) xyYC_exy

    public static readonly DependencyProperty xyYC_exyProperty = DependencyProperty.Register(nameof(xyYC_exy), typeof(System.Windows.Media.Media3D.Point3D), typeof(ColorModelEffect), new FrameworkPropertyMetadata(default(System.Windows.Media.Media3D.Point3D), PixelShaderConstantCallback(38)));
#pragma warning disable IDE1006 // Naming Styles
    public System.Windows.Media.Media3D.Point3D xyYC_exy
#pragma warning restore IDE1006 // Naming Styles
    {
        get => (System.Windows.Media.Media3D.Point3D)GetValue(xyYC_exyProperty);
        set => SetValue(xyYC_exyProperty, value);
    }

    #endregion

    #endregion

    #region ColorModelEffect

    public ColorModelEffect() : base()
    {

        ///

        UpdateShaderValue(ActualModelProperty);

        UpdateShaderValue(XComponentProperty);
        UpdateShaderValue(YComponentProperty);

        UpdateShaderValue(ActualModeProperty);
        UpdateShaderValue(ActualShapeProperty);
        UpdateShaderValue(DepthProperty);

        UpdateShaderValue(XProperty);
        UpdateShaderValue(YProperty);
        UpdateShaderValue(ZProperty);
        UpdateShaderValue(WProperty);

        UpdateShaderValue(CompandingProperty);

        UpdateShaderValue(Compression_AProperty);
        UpdateShaderValue(Compression_BProperty);
        UpdateShaderValue(Compression_CProperty);
        UpdateShaderValue(Compression_DProperty);
        UpdateShaderValue(Compression_EProperty);

        UpdateShaderValue(WhiteXProperty);
        UpdateShaderValue(WhiteYProperty);

        ///

        UpdateShaderValue(LMS_XYZ_xProperty);
        UpdateShaderValue(LMS_XYZ_yProperty);
        UpdateShaderValue(LMS_XYZ_zProperty);

        UpdateShaderValue(RGB_XYZ_xProperty);
        UpdateShaderValue(RGB_XYZ_yProperty);
        UpdateShaderValue(RGB_XYZ_zProperty);

        UpdateShaderValue(XYZ_LMS_xProperty);
        UpdateShaderValue(XYZ_LMS_yProperty);
        UpdateShaderValue(XYZ_LMS_zProperty);

        UpdateShaderValue(XYZ_RGB_xProperty);
        UpdateShaderValue(XYZ_RGB_yProperty);
        UpdateShaderValue(XYZ_RGB_zProperty);

        ///

        UpdateShaderValue(LABk_LMSk_xProperty);
        UpdateShaderValue(LABk_LMSk_yProperty);
        UpdateShaderValue(LABk_LMSk_zProperty);

        UpdateShaderValue(LMSk_LABk_xProperty);
        UpdateShaderValue(LMSk_LABk_yProperty);
        UpdateShaderValue(LMSk_LABk_zProperty);

        UpdateShaderValue(LMSk_XYZk_xProperty);
        UpdateShaderValue(LMSk_XYZk_yProperty);
        UpdateShaderValue(LMSk_XYZk_zProperty);

        UpdateShaderValue(XYZk_LMSk_xProperty);
        UpdateShaderValue(XYZk_LMSk_yProperty);
        UpdateShaderValue(XYZk_LMSk_zProperty);

        ///

        SetCurrentValue(LABk_LMSk_xProperty, GetPoint(Labk.LAB_LMS, 0));
        SetCurrentValue(LABk_LMSk_yProperty, GetPoint(Labk.LAB_LMS, 1));
        SetCurrentValue(LABk_LMSk_zProperty, GetPoint(Labk.LAB_LMS, 2));

        SetCurrentValue(LMSk_LABk_xProperty, GetPoint(Labk.LMS_LAB, 0));
        SetCurrentValue(LMSk_LABk_yProperty, GetPoint(Labk.LMS_LAB, 1));
        SetCurrentValue(LMSk_LABk_zProperty, GetPoint(Labk.LMS_LAB, 2));

        SetCurrentValue(LMSk_XYZk_xProperty, GetPoint(Labk.LMS_XYZ, 0));
        SetCurrentValue(LMSk_XYZk_yProperty, GetPoint(Labk.LMS_XYZ, 1));
        SetCurrentValue(LMSk_XYZk_zProperty, GetPoint(Labk.LMS_XYZ, 2));

        SetCurrentValue(XYZk_LMSk_xProperty, GetPoint(Labk.XYZ_LMS, 0));
        SetCurrentValue(XYZk_LMSk_yProperty, GetPoint(Labk.XYZ_LMS, 1));
        SetCurrentValue(XYZk_LMSk_zProperty, GetPoint(Labk.XYZ_LMS, 2));

        ///

        UpdateShaderValue(xyYC_exyProperty);
        SetCurrentValue(xyYC_exyProperty, new System.Windows.Media.Media3D.Point3D(xyYC.Colors[xyYC.MinHue][1], xyYC.Colors[xyYC.MinHue][2], xyYC.Colors[xyYC.MinHue][3]));

        OnProfileChanged(new(Profile));
    }

    #endregion

    #region Methods

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
    private void Update_xyYC(Double1 input)
    {
        int j = Convert.ToInt32(new Range<double>(0, 1).ToRange(10, 76, input).Round());

        Numeral.Vector row = default;
        for (var i = xyYC.MinHue; i < xyYC.MaxHue + 1; i++)
        {
            if (xyYC.Colors.ContainsKey(i))
            {
                if (j <= xyYC.Colors[i][0])
                {
                    row = xyYC.Colors[i];
                    break;
                }
            }
        }
        SetCurrentValue(xyYC_exyProperty, new System.Windows.Media.Media3D.Point3D(row[1], row[2], row[3]));
    }

    private static System.Windows.Media.Media3D.Point3D GetPoint(IMatrix3x3<double> m, int y) => new(m[y, 0], m[y, 1], m[y, 2]);

    private static int GetCompression(ICompress compress)
    {
        if (compress is GammaCompression)
            return 1;

        if (compress is GammaLogCompression)
            return 2;

        if (compress is PQCompression)
            return 3;

        return 0;
    }

    protected virtual void OnProfileChanged(ValueChange<ColorProfile> input)
    {
        SetCurrentValue(CompandingProperty, GetCompression(input.NewValue.Compression).ToDouble());
        if (input.NewValue.Compression is Compression compression)
        {
            SetCurrentValue(Compression_AProperty,
                compression.γ);
            SetCurrentValue(Compression_BProperty,
                compression.α);
            SetCurrentValue(Compression_CProperty,
                compression.β);
            SetCurrentValue(Compression_DProperty,
                compression.δ);
            SetCurrentValue(Compression_EProperty,
                compression.βδ);
        }
        else if (input.NewValue.Compression is GammaCompression gammaCompression)
        {
            SetCurrentValue(Compression_AProperty,
                gammaCompression.Gamma);
        }

        SetCurrentValue(WhiteXProperty, input.NewValue.Chromacity.X);
        SetCurrentValue(WhiteYProperty, input.NewValue.Chromacity.Y);

        ///

        var m_RGB_XYZ
            = new ChromacityMatrix(input.NewValue);
        var m_XYZ_LMS
            = input.NewValue.Adaptation;

        var m_LMS_XYZ
            = (m_XYZ_LMS as IMatrix3x3<double>).Invert();
        var m_XYZ_RGB
            = (m_RGB_XYZ as IMatrix3x3<double>).Invert();

        SetCurrentValue(LMS_XYZ_xProperty, GetPoint(m_LMS_XYZ, 0));
        SetCurrentValue(LMS_XYZ_yProperty, GetPoint(m_LMS_XYZ, 1));
        SetCurrentValue(LMS_XYZ_zProperty, GetPoint(m_LMS_XYZ, 2));

        SetCurrentValue(RGB_XYZ_xProperty, GetPoint(m_RGB_XYZ, 0));
        SetCurrentValue(RGB_XYZ_yProperty, GetPoint(m_RGB_XYZ, 1));
        SetCurrentValue(RGB_XYZ_zProperty, GetPoint(m_RGB_XYZ, 2));

        SetCurrentValue(XYZ_LMS_xProperty, GetPoint(m_XYZ_LMS, 0));
        SetCurrentValue(XYZ_LMS_yProperty, GetPoint(m_XYZ_LMS, 1));
        SetCurrentValue(XYZ_LMS_zProperty, GetPoint(m_XYZ_LMS, 2));

        SetCurrentValue(XYZ_RGB_xProperty, GetPoint(m_XYZ_RGB, 0));
        SetCurrentValue(XYZ_RGB_yProperty, GetPoint(m_XYZ_RGB, 1));
        SetCurrentValue(XYZ_RGB_zProperty, GetPoint(m_XYZ_RGB, 2));
    }

    #endregion
}