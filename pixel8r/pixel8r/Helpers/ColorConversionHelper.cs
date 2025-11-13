using System.Collections.Generic;
using System.Drawing;
using SkiaSharp;
using Wacton.Unicolour;

namespace pixel8r.Helpers
{
    public class ColorConversionHelper
    {
        public static Unicolour getUnicolourFromSKColor(SKColor color)
        {
            return new Unicolour(ColourSpace.Rgb255, color.Red, color.Green, color.Blue);
        }

        public static Unicolour[] getUnicoloursFromSKColors(SKColor[] colors)
        {
            List<Unicolour> unicolours = new List<Unicolour>();
            foreach (SKColor color in colors)
            {
                unicolours.Add(getUnicolourFromSKColor(color));
            }

            return unicolours.ToArray();
        }

        public static SKColor getSaturatedColor(float h, float s, float l)
        {
            Unicolour unicolour = new Unicolour(ColourSpace.Hsl, h, s, l);
            Rgb255 rgb = unicolour.Rgb.Byte255;
            return new SKColor((byte)rgb.R, (byte)rgb.G, (byte)rgb.B);
        }
    }
}
