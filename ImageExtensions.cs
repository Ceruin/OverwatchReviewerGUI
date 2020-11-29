using System;
using System.Drawing;

namespace OverwatchReviewerGUI
{
    public static class ImageExtensions
    {
        public static void VisualizeRectangle(Image image, Rectangle rectangle)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.DrawRectangle(new Pen(Color.Red), rectangle);
            }
        }

        public static void GetImageSections(string imagepath, out Rectangle sMap, out Rectangle sVictory, out Rectangle sKills, out Rectangle sDeaths)
        {
            Image image = Image.FromFile(imagepath); // current image being processed
            Console.WriteLine($"{image.Width}" + " " + $"{ image.Height}"); // lame comment

            const int maxResolutionImageWidth = 2560; // max width of an image allowed
            const int maxResolutionImageHeight = 1440; // max height of an image allowed

            float widthPercentDiffference = Utility.GetPercentDifference((float)image.Width, (float)maxResolutionImageWidth); // difference between the current processing image and the max allowed
            float heighPercentDiffference = Utility.GetPercentDifference((float)image.Height, (float)maxResolutionImageHeight); // difference between the current processing image and the max allowed

            // Scaling functions
            int ScaleWidthValue_BasedOnMaxResolution(int val)
            {
                return Utility.ScaleValue_BasedOnMaxResolution(widthPercentDiffference, val);
            }

            int ScaleHeightValue_BasedOnMaxResolution(int val)
            {
                return Utility.ScaleValue_BasedOnMaxResolution(heighPercentDiffference, val);
            }

            // Map
            sMap = new Rectangle()
            {
                X = ScaleWidthValue_BasedOnMaxResolution(380),
                Y = ScaleHeightValue_BasedOnMaxResolution(47),
                Width = ScaleWidthValue_BasedOnMaxResolution(380),
                Height = ScaleHeightValue_BasedOnMaxResolution(45)
            };
            VisualizeRectangle(image, sMap);
            // Win/Loss
            sVictory = new Rectangle()
            {
                X = ScaleWidthValue_BasedOnMaxResolution(55),
                Y = ScaleHeightValue_BasedOnMaxResolution(49),
                Width = ScaleWidthValue_BasedOnMaxResolution(323),
                Height = ScaleHeightValue_BasedOnMaxResolution(95)
            };
            VisualizeRectangle(image, sVictory);
            // Kills
            sKills = new Rectangle()
            {
                X = ScaleWidthValue_BasedOnMaxResolution(284),
                Y = ScaleHeightValue_BasedOnMaxResolution(560),
                Width = ScaleWidthValue_BasedOnMaxResolution(175),
                Height = ScaleHeightValue_BasedOnMaxResolution(130)
            };
            VisualizeRectangle(image, sKills);
            // Deaths
            sDeaths = new Rectangle()
            {
                X = ScaleWidthValue_BasedOnMaxResolution(2120),
                Y = ScaleHeightValue_BasedOnMaxResolution(560),
                Width = ScaleWidthValue_BasedOnMaxResolution(150),
                Height = ScaleHeightValue_BasedOnMaxResolution(130)
            };
            VisualizeRectangle(image, sDeaths);
        }
    }
}