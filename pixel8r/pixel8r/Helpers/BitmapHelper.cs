using System;
using System.Drawing;
using SkiaSharp;
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace pixel8r.Helpers
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
            SKBitmap skBitmap = ConvertToSKBitmap(bitmap);
            for (int y = 0; y < skBitmap.Height; y++)
            {
                for (int x = 0; x < skBitmap.Width; x++)
                {
                    SKColor color = PaletteMatchingHelper.getMatchedColor(skBitmap.GetPixel(x, y), palette, algorithm);
                    skBitmap.SetPixel(x, y, color);
                }
            }
            // reset the color matching dictionary
            GlobalVars.colorMatches.Clear();
            return ConvertFromSkBitmap(skBitmap);
        }

        public static Bitmap transpose(Bitmap bitmap, string selection)
        {
            SKBitmap skBitmap = ConvertToSKBitmap(bitmap);
            for (int y = 0; y < skBitmap.Height; y++)
            {
                for (int x = 0; x < skBitmap.Width; x++)
                {
                    SKColor color = TransposeHelper.getTransposedColor(skBitmap.GetPixel(x, y), selection);
                    skBitmap.SetPixel(x, y, color);
                }
            }
            return ConvertFromSkBitmap(skBitmap);
        }

        public static Bitmap reduceFidelity(Bitmap bitmap, string selection)
        {
            SKBitmap skBitmap = ConvertToSKBitmap(bitmap);
            for (int y = 0; y < skBitmap.Height; y++)
            {
                for (int x = 0; x < skBitmap.Width; x++)
                {
                    SKColor color = ReduceFidelityHelper.getReducedColor(skBitmap.GetPixel(x, y), selection);
                    skBitmap.SetPixel(x, y, color);
                }
            }
            return ConvertFromSkBitmap(skBitmap);
        }

        public static Bitmap saturate(Bitmap bitmap, int percent)
        {
            SKBitmap skBitmap = ConvertToSKBitmap(bitmap);
            for (int y = 0; y < skBitmap.Height; y++)
            {
                for (int x = 0; x < skBitmap.Width; x++)
                {
                    SKColor color = SaturationHelper.getSaturatedColor(skBitmap.GetPixel(x, y), percent);
                    skBitmap.SetPixel(x, y, color);
                }
            }
            return ConvertFromSkBitmap(skBitmap);
        }

        public static Bitmap tintScale(Bitmap bitmap, string selection)
        {
            SKBitmap skBitmap = ConvertToSKBitmap(bitmap);
            for (int y = 0; y < skBitmap.Height; y++)
            {
                for (int x = 0; x < skBitmap.Width; x++)
                {
                    SKColor color = TintHelper.getTintScaleColor(skBitmap.GetPixel(x, y), selection);
                    skBitmap.SetPixel(x, y, color);
                }
            }
            return ConvertFromSkBitmap(skBitmap);
        }

        public static Bitmap tint(Bitmap bitmap, string selection, int value, bool isHard)
        {
            SKBitmap skBitmap = ConvertToSKBitmap(bitmap);
            for (int y = 0; y < skBitmap.Height; y++)
            {
                for (int x = 0; x < skBitmap.Width; x++)
                {
                    SKColor color = TintHelper.getTintColor(skBitmap.GetPixel(x, y), selection, value, isHard);
                    skBitmap.SetPixel(x, y, color);
                }
            }
            return ConvertFromSkBitmap(skBitmap);
        }

        public static Bitmap cropBitmap(Bitmap bitmap, int xStart, int yStart, int resizeWidth, int resizeHeight)
        {
            SKBitmap skBitmap = ConvertToSKBitmap(bitmap);
            SKRectI cropArea = new SKRectI(xStart, yStart, xStart + resizeWidth, yStart + resizeHeight);
            SKBitmap newImage = new SKBitmap(resizeWidth, resizeHeight);
            skBitmap.ExtractSubset(newImage, cropArea);
            return ConvertFromSkBitmap(newImage);
        }

        public static Bitmap pixelate(Bitmap bitmap)
        {
            SKBitmap skBitmap = ConvertToSKBitmap(bitmap);
            int x = 1, y = 1;
            while (x < skBitmap.Width && y < skBitmap.Height)
            {
                SKColor color = skBitmap.GetPixel(x, y);
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        try
                        {
                            skBitmap.SetPixel(x + i, y + j, color);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            // just handle the exception rather than try to account for the image not being divisible by 3
                        }
                    }
                }
                if (x + 3 < skBitmap.Width)
                {
                    x += 3;
                }
                else if (y + 3 < skBitmap.Height)
                {
                    y += 3;
                    x = 1;
                }
                else break;
            }

            return ConvertFromSkBitmap(skBitmap);
        }

        public static Bitmap scanlines(Bitmap bitmap)
        {
            SKBitmap skBitmap = ConvertToSKBitmap(bitmap);
            for (int y = 0; y < skBitmap.Height; y++)
            {
                for (int x = 0; x < skBitmap.Width; x++)
                {
                    SKColor color = skBitmap.GetPixel(x, y);
                    if (y % 2 == 0)
                    {
                        color = TintHelper.getTintColor(color, "Black < -- > White", 10, false);
                    }
                    else
                    {
                        color = TintHelper.getTintColor(color, "Black < -- > White", -10, false);
                    }
                    skBitmap.SetPixel(x, y, color);
                }
            }
            return ConvertFromSkBitmap(skBitmap);
        }

        public static Bitmap DrawPalette(string palette)
        {
            SKColor[] targetPalette = Constants.Palettes.TryGetValue(palette, out var p) ? p : [];
            SKBitmap bitmap = new SKBitmap(240, 192);
            int x = 0;
            int y = 0;
            // attempts to fill the preview area as much as possible based on the size of the palette
            // the total available pixels are 240*192 = 46,080, adjust side length to 8, 16, 24, 48 (common factors of 240 and 192)
            int rawTileSize = (int)Math.Sqrt(46080 / targetPalette.Length);
            int tileSize = rawTileSize > 48 ? 48 : (rawTileSize > 24 ? 24 : (rawTileSize > 16 ? 16 : (rawTileSize > 8 ? 8 : rawTileSize)));
            foreach (SKColor color in targetPalette)
            {
                // step down to next row if it would overflow the current row
                if (x + tileSize > 240)
                {
                    x = 0;
                    y += tileSize;
                }
                using (SKCanvas canvas = new SKCanvas(bitmap))
                {
                    SKColor skColor = new SKColor(color.Red, color.Green, color.Blue);
                    using (SKPaint paint = new SKPaint { Color = skColor, Style = SKPaintStyle.Fill })
                    {
                        canvas.DrawRect(new SKRect(x, y, x + tileSize, y + tileSize), paint);
                    }
                }
                x += tileSize;
            }
            return ConvertFromSkBitmap(bitmap);
        }

        private static Bitmap ConvertFromSkBitmap(SKBitmap skBitmap)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                using (var data = skBitmap.Encode(SKEncodedImageFormat.Png, 100))
                {
                    data.SaveTo(memory);
                }
                memory.Position = 0;
                return new Bitmap(memory);
            }
        }

        private static SKBitmap ConvertToSKBitmap(Bitmap bitmap)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bitmap.Save(memory);
                memory.Position = 0;
                return SKBitmap.Decode(memory);
            }
        }
    }
}
