using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvConversion
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please provide the input file path.");
            }
            var inputPath = args[0];
            var outputPath = ColorConverter.ProcessFile(inputPath);
            Console.WriteLine($"Converted file saved in ${outputPath}");
        }
    }
}
