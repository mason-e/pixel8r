namespace pixel8r
{
    public class BitmapFunction
    {
        public static Bitmap generateBitmap()
        {
            Bitmap original = new Bitmap(GlobalVars.FilePath);
            if (original.Width > 1118 || original.Height > 720)
            {
                float scaleFactor = Math.Max((float)original.Width / 1118, (float)original.Height / 720);
                return resize(original, scaleFactor);
            }
            else
            {
                GlobalVars.ImageWidth = original.Width;
                GlobalVars.ImageHeight = original.Height;
                return original;
            }
        }

        public static Bitmap resize(Bitmap original, float scaleFactor)
        {
            GlobalVars.ImageWidth = (int)(original.Width / scaleFactor);
            GlobalVars.ImageHeight = (int)(original.Height / scaleFactor);
            return new Bitmap(original, new Size(GlobalVars.ImageWidth, GlobalVars.ImageHeight));
        }

        public static Bitmap paletteSwapPredefined(Image image, string palette, string algorithm, bool dither)
        {
            Bitmap bitmap = new Bitmap(image);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    if (dither && (x-1) % 3 == 0 && (y-1) % 3 == 0)
                    {
                        color = PaletteMatchingFunctions.getMatchedColor(color, palette, algorithm, true);
                    }
                    else
                    {
                        color = PaletteMatchingFunctions.getMatchedColor(color, palette, algorithm, false);
                    }
                    bitmap.SetPixel(x, y, color);
                }
            }
            return bitmap;
        }

        public static Bitmap paletteSwapProgrammatic(Image image, string palette)
        {
            Bitmap bitmap = new Bitmap(image);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = PaletteProgrammaticFunctions.getProgrammaticColor(bitmap.GetPixel(x, y), palette);
                    bitmap.SetPixel(x, y, color);
                }
            }
            return bitmap;
        }

        public static Bitmap tint(Image image, string palette)
        {
            Bitmap bitmap = new Bitmap(image);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = TintFunctions.getTintColor(bitmap.GetPixel(x, y), palette);
                    bitmap.SetPixel(x, y, color);
                }
            }
            return bitmap;
        }

        public static Bitmap pixelate(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            int x = 1, y = 1;
            Random r = new Random();
            while (x < bitmap.Width && y < bitmap.Height)
            {
                // use color of pixel one to the left as new color - otherwise it could clash with dither pixel
                Color color = bitmap.GetPixel(x - 1, y);
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        try
                        {
                            bitmap.SetPixel(x + i, y + j, color);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            // just handle the exception rather than try to account for the image not being divisible by 3
                        }
                    }
                }
                if (x + 3 < bitmap.Width)
                {
                    x += 3;
                }
                else if (y + 3 < bitmap.Height)
                {
                    y += 3;
                    x = 1;
                }
                else break;
            }
            
                return bitmap;           
        }

        public static Bitmap scanlines(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    if (y % 2 == 0)
                    {
                        color = TintFunctions.getTintColor(color, "White (Brighten)");
                    }
                    else
                    {
                        color = TintFunctions.getTintColor(color, "Black (Darken)");
                    }
                    bitmap.SetPixel(x, y, color);
                }
            }
            return bitmap;
        }

        public static Bitmap drawPalette(string palette)
        {
            Color[] targetPalette = [];
            if (palette == "NES")
            {
                targetPalette = GlobalVars.mesenColors;
            }
            if (palette == "Web Colors")
            {
                targetPalette = GlobalVars.webColors;
            }
            Bitmap bitmap = new Bitmap(240, 192);
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
            return bitmap;
        }
    }
}
