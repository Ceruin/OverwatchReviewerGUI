using System;
using System.Collections.Generic;
using System.IO;
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
            string inputDir, outputFilePath;
            FileController.DirectorySetup(out inputDir, out outputFilePath);

            Console.WriteLine("Reading Images...");
            if (Directory.Exists(inputDir))
            {
                ProcessImages(inputDir, outputFilePath);
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

        private void ProcessImages(string inputDir, string outputFilePath)
        {
            string[] imagepaths = Directory.GetFiles(inputDir);
            List<OcrSection> results = new List<OcrSection>();
            OcrController.GetOcrSectionsFromImages(imagepaths, results);

            foreach (string imagepath in imagepaths)
            {
                GoogleVisionController.PrintImageText(imagepath);
            }

            List<OcrSummary> summary = new List<OcrSummary>();
            OcrController.GetOcrSummaryResults(results, summary);

            StringBuilder fileText = new StringBuilder();
            FileController.CreateSummaryFileText(fileText, summary);
            FileController.CreateSummaryFile(outputFilePath, fileText);
        }
    }
}