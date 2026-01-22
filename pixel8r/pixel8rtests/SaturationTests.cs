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
            SKColor saturated = SaturationHelper.getSaturatedColor(new SKColor(191, 64, 64), 5);
            Assert.AreEqual(new SKColor(197, 58, 58), saturated);
        }

        [TestMethod()]
        public void testSingleDesaturation()
        {
            SKColor saturated = SaturationHelper.getSaturatedColor(new SKColor(191, 64, 64), -5);
            Assert.AreEqual(new SKColor(185, 70, 70), saturated);
        }

        [TestMethod()]
        public void testSaturateTo100MultipleSteps()
        {
            // this color has 50% saturation, so this puts it at 99%
            SKColor saturated = SaturationHelper.getSaturatedColor(new SKColor(191, 64, 64), 49);
            Assert.AreEqual(new SKColor(253, 2, 2), saturated);

            // limit to 100% even if saturation would go over
            saturated = SaturationHelper.getSaturatedColor(saturated, 15);
            Assert.AreEqual(new SKColor(255, 0, 0), saturated);
        }

        [TestMethod()]
        public void testDesaturateTo100MultipleSteps()
        {
            // this color has 50% saturation, so this puts it at 1%
            SKColor desaturated = SaturationHelper.getSaturatedColor(new SKColor(191, 64, 64), -49);
            Assert.AreEqual(new SKColor(129, 126, 126), desaturated);

            // limit to 0% even if desaturation would go under
            desaturated = SaturationHelper.getSaturatedColor(desaturated, -15);
            Assert.AreEqual(new SKColor(128, 128, 128), desaturated);
        }

        [TestMethod()]
        public void testFullySaturatedNoChange()
        {
            SKColor saturated = SaturationHelper.getSaturatedColor(new SKColor(255, 0, 0), 1);
            Assert.AreEqual(new SKColor(255, 0, 0), saturated);
        }

        [TestMethod()]
        public void testFullyDesaturatedNoChange()
        {
            SKColor saturated = SaturationHelper.getSaturatedColor(new SKColor(128, 128, 128), -1);
            Assert.AreEqual(new SKColor(128, 128, 128), saturated);
        }
    }
}