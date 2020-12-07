using OverwatchReviewerGUI.Classes;
using OverwatchReviewerGUI.File;
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

        public static CurrentDirectoryInfo DirectorySetup()
        {
            return new CurrentDirectoryInfo();
        }

        public enum DirectoryType
        {
            Input,
            Minime,
            Output
        }

        public static string GetDirectory(DirectoryType directoryType)
        {
            switch (directoryType)
            {
                case DirectoryType.Input:
                    return Directory.GetCurrentDirectory() + FileLibrary.inputDir;

                case DirectoryType.Minime:
                    return Directory.GetCurrentDirectory() + FileLibrary.minimeDir;

                case DirectoryType.Output:
                    return Directory.GetCurrentDirectory() + FileLibrary.outputDir;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void ClearDirectory(DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
        }

        public static void CreateSummaryFile(string filePath, string fileText)
        {
            FileState fileState = FileState.Discard;
            if (System.IO.File.Exists(filePath))
            {
                fileState = FileState.Update;
            }
            else
            {
                fileState = FileState.Create;
            }

            if (fileState == FileState.Create || fileState == FileState.Update)
            {
                System.IO.File.WriteAllText(filePath, fileText);
            }
        }

        public static string CreateSummaryFileText(ICollection<MapSummary> summary)
        {
            StringBuilder fileText = new StringBuilder();
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
            return string.Join(Environment.NewLine, fileText);
        }
    }
}