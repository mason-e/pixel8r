using System.Collections.Generic;
using SkiaSharp;

namespace pixel8r.Helpers
{
    public class GlobalVars
    {
        public static int ImageWidth = 0;
        public static int ImageHeight = 0;

        public static List<SKColor> CurrentPalette = new List<SKColor>();
        public static Dictionary<SKColor, SKColor> colorMatches = new Dictionary<SKColor, SKColor>();
    }
}
