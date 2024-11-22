using System.Diagnostics;
using ImageMagick;

namespace PicView.Core.ImageDecoding
{
    public static class ImageFunctionHelper
    {
        /// <summary>
        ///     Gets the number of frames in an image.
        /// </summary>
        /// <param name="file">The path to the image file.</param>
        /// <returns>The number of frames in the image. Returns 0 if an error occurs.</returns>
        /// <remarks>
        ///     This method uses the Magick.NET library to load the image and retrieve the frame count.
        /// </remarks>
        public static int GetImageFrames(string file)
        {
            try
            {
                using var magickImageCollection = new MagickImageCollection();
                magickImageCollection.Ping(file);
                return magickImageCollection.Count;
            }
            catch (MagickException ex)
            {
#if DEBUG
                Trace.WriteLine($"{nameof(GetImageFrames)} Exception \n{ex}");
#endif

                return 0;
            }
        }
        
        public static uint GetCompressionQuality(string file)
        {
            using var magickImage = new MagickImage();
            magickImage.Ping(file);
            return magickImage.Quality;
        }
    }
}