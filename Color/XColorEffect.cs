using Ion.Collect;
using Ion.Colors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ion.Effects;

[Extend<IColorEffect>]
public static class XColorEffect
{
    /// <inheritdoc cref="GetModelIndex(IColorEffect, Type)"/>
    private static readonly Dictionary<Type, int> ModelIndex = [];

    /// <inheritdoc cref="GetModelType(IColorEffect, int)"/>
    private static readonly Dictionary<int, Type> ModelType = [];

    /// <summary>Get the index (based on type) that corresponds to each <see cref="IColor"/> in the shader file.</summary>
    public static int GetModelIndex(this IColorEffect _, Type color) => ModelIndex[color];

    /// <summary>Get the type (based on index) that corresponds to each <see cref="IColor"/> in the shader file.</summary>
    public static Type GetModelType(this IColorEffect _, int color) => ModelType[color];

    /// <summary>Get all color models supported by shader file as an <see cref="ListObservable{T}"/>.</summary>
    public static ListObservable<Type> Models => new(ModelType.Select(i => i.Value));

    static XColorEffect() => Initialize();

    /// <summary>Initialize important stuff for the effect to work properly.</summary>
    /// <remarks>Rearrange lines to match indices in shader file.</remarks>
    private static void Initialize()
    {
        int index = 0;
        void i<T>()
        {
            ModelType.Add(index, typeof(T)); ModelIndex.Add(typeof(T), index);
            index++;
        }

        /// <see cref="IColor3"/>
        i<RCA>(); i<RGB>(); i<RGV>(); i<RYB>();
        i<CMY>();
        i<HCV>(); i<HCY>();
        i<HPLuv>();
        i<HSB>();
        i<HSL>(); i<HSLuv>();
        i<HSM>();
        i<HSP>();
        i<HWBsb>();
        i<IPT>();
        i<JCh>(); i<JMh>(); i<Jsh>();
        i<JPEG>();
        i<Lab>(); i<Labh>(); /*i<Labi>();*/ i<Labj>(); i<Labk>(); i<Labksl>(); i<Labksb>(); i<Labkwb>();
        i<LCHab>(); i<LCHabh>(); i<LCHabj>(); i<LCHrg>(); i<LCHuv>(); i<LCHxy>();
        i<LMS>(); i<Luv>();
        i<QCh>(); i<QMh>(); i<Qsh>();
        i<rgG>();
        i<TSL>();
        i<UCS>();
        i<UVW>();
        i<xvYCC>();
        i<xyY>(); i<xyYC>();
        i<XYZ>();
        i<YCbCr>(); i<YCoCg>(); i<YDbDr>(); i<YES>(); i<YIQ>(); i<YPbPr>(); i<YUV>();

        /// <see cref="IColor4"/>
        i<CMYK>(); i<CMYW>(); i<RGBK>(); i<RGBW>();
    }
}