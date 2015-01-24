using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIFrameworksPerformanceTester.Log
{
    /// <summary>
    /// Class that will be used for logging data results in textual files for now.
    /// </summary>
    public class Data
    {
        static List<string> categories = new List<string>
        {
            "NotSingleton",
            "Singleton   ",
            "Combined    ",
            "Generic     ",
            "Collection  ",
			//"Conditional ",
            //"Dynamic     "
        };
        static string path = AppDomain.CurrentDomain.BaseDirectory;
        public static void LogToAllResults(List<string> results)
        {
            if (results == null || results.Count == 0)
                return;
            var allResultsFile = path + "\\AllData.txt";
            if(!File.Exists(allResultsFile))            
                File.Create(allResultsFile);
            using (var stream = new StreamWriter(allResultsFile))
            {
                stream.WriteLine("StructureMap\t\tNinject\t\t SimpleInjector");
                stream.WriteLine(string.Join(" ", results));
            }
        }
        public static void LogToCategoryResults(List<List<string>> results)
        {
            if (results == null || results.Count == 0)
                return;
            var categoryResultsFile = path + "\\CategoryData.txt";
            if (!File.Exists(categoryResultsFile))
                File.Create(categoryResultsFile);
            using (var stream = new StreamWriter(categoryResultsFile))
            {
                stream.WriteLine("\t\t\t\tStructureMap\t Ninject\t SimpleInjector");
                for (int i = 0; i < results.Count || i < categories.Count; i++)
                {
                    stream.Write(categories[i] + " ");
                    stream.WriteLine(string.Join(" ", results[i]));
                }
            }
        }
    }
}
