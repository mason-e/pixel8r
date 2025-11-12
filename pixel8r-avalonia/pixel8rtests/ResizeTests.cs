using pixel8r_avalonia;
using pixel8r_avalonia.Helpers;

namespace pixel8rtests
{
    [TestClass()]
    public class ResizeTests
    {
        [TestMethod()]
        [DoNotParallelize]
        public void testCrop16To9Ratio()
        {
            // square image
            GlobalVars.ImageWidth = 500;
            GlobalVars.ImageHeight = 500;
            (int cropX, int cropY) = ResizeHelper.getCropDimensions(16, 9);
            Assert.AreEqual(500, cropX);
            Assert.AreEqual(281, cropY);

            // tall image
            GlobalVars.ImageWidth = 200;
            GlobalVars.ImageHeight = 600;
            (cropX, cropY) = ResizeHelper.getCropDimensions(16, 9);
            Assert.AreEqual(200, cropX);
            Assert.AreEqual(112, cropY);

            // wide image
            GlobalVars.ImageWidth = 1000;
            GlobalVars.ImageHeight = 200;
            (cropX, cropY) = ResizeHelper.getCropDimensions(16, 9);
            Assert.AreEqual(355, cropX);
            Assert.AreEqual(200, cropY);
        }

        [TestMethod()]
        [DoNotParallelize]
        public void testCrop4to3Ratio()
        {
            // square image
            GlobalVars.ImageWidth = 500;
            GlobalVars.ImageHeight = 500;
            (int cropX, int cropY) = ResizeHelper.getCropDimensions(4, 3);
            Assert.AreEqual(500, cropX);
            Assert.AreEqual(375, cropY);

            // tall image
            GlobalVars.ImageWidth = 200;
            GlobalVars.ImageHeight = 600;
            (cropX, cropY) = ResizeHelper.getCropDimensions(4, 3);
            Assert.AreEqual(200, cropX);
            Assert.AreEqual(150, cropY);

            // wide image
            GlobalVars.ImageWidth = 1000;
            GlobalVars.ImageHeight = 200;
            (cropX, cropY) = ResizeHelper.getCropDimensions(4, 3);
            Assert.AreEqual(266, cropX);
            Assert.AreEqual(200, cropY  );
        }

        [TestMethod()]
        [DoNotParallelize]
        public void testCrop1to1Ratio()
        {
            // square image
            GlobalVars.ImageWidth = 500;
            GlobalVars.ImageHeight = 500;
            (int cropX, int cropY) = ResizeHelper.getCropDimensions(1, 1);
            Assert.AreEqual(500, cropX);
            Assert.AreEqual(500, cropY);

            // tall image
            GlobalVars.ImageWidth = 200;
            GlobalVars.ImageHeight = 600;
            (cropX, cropY) = ResizeHelper.getCropDimensions(1, 1);
            Assert.AreEqual(200, cropX);
            Assert.AreEqual(200, cropY);

            // wide image
            GlobalVars.ImageWidth = 1000;
            GlobalVars.ImageHeight = 200;
            (cropX, cropY) = ResizeHelper.getCropDimensions(1, 1);
            Assert.AreEqual(200, cropX);
            Assert.AreEqual(200, cropY);
        }

        [TestMethod()]
        [DoNotParallelize]
        public void testCrop16to15Ratio()
        {
            // square image
            GlobalVars.ImageWidth = 500;
            GlobalVars.ImageHeight = 500;
            (int cropX, int cropY) = ResizeHelper.getCropDimensions(16, 15);
            Assert.AreEqual(500, cropX);
            Assert.AreEqual(468, cropY);

            // tall image
            GlobalVars.ImageWidth = 200;
            GlobalVars.ImageHeight = 600;
            (cropX, cropY) = ResizeHelper.getCropDimensions(16, 15);
            Assert.AreEqual(200, cropX);
            Assert.AreEqual(187, cropY);

            // wide image
            GlobalVars.ImageWidth = 1000;
            GlobalVars.ImageHeight = 200;
            (cropX, cropY) = ResizeHelper.getCropDimensions(16, 15);
            Assert.AreEqual(213, cropX);
            Assert.AreEqual(200, cropY);
        }

