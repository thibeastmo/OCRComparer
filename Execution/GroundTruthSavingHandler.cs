using OCRComparer.Utils;
using System;
using System.IO;

namespace OCRComparer.Execution
{
    internal class GroundTruthSavingHandler
    {
        public static void SaveGroundTruth(string relativePath, string groundTruth, string imageName)
        {
            string gtLocation = LocationUtil.GetFullPath(Path.Combine(relativePath, imageName + ".gt.txt"));
            try
            {
                File.WriteAllText(gtLocation, groundTruth);
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowErrorMessageBox(ex.Message);
                throw ex;
            }
        }
    }
}
