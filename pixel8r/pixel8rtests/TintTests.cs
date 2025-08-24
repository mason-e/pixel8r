using pixel8r;
using System.Drawing;

namespace pixel8rtests
{
    [TestClass()]
    public class TintTests
    {
        [TestMethod()]
        public void testBrighten()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 0, 250), "White (Brighten)");
            Assert.AreEqual(tinted, Color.FromArgb(110, 10, 255));
        }

        [TestMethod()]
        public void testDarken()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(5, 255, 100), "Black (Darken)");
            Assert.AreEqual(tinted, Color.FromArgb(0, 245, 90));
        }

        [TestMethod()]
        public void testSoftRed()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(246, 100, 100), "Red (Soft)");
            Assert.AreEqual(tinted, Color.FromArgb(255, 100, 100));

            // no change when red already maxed
            tinted = TintFunctions.getTintColor(tinted, "Red (Soft)");
            Assert.AreEqual(tinted, Color.FromArgb(255, 100, 100));
        }

        [TestMethod()]
        public void testHardRed()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(246, 100, 100), "Red (Hard)");
            Assert.AreEqual(tinted, Color.FromArgb(255, 90, 90));

            // decrease others even when red already maxed
            tinted = TintFunctions.getTintColor(tinted, "Red (Hard)");
            Assert.AreEqual(tinted, Color.FromArgb(255, 80, 80));
        }

        [TestMethod()]
        public void testSoftGreen()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 246, 100), "Green (Soft)");
            Assert.AreEqual(tinted, Color.FromArgb(100, 255, 100));

            // no change when green already maxed
            tinted = TintFunctions.getTintColor(tinted, "Green (Soft)");
            Assert.AreEqual(tinted, Color.FromArgb(100, 255, 100));
        }

        [TestMethod()]
        public void testHardGreen()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 246, 100), "Green (Hard)");
            Assert.AreEqual(tinted, Color.FromArgb(90, 255, 90));

            // decrease others even when green already maxed
            tinted = TintFunctions.getTintColor(tinted, "Green (Hard)");
            Assert.AreEqual(tinted, Color.FromArgb(80, 255, 80));
        }

        [TestMethod()]
        public void testSoftBlue()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 100, 246), "Blue (Soft)");
            Assert.AreEqual(tinted, Color.FromArgb(100, 100, 255));

            // no change when blue already maxed
            tinted = TintFunctions.getTintColor(tinted, "Blue (Soft)");
            Assert.AreEqual(tinted, Color.FromArgb(100, 100, 255));
        }

        [TestMethod()]
        public void testHardBlue()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 100, 246), "Blue (Hard)");
            Assert.AreEqual(tinted, Color.FromArgb(90, 90, 255));

            // decrease others even when blue already maxed
            tinted = TintFunctions.getTintColor(tinted, "Blue (Hard)");
            Assert.AreEqual(tinted, Color.FromArgb(80, 80, 255));
        }

        [TestMethod()]
        public void testSoftCyan()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 247, 246), "Cyan (Soft)");
            Assert.AreEqual(tinted, Color.FromArgb(100, 255, 255));

            // no change when cyan already maxed
            tinted = TintFunctions.getTintColor(tinted, "Cyan (Soft)");
            Assert.AreEqual(tinted, Color.FromArgb(100, 255, 255));
        }

        [TestMethod()]
        public void testHardCyan()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 247, 246), "Cyan (Hard)");
            Assert.AreEqual(tinted, Color.FromArgb(90, 255, 255));

            // decrease red even when cyan already maxed
            tinted = TintFunctions.getTintColor(tinted, "Cyan (Hard)");
            Assert.AreEqual(tinted, Color.FromArgb(80, 255, 255));
        }

        [TestMethod()]
        public void testSoftMagenta()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(247, 100, 246), "Magenta (Soft)");
            Assert.AreEqual(tinted, Color.FromArgb(255, 100, 255));

            // no change when magenta already maxed
            tinted = TintFunctions.getTintColor(tinted, "Magenta (Soft)");
            Assert.AreEqual(tinted, Color.FromArgb(255, 100, 255));
        }

        [TestMethod()]
        public void testHardMagenta()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(247, 100, 246), "Magenta (Hard)");
            Assert.AreEqual(tinted, Color.FromArgb(255, 90, 255));

            // decrease green even when magenta already maxed
            tinted = TintFunctions.getTintColor(tinted, "Magenta (Hard)");
            Assert.AreEqual(tinted, Color.FromArgb(255, 80, 255));
        }

        [TestMethod()]
        public void testSoftYellow()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(246, 247, 100), "Yellow (Soft)");
            Assert.AreEqual(tinted, Color.FromArgb(255, 255, 100));

            // no change when yellow already maxed
            tinted = TintFunctions.getTintColor(tinted, "Yellow (Soft)");
            Assert.AreEqual(tinted, Color.FromArgb(255, 255, 100));
        }

        [TestMethod()]
        public void testHardYellow()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(246, 247, 100), "Yellow (Hard)");
            Assert.AreEqual(tinted, Color.FromArgb(255, 255, 90));

            // decrease blue even when yellow already maxed
            tinted = TintFunctions.getTintColor(tinted, "Yellow (Hard)");
            Assert.AreEqual(tinted, Color.FromArgb(255, 255, 80));
        }

        [TestMethod()]
        public void testGrayscaleSameSumsSameResult()
        {
            // all sum up to 300
            Color tinted1 = TintFunctions.getTintColor(Color.FromArgb(255, 0, 45), "Grayscale");
            Color tinted2 = TintFunctions.getTintColor(Color.FromArgb(100, 150, 50), "Grayscale");
            Color tinted3 = TintFunctions.getTintColor(Color.FromArgb(37, 73, 190), "Grayscale");
            Assert.AreEqual(tinted1, Color.FromArgb(100, 100, 100));
            Assert.AreEqual(tinted1, tinted2);
            Assert.AreEqual(tinted2, tinted3);
        }

        [TestMethod()]
        public void testRedscale()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 100, 100), "Redscale");
            Assert.AreEqual(tinted, Color.FromArgb(90, 5, 5));
        }

        [TestMethod()]
        public void testGreenscale()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 100, 100), "Greenscale");
            Assert.AreEqual(tinted, Color.FromArgb(10, 80, 10));
        }

        [TestMethod()]
        public void testBluescale()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 100, 100), "Bluescale");
            Assert.AreEqual(tinted, Color.FromArgb(15, 30, 55));
        }

        [TestMethod()]
        public void testCyanscale()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 100, 100), "Cyanscale");
            Assert.AreEqual(tinted, Color.FromArgb(10, 45, 45));
        }

        [TestMethod()]
        public void testMagentascale()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 100, 100), "Magentascale");
            Assert.AreEqual(tinted, Color.FromArgb(60, 10, 30));
        }

        [TestMethod()]
        public void testYellowscale()
        {
            Color tinted = TintFunctions.getTintColor(Color.FromArgb(100, 100, 100), "Yellowscale");
            Assert.AreEqual(tinted, Color.FromArgb(55, 40, 5));
        }
    }
}