using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace OCRComparer.Utils
{
    internal class ImageChecker
    {
        public static bool CheckIfImageHasAlreadyBeenUsed(string relativepath, string imageFullPath)
        {
            if (!File.Exists(imageFullPath))
            {
                throw new FileNotFoundException($"Image not found: {imageFullPath}");
            }

            string fullPath = LocationUtil.GetFullPath(relativepath);
            string[] files = Directory.GetFiles(fullPath, "*.png");

            // Use Parallel.ForEach to process messages concurrently
            bool imageAlreadyUsed = false;
            Parallel.ForEach(files, file =>
            {
                if (AreImagesEqual(imageFullPath, file))
                {
                    imageAlreadyUsed = true;
                    // Exit the loop early if a match is found
                    Parallel.ForEach(file, (item, state) => state.Stop());
                    
                }
            });

            return imageAlreadyUsed; // Image not found in any messages
        }
        static bool AreImagesEqual(string imagePath1, string imagePath2)
        {
            using (Image<Rgba32> image1 = Image.Load<Rgba32>(imagePath1))
            using (Image<Rgba32> image2 = Image.Load<Rgba32>(imagePath2))
            {
                if (image1.Width != image2.Width || image1.Height != image2.Height)
                {
                    return false; // Images have different dimensions
                }

                const int margin = 10;
                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        var pix1 = image1[x, y];
                        var pix2 = image2[x, y];
                        if (image1[x, y] != image2[x, y])
                        {
                            if (pix1.R < pix2.R - margin || pix1.R > pix2.R + margin ||
                            pix1.G < pix2.G - margin || pix1.G > pix2.G + margin ||
                            pix1.B < pix2.B - margin || pix1.B > pix2.B + margin)
                            {
                                return false; // Pixels are different
                            }
                        }
                    }
                }

                return true; // All pixels are the same
            }
        }
    }
}
