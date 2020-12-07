using OverwatchReviewerGUI.File.Image;
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

        public static Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        public static ImageExtra CreateCroppedImage(Image image, Rectangle bounds, string imageSaveLocation)
        {
            Image croppedImage = CropImage(image, bounds);
            croppedImage.Save(imageSaveLocation, System.Drawing.Imaging.ImageFormat.Jpeg);

            return new ImageExtra(croppedImage, imageSaveLocation);
        }

        public static void CreateBoundsVisualization(ImageSection newSection, Image image)
        {
            VisualizeRectangle(image, newSection.TLeft);
            VisualizeRectangle(image, newSection.Kills);
            VisualizeRectangle(image, newSection.Deaths);
        }
    }
}