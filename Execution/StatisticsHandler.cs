using Newtonsoft.Json;
using OCRComparer.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace OCRComparer.Execution
{
    internal class StatisticsHandler
    {
        public static Dictionary<string, List<Point>> GetPointsForOCRs()
        {
            Dictionary<string, List<Point>> pointsPerOCR = new Dictionary<string, List<Point>>();
            string[] ocrs = Directory.GetDirectories(LocationUtil.GetFullPath(Constants.TestImagesPath));
            foreach (string ocrDir in ocrs)
            {
                string ocrName = Path.GetFileName(ocrDir);
                List<TestData> testDatas = InitializeTestDataList(ocrDir);
                var exceptions = CreateStatisticPerImage(ocrDir, testDatas);
                if (exceptions.Any())
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var exception in exceptions)
                    {
                        sb.AppendLine(exception.Message + Environment.NewLine);
                    }
                    ErrorHandler.ShowWarningMessageBox("Not all results could be generated for " + ocrName.Replace("_", string.Empty) + ". Here are the reasons why:\n" + sb.ToString());
                }
                List<Point> points = new List<Point>();
                foreach (var testData in testDatas)
                {
                    if (testData.Statistics != null)
                    {
                        if (testData.Creation > Constants.ApplicationStartTime)
                        {
                            points.Add(new Point(testData.Statistics.WER, -testData.Statistics.TextSize));
                        }
                        else
                        {
                            points.Add(new Point(testData.Statistics.WER, testData.Statistics.TextSize));
                        }
                    }
                }
                pointsPerOCR.Add(ocrName, points);
            }
            return pointsPerOCR;
        }

        private static List<TestData> InitializeTestDataList(string ocrDir)
        {
            List<TestData> testDatas = new List<TestData>();

            List<string> testDatasToGenerate = new List<string>();
            string[] allFiles = Directory.GetFiles(ocrDir);
            foreach (string file in allFiles)
            {
                string fileName = Path.GetFileName(file);
                string fileNameWithoutExtension = fileName.Split('.')[0];
                if (!testDatas.Any(td => td.Name == fileNameWithoutExtension))
                {
                    testDatas.Add(new TestData(fileNameWithoutExtension, GetCreationDateOfFile(file)));
                }
                foreach (var testData in testDatas)
                {
                    if (testData.Name == fileNameWithoutExtension)
                    {
                        if (fileName.EndsWith(".gt.txt"))
                        {
                            testData.IsGroundTruthPresent = true;
                        }
                        else if (fileName.EndsWith(".txt"))
                        {
                            testData.IsResultPresent = true;
                        }
                        else if (fileName.EndsWith(".png"))
                        {
                            testData.IsImagePresent = true;
                        }
                        else if (fileName.EndsWith(".json"))
                        {
                            testData.AreStatisticsPresent = true;
                        }
                        else if (fileName.EndsWith(".jpg"))
                        {
                            string newFileName = Path.ChangeExtension(file, ".png");
                            File.Move(file, newFileName); // Rename to .png
                            testData.IsImagePresent = true;
                        }
                        break;
                    }
                }
            }
            return testDatas;
        }

        private static DateTime GetCreationDateOfFile(string fileFullPath)
        {
            DateTime creation = DateTime.Now;
            try
            {
                creation = File.GetCreationTime(fileFullPath);
            }
            catch
            {

            }
            return creation;
        }

        private static List<Exception> CreateStatisticPerImage(string ocrDir, List<TestData> testDatas)
        {
            List<Exception> exceptions = new List<Exception>();
            foreach (var testData in testDatas)
            {
                if (testData.IsResultPresent && testData.IsGroundTruthPresent && testData.IsImagePresent && !testData.AreStatisticsPresent)
                {
                    try
                    {
                        var testStatistics = new TestStatistics();
                        string gtPath = Path.Combine(ocrDir, testData.GroundTruthFileName);
                        string resultPath = Path.Combine(ocrDir, testData.ResultFileName);
                        string textGT = File.ReadAllText(gtPath);
                        string textResult = GetTextFromResult(ocrDir, resultPath);
                        testStatistics.WER = (int)LevenshteinDistanceCalculator.Calculate(textGT, textResult);
                        testStatistics.TextSize = textGT.Length;

                        testData.Statistics = testStatistics;

                        string json = JsonConvert.SerializeObject(testStatistics);
                        File.WriteAllText(Path.Combine(ocrDir, testData.Name + ".json"), json);
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                }
                else if (testData.IsEverythingPresent)
                {
                    string json = File.ReadAllText(Path.Combine(ocrDir, testData.StatisticsFileName));
                    testData.Statistics = JsonConvert.DeserializeObject<TestStatistics>(json);
                }
                else
                {
                    exceptions.Add(new Exception("Not all files for " + testData.Name + " were present."));
                }
            }
            return exceptions;
        }
        private static string GetTextFromResult(string ocrDir, string resultFileFullPath)
        {
            string ocrName = Path.GetFileName(ocrDir);
            string text = null;
            switch (ocrName)
            {
                case "Transkribus": text = TranskribusExecution.GetRawTextFromResult(resultFileFullPath); break;
                case "Tesseract": text = TesseractExecution.GetRawTextFromResult(resultFileFullPath); break;
                case "OCR_Space": text = Api.GetRawTextFromOCRSpace(resultFileFullPath); break;
                case "Google": text = Api.GetRawTextFromGoogle(resultFileFullPath); break;
                case "NewOcr": text = Api.GetRawTextFromNewOcr(resultFileFullPath); break;
            }
            return text;
        }
    }
}
