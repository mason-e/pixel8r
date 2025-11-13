using pixel8r.Helpers;
using SkiaSharp;

namespace pixel8rtests
{
    [TestClass()]
    public class TintTests
    {
        [TestMethod()]
        public void testBrighten()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 0, 250), "White (Brighten)");
            Assert.AreEqual(new SKColor(110, 10, 255), tinted);
        }

        [TestMethod()]
        public void testDarken()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(5, 255, 100), "Black (Darken)");
            Assert.AreEqual(new SKColor(0, 245, 90), tinted);
        }

        [TestMethod()]
        public void testSoftRed()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(246, 100, 100), "Red (Soft)");
            Assert.AreEqual(new SKColor(255, 100, 100), tinted);

            // no change when red already maxed
            tinted = TintHelper.getTintColor(tinted, "Red (Soft)");
            Assert.AreEqual(new SKColor(255, 100, 100), tinted);
        }

        [TestMethod()]
        public void testHardRed()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(246, 100, 100), "Red (Hard)");
            Assert.AreEqual(new SKColor(255, 90, 90), tinted);

            // decrease others even when red already maxed
            tinted = TintHelper.getTintColor(tinted, "Red (Hard)");
            Assert.AreEqual(new SKColor(255, 80, 80), tinted);
        }

        [TestMethod()]
        public void testSoftGreen()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 246, 100), "Green (Soft)");
            Assert.AreEqual(new SKColor(100, 255, 100), tinted);

            // no change when green already maxed
            tinted = TintHelper.getTintColor(tinted, "Green (Soft)");
            Assert.AreEqual(new SKColor(100, 255, 100), tinted);
        }

        [TestMethod()]
        public void testHardGreen()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 246, 100), "Green (Hard)");
            Assert.AreEqual(new SKColor(90, 255, 90), tinted);

            // decrease others even when green already maxed
            tinted = TintHelper.getTintColor(tinted, "Green (Hard)");
            Assert.AreEqual(new SKColor(80, 255, 80), tinted);
        }

        [TestMethod()]
        public void testSoftBlue()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 100, 246), "Blue (Soft)");
            Assert.AreEqual(new SKColor(100, 100, 255), tinted);

            // no change when blue already maxed
            tinted = TintHelper.getTintColor(tinted, "Blue (Soft)");
            Assert.AreEqual(new SKColor(100, 100, 255), tinted);
        }

        [TestMethod()]
        public void testHardBlue()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 100, 246), "Blue (Hard)");
            Assert.AreEqual(new SKColor(90, 90, 255), tinted);

            // decrease others even when blue already maxed
            tinted = TintHelper.getTintColor(tinted, "Blue (Hard)");
            Assert.AreEqual(new SKColor(80, 80, 255), tinted);
        }

        [TestMethod()]
        public void testSoftCyan()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 247, 246), "Cyan (Soft)");
            Assert.AreEqual(new SKColor(100, 255, 255), tinted);

            // no change when cyan already maxed
            tinted = TintHelper.getTintColor(tinted, "Cyan (Soft)");
            Assert.AreEqual(new SKColor(100, 255, 255), tinted);
        }

        [TestMethod()]
        public void testHardCyan()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 247, 246), "Cyan (Hard)");
            Assert.AreEqual(new SKColor(90, 255, 255), tinted);

            // decrease red even when cyan already maxed
            tinted = TintHelper.getTintColor(tinted, "Cyan (Hard)");
            Assert.AreEqual(new SKColor(80, 255, 255), tinted);
        }

        [TestMethod()]
        public void testSoftMagenta()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(247, 100, 246), "Magenta (Soft)");
            Assert.AreEqual(new SKColor(255, 100, 255), tinted);

            // no change when magenta already maxed
            tinted = TintHelper.getTintColor(tinted, "Magenta (Soft)");
            Assert.AreEqual(new SKColor(255, 100, 255), tinted);
        }

        [TestMethod()]
        public void testHardMagenta()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(247, 100, 246), "Magenta (Hard)");
            Assert.AreEqual(new SKColor(255, 90, 255), tinted);

            // decrease green even when magenta already maxed
            tinted = TintHelper.getTintColor(tinted, "Magenta (Hard)");
            Assert.AreEqual(new SKColor(255, 80, 255), tinted);
        }

        [TestMethod()]
        public void testSoftYellow()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(246, 247, 100), "Yellow (Soft)");
            Assert.AreEqual(new SKColor(255, 255, 100), tinted);

            // no change when yellow already maxed
            tinted = TintHelper.getTintColor(tinted, "Yellow (Soft)");
            Assert.AreEqual(new SKColor(255, 255, 100), tinted);
        }

        [TestMethod()]
        public void testHardYellow()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(246, 247, 100), "Yellow (Hard)");
            Assert.AreEqual(new SKColor(255, 255, 90), tinted);

            // decrease blue even when yellow already maxed
            tinted = TintHelper.getTintColor(tinted, "Yellow (Hard)");
            Assert.AreEqual(new SKColor(255, 255, 80), tinted);
        }

        [TestMethod()]
        public void testGrayscaleSameSumsSameResult()
        {
            // all sum up to 300
            SKColor tinted1 = TintHelper.getTintColor(new SKColor(255, 0, 45), "Grayscale");
            SKColor tinted2 = TintHelper.getTintColor(new SKColor(100, 150, 50), "Grayscale");
            SKColor tinted3 = TintHelper.getTintColor(new SKColor(37, 73, 190), "Grayscale");
            Assert.AreEqual(tinted1, new SKColor(100, 100, 100));
            Assert.AreEqual(tinted1, tinted2);
            Assert.AreEqual(tinted2, tinted3);
        }

        [TestMethod()]
        public void testRedscale()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 100, 100), "Redscale");
            Assert.AreEqual(new SKColor(90, 5, 5), tinted);
        }

        [TestMethod()]
        public void testGreenscale()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 100, 100), "Greenscale");
            Assert.AreEqual(new SKColor(10, 80, 10), tinted);
        }

        [TestMethod()]
        public void testBluescale()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 100, 100), "Bluescale");
            Assert.AreEqual(new SKColor(15, 30, 55), tinted);
        }

        [TestMethod()]
        public void testCyanscale()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 100, 100), "Cyanscale");
            Assert.AreEqual(new SKColor(10, 45, 45), tinted);
        }

        [TestMethod()]
        public void testMagentascale()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 100, 100), "Magentascale");
            Assert.AreEqual(new SKColor(60, 10, 30), tinted);
        }

        [TestMethod()]
        public void testYellowscale()
        {
            SKColor tinted = TintHelper.getTintColor(new SKColor(100, 100, 100), "Yellowscale");
            Assert.AreEqual(new SKColor(55, 40, 5), tinted);
        }

        [TestMethod()]
        public void testInvalidSelectionSameColor()
        {
            SKColor unmodified = TintHelper.getTintColor(new SKColor(122, 93, 201), "mydropdownisbroken");
            Assert.AreEqual(new SKColor(122, 93, 201), unmodified);
        }
    }
}