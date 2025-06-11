using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGenerator
{
    static class GlobalVars
    {
        public static Color[] mesenColors = [
            Color.FromArgb(102, 102, 102),
            Color.FromArgb(0, 42, 136),
            Color.FromArgb(20, 18, 167),
            Color.FromArgb(59, 0, 164),
            Color.FromArgb(92, 0, 126),
            Color.FromArgb(110, 0, 64),
            Color.FromArgb(108, 6, 0),
            Color.FromArgb(86, 29, 0),
            Color.FromArgb(51, 53, 0),
            Color.FromArgb(11, 72, 0),
            Color.FromArgb(0, 82, 0),
            Color.FromArgb(0, 79, 8),
            Color.FromArgb(0, 64, 77),
            Color.FromArgb(0, 0, 0),
            Color.FromArgb(173, 173, 173),
            Color.FromArgb(21, 95, 217),
            Color.FromArgb(64, 64, 255),
            Color.FromArgb(117, 39, 254),
            Color.FromArgb(160, 26, 204),
            Color.FromArgb(183, 30, 123),
            Color.FromArgb(181, 49, 32),
            Color.FromArgb(153, 78, 0),
            Color.FromArgb(107, 109, 0),
            Color.FromArgb(56, 135, 0),
            Color.FromArgb(12, 147, 0),
            Color.FromArgb(0, 143, 50),
            Color.FromArgb(0, 124, 141),
            Color.FromArgb(255, 254, 255),
            Color.FromArgb(100, 176, 255),
            Color.FromArgb(146, 144, 255),
            Color.FromArgb(198, 118, 255),
            Color.FromArgb(243, 106, 255),
            Color.FromArgb(254, 110, 204),
            Color.FromArgb(254, 129, 112),
            Color.FromArgb(234, 158, 34),
            Color.FromArgb(188, 190, 0),
            Color.FromArgb(136, 216, 0),
            Color.FromArgb(92, 228, 48),
            Color.FromArgb(69, 224, 130),
            Color.FromArgb(75, 205, 222),
            Color.FromArgb(79, 79, 79),
            Color.FromArgb(255, 254, 255),
            Color.FromArgb(192, 223, 255),
            Color.FromArgb(211, 210, 255),
            Color.FromArgb(232, 200, 255),
            Color.FromArgb(215, 194, 255),
            Color.FromArgb(254, 196, 234),
            Color.FromArgb(254, 204, 197),
            Color.FromArgb(247, 216, 165),
            Color.FromArgb(228, 229, 148),
            Color.FromArgb(207, 239, 150),
            Color.FromArgb(189, 244, 171),
            Color.FromArgb(179, 243, 204),
            Color.FromArgb(181, 235, 242),
            Color.FromArgb(184, 184, 184),
        ];

        public static ColorPalette mesenPalette = new ColorPalette(mesenColors);

        private static string filePath1;
        public static string FilePath1
        {
            get { return filePath1; }
            set { filePath1 = value; }
        }

        private static string filePath2;
        public static string FilePath2
        {
            get { return filePath2; }
            set { filePath2 = value; }
        }

        private static string filePath3;
        public static string FilePath3
        {
            get { return filePath3; }
            set { filePath3 = value; }
        }

        private static int size;
        public static int Size
        {
            get { return size; }
            set { size = value; }
        }

        private static int sideLength;
        public static int SideLength
        {
            get { return sideLength; }
            set { sideLength = value; }
        }

        private static string tintColor1;
        public static string TintColor1
        {
            get { return tintColor1; }
            set { tintColor1 = value; }
        }

        private static string tintColor2;
        public static string TintColor2
        {
            get { return tintColor2; }
            set { tintColor2 = value; }
        }

        private static bool invert;
        public static bool Invert
        {
            get { return invert; }
            set { invert = value; }
        }

        private static int tintWeight;
        public static int TintWeight
        {
            get { return tintWeight; }
            set { tintWeight = value; }
        }
    }
}
