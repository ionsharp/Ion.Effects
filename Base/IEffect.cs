namespace Ion.Effects;

/// <inheritdoc cref="System.Windows.Media.Effects.Effect"/>
/// <remarks>See <see cref="System.Windows.Media.Effects.Effect"/>.</remarks>
public interface IEffect
{
    /// <remarks>The file path of the shader used for the effect.</remarks>
    string FilePath { get; }
}