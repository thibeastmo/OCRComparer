using System;
using System.IO;

namespace OCRComparer.Utils
{
    internal class LocationUtil
    {
        public static string GetFullPath(string relativePath)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            if (baseDir.EndsWith("Debug\\") || baseDir.EndsWith("Release\\"))
            {
                baseDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(baseDir)));
            }
            return Path.Combine(baseDir, relativePath);
        }
    }
}
