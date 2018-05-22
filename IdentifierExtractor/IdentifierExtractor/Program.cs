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
            //var words = r.GetWordsFromFile(@"D:\Programmin\angular\aio\src\app\custom-elements\announcement-bar\announcement-bar.component.ts");
            string[] files = Directory.GetFiles(@"D:\Work\efcOnline\ngOnline\ngOnlineServer\Controllers", "*.cs");
            var words = new List<string>();
            foreach (var file in files)
            {
                words.AddRange(r.GetWordsFromFile(file));
            }
            File.WriteAllLines("id.txt", words.Distinct(StringComparer.OrdinalIgnoreCase));
        }
    }
}
