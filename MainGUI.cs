using OverwatchReviewerGUI.Classes;
using OverwatchReviewerGUI.File.Image;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OverwatchReviewerGUI
{
    /*
     * Extract Code to Class
     * Google Cloud Vision API
     */

    public partial class MainGUI : Form
    {
        public MainGUI()
        {
            InitializeComponent();
        }

        public void RunApp()
        {
            string inputDir, outputFilePath, minimeDir;
            FileController.DirectorySetup(out inputDir, out outputFilePath, out minimeDir);

            Console.WriteLine("Reading Images...");
            if (Directory.Exists(inputDir))
            {
                ProcessImages(inputDir, outputFilePath, minimeDir);
            }
            else
            {
                Console.WriteLine("Bad Directory");
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            RunApp();
        }

        private void ProcessImages(string inputDir, string outputFilePath, string minimeDir)
        {
            string[] imagepaths = Directory.GetFiles(inputDir);
            //List<OcrSection> results = new List<OcrSection>();
            //OcrController.GetOcrSectionsFromImages(imagepaths, results);

            List<ImageSummary> results = new List<ImageSummary>();
            foreach (string imagepath in imagepaths)
            {
                results.Add(CreateCropMiniMes(imagepath, pbLoad));
            }

            //List<OcrSummary> summary = new List<OcrSummary>();
            //OcrController.GetOcrSummaryResults(results, summary);

            StringBuilder fileText = new StringBuilder();
            FileController.CreateSummaryFileText(fileText, MapSummaryController.GetMapSummaries(results));
            FileController.CreateSummaryFile(outputFilePath, fileText);
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        private static ImageSummary CreateCropMiniMes(string imagepath, PictureBox pictureBox = null)
        {
            // print image text returns empty for 0 lmao
            ImageSummary newSummary = new ImageSummary();
            ImageSection newSection = new ImageSection(imagepath);
            Image image = Image.FromFile(imagepath);
            CreateBoundsVisualization(newSection, image);
            if (pictureBox != null)
                pictureBox.Image = image;

            FileController.MiniDirectoryClear();

            var vision2 = GoogleVisionController.PrintImageText(CreateCroppedImage(imagepath, newSection.TLeft).imagePath).ToArray();
            var te = vision2[0];
            var visio2 = te.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            newSummary.Victory = visio2[1].ToUpper().Contains("VIC");


            var vision1 = GoogleVisionController.PrintImageText(CreateCroppedImage(imagepath, newSection.TLeft).imagePath).ToList();
            var visio1 = vision1[0].Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            newSummary.Map = visio1[0];

            //newSummary.Victory = GoogleVisionController.PrintImageText(CreateCroppedImage(imagepath, newSection.Victory).imagePath).First().Replace(@"\n", "").ToUpper().Contains("VIC");
            //newSummary.Map = GoogleVisionController.PrintImageText(CreateCroppedImage(imagepath, newSection.Map).imagePath).First().Replace(@"\n", "");

            var ted = GoogleVisionController.PrintImageText(CreateCroppedImage(imagepath, newSection.Kills).imagePath).FirstOrDefault();
            newSummary.Kills = ted == null ? 0 : int.Parse(ted.Replace(@"\n", "").Replace("°", ""));
            var ted2 = GoogleVisionController.PrintImageText(CreateCroppedImage(imagepath, newSection.Deaths).imagePath).FirstOrDefault();
            newSummary.Deaths = ted2 == null ? 0 : int.Parse(ted2.Replace(@"\n", "").Replace("°", ""));
            FileController.MiniDirectoryClear();
            return newSummary;
            // run for each file


            //foreach (DirectoryInfo dir in di.GetDirectories())
            //{
            //    dir.Delete(true);
            //}
        }

        private static void CreateBoundsVisualization(ImageSection newSection, Image image)
        {
            ImageExtensions.VisualizeRectangle(image, newSection.TLeft);
            //ImageExtensions.VisualizeRectangle(image, newSection.Victory);
            //ImageExtensions.VisualizeRectangle(image, newSection.Map);
            ImageExtensions.VisualizeRectangle(image, newSection.Kills);
            ImageExtensions.VisualizeRectangle(image, newSection.Deaths);
        }

        private static ImageExtra CreateCroppedImage(string imagepath, Rectangle bounds)
        {
            // check file directory files count.
            if (Directory.GetFiles(Directory.GetCurrentDirectory() + FileLibrary.minimeDir).Length > 0)
            {

            }

            Image croppedImage = cropImage(System.Drawing.Image.FromFile(imagepath), bounds);
            croppedImage.Save(Directory.GetCurrentDirectory() + FileLibrary.minimeDir + FileLibrary.minimeFile + FileLibrary.jpg, System.Drawing.Imaging.ImageFormat.Jpeg);

            return new ImageExtra(croppedImage, Directory.GetCurrentDirectory() + FileLibrary.minimeDir + FileLibrary.minimeFile + FileLibrary.jpg);
        }
    }
}