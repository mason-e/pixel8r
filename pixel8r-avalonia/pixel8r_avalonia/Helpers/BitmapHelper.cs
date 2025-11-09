using System;
using System.Drawing;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using SysBitmap = System.Drawing.Bitmap;

namespace pixel8r_avalonia.Helpers
{
    public class BitmapHelper
    {
        public static Bitmap generateBitmap(string filePath)
        {
            Bitmap original = new Bitmap(filePath);
            if (original.Size.Width > 1280 || original.Size.Height > 720)
            {
                float scaleFactor = Math.Max((float)original.Size.Width / 1280, (float)original.Size.Height / 720);
                return resize(original, scaleFactor);
            }
            else
            {
                return original;
            }
        }

        public static Bitmap resize(Bitmap original, float scaleFactor)
        {
            int width = (int)(original.Size.Width / scaleFactor);
            int height = (int)(original.Size.Height / scaleFactor);
            return original.CreateScaledBitmap(new Avalonia.PixelSize(width, height));
        }

        public static Bitmap paletteSwapPredefined(Bitmap bitmap, string palette, string algorithm)
        {
            SysBitmap sysBitmap = ConvertToSysBitmap(bitmap);
            for (int y = 0; y < sysBitmap.Height; y++)
            {
                for (int x = 0; x < sysBitmap.Width; x++)
                {
                    Color color = PaletteMatchingHelper.getMatchedColor(sysBitmap.GetPixel(x, y), palette, algorithm);
                    sysBitmap.SetPixel(x, y, color);
                }
            }
            // reset the color matching dictionary
            Constants.colorMatches.Clear();
            return ConvertFromSysBitmap(sysBitmap);
        }

        public static Bitmap paletteSwapProgrammatic(Bitmap bitmap, string palette)
        {
            SysBitmap sysBitmap = ConvertToSysBitmap(bitmap);
            for (int y = 0; y < sysBitmap.Height; y++)
            {
                for (int x = 0; x < sysBitmap.Width; x++)
                {
                    Color color = PaletteProgrammaticHelper.getProgrammaticColor(sysBitmap.GetPixel(x, y), palette);
                    sysBitmap.SetPixel(x, y, color);
                }
            }
            return ConvertFromSysBitmap(sysBitmap);
        }

        public static Bitmap tint(Bitmap bitmap, string palette)
        {
            SysBitmap sysBitmap = ConvertToSysBitmap(bitmap);
            for (int y = 0; y < sysBitmap.Height; y++)
            {
                for (int x = 0; x < sysBitmap.Width; x++)
                {
                    Color color = TintHelper.getTintColor(sysBitmap.GetPixel(x, y), palette);
                    sysBitmap.SetPixel(x, y, color);
                }
            }
            return ConvertFromSysBitmap(sysBitmap);
        }

        //public static Bitmap pixelate(Image image)
        //{
        //    Bitmap bitmap = new Bitmap(image);
        //    int x = 1, y = 1;
        //    while (x < bitmap.Width && y < bitmap.Height)
        //    {
        //        Color color = bitmap.GetPixel(x, y);
        //        for (int i = -1; i <= 1; i++)
        //        {
        //            for (int j = -1; j <= 1; j++)
        //            {
        //                try
        //                {
        //                    bitmap.SetPixel(x + i, y + j, color);
        //                }
        //                catch (ArgumentOutOfRangeException)
        //                {
        //                    // just handle the exception rather than try to account for the image not being divisible by 3
        //                }
        //            }
        //        }
        //        if (x + 3 < bitmap.Width)
        //        {
        //            x += 3;
        //        }
        //        else if (y + 3 < bitmap.Height)
        //        {
        //            y += 3;
        //            x = 1;
        //        }
        //        else break;
        //    }

        //    return bitmap;
        //}

        //public static Bitmap scanlines(Image image)
        //{
        //    Bitmap bitmap = new Bitmap(image);
        //    for (int y = 0; y < bitmap.Height; y++)
        //    {
        //        for (int x = 0; x < bitmap.Width; x++)
        //        {
        //            Color color = bitmap.GetPixel(x, y);
        //            if (y % 2 == 0)
        //            {
        //                color = TintFunctions.getTintColor(color, "White (Brighten)");
        //            }
        //            else
        //            {
        //                color = TintFunctions.getTintColor(color, "Black (Darken)");
        //            }
        //            bitmap.SetPixel(x, y, color);
        //        }
        //    }
        //    return bitmap;
        //}

        public static Bitmap DrawPalette(string palette)
        {
            Color[] targetPalette = [];
            if (palette == "NES")
            {
                targetPalette = Constants.mesenColors;
            }
            if (palette == "Web Colors")
            {
                targetPalette = Constants.webColors;
            }
            SysBitmap bitmap = new SysBitmap(240, 192);
            int x = 0;
            int y = 0;
            // attempts to fill the preview area as much as possible based on the size of the palette
            // the total available pixels are 240*192 = 46,080, adjust side length to 8, 16, 24, 48 (common factors of 240 and 192)
            int rawTileSize = (int)Math.Sqrt(46080 / targetPalette.Length);
            int tileSize = rawTileSize > 48 ? 48 : (rawTileSize > 24 ? 24 : (rawTileSize > 16 ? 16 : (rawTileSize > 8 ? 8 : rawTileSize)));
            foreach (Color color in targetPalette)
            {
                // step down to next row if it would overflow the current row
                if (x + tileSize > 240)
                {
                    x = 0;
                    y += tileSize;
                }
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.FillRectangle(new SolidBrush(color), x, y, tileSize, tileSize);
                }
                x += tileSize;
            }
            return ConvertFromSysBitmap(bitmap);
        }
        
        private static Bitmap ConvertFromSysBitmap(SysBitmap sysBitmap)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                sysBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                return new Bitmap(memory);
            }
        }

        private static SysBitmap ConvertToSysBitmap(Bitmap bitmap)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bitmap.Save(memory);
                memory.Position = 0;
                return new SysBitmap(memory);
            }
        }
    }
}
