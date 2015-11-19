using System;
using System.IO;
using System.Text;

namespace CsvConversion
{
    public static class ColorConverter
    {
        public static string ProcessFile(string inputPath)
        {

            var rawData = File.ReadAllLines(inputPath);
            var sb = new StringBuilder();

            for (int index = 1; index < rawData.Length; index++)
            {
                var line = rawData[index];
                var processedLine = processLine(line);
                sb.Append(line);
            }

            var inputDirectory = Path.GetDirectoryName(inputPath);
            var inputFile = Path.GetFileNameWithoutExtension(inputPath);
            var outputFile = inputFile + "Converted";
            var inputExtension = Path.GetExtension(inputPath);
            var outputPath = inputDirectory + outputFile + inputExtension;

            var outputData = sb.ToString();
            File.WriteAllText(outputPath, outputData);
            return outputPath;
        }

        private static string processLine(string line)
        {
            var lineParts = line.Split(',');
            var L = Double.Parse(lineParts[1]);
            var a = Double.Parse(lineParts[2]);
            var b = Double.Parse(lineParts[3]);
            var xyz = LabToXyz(L, a, b);
            var rgb = XyzToRgb(xyz);
            return $"{lineParts[0]},{rgb.Item1},{rgb.Item2},{rgb.Item3},{lineParts[4]}";
        }

        public static Tuple<double, double, double> LabToXyz(double L, double a, double b)
        {
            var y = (L + 16) / 116;
            var x = a / 500 + y;
            var z = y - b / 200;

            if (Math.Pow(y, 3) > 0.008856)
                y = Math.Pow(y, 3);
            else
                y = (y - 16 / 116) / 7.787;

            if (Math.Pow(x, 3) > 0.008856)
                x = Math.Pow(x, 3);
            else
                x = (x - 16 / 116) / 7.787;
            if (Math.Pow(z, 3) > 0.008856)
                z = Math.Pow(z, 3);
            else
                z = (z - 16 / 116) / 7.787;

            // Multiply by reference values for Observer= 2°, Illuminant= D65
            x *= 95.047;
            y *= 100.000;
            z *= 108.883;
            return new Tuple<double, double, double>(x, y, z);
        }

        public static Tuple<double, double, double> XyzToRgb(Tuple<double, double, double> xyz)
        {
            var x = xyz.Item1 / 100;
            var y = xyz.Item2 / 100;
            var z = xyz.Item3 / 100;

            var r = x * 3.2406 + y * -1.5372 + z * -0.4986;
            var g = x * -0.9689 + y * 1.8758 + z * 0.0415;
            var b = x * 0.0557 + y * -0.2040 + z * 1.0570;

            if (r > 0.0031308)
                r = 1.055 * (Math.Pow(r, (1 / 2.4))) - 0.055;
            else
                r = 12.92 * r;
            if (g > 0.0031308)
                g = 1.055 * (Math.Pow(g, (1 / 2.4))) - 0.055;
            else
                g = 12.92 * g;
            if (b > 0.0031308)
                b = 1.055 * (Math.Pow(b, (1 / 2.4))) - 0.055;
            else
                b = 12.92 * b;


            r *= 255;
            g *= 255;
            b *= 255;
            return new Tuple<double, double, double>(r, g, b);
        }
    }
}