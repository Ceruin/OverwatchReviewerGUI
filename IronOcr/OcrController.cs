using IronOcr;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace OverwatchReviewerGUI
{
    public static class OcrController
    {
        private static IronTesseract Ocr = new IronTesseract()
        {
            Configuration = new TesseractConfiguration()
            {
                PageSegmentationMode = TesseractPageSegmentationMode.SingleWord,
                TesseractVersion = TesseractVersion.Tesseract5,
                EngineMode = TesseractEngineMode.LstmOnly,
                RenderSearchablePdfsAndHocr = false
                //EngineMode = TesseractEngineMode.TesseractAndLstm,
                //PageSegmentationMode = TesseractPageSegmentationMode.SingleWord
            }
        };

        public static OcrResult ProcessSection(string image, Rectangle sBounds, bool isItalic = false)
        {
            using (OcrInput Input = new OcrInput(image, sBounds))
            {
                //Input.Deskew();
                //Input.EnhanceResolution(300);
                //Input.ToGrayScale();
                //Input.Binarize();

                OcrResult Result = Ocr.Read(Input);
                Console.WriteLine(Result.Text);
                return Result;
            }
        }

        public static void GetOcrSummaryResults(List<OcrSection> results, List<OcrSummary> summary)
        {
            foreach (var res in results)
            {
                // Get the Maps and Combine the Map Data
                var test3 = summary.FirstOrDefault(p => p.Map == res.Map.Text);
                if (test3 == null)
                {
                    OcrSummary ocrSummary = new OcrSummary();
                    ocrSummary.Map = res.Map.Text;
                    if (res.Victory.Text.ToUpper().Contains("VICTORY"))
                        ocrSummary.Victory.Add(true);
                    if (res.Victory.Text.ToUpper().Contains("DEFEAT"))
                        ocrSummary.Victory.Add(false);
                    int tKills;
                    int tDeaths;
                    if (Int32.TryParse(res.Kills.Text, out tKills))
                    {
                        ocrSummary.Kills.Add(tKills);
                    }
                    if (Int32.TryParse(res.Deaths.Text, out tDeaths))
                    {
                        ocrSummary.Deaths.Add(tDeaths);
                    }
                    summary.Add(ocrSummary);
                }
                else
                {
                    test3.Victory.Add(res.Victory.Text.ToUpper().Contains("VICTORY"));
                    int tKills;
                    int tDeaths;
                    if (Int32.TryParse(res.Kills.Text, out tKills))
                    {
                        test3.Kills.Add(tKills);
                    }
                    if (Int32.TryParse(res.Deaths.Text, out tDeaths))
                    {
                        test3.Deaths.Add(tDeaths);
                    }
                }
            }
        }

        public static void GetOcrSectionsFromImages(string[] imagepaths, List<OcrSection> results)
        {
            foreach (var imagepath in imagepaths)
            {
                try
                {
                    if (System.IO.File.Exists(imagepath))
                    {
                        Rectangle sMap, sVictory, sKills, sDeaths;
                        //ImageExtensions.GetImageSections(imagepath, out sMap, out sVictory, out sKills, out sDeaths);

                        OcrSection ocrSection = new OcrSection();
                        //ocrSection.Map = ProcessSection(imagepath, sMap);
                        //ocrSection.Victory = ProcessSection(imagepath, sVictory);
                        //ocrSection.Kills = ProcessSection(imagepath, sKills, true);
                        //ocrSection.Deaths = ProcessSection(imagepath, sDeaths, true);
                        results.Add(ocrSection);
                    }
                }
                catch
                {
                    Console.WriteLine("Idk Error");
                }
                Console.WriteLine();
            }
        }
    }
}