using pixel8r.Helpers;
using SkiaSharp;

namespace pixel8rtests
{
    [TestClass()]
    public class SaturationTests
    {
        [TestMethod()]
        public void testSingleSaturation()
        {
            SKColor saturated = SaturationHelper.getSaturatedColor(new SKColor(191, 64, 64), "Saturate");
            Assert.AreEqual(new SKColor(197, 58, 58), saturated);
        }

        [TestMethod()]
        public void testFullySaturatedNoChange()
        {
            SKColor saturated = SaturationHelper.getSaturatedColor(new SKColor(255, 0, 0), "Saturate");
            Assert.AreEqual(new SKColor(255, 0, 0), saturated);
        }

        [TestMethod()]
        public void testInvalidSelectionSameColor()
        {
            SKColor unmodified = SaturationHelper.getSaturatedColor(new SKColor(44, 87, 105), "mydropdownisbroken");
            Assert.AreEqual(new SKColor(44, 87, 105), unmodified);
        }
    }
}