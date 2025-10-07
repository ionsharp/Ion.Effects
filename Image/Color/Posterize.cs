using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;
using static System.Math;

namespace Ion.Effects;

[Group(ImageEffectGroup.Color)]
public class PosterizeEffect : BaseBlendEffect
{
    private readonly int defaultThreshold = 64;
    private float NumAreas;
    private float NumValues;

    public static readonly DependencyProperty ThresholdProperty = DependencyProperty.Register("Threshold", typeof(double), typeof(PosterizeEffect), new FrameworkPropertyMetadata(6.0, PixelShaderConstantCallback(0)));
    [Range(3.0, 64.0)]
    public double Threshold
    {
        get => (double)GetValue(ThresholdProperty);
        set => SetValue(ThresholdProperty, value);
    }

    public PosterizeEffect() : base()
    {
        Threshold = defaultThreshold;
        Update();

        UpdateShaderValue(ThresholdProperty);
    }

    private void Update()
    {
        NumAreas = 256f / Convert.ToSingle(Threshold);
        NumValues = 255f / (Convert.ToSingle(Threshold) - 1f);
    }

    public override Color Apply(Color color, double amount = 1)
    {
        int currentRed = color.R, currentGreen = color.G, currentBlue = color.B;

        float redAreaFloat = Convert.ToSingle(currentRed) / NumAreas;
        int redArea = Convert.ToInt32(redAreaFloat);
        if (redArea > redAreaFloat) redArea--;
        float newRedFloat = NumValues * redArea;
        int newRed = Convert.ToInt32(newRedFloat);
        if (newRed > newRedFloat) newRed--;

        float greenAreaFloat = Convert.ToSingle(currentGreen) / NumAreas;
        int greenArea = Convert.ToInt32(greenAreaFloat);
        if (greenArea > greenAreaFloat) greenArea--;
        float newGreenFloat = NumValues * greenArea;
        int newGreen = Convert.ToInt32(newGreenFloat);
        if (newGreen > newGreenFloat) newGreen--;

        float blueAreaFloat = Convert.ToSingle(currentBlue) / NumAreas;
        int blueArea = Convert.ToInt32(blueAreaFloat);
        if (blueArea > blueAreaFloat) blueArea--;
        float newBlueFloat = NumValues * blueArea;
        int newBlue = Convert.ToInt32(newBlueFloat);
        if (newBlue > newBlueFloat) newBlue--;

        return Color.FromArgb(color.A, Convert.ToByte(Clamp(newRed, 0, 255)), Convert.ToByte(Clamp(newGreen, 0, 255)), Convert.ToByte(Clamp(newBlue, 0, 255)));
    }
}