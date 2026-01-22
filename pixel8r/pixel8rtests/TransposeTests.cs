using pixel8r.Helpers;
using SkiaSharp;

namespace pixel8rtests
{
    [TestClass()]
    public class TransposeTests
    {
        [TestMethod()]
        public void testTransposeRBG()
        {
            SKColor transposed = TransposeHelper.getTransposedColor(new SKColor(10, 20, 30), "RBG");
            Assert.AreEqual(new SKColor(10, 30, 20), transposed);
        }

        [TestMethod()]
        public void testTransposeGRB()
        {
            SKColor transposed =  TransposeHelper.getTransposedColor(new SKColor(10, 20, 30), "GRB");
            Assert.AreEqual(new SKColor(20, 10, 30), transposed);
        }

        [TestMethod()]
        public void testTransposeGBR()
        {
            SKColor transposed = TransposeHelper.getTransposedColor(new SKColor(10, 20, 30), "GBR");
            Assert.AreEqual(new SKColor(20, 30, 10), transposed);
        }

        [TestMethod()]
        public void testTransposeBRG()
        {
            SKColor transposed = TransposeHelper.getTransposedColor(new SKColor(10, 20, 30), "BRG");
            Assert.AreEqual(new SKColor(30, 10, 20), transposed);
        }

        [TestMethod()]
        public void testTransposeBGR()
        {
            SKColor transposed = TransposeHelper.getTransposedColor(new SKColor(10, 20, 30), "BGR");
            Assert.AreEqual(new SKColor(30, 20, 10), transposed);
        }

        [TestMethod()]
        public void testInvalidSelectionSameColor()
        {
            SKColor unmodified = TransposeHelper.getTransposedColor(new SKColor(44, 87, 105), "mydropdownisbroken");
            Assert.AreEqual(new SKColor(44, 87, 105), unmodified);
        }
    }
}