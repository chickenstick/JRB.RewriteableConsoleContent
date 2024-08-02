using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JRB.RewriteableConsoleContent.Colorization;

namespace JRB.RewriteableConsoleContent.Test
{
    [TestClass]
    public class ColorSplashFactoryTests
    {

        #region - Fields -

        private List<ConsoleColor> _colors;

        #endregion

        #region - Constructor -

        public ColorSplashFactoryTests()
        {
            _colors = new List<ConsoleColor>()
            {
                ConsoleColor.Yellow,
                ConsoleColor.Green,
                ConsoleColor.Red,
                ConsoleColor.Blue,
                ConsoleColor.Cyan
            };
        }

        #endregion

        #region - Tests -

        [TestMethod]
        public void TestGetFullTextColorSplashProvider()
        {
            ColorSplashBase colorSplash = ColorSplashFactory.Create("Test", ColorSplashBehavior.FullText, Console.ForegroundColor, _colors, null);
            Assert.IsInstanceOfType<FullTextColorSplash>(colorSplash);
        }

        [TestMethod]
        public void TestGetSequentialColorSplashProvider()
        {
            ColorSplashBase colorSplash = ColorSplashFactory.Create("Test", ColorSplashBehavior.Sequential, Console.ForegroundColor, _colors, null);
            Assert.IsInstanceOfType<SequentialColorSplash>(colorSplash);
        }

        [TestMethod]
        public void TestGetRandomColorSplashProvider()
        {
            ColorSplashBase colorSplash = ColorSplashFactory.Create("Test", ColorSplashBehavior.Random, Console.ForegroundColor, _colors, 2);
            Assert.IsInstanceOfType<RandomColorSplash>(colorSplash);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetRandomColorSplashProviderWithoutTurnsParam()
        {
            _ = ColorSplashFactory.Create("Test", ColorSplashBehavior.Random, Console.ForegroundColor, _colors, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetRandomColorSplashProviderWithNegativeTurnsParam()
        {
            ColorSplashBase colorSplash = ColorSplashFactory.Create("Test", ColorSplashBehavior.Random, Console.ForegroundColor, _colors, -1);
        }

        #endregion

    }
}
