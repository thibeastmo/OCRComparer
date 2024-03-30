using OCRComparer.Utils;
using System;
using System.Drawing;
using System.IO;

namespace OCRComparer.Execution
{
    internal class ImageSavingHandler
    {
        public static string SaveImage(string relativePath, Image image, string fileName)
        {
            try
            {
                string imageLocation = LocationUtil.GetFullPath(Path.Combine(relativePath, fileName + ".png"));
                image.Save(imageLocation, System.Drawing.Imaging.ImageFormat.Png);
                return imageLocation;
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowErrorMessageBox(ex.Message);
                throw ex;
            }
        }
    }
}
