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
        public void testRGBMultipleOf3CorrectRounding()
        {
            SKColor factored = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(121, 122, 1), "RGB Multiples of 3");
            Assert.AreEqual(new SKColor(120, 123, 0), factored);
        }

        [TestMethod()]
        public void testRGBMultipleOf5CorrectRounding()
        {
            SKColor factored = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(102, 103, 2), "RGB Multiples of 5");
            Assert.AreEqual(new SKColor(100, 105, 0), factored);
        }

        [TestMethod()]
        public void testRGBMultipleOf15CorrectRounding()
        {
            SKColor factored = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(157, 158, 7), "RGB Multiples of 15");
            Assert.AreEqual(new SKColor(150, 165, 0), factored);
        }

        [TestMethod()]
        public void testRGBMultipleOf17CorrectRounding()
        {
            SKColor factored = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(178, 179, 8), "RGB Multiples of 17");
            Assert.AreEqual(new SKColor(170, 187, 0), factored);
        }

        [TestMethod()]
        public void testRGBMultipleOf51CorrectRounding()
        {
            SKColor factored = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(127, 128, 25), "RGB Multiples of 51");
            Assert.AreEqual(new SKColor(102, 153, 0), factored);
        }

        [TestMethod()]
        public void testRGBMultipleOf85CorrectRounding()
        {
            SKColor factored = PaletteProgrammaticHelper.getProgrammaticColor(new SKColor(212, 213, 42), "RGB Multiples of 85");
            Assert.AreEqual(new SKColor(170, 255, 0), factored);
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