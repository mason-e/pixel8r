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

        public static Bitmap paletteSwapPredefined(Bitmap bitmap, string algorithm, bool fastMode)
        {
            SKBitmap skBitmap = ConvertToSKBitmap(bitmap);
            for (int y = 0; y < skBitmap.Height; y++)
            {
                for (int x = 0; x < skBitmap.Width; x++)
                {
                    SKColor color = skBitmap.GetPixel(x, y);
                    if (fastMode && algorithm != "RGB Euclidean" && algorithm != "RGB Redmean")
                    {
                        color = ReduceFidelityHelper.getReducedColor(color, "18 Bit RGB");
                    }
                    color = PaletteMatchingHelper.getMatchedColor(color, algorithm);
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

        public static Bitmap pixelate(Bitmap bitmap, int pixelSize)
        {
            SKBitmap skBitmap = ConvertToSKBitmap(bitmap);
            int x = 0, y = 0;
            while (x < skBitmap.Width && y < skBitmap.Height)
            {
                int xToEdge = Math.Min(pixelSize, skBitmap.Width - x);
                int yToEdge = Math.Min(pixelSize, skBitmap.Height - y);
                SKColor color = PixelationHelper.getAverageColor(skBitmap, x, y, xToEdge, yToEdge);
                for (int i = 0; i < xToEdge; i++)
                {
                    for (int j = 0; j < yToEdge; j++)
                    {
                        skBitmap.SetPixel(x + i, y + j, color);
                    }
                }
                if (x + pixelSize < skBitmap.Width)
                {
                    x += pixelSize;
                }
                else if (y + pixelSize < skBitmap.Height)
                {
                    y += pixelSize;
                    x = 0;
                }
                else break;
            }

            return ConvertFromSkBitmap(skBitmap);
        }

        public static Bitmap scanlines(Bitmap bitmap, int scanlineHeight)
        {
            SKBitmap skBitmap = ConvertToSKBitmap(bitmap);
            for (int y = 0; y < skBitmap.Height; y++)
            {
                for (int x = 0; x < skBitmap.Width; x++)
                {
                    SKColor color = skBitmap.GetPixel(x, y);
                    if (y % scanlineHeight == 0)
                    {
                        color = TintHelper.getTintColor(color, "Black < -- > White", -10 * scanlineHeight, false);
                    }
                    else
                    {
                        color = TintHelper.getTintColor(color, "Black < -- > White", 10, false);
                    }
                    skBitmap.SetPixel(x, y, color);
                }
            }
            return ConvertFromSkBitmap(skBitmap);
        }

        public static Bitmap DrawPalette()
        {
            SKBitmap bitmap = new SKBitmap(240, 192);
            int x = 0;
            int y = 0;
            // attempts to fill the preview area as much as possible based on the size of the palette
            // the total available pixels are 240*192 = 46,080, adjust side length to 8, 16, 24, 48 (common factors of 240 and 192)
            int rawTileSize = (int)Math.Sqrt(46080 / GlobalVars.CurrentPalette.Count);
            int tileSize = rawTileSize > 48 ? 48 : (rawTileSize > 24 ? 24 : (rawTileSize > 16 ? 16 : (rawTileSize > 8 ? 8 : rawTileSize)));
            foreach (SKColor color in GlobalVars.CurrentPalette)
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
