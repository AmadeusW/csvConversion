using System;
using Xunit;

namespace CsvConversion.Tests
{
    public class ColorConverterTests
    {
        [Fact]
        public void RunAll()
        {
            ColorConverter.ProcessFile(@"..\..\..\colors.csv");
        }

        [Fact]
        public void LABtoRGB()
        {

        }
    }
}
