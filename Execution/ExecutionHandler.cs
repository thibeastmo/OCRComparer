using OCRComparer.Execution.Google;
using OCRComparer.Execution.NewOcr;
using OCRComparer.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace OCRComparer.Execution
{
    internal class ExecutionHandler
    {

        public static async Task Handle(string ocrName,
            Image image,
            string imageLocation,
            string groundTruth)
        {
            string relativePath = Constants.TestImagesPath + ocrName;
            string fullPath = LocationUtil.GetFullPath(relativePath);
            bool checkIfImageAlreadyExisted = true;
            bool createAndStoreResult = true;

            // Check if the directory exists
            if (!Directory.Exists(fullPath))
            {
                // If not, create the directory
                try
                {
                    Directory.CreateDirectory(fullPath);
                    checkIfImageAlreadyExisted = false;
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowErrorMessageBox("The image directory for " + ocrName + " could not be saved correctly:\n" + ex.Message, title: ocrName);
                    createAndStoreResult = false;
                }
            }
            string relativeResultsPath = Constants.TestImagesPath + ocrName;
            string resultsFullPath = LocationUtil.GetFullPath(relativeResultsPath);
            if (!Directory.Exists(resultsFullPath))
            {
                // If not, create the directory
                try
                {
                    Directory.CreateDirectory(resultsFullPath);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowErrorMessageBox("The result directory for " + ocrName + " could not be saved correctly:\n" + ex.Message, title: ocrName);
                    createAndStoreResult = false;
                }
            }
            if (checkIfImageAlreadyExisted)
            {
                if (ImageChecker.CheckIfImageHasAlreadyBeenUsed(relativePath, imageLocation))
                {
                    createAndStoreResult = false;
                    ErrorHandler.ShowWarningMessageBox("This image has already being used for this api.", title: ocrName);
                }
            }
            if (createAndStoreResult)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                bool imageSaved = true;
                bool groundTruthSaved = true;
                bool resultSaved = true;
                string savedImageLocation = "";
                try
                {
                    savedImageLocation = ImageSavingHandler.SaveImage(relativePath, image, fileName);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowErrorMessageBox("The image could not be saved correctly:\n" + ex.Message, title: ocrName);
                    imageSaved = false;
                }
                try
                {
                    GroundTruthSavingHandler.SaveGroundTruth(relativePath, groundTruth, fileName);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowErrorMessageBox("The ground truth could not be saved correctly:\n" + ex.Message, title: ocrName);
                    groundTruthSaved = false;
                }
                if (imageSaved && groundTruthSaved)
                {
                    bool resultFromOCRReceived = true;
                    string result = "";
                    try
                    {
                        switch (ocrName)
                        {
                            case "OCR_Space":
                                result = await OCRSpace.Requester.PostRequest(imageLocation); break;
                            case "NewOcr":
                                result = await Requester.PostImageToNewOcr(imageLocation); break;
                            case "Google":
                                result = await VisionApi.PerformOCR(imageLocation);
                                break;
                            case "Tesseract":
                                result = TesseractExecution.UseTesseract(imageLocation);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorHandler.ShowErrorMessageBox("The result of the api request could not be retrieved correctly:\n" + ex.Message, title: ocrName);
                        resultFromOCRReceived = false;
                        resultSaved = false;
                    }
                    if (resultFromOCRReceived)
                    {
                        resultsFullPath = Path.Combine(resultsFullPath, fileName + ".txt");
                        try
                        {
                            File.WriteAllText(resultsFullPath, result);
                        }
                        catch (Exception ex)
                        {
                            ErrorHandler.ShowErrorMessageBox("The result of the api request could not be saved correctly:\n" + ex.Message, title: ocrName);
                            resultSaved = false;
                        }
                        if (resultSaved)
                        {
                            ErrorHandler.ShowInfoMessageBox("Image saved in statistics.", title: ocrName);
                        }
                    }
                }
                if (!resultSaved || !imageSaved || !groundTruthSaved)
                {
                    bool imageDeletedProperly = true;
                    bool groundTruthDeletedProperly = true;
                    if (imageSaved)
                    {
                        try
                        {
                            string imageSavedLocation = LocationUtil.GetFullPath(Path.Combine(relativePath, fileName + ".png"));
                            File.Delete(imageSavedLocation);
                        }
                        catch (Exception ex)
                        {
                            imageDeletedProperly = false;
                            ErrorHandler.ShowErrorMessageBox("The image could not be deleted properly:\n" + ex.Message, title: ocrName);
                        }
                    }

                    if (groundTruthSaved)
                    {
                        try
                        {
                            string gtLocation = LocationUtil.GetFullPath(Path.Combine(relativePath, fileName + ".gt.txt"));
                            File.Delete(gtLocation);
                        }
                        catch (Exception ex)
                        {
                            groundTruthDeletedProperly = false;
                            ErrorHandler.ShowErrorMessageBox("The ground truth could not be deleted properly:\n" + ex.Message, title: ocrName);
                        }
                    }

                    if (groundTruthDeletedProperly && imageDeletedProperly)
                    {
                        ErrorHandler.ShowErrorMessageBox("The process has been cancelled and the used files were not added to the existing testset.", title: ocrName);
                    }
                    else
                    {
                        ErrorHandler.ShowErrorMessageBox("The process has been cancelled.\n!! You need to deleted the files that were added to the testset manually!!"
                        + (imageSaved && !imageDeletedProperly ? "\n- Delete the image manually (.png)" : "") + (groundTruthSaved && !groundTruthDeletedProperly ? "\n- Delete the ground truth manually (.gt.txt)" : "") + "\n\nFilename: " + fileName, title: ocrName);
                    }
                }
            }
        }
    }
}
