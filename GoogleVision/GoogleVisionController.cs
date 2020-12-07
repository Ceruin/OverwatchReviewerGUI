using Google.Cloud.Storage.V1;
using Google.Cloud.Vision.V1;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OverwatchReviewerGUI
{
    public static class GoogleVisionController
    {
        public static ICollection<string> GetImageText(string imagepath)
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Directory.GetCurrentDirectory() + FileLibrary.googleAPI);
            IReadOnlyList<EntityAnnotation> response = ImageAnnotatorClient.Create().DetectText(Image.FromFile(imagepath));
            List<string> printResults = new List<string>();

            foreach (EntityAnnotation annotation in response)
            {
                if (annotation.Description != null)
                {
                    printResults.Add(annotation.Description);
                }
            }

            if (printResults.Count <= 0)
            {
                var docResponse = ImageAnnotatorClient.Create().DetectDocumentText(Image.FromFile(imagepath));

                printResults.Add(docResponse.Text);
                foreach (string docAnnotation in docResponse.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
                {
                    printResults.Add(docAnnotation);
                }
            }

            return printResults;
        }
    }
}