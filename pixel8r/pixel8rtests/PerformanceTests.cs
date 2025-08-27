using System.Drawing;
using pixel8r;

namespace pixel8rtests
{
    // test class for executing more computation-heavy functions to get an idea of performance in case of future refactors
    [TestClass()]
    public class PerformanceTests
    {
        [TestMethod()]
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
            for (int i = 0; i < 50000; i++)
            {
                Random random = new Random();
                int r = random.Next(255);
                int g = random.Next(255);
                int b = random.Next(255);
                PaletteMatchingFunctions.getMatchedColor(Color.FromArgb(r, g, b), "NES", algorithm, false);
            }
        }
    }
}
