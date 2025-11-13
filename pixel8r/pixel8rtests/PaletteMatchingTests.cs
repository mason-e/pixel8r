using pixel8r.Helpers;
using SkiaSharp;

namespace pixel8rtests
{
    [TestClass()]
    public class PaletteMatchingTests
    {
        [TestMethod()]
        [DoNotParallelize]
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
        public void testColorMatchedToNESPalette(string algorithm)
        {
            // start with a random color for a bit of variation in the test
            Random random = new Random();
            int r = random.Next(255);
            int g = random.Next(255);
            int b = random.Next(255);
            SKColor color = new SKColor((byte)r, (byte)g, (byte)b);

            // only asserting that a match is done, not what the color "should" be
            // if we knew what they should be, there wouldn't be so many color matching algorithms!
            color = PaletteMatchingHelper.getMatchedColor(color, "NES", algorithm);
            CollectionAssert.Contains(Constants.mesenColors, color);
        }
    }
}