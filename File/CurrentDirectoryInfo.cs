using System.IO;
using static OverwatchReviewerGUI.FileController;

namespace OverwatchReviewerGUI.File
{
    public class CurrentDirectoryInfo
    {
        public string CurrentInputDirectory { get; private set; }
        public string CurrentMinimeDirectory { get; private set; }
        public string CurrentOutputDirectory { get; private set; }
        public string CurrentMinimeFilePath { get; set; }
        public string CurrentOutputFilePath { get; set; }

        public CurrentDirectoryInfo()
        {
            string inputDir = FileController.GetDirectory(DirectoryType.Input);
            string minimeDir = FileController.GetDirectory(DirectoryType.Minime);
            string outputDir = FileController.GetDirectory(DirectoryType.Output);

            if (!Directory.Exists(inputDir))
                this.CurrentInputDirectory = Directory.CreateDirectory(inputDir).FullName;
            else
                this.CurrentInputDirectory = inputDir;

            if (!Directory.Exists(minimeDir))
                this.CurrentMinimeDirectory = Directory.CreateDirectory(minimeDir).FullName;
            else
                this.CurrentMinimeDirectory = minimeDir;

            if (!Directory.Exists(outputDir))
                this.CurrentOutputDirectory = Directory.CreateDirectory(outputDir).FullName;
            else
                this.CurrentOutputDirectory = outputDir;

            this.CurrentMinimeFilePath = CurrentMinimeDirectory + FileLibrary.minimeFile + FileLibrary.jpg;
            this.CurrentOutputFilePath = Path.Combine(CurrentOutputDirectory, FileLibrary.outputFile);
        }
    }
}