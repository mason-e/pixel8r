using System.Collections.Generic;
using SkiaSharp;

namespace pixel8r.Helpers
{
    public static class Constants
    {
        public static readonly SKColor[] mesenColors = [
            new SKColor(102, 102, 102),
            new SKColor(0, 42, 136),
            new SKColor(20, 18, 167),
            new SKColor(59, 0, 164),
            new SKColor(92, 0, 126),
            new SKColor(110, 0, 64),
            new SKColor(108, 6, 0),
            new SKColor(86, 29, 0),
            new SKColor(51, 53, 0),
            new SKColor(11, 72, 0),
            new SKColor(0, 82, 0),
            new SKColor(0, 79, 8),
            new SKColor(0, 64, 77),
            new SKColor(0, 0, 0),
            new SKColor(173, 173, 173),
            new SKColor(21, 95, 217),
            new SKColor(64, 64, 255),
            new SKColor(117, 39, 254),
            new SKColor(160, 26, 204),
            new SKColor(183, 30, 123),
            new SKColor(181, 49, 32),
            new SKColor(153, 78, 0),
            new SKColor(107, 109, 0),
            new SKColor(56, 135, 0),
            new SKColor(12, 147, 0),
            new SKColor(0, 143, 50),
            new SKColor(0, 124, 141),
            new SKColor(255, 254, 255),
            new SKColor(100, 176, 255),
            new SKColor(146, 144, 255),
            new SKColor(198, 118, 255),
            new SKColor(243, 106, 255),
            new SKColor(254, 110, 204),
            new SKColor(254, 129, 112),
            new SKColor(234, 158, 34),
            new SKColor(188, 190, 0),
            new SKColor(136, 216, 0),
            new SKColor(92, 228, 48),
            new SKColor(69, 224, 130),
            new SKColor(75, 205, 222),
            new SKColor(79, 79, 79),
            new SKColor(255, 254, 255),
            new SKColor(192, 223, 255),
            new SKColor(211, 210, 255),
            new SKColor(232, 200, 255),
            new SKColor(215, 194, 255),
            new SKColor(254, 196, 234),
            new SKColor(254, 204, 197),
            new SKColor(247, 216, 165),
            new SKColor(228, 229, 148),
            new SKColor(207, 239, 150),
            new SKColor(189, 244, 171),
            new SKColor(179, 243, 204),
            new SKColor(181, 235, 242),
            new SKColor(184, 184, 184),
        ];

        public static readonly SKColor[] gbColors = [
            SKColor.Parse("294139"),
            SKColor.Parse("39594a"),
            SKColor.Parse("5a7942"),
            SKColor.Parse("7b8210")
        ];

        public static readonly SKColor[] gbPColors = [
            SKColor.Parse("181818"),
            SKColor.Parse("4a5138"),
            SKColor.Parse("8c926b"),
            SKColor.Parse("c5caa4")
        ];

        public static readonly SKColor[] gbLColors = [
            SKColor.Parse("004f3a"),
            SKColor.Parse("00694a"),
            SKColor.Parse("009a70"),
            SKColor.Parse("00b582")
        ];

        public static readonly Dictionary<string, SKColor[]> Palettes = new()
        {
            { "NES", mesenColors },
            { "GB", gbColors },
            { "GB Pocket", gbPColors },
            { "GB Light", gbLColors }
        };
    }
}