        [TestMethod()]
        [DoNotParallelize]
        public void testCrop8to7Ratio()
        {
            // square image
            GlobalVars.ImageWidth = 500;
            GlobalVars.ImageHeight = 500;
            (int cropX, int cropY) = ResizeHelper.getCropDimensions(8, 7);
            Assert.AreEqual(500, cropX);
            Assert.AreEqual(437, cropY);

            // tall image
            GlobalVars.ImageWidth = 200;
            GlobalVars.ImageHeight = 600;
            (cropX, cropY) = ResizeHelper.getCropDimensions(8, 7);
            Assert.AreEqual(200, cropX);
            Assert.AreEqual(175, cropY);

            // wide image
            GlobalVars.ImageWidth = 1000;
            GlobalVars.ImageHeight = 200;
            (cropX, cropY) = ResizeHelper.getCropDimensions(8, 7);
            Assert.AreEqual(228, cropX);
            Assert.AreEqual(200, cropY);
        }

        [TestMethod()]
        [DoNotParallelize]
        public void testCrop10to7Ratio()
        {
            // square image
            GlobalVars.ImageWidth = 500;
            GlobalVars.ImageHeight = 500;
            (int cropX, int cropY) = ResizeHelper.getCropDimensions(10, 7);
            Assert.AreEqual(500, cropX);
            Assert.AreEqual(350, cropY);

            // tall image
            GlobalVars.ImageWidth = 200;
            GlobalVars.ImageHeight = 600;
            (cropX, cropY) = ResizeHelper.getCropDimensions(10, 7);
            Assert.AreEqual(200, cropX);
            Assert.AreEqual(140, cropY);

            // wide image
            GlobalVars.ImageWidth = 1000;
            GlobalVars.ImageHeight = 200;
            (cropX, cropY) = ResizeHelper.getCropDimensions(10, 7);
            Assert.AreEqual(285, cropX);
            Assert.AreEqual(200, cropY);
        }

        [TestMethod()]
        [DoNotParallelize]
        public void testCrop10to9Ratio()
        {
            // square image
            GlobalVars.ImageWidth = 500;
            GlobalVars.ImageHeight = 500;
            (int cropX, int cropY) = ResizeHelper.getCropDimensions(10, 9);
            Assert.AreEqual(500, cropX);
            Assert.AreEqual(450, cropY);

            // tall image
            GlobalVars.ImageWidth = 200;
            GlobalVars.ImageHeight = 600;
            (cropX, cropY) = ResizeHelper.getCropDimensions(10, 9);
            Assert.AreEqual(200, cropX);
            Assert.AreEqual(180, cropY);

            // wide image
            GlobalVars.ImageWidth = 1000;
            GlobalVars.ImageHeight = 200;
            (cropX, cropY) = ResizeHelper.getCropDimensions(10, 9);
            Assert.AreEqual(222, cropX);
            Assert.AreEqual(200, cropY);
        }

        [TestMethod()]
        [DoNotParallelize]
        public void testCrop3to2Ratio()
        {
            // square image
            GlobalVars.ImageWidth = 500;
            GlobalVars.ImageHeight = 500;
            (int cropX, int cropY) = ResizeHelper.getCropDimensions(3, 2);
            Assert.AreEqual(500, cropX);
            Assert.AreEqual(333, cropY);

            // tall image
            GlobalVars.ImageWidth = 200;
            GlobalVars.ImageHeight = 600;
            (cropX, cropY) = ResizeHelper.getCropDimensions(3, 2);
            Assert.AreEqual(200, cropX);
            Assert.AreEqual(133, cropY);

            // wide image
            GlobalVars.ImageWidth = 1000;
            GlobalVars.ImageHeight = 200;
            (cropX, cropY) = ResizeHelper.getCropDimensions(3, 2);
            Assert.AreEqual(300, cropX);
            Assert.AreEqual(200, cropY);
        }
    }
}