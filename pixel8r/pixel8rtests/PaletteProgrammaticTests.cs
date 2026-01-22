using pixel8r.Helpers;
using SkiaSharp;

namespace pixel8rtests
{
    [TestClass()]
    public class PaletteProgrammaticTests
    {
        [TestMethod()]
        public void testSingleSaturation()
        {
            SKColor saturated = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(191, 64, 64), "Saturate");
            Assert.AreEqual(new SKColor(197, 58, 58), saturated);
        }

        [TestMethod()]
        public void testFullySaturatedNoChange()
        {
            SKColor saturated = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(255, 0, 0), "Saturate");
            Assert.AreEqual(new SKColor(255, 0, 0), saturated);
        }

        [TestMethod()]
        public void test3BitRGB()
        {
            SKColor reduced = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(127, 129, 45), "3 Bit RGB");
            Assert.AreEqual(new SKColor(0, 255, 0), reduced);
        }

        [TestMethod()]
        public void test6BitRGB()
        {
            SKColor reduced = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(212, 213, 42), "6 Bit RGB");
            Assert.AreEqual(new SKColor(170, 255, 0), reduced);
        }

        [TestMethod()]
        public void test9BitRGB()
        {
            SKColor reduced = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(17, 19, 100), "9 Bit RGB");
            Assert.AreEqual(new SKColor(0, 36, 108), reduced);
        }

        [TestMethod()]
        public void test12BitRGB()
        {
            SKColor reduced = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(8, 9, 200), "12 Bit RGB");
            Assert.AreEqual(new SKColor(0, 17, 204), reduced);
        }

        [TestMethod()]
        public void test15BitRGB()
        {
            SKColor reduced = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(3, 5, 64), "15 Bit RGB");
            Assert.AreEqual(new SKColor(0, 8, 64), reduced);
        }

        [TestMethod()]
        public void test18BitRGB()
        {
            SKColor reduced = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(1, 3, 204), "18 Bit RGB");
            Assert.AreEqual(new SKColor(0, 4, 200), reduced);
        }

        [TestMethod()]
        public void testTransposeRBG()
        {
            SKColor transposed = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(10, 20, 30), "Transpose - RBG");
            Assert.AreEqual(new SKColor(10, 30, 20), transposed);
        }

        [TestMethod()]
        public void testTransposeGRB()
        {
            SKColor transposed =  PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(10, 20, 30), "Transpose - GRB");
            Assert.AreEqual(new SKColor(20, 10, 30), transposed);
        }

        [TestMethod()]
        public void testTransposeGBR()
        {
            SKColor transposed = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(10, 20, 30), "Transpose - GBR");
            Assert.AreEqual(new SKColor(20, 30, 10), transposed);
        }

        [TestMethod()]
        public void testTransposeBRG()
        {
            SKColor transposed = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(10, 20, 30), "Transpose - BRG");
            Assert.AreEqual(new SKColor(30, 10, 20), transposed);
        }

        [TestMethod()]
        public void testTransposeBGR()
        {
            SKColor transposed = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(10, 20, 30), "Transpose - BGR");
            Assert.AreEqual(new SKColor(30, 20, 10), transposed);
        }

        [TestMethod()]
        public void testInvalidSelectionSameColor()
        {
            SKColor unmodified = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(44, 87, 105), "mydropdownisbroken");
            Assert.AreEqual(new SKColor(44, 87, 105), unmodified);
        }
    }
}