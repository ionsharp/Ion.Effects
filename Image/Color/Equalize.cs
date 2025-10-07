using System.Windows.Media;

namespace Ion.Effects;

[Group(ImageEffectGroup.Color)]
public class EqualizeEffect() : BaseBlendEffect()
{
    public override Color Apply(Color color, double amount = 1) => color;

    /*
    public override void Apply(ColorMatrix4 oldColors, ColorMatrix4 newColors)
    {
        int height = oldColors.Rows.ToInt32(), width = oldColors.Columns.ToInt32();

        var rHistogram = Histogram.GetPoints(HistogramChannel.Red, oldColors);
        var gHistogram = Histogram.GetPoints(HistogramChannel.Green, oldColors);
        var bHistogram = Histogram.GetPoints(HistogramChannel.Blue, oldColors);

        var histR = new float[256];
        var histG = new float[256];
        var histB = new float[256];

        histR[0] = (rHistogram[0] * rHistogram.Length) / (width * height).ToSingle();
        histG[0] = (gHistogram[0] * gHistogram.Length) / (width * height).ToSingle();
        histB[0] = (bHistogram[0] * bHistogram.Length) / (width * height).ToSingle();

        long cumulativeR = rHistogram[0];
        long cumulativeG = gHistogram[0];
        long cumulativeB = bHistogram[0];

        for (var i = 1; i < histR.Length; i++)
        {
            cumulativeR += rHistogram[i];
            histR[i] = (cumulativeR * rHistogram.Length).ToSingle() / (width * height).ToSingle();

            cumulativeG += gHistogram[i];
            histG[i] = (cumulativeG * gHistogram.Length).ToSingle() / (width * height).ToSingle();

            cumulativeB += bHistogram[i];
            histB[i] = (cumulativeB * bHistogram.Length).ToSingle() / (width * height).ToSingle();
        }

        oldColors.Each((y, x, oldColor) =>
        {
            var intensityR = oldColor.R;
            var intensityG = oldColor.G;
            var intensityB = oldColor.B;

            var nValueR = (byte)histR[intensityR].Coerce(255);
            var nValueG = (byte)histG[intensityG].Coerce(255);
            var nValueB = (byte)histB[intensityB].Coerce(255);

            newColors.SetValue(y.ToUInt32(), x.ToUInt32(), Color.FromArgb(oldColor.A, nValueR, nValueG, nValueB));
            return oldColor;
        });
    }
    */
}