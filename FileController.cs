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

        public static void DirectorySetup(out string inputDir, out string outputFilePath)
        {
            // Create Directory Structure
            string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            inputDir = currentDir + @"\owr_input";
            string outputDir = currentDir + @"\owr_output";
            string outputFileName = @"MapSummaries.txt";
            if (!Directory.Exists(inputDir))
                inputDir = Directory.CreateDirectory(inputDir).FullName;
            if (!Directory.Exists(outputDir))
                outputDir = Directory.CreateDirectory(outputDir).FullName;

            outputFilePath = Path.Combine(outputDir, outputFileName);
        }

        public static void CreateSummaryFile(string filePath, StringBuilder fileText)
        {
            FileState fileState = FileState.Discard;
            string input = string.Empty;
            if (File.Exists(filePath))
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
                File.WriteAllText(filePath, string.Join(Environment.NewLine, fileText));
                Console.WriteLine("File Created/Updated...");
            }
            else
            {
                Console.WriteLine("No File Created/Updated...");
            }
        }

        public static void CreateSummaryFileText(StringBuilder fileText, List<OcrSummary> summary)
        {
            foreach (var sum in summary)
            {
                fileText.AppendLine(sum.Map);
                List<bool> victories = new List<bool>();
                List<bool> defeats = new List<bool>();
                victories = sum.Victory.FindAll(p => p == true);
                defeats = sum.Victory.FindAll(p => p == false);
                fileText.AppendLine("🎊 Victories: " + victories.Count.ToString());
                fileText.AppendLine("🏳 Defeats: " + defeats.Count.ToString());
                if (sum.Kills.Count > 0)
                    fileText.AppendLine("⚔️ Average Kills: " + sum.Kills.Average().ToString());
                if (sum.Deaths.Count > 0)
                    fileText.AppendLine("⚰️ Average Deaths: " + sum.Deaths.Average().ToString());
                fileText.AppendLine();
            }
        }
    }
}