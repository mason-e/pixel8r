using System.Drawing;
using Wacton.Unicolour;

namespace CSharpGenerator
{
    internal class ColorConversionFunctions
    {
        public static Unicolour getUnicolorFromSystemColor(Color color)
        {
            return new Unicolour(ColourSpace.Rgb255, color.R, color.G, color.B);
        }

        public static Unicolour[] getUnicolorsFromSystemColors(Color[] colors)
        {
            List<Unicolour> unicolors = new List<Unicolour>();
            foreach(Color color in colors)
            {
                unicolors.Add(getUnicolorFromSystemColor(color));
            }

            return unicolors.ToArray();
        }

        public static Color getSaturatedColor(float h, float s, float l)
        {
            Unicolour unicolor = new Unicolour(ColourSpace.Hsl, h, s, l);
            Rgb255 rgb = unicolor.Rgb.Byte255;
            return Color.FromArgb(rgb.R, rgb.G, rgb.B);
        }
    }
}
