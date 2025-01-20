using Avalonia.Media.Imaging;
using ImageMagick;
using PicView.Avalonia.ImageHandling;
using PicView.Avalonia.ViewModels;
using PicView.Core.FileHandling;

namespace PicView.Avalonia.ImageEffects;

public static class ImageEffectsHelper
{
    public static async Task<WriteableBitmap> GetRadialBlur(string file)
    {
        using var magick = new MagickImage();
        await magick.ReadAsync(file).ConfigureAwait(false);
        // Apply radial blur
        magick.AdaptiveBlur(10);

        // Create a new morphology settings
        var morphology = new MorphologySettings
        {
            Kernel = Kernel.DoG,
            Method = MorphologyMethod.Convolve
        };
        magick.Morphology(morphology);
        return magick.ToWriteableBitmap();
    }
    
    public static async Task ApplyEffects(MainViewModel vm, ImageEffectConfig config,
        CancellationToken cancellationToken)
    {
        vm.IsLoading = true;
        try
        {
            await Task.Run(async () =>
            {
                var fileInfo = vm.FileInfo;
                await using var filestream = FileHelper.GetOptimizedFileStream(fileInfo);
                using var magick = new MagickImage();
                if (fileInfo.Length >= 2147483648)
                {
                    // Fixes "The file is too long. This operation is currently limited to supporting files less than 2 gigabytes in size."
                    // ReSharper disable once MethodHasAsyncOverloadWithCancellation
                    magick.Read(filestream);
                }
                else
                {
                    await magick.ReadAsync(filestream, cancellationToken).ConfigureAwait(false);
                }

                cancellationToken.ThrowIfCancellationRequested();

                magick.BrightnessContrast(config.Brightness, config.Contrast);
                magick.BackgroundColor = MagickColors.Transparent;
                magick.Settings.BackgroundColor = MagickColors.Transparent;
                magick.Settings.FillColor = MagickColors.Transparent;

                if (config.Negative)
                {
                    magick.Negate();
                }

                if (config.BlackAndWhite)
                {
                    magick.Grayscale();
                }

                if (config.OldMovie)
                {
                    // 1. Apply sepia tone
                    magick.SepiaTone(new Percentage(80));

                    // 2. Add noise
                    magick.AddNoise(NoiseType.MultiplicativeGaussian);

                    var random = new Random();

                    // 3. Add vertical bands (simulate scratches)
                    for (var i = 0; i < magick.Width; i += random.Next(1, 50))
                    {
                        using var band = new MagickImage(new MagickColor("#3E382A"), (uint)random.Next(1, 3),
                            magick.Height);
                        band.Evaluate(Channels.Alpha, EvaluateOperator.Set, 0.2); // semi-transparent
                        magick.Composite(band, i, 0, CompositeOperator.Over);
                    }
                }

                if (config.SketchStrokeWidth is not 0)
                {
                    magick.Charcoal(config.SketchStrokeWidth, 3);
                }

                if (config.PosterizeLevel is not 0)
                {
                    magick.Posterize(config.PosterizeLevel);
                }

                if (config.BlurLevel is not 0)
                {
                    magick.Blur(0, config.BlurLevel);
                }

                if (config.Solarize.ToUInt32() is not 0)
                {
                    magick.Solarize(config.Solarize);
                }

                cancellationToken.ThrowIfCancellationRequested();

                var bitmap = magick.ToWriteableBitmap();
                vm.ImageSource = bitmap;
            }, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            //
        }
        catch (Exception e)
        {
#if DEBUG
            Console.WriteLine(e);
#endif
        }
        finally
        {
            vm.IsLoading = false;
        }
    }
}
