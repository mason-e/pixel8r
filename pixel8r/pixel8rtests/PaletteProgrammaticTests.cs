using pixel8r;
using pixel8r.Helpers;
using System.Drawing;

namespace pixel8rtests
{
    [TestClass()]
    public class PaletteProgrammaticTests
    {
        [TestMethod()]
        public void testSingleSaturation()
        {
            Color saturated = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(191, 64, 64), "Saturate");
            Assert.AreEqual(Color.FromArgb(197, 58, 58), saturated);
        }

        [TestMethod()]
        public void testFullySaturatedNoChange()
        {
            Color saturated = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(255, 0, 0), "Saturate");
            Assert.AreEqual(Color.FromArgb(255, 0, 0), saturated);
        }

        [TestMethod()]
        public void testRGBMultipleOf3CorrectRounding()
        {
            Color factored = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(121, 122, 1), "RGB Multiples of 3");
            Assert.AreEqual(Color.FromArgb(120, 123, 0), factored);
        }

        [TestMethod()]
        public void testRGBMultipleOf5CorrectRounding()
        {
            Color factored = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(102, 103, 2), "RGB Multiples of 5");
            Assert.AreEqual(Color.FromArgb(100, 105, 0), factored);
        }

        [TestMethod()]
        public void testRGBMultipleOf15CorrectRounding()
        {
            Color factored = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(157, 158, 7), "RGB Multiples of 15");
            Assert.AreEqual(Color.FromArgb(150, 165, 0), factored);
        }

        [TestMethod()]
        public void testRGBMultipleOf17CorrectRounding()
        {
            Color factored = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(178, 179, 8), "RGB Multiples of 17");
            Assert.AreEqual(Color.FromArgb(170, 187, 0), factored);
        }

        [TestMethod()]
        public void testRGBMultipleOf51CorrectRounding()
        {
            Color factored = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(127, 128, 25), "RGB Multiples of 51");
            Assert.AreEqual(Color.FromArgb(102, 153, 0), factored);
        }

        [TestMethod()]
        public void testRGBMultipleOf85CorrectRounding()
        {
            Color factored = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(212, 213, 42), "RGB Multiples of 85");
            Assert.AreEqual(Color.FromArgb(170, 255, 0), factored);
        }

        [TestMethod()]
        public void testTransposeRBG()
        {
            Color transposed = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(10, 20, 30), "Transpose - RBG");
            Assert.AreEqual(Color.FromArgb(10, 30, 20), transposed);
        }

        [TestMethod()]
        public void testTransposeGRB()
        {
            Color transposed =  PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(10, 20, 30), "Transpose - GRB");
            Assert.AreEqual(Color.FromArgb(20, 10, 30), transposed);
        }

        [TestMethod()]
        public void testTransposeGBR()
        {
            Color transposed = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(10, 20, 30), "Transpose - GBR");
            Assert.AreEqual(Color.FromArgb(20, 30, 10), transposed);
        }

        [TestMethod()]
        public void testTransposeBRG()
        {
            Color transposed = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(10, 20, 30), "Transpose - BRG");
            Assert.AreEqual(Color.FromArgb(30, 10, 20), transposed);
        }

        [TestMethod()]
        public void testTransposeBGR()
        {
            Color transposed = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(10, 20, 30), "Transpose - BGR");
            Assert.AreEqual(Color.FromArgb(30, 20, 10), transposed);
        }

        [TestMethod()]
        public void testInvalidSelectionSameColor()
        {
            Color unmodified = PaletteProgrammaticHelper.getProgrammaticColor(Color.FromArgb(44, 87, 105), "mydropdownisbroken");
            Assert.AreEqual(Color.FromArgb(44, 87, 105), unmodified);
        }
    }
}