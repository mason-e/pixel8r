using System.Drawing;
using Wacton.Unicolour;

namespace pixel8r
{
    internal class ColorConversionFunctions
    {
        public static Unicolour getUnicolourFromSystemColor(Color color)
        {
            return new Unicolour(ColourSpace.Rgb255, color.R, color.G, color.B);
        }

        public static Unicolour[] getUnicoloursFromSystemColors(Color[] colors)
        {
            List<Unicolour> unicolours = new List<Unicolour>();
            foreach(Color color in colors)
            {
                unicolours.Add(getUnicolourFromSystemColor(color));
            }

            return unicolours.ToArray();
        }

        public static Color getSaturatedColor(float h, float s, float l)
        {
            Unicolour unicolour = new Unicolour(ColourSpace.Hsl, h, s, l);
            Rgb255 rgb = unicolour.Rgb.Byte255;
            return Color.FromArgb(rgb.R, rgb.G, rgb.B);
        }
    }
}
