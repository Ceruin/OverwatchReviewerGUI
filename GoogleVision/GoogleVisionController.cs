using Google.Cloud.Storage.V1;
using Google.Cloud.Vision.V1;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace OverwatchReviewerGUI
{
    public class Results
    {
        public int eliminations { get; set; }
        public int objectiveKills { get; set; }
        public DateTime? objectiveTime { get; set; }
        public int damageDone { get; set; }
        public int healingDone { get; set; }
        public int deaths { get; set; }
    }

    public static class GoogleVisionController
    {
        public static (bool, int) CastInt(string value)
        {
            int castValue;          
            return (int.TryParse(value.Replace(",", ""), out castValue), castValue);
        }

        public static (bool, DateTime?) CastDateTime(string value)
        {
            DateTime castValue;
            DateTime? castRealValue = null;
            var castResults = (DateTime.TryParse(value, out castValue), castValue);
            if (castResults.Item1) castRealValue = castValue;
            return (castResults.Item1, castRealValue);
        }

        public static ICollection<string> PrintImageText(string imagepath)
        {
            Google.Cloud.Vision.V1.Image image = Google.Cloud.Vision.V1.Image.FromFile(imagepath);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<EntityAnnotation> response = client.DetectText(image);
            List<string> printResults = new List<string>();
            foreach (EntityAnnotation annotation in response)
            {
                if (annotation.Description != null)
                {
                    printResults.Add(annotation.Description);
                }
            }
            return printResults;

            //List<string> maps = new List<string>() { "NUMBANI", "DORADO" };
            //List<string> gameconditions = new List<string>() { "VICTORY", "DEFEAT" };
            //List<string> heroes = new List<string>() { "TORB", "HEROES" };


            //var newt = test[0].Split('\n', StringSplitOptions.None).ToList();

            //// Process to get the elim values
            //int indexOfHero = -1;
            //foreach (string hero in heroes)
            //{
            //    indexOfHero = newt.FindIndex(p => p.ToUpper().Contains(hero));
            //}

            //int[] numVals = new int[6]; // change to class that matches values so i can get the time
            //// int int string int int int?
            //if (indexOfHero >= 0)
            //{
            //    Results gamer = new Results();

            //    int totalVals = 6;
            //    for (int i = 6; i > 0; i--)
            //    {
            //        int newVal = -1;
            //        indexOfHero++;
            //        bool result = Int32.TryParse(newt[indexOfHero].Replace(",", ""), out newVal);
            //        if (!result) newVal = 0;

            //    }

            //    var elims = CastInt(newt[indexOfHero + 1]);
            //    gamer.eliminations = elims.Item1 ? elims.Item2 : -1;
            //    var objKills = CastInt(newt[indexOfHero + 2]);
            //    gamer.objectiveKills = objKills.Item1 ? objKills.Item2 : -1;
            //    var objTme = CastDateTime(newt[indexOfHero + 3]);
            //    gamer.objectiveTime = objTme.Item1 ? objTme.Item2 : null;
            //    var damage = CastInt(newt[indexOfHero + 4]);
            //    gamer.damageDone = damage.Item1 ? damage.Item2 : -1;
            //    var healing = CastInt(newt[indexOfHero + 5]);
            //    gamer.healingDone = healing.Item1 ? healing.Item2 : -1;
            //    var deaths = CastInt(newt[indexOfHero + 6]);
            //    gamer.deaths = deaths.Item1 ? deaths.Item2 : -1;

            //    for (int i = 0; i < 6; i++)
            //    {
            //        int newVal = -1;
            //        indexOfHero++;
            //        bool result = Int32.TryParse(newt[indexOfHero].Replace(",", ""), out newVal);
            //        if (!result) newVal = 0;
            //        numVals[i] = newVal;
            //    }
            //}

            // or we find the locations and make sure they are yes


            // list of victory/defeat
            // list of maps
            // list of heroes
            // take the next 6 values after hero
        }
    }
}