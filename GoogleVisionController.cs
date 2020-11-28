using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace OverwatchReviewerGUI
{
    public static class GoogleVisionController
    {
        public static void PrintImageText(string imagepath)
        {
            Google.Cloud.Vision.V1.Image image = Google.Cloud.Vision.V1.Image.FromFile(imagepath);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<EntityAnnotation> response = client.DetectText(image);
            string test = string.Empty;
            foreach (EntityAnnotation annotation in response)
            {
                if (annotation.Description != null)
                {
                    Console.WriteLine(annotation.Description);
                    test += Environment.NewLine + annotation.Description;
                }
            }
        }
    }
}
