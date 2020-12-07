using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace OverwatchReviewerGUI.File.Image
{
    public class ImageExtra
    {
        public System.Drawing.Image baseImage { get; private set; }
        public string imagePath { get; private set; }
        public List<string> imageOutput { get; set; }
        public CurrentDirectoryInfo currentDirectoryInfo { get; set; } = new CurrentDirectoryInfo();
        public ImageSection boundaries { get; set; }

        public ImageExtra(string imagePath)
        {
            this.baseImage = System.Drawing.Image.FromFile(imagePath);
            this.imagePath = imagePath;
        }

        public ImageExtra(System.Drawing.Image image, string imagePath)
        {
            this.baseImage = image;
            this.imagePath = imagePath;
        }

        public void GetImageOutput(Rectangle bounds)
        {
            imageOutput = GoogleVisionController.GetImageText(CreateCroppedVarient(bounds).imagePath).ToList();
        }

        public ImageExtra CreateCroppedVarient(Rectangle bounds)
        {
            return ImageExtensions.CreateCroppedImage(baseImage, bounds, currentDirectoryInfo.CurrentMinimeFilePath);
        }
    }
}