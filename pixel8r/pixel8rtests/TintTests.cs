using pixel8r;
using pixel8r.Helpers;
using System.Drawing;

namespace pixel8rtests
{
    [TestClass()]
    public class TintTests
    {
        [TestMethod()]
        public void testBrighten()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 0, 250), "White (Brighten)");
            Assert.AreEqual(Color.FromArgb(110, 10, 255), tinted);
        }

        [TestMethod()]
        public void testDarken()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(5, 255, 100), "Black (Darken)");
            Assert.AreEqual(Color.FromArgb(0, 245, 90), tinted);
        }

        [TestMethod()]
        public void testSoftRed()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(246, 100, 100), "Red (Soft)");
            Assert.AreEqual(Color.FromArgb(255, 100, 100), tinted);

            // no change when red already maxed
            tinted = TintHelper.getTintColor(tinted, "Red (Soft)");
            Assert.AreEqual(Color.FromArgb(255, 100, 100), tinted);
        }

        [TestMethod()]
        public void testHardRed()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(246, 100, 100), "Red (Hard)");
            Assert.AreEqual(Color.FromArgb(255, 90, 90), tinted);

            // decrease others even when red already maxed
            tinted = TintHelper.getTintColor(tinted, "Red (Hard)");
            Assert.AreEqual(Color.FromArgb(255, 80, 80), tinted);
        }

        [TestMethod()]
        public void testSoftGreen()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 246, 100), "Green (Soft)");
            Assert.AreEqual(Color.FromArgb(100, 255, 100), tinted);

            // no change when green already maxed
            tinted = TintHelper.getTintColor(tinted, "Green (Soft)");
            Assert.AreEqual(Color.FromArgb(100, 255, 100), tinted);
        }

        [TestMethod()]
        public void testHardGreen()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 246, 100), "Green (Hard)");
            Assert.AreEqual(Color.FromArgb(90, 255, 90), tinted);

            // decrease others even when green already maxed
            tinted = TintHelper.getTintColor(tinted, "Green (Hard)");
            Assert.AreEqual(Color.FromArgb(80, 255, 80), tinted);
        }

        [TestMethod()]
        public void testSoftBlue()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 100, 246), "Blue (Soft)");
            Assert.AreEqual(Color.FromArgb(100, 100, 255), tinted);

            // no change when blue already maxed
            tinted = TintHelper.getTintColor(tinted, "Blue (Soft)");
            Assert.AreEqual(Color.FromArgb(100, 100, 255), tinted);
        }

        [TestMethod()]
        public void testHardBlue()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 100, 246), "Blue (Hard)");
            Assert.AreEqual(Color.FromArgb(90, 90, 255), tinted);

            // decrease others even when blue already maxed
            tinted = TintHelper.getTintColor(tinted, "Blue (Hard)");
            Assert.AreEqual(Color.FromArgb(80, 80, 255), tinted);
        }

        [TestMethod()]
        public void testSoftCyan()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 247, 246), "Cyan (Soft)");
            Assert.AreEqual(Color.FromArgb(100, 255, 255), tinted);

            // no change when cyan already maxed
            tinted = TintHelper.getTintColor(tinted, "Cyan (Soft)");
            Assert.AreEqual(Color.FromArgb(100, 255, 255), tinted);
        }

        [TestMethod()]
        public void testHardCyan()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 247, 246), "Cyan (Hard)");
            Assert.AreEqual(Color.FromArgb(90, 255, 255), tinted);

            // decrease red even when cyan already maxed
            tinted = TintHelper.getTintColor(tinted, "Cyan (Hard)");
            Assert.AreEqual(Color.FromArgb(80, 255, 255), tinted);
        }

        [TestMethod()]
        public void testSoftMagenta()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(247, 100, 246), "Magenta (Soft)");
            Assert.AreEqual(Color.FromArgb(255, 100, 255), tinted);

            // no change when magenta already maxed
            tinted = TintHelper.getTintColor(tinted, "Magenta (Soft)");
            Assert.AreEqual(Color.FromArgb(255, 100, 255), tinted);
        }

        [TestMethod()]
        public void testHardMagenta()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(247, 100, 246), "Magenta (Hard)");
            Assert.AreEqual(Color.FromArgb(255, 90, 255), tinted);

            // decrease green even when magenta already maxed
            tinted = TintHelper.getTintColor(tinted, "Magenta (Hard)");
            Assert.AreEqual(Color.FromArgb(255, 80, 255), tinted);
        }

        [TestMethod()]
        public void testSoftYellow()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(246, 247, 100), "Yellow (Soft)");
            Assert.AreEqual(Color.FromArgb(255, 255, 100), tinted);

            // no change when yellow already maxed
            tinted = TintHelper.getTintColor(tinted, "Yellow (Soft)");
            Assert.AreEqual(Color.FromArgb(255, 255, 100), tinted);
        }

        [TestMethod()]
        public void testHardYellow()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(246, 247, 100), "Yellow (Hard)");
            Assert.AreEqual(Color.FromArgb(255, 255, 90), tinted);

            // decrease blue even when yellow already maxed
            tinted = TintHelper.getTintColor(tinted, "Yellow (Hard)");
            Assert.AreEqual(Color.FromArgb(255, 255, 80), tinted);
        }

        [TestMethod()]
        public void testGrayscaleSameSumsSameResult()
        {
            // all sum up to 300
            Color tinted1 = TintHelper.getTintColor(Color.FromArgb(255, 0, 45), "Grayscale");
            Color tinted2 = TintHelper.getTintColor(Color.FromArgb(100, 150, 50), "Grayscale");
            Color tinted3 = TintHelper.getTintColor(Color.FromArgb(37, 73, 190), "Grayscale");
            Assert.AreEqual(tinted1, Color.FromArgb(100, 100, 100));
            Assert.AreEqual(tinted1, tinted2);
            Assert.AreEqual(tinted2, tinted3);
        }

        [TestMethod()]
        public void testRedscale()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 100, 100), "Redscale");
            Assert.AreEqual(Color.FromArgb(90, 5, 5), tinted);
        }

        [TestMethod()]
        public void testGreenscale()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 100, 100), "Greenscale");
            Assert.AreEqual(Color.FromArgb(10, 80, 10), tinted);
        }

        [TestMethod()]
        public void testBluescale()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 100, 100), "Bluescale");
            Assert.AreEqual(Color.FromArgb(15, 30, 55), tinted);
        }

        [TestMethod()]
        public void testCyanscale()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 100, 100), "Cyanscale");
            Assert.AreEqual(Color.FromArgb(10, 45, 45), tinted);
        }

        [TestMethod()]
        public void testMagentascale()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 100, 100), "Magentascale");
            Assert.AreEqual(Color.FromArgb(60, 10, 30), tinted);
        }

        [TestMethod()]
        public void testYellowscale()
        {
            Color tinted = TintHelper.getTintColor(Color.FromArgb(100, 100, 100), "Yellowscale");
            Assert.AreEqual(Color.FromArgb(55, 40, 5), tinted);
        }

        [TestMethod()]
        public void testInvalidSelectionSameColor()
        {
            Color unmodified = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(122, 93, 201), "mydropdownisbroken");
            Assert.AreEqual(Color.FromArgb(122, 93, 201), unmodified);
        }
    }
}