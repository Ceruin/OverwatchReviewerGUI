using System.Drawing;

namespace OverwatchReviewerGUI.File.Image
{
    public class ImageSection
    {
        public Rectangle TLeft { get; private set; }

        public Rectangle Kills { get; private set; }

        public Rectangle Deaths { get; private set; }

        public ImageSection(string imagePath)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath); // current image being processed

            float widthPercentDiffference = Utility.GetPercentDifference((float)image.Width, (float)ImageMaxProperties.width); // difference between the current processing image and the max allowed
            float heighPercentDiffference = Utility.GetPercentDifference((float)image.Height, (float)ImageMaxProperties.height); // difference between the current processing image and the max allowed

            TLeft = new Rectangle()
            {
                X = Utility.ScaleValue_BasedOnMaxResolution(widthPercentDiffference, ImageMaxProperties.tleftX),
                Y = Utility.ScaleValue_BasedOnMaxResolution(heighPercentDiffference, ImageMaxProperties.tleftY),
                Width = Utility.ScaleValue_BasedOnMaxResolution(widthPercentDiffference, ImageMaxProperties.tleftWidth),
                Height = Utility.ScaleValue_BasedOnMaxResolution(heighPercentDiffference, ImageMaxProperties.tleftHeight)
            };

            Kills = new Rectangle()
            {
                X = Utility.ScaleValue_BasedOnMaxResolution(widthPercentDiffference, ImageMaxProperties.killsX),
                Y = Utility.ScaleValue_BasedOnMaxResolution(heighPercentDiffference, ImageMaxProperties.killsY),
                Width = Utility.ScaleValue_BasedOnMaxResolution(widthPercentDiffference, ImageMaxProperties.killsWidth),
                Height = Utility.ScaleValue_BasedOnMaxResolution(heighPercentDiffference, ImageMaxProperties.killsHeight)
            };

            Deaths = new Rectangle()
            {
                X = Utility.ScaleValue_BasedOnMaxResolution(widthPercentDiffference, ImageMaxProperties.deathsX),
                Y = Utility.ScaleValue_BasedOnMaxResolution(heighPercentDiffference, ImageMaxProperties.deathsY),
                Width = Utility.ScaleValue_BasedOnMaxResolution(widthPercentDiffference, ImageMaxProperties.deathsWidth),
                Height = Utility.ScaleValue_BasedOnMaxResolution(heighPercentDiffference, ImageMaxProperties.deathsHeight)
            };
        }

        ~ImageSection()
        {
            // Deth
        }
    }
}