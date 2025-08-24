using Microsoft.VisualStudio.TestTools.UnitTesting;
using pixel8r;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel8rtests
{
    [TestClass()]
    public class PaletteProgrammaticTests
    {
        [TestMethod()]
        public void testSingleSaturation()
        {
            Color saturated = PaletteProgrammaticFunctions.getProgrammaticColor(Color.FromArgb(191, 64, 64), "Saturate");
            Assert.AreEqual(saturated, Color.FromArgb(197, 58, 58));
        }

        [TestMethod()]
        public void testFullySaturatedNoChange()
        {
            Color saturated = PaletteProgrammaticFunctions.getProgrammaticColor(Color.FromArgb(255, 0, 0), "Saturate");
            Assert.AreEqual(saturated, Color.FromArgb(255, 0, 0));
        }
    }
}