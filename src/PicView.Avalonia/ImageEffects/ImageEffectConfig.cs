using ImageMagick;

namespace PicView.Avalonia.ImageEffects;

public struct ImageEffectConfig(Percentage brightness, Percentage contrast, double sketchStrokeWidth, double blurRadius, int posterizeLevel, bool negative, bool blackAndWhite, bool oldMovie)
{
    public Percentage Brightness = brightness;
    public Percentage Contrast = contrast;
    
    public double SketchStrokeWidth = sketchStrokeWidth;
    
    public int PosterizeLevel = posterizeLevel;
    
    public bool Negative = negative;
    public bool BlackAndWhite = blackAndWhite;
    public bool OldMovie = oldMovie;
    public double BlurRadius = blurRadius;
}
