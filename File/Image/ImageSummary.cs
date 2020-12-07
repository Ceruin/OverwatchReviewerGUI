using System.IO;

namespace OverwatchReviewerGUI.File.Image
{
    public class ImageSummary
    {
        public ImageExtra image { get; set; }
        public bool Victory { get; set; }
        public string Map { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }

        public ImageSummary(ImageExtra image)
        {
            this.image = image;
            ParseImage();
        }

        public void ParseImage()
        {
            FileController.ClearDirectory(new DirectoryInfo(FileController.GetDirectory(FileController.DirectoryType.Minime)));

            image.GetImageOutput(image.boundaries.TLeft);
            if (image.imageOutput.Count > 0)
                Victory = ImageOutputParser.ParseBool(image.imageOutput, ImageOutputParser.OutputType.Victory);

            image.GetImageOutput(image.boundaries.TLeft);
            if (image.imageOutput.Count > 0)
                Map = ImageOutputParser.ParseString(image.imageOutput, ImageOutputParser.OutputType.Map);

            image.GetImageOutput(image.boundaries.Kills);
            if (image.imageOutput.Count > 0)
                Kills = ImageOutputParser.ParseInt(image.imageOutput, ImageOutputParser.OutputType.Kills);

            image.GetImageOutput(image.boundaries.Deaths);
            if (image.imageOutput.Count > 0)
                Deaths = ImageOutputParser.ParseInt(image.imageOutput, ImageOutputParser.OutputType.Deaths);

            FileController.ClearDirectory(new DirectoryInfo(FileController.GetDirectory(FileController.DirectoryType.Minime)));
        }
    }
}