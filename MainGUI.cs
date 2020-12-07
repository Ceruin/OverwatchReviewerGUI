using OverwatchReviewerGUI.Classes;
using OverwatchReviewerGUI.File;
using OverwatchReviewerGUI.File.Image;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OverwatchReviewerGUI
{
    public partial class MainGUI : Form
    {
        private static CurrentDirectoryInfo directoryInfo = null;

        public MainGUI()
        {
            InitializeComponent();
        }

        public void RunApp()
        {
            directoryInfo = FileController.DirectorySetup();
            ProcessImages();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            RunApp();
        }

        private void ProcessImages()
        {
            List<ImageSummary> results = new List<ImageSummary>();
            int counter = 0;
            foreach (string imagepath in Directory.GetFiles(directoryInfo.CurrentInputDirectory))
            {
                results.Add(CreateCropMiniMes(imagepath));
                ImageExtensions.CreateBoundsVisualization(results[counter].image.boundaries, results[counter].image.baseImage);
                if (pbLoad != null)
                    pbLoad.Image = results[counter].image.baseImage;
            }

            FileController.CreateSummaryFile(
                directoryInfo.CurrentOutputFilePath,
                FileController.CreateSummaryFileText(MapSummaryController.GetMapSummaries(results)));
        }

        private static ImageSummary CreateCropMiniMes(string imagepath)
        {
            ImageExtra image = new ImageExtra(imagepath);
            image.currentDirectoryInfo = directoryInfo;
            image.boundaries = new ImageSection(image.imagePath);

            return new ImageSummary(image); ;
        }
    }
}