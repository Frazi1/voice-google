using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentifierExtractor
{
    class Program
    {
        static void Main(string[] args)
        {

            FileReader r = new FileReader();
            string searchPattern;
            string path;
            if (args.Length < 2)
            {
                path = @"D:\Programmin\angular\aio\src\app";
                searchPattern = "*.ts";
            }
            else
            {
                path = args[0];
                searchPattern = args[1];
            }

            //var words = r.GetWordsFromFile(@"D:\Programmin\angular\aio\src\app\custom-elements\announcement-bar\announcement-bar.component.ts");
            //string[] files = Directory.GetFiles(@"D:\Programmin\angular\aio\src\app\custom-elements\announcement-bar\", "*.ts");
            var words = new List<string>();
            
            foreach (var file in Directory.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories))
            {
                words.AddRange(r.GetWordsFromFile(file));
            }
            words = words.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            string outputPath = new DirectoryInfo(path).Name + $"{searchPattern.Replace("*", "")}" + ".id.txt";
            File.WriteAllLines(outputPath, words);
            Console.WriteLine($"Found {words.Count} identifiers");
        }
    }
}
