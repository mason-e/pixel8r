using pixel8r.Helpers;
using SkiaSharp;

namespace pixel8rtests
{
    // test class for executing more computation-heavy functions to get an idea of performance in case of future refactors
    [TestClass()]
    public class PerformanceTests
    {
        [TestMethod()]
        [DoNotParallelize]
        [Ignore("Manually enable when performance testing is desired")]
        [DataRow("RGB Euclidean")]
        [DataRow("RGB Redmean")]
        [DataRow("Lab CIE76")]
        [DataRow("Lab Hybrid")]
        [DataRow("Lab CIE94")]
        [DataRow("LCh CIEDE2000")]
        [DataRow("CMC Acceptability")]
        [DataRow("CMC Perceptibility")]
        [DataRow("ITP")]
        [DataRow("Z")]
        [DataRow("OK")]
        [DataRow("CAM02")]
        [DataRow("CAM16")]
        public void testColorMatchingPerformance(string algorithm)
        {
            // configure an example palette
            GlobalVars.CurrentPalette = new List<SKColor>{
                SKColors.Red,
                SKColors.Green,
                SKColors.Blue,
                SKColors.Yellow,
                SKColors.Cyan,
                SKColors.Magenta,
                SKColors.Black,
                SKColors.White,
                SKColors.Gray,
                SKColors.Orange,
                SKColors.Purple,
                SKColors.Brown,
                SKColors.Pink,
                SKColors.Lime,
                SKColors.Navy,
                SKColors.Teal,
                SKColors.Olive,
                SKColors.Maroon,
                SKColors.Silver
            };

            for (int i = 0; i < 50000; i++)
            {
                Random random = new Random();
                int r = random.Next(255);
                int g = random.Next(255);
                int b = random.Next(255);
                PaletteMatchingHelper.getMatchedColor(new SKColor((byte)r, (byte)g, (byte)b),  algorithm);
            }
        }

        [TestMethod()]
        [DoNotParallelize]
        [Ignore("Manually enable when performance testing is desired")]
        [DataRow("Lab CIE76")]
        [DataRow("Lab Hybrid")]
        [DataRow("Lab CIE94")]
        [DataRow("LCh CIEDE2000")]
        [DataRow("CMC Acceptability")]
        [DataRow("CMC Perceptibility")]
        [DataRow("ITP")]
        [DataRow("Z")]
        [DataRow("OK")]
        [DataRow("CAM02")]
        [DataRow("CAM16")]
        public void testColorMatchingFastPerformance(string algorithm)
        {
            // configure an example palette
            GlobalVars.CurrentPalette = new List<SKColor>{
                SKColors.Red,
                SKColors.Green,
                SKColors.Blue,
                SKColors.Yellow,
                SKColors.Cyan,
                SKColors.Magenta,
                SKColors.Black,
                SKColors.White,
                SKColors.Gray,
                SKColors.Orange,
                SKColors.Purple,
                SKColors.Brown,
                SKColors.Pink,
                SKColors.Lime,
                SKColors.Navy,
                SKColors.Teal,
                SKColors.Olive,
                SKColors.Maroon,
                SKColors.Silver
            };

            for (int i = 0; i < 50000; i++)
            {
                Random random = new Random();
                int r = random.Next(255);
                int g = random.Next(255);
                int b = random.Next(255);
                // roughly equivalent to the fast mode option, which does
                // this reduction in the bitmap helper
                SKColor color = ReduceFidelityHelper.getReducedColor(new SKColor((byte)r, (byte)g, (byte)b), "18 Bit RGB");
                PaletteMatchingHelper.getMatchedColor(color,  algorithm);
            }
        }
    }
}
