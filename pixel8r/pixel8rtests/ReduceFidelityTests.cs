using pixel8r.Helpers;
using SkiaSharp;

namespace pixel8rtests
{
    [TestClass()]
    public class ReduceFidelityTests
    {
        [TestMethod()]
        public void test3BitRGB()
        {
            SKColor reduced = ReduceFidelityHelper.getReducedColor(new SKColor(127, 129, 45), "3 Bit RGB");
            Assert.AreEqual(new SKColor(0, 255, 0), reduced);
        }

        [TestMethod()]
        public void test6BitRGB()
        {
            SKColor reduced = ReduceFidelityHelper.getReducedColor(new SKColor(212, 213, 42), "6 Bit RGB");
            Assert.AreEqual(new SKColor(170, 255, 0), reduced);
        }

        [TestMethod()]
        public void test9BitRGB()
        {
            SKColor reduced = ReduceFidelityHelper.getReducedColor(new SKColor(17, 19, 100), "9 Bit RGB");
            Assert.AreEqual(new SKColor(0, 36, 108), reduced);
        }

        [TestMethod()]
        public void test12BitRGB()
        {
            SKColor reduced = ReduceFidelityHelper.getReducedColor(new SKColor(8, 9, 200), "12 Bit RGB");
            Assert.AreEqual(new SKColor(0, 17, 204), reduced);
        }

        [TestMethod()]
        public void test15BitRGB()
        {
            SKColor reduced = ReduceFidelityHelper.getReducedColor(new SKColor(3, 5, 64), "15 Bit RGB");
            Assert.AreEqual(new SKColor(0, 8, 64), reduced);
        }

        [TestMethod()]
        public void test18BitRGB()
        {
            SKColor reduced = ReduceFidelityHelper.getReducedColor(new SKColor(1, 3, 204), "18 Bit RGB");
            Assert.AreEqual(new SKColor(0, 4, 200), reduced);
        }

        [TestMethod()]
        public void testInvalidSelectionSameColor()
        {
            SKColor unmodified = ReduceFidelityHelper.getReducedColor(new SKColor(44, 87, 105), "mydropdownisbroken");
            Assert.AreEqual(new SKColor(44, 87, 105), unmodified);
        }
    }
}