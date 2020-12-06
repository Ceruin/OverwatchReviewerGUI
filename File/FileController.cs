using OverwatchReviewerGUI.Classes;
using OverwatchReviewerGUI.File.Image;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OverwatchReviewerGUI
{
    public static class FileController
    {
        private enum FileState
        {
            Create,
            Update,
            Discard
        }

        public static void DirectorySetup(out string inputDir, out string outputFilePath, out string minimeDir)
        {
            // Create Directory Structure
            string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            inputDir = currentDir + FileLibrary.inputDir;
            string outputDir = currentDir + FileLibrary.outputDir;
            string outputFileName = FileLibrary.outputFile;
            minimeDir = currentDir + FileLibrary.minimeDir;
            if (!Directory.Exists(inputDir))
                inputDir = Directory.CreateDirectory(inputDir).FullName;
            if (!Directory.Exists(outputDir))
                outputDir = Directory.CreateDirectory(outputDir).FullName;
            if (!Directory.Exists(minimeDir))
                minimeDir = Directory.CreateDirectory(minimeDir).FullName;

            outputFilePath = Path.Combine(outputDir, outputFileName);
        }

        public static void MiniDirectoryClear()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory() + FileLibrary.minimeDir);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        public static void CreateSummaryFile(string filePath, StringBuilder fileText)
        {
            FileState fileState = FileState.Discard;
            string input = string.Empty;
            if (System.IO.File.Exists(filePath))
            {
                Console.WriteLine("Overwrite File? [Y/N]: " + filePath);
                input = Console.ReadLine();
                input = "Y";
            }
            else
            {
                fileState = FileState.Create;
            }

            if (input.ToUpper().Contains("Y"))
                fileState = FileState.Update;

            if (fileState == FileState.Create || fileState == FileState.Update)
            {
                System.IO.File.WriteAllText(filePath, string.Join(Environment.NewLine, fileText));
                Console.WriteLine("File Created/Updated...");
            }
            else
            {
                Console.WriteLine("No File Created/Updated...");
            }
        }

        public static void CreateSummaryFileText(StringBuilder fileText, ICollection<MapSummary> summary)
        {
            foreach (var sum in summary)
            {
                fileText.AppendLine(sum.Map);
                fileText.AppendLine("🎊 Victories: " + sum.Victories.ToString());
                fileText.AppendLine("🏳 Defeats: " + sum.Defeats.ToString());
                if (sum.Kills.Count() >= 0)
                    fileText.AppendLine("⚔️ Average Kills: " + MathF.Round((float)sum.Kills.Average()).ToString());
                if (sum.Deaths.Count() >= 0)
                    fileText.AppendLine("⚰️ Average Deaths: " + MathF.Round((float)sum.Deaths.Average()).ToString()); // round up?
                fileText.AppendLine();
            }
        }
    }
}