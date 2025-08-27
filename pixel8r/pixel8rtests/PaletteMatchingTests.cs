using System.Drawing;
using pixel8r;

namespace pixel8rtests
{
    [TestClass()]
    public class PaletteMatchingTests
    {
        [TestMethod()]
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
            Color color = Color.FromArgb(r, g, b);

            // only asserting that a match is done, not what the color "should" be
            // if we knew what they should be, there wouldn't be so many color matching algorithms!
            color = PaletteMatchingFunctions.getMatchedColor(color, "NES", algorithm, false);
            CollectionAssert.Contains(GlobalVars.mesenColors, color);
        }

        [TestMethod()]
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
        public void testColorMatchedToWebPalette(string algorithm)
        {
            // start with a random color for a bit of variation in the test
            Random random = new Random();
            int r = random.Next(255);
            int g = random.Next(255);
            int b = random.Next(255);
            Color color = Color.FromArgb(r, g, b);

            // only asserting that a match is done, not what the color "should" be
            // if we knew what they should be, there wouldn't be so many color matching algorithms!
            color = PaletteMatchingFunctions.getMatchedColor(color, "Web Colors", algorithm, false);
            CollectionAssert.Contains(GlobalVars.webColors, color);
        }
    }
}