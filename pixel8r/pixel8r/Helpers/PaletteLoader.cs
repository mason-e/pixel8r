using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SkiaSharp;

namespace pixel8r.Helpers
{
    public class PaletteLoader 
    {
        private static readonly string palettesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Palettes");

        public static List<string> getPalettes() 
        {
            if (!Directory.Exists(palettesFolder))
                return new List<string>
                {
                    "ERROR - Palette folder not found"
                };
            
            return Directory.GetFiles(palettesFolder)
                .Select(file => Path.GetFileNameWithoutExtension(file))
                .ToList();
        }

        public static void setCurrentPalette(string paletteName)
        {
            string palettePath = Path.Combine(palettesFolder, paletteName + ".hex");

            if (!File.Exists(palettePath))
                throw new FileNotFoundException($"Palette file '{palettePath}' not found.");

            List<SKColor> colors = new List<SKColor>();
            foreach (var line in File.ReadAllLines(palettePath))
            {
                colors.Add(SKColor.Parse(line));
            }

            GlobalVars.CurrentPalette = colors;
        }
    }
}