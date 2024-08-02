using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using JRB.RewriteableConsoleContent.Colorization;

namespace JRB.RewriteableConsoleContent.Test
{
    [TestClass]
    public class ColorSplashTextProviderTests
    {

        #region - Fields -

        private List<ConsoleColor> _colors;

        #endregion

        #region - Constructor -

        public ColorSplashTextProviderTests()
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructorOriginalTextNull()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            FullTextColorSplash colorSplash = new FullTextColorSplash("", Console.ForegroundColor, _colors);
            _ = new ColorSplashTextProvider(null, Alignment.Left, 1, colorSplash);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConstructorFixedLengthLessThanLengthOfOriginalText()
        {
            FullTextColorSplash colorSplash = new FullTextColorSplash("", Console.ForegroundColor, _colors);
            _ = new ColorSplashTextProvider("Test", Alignment.Left, 3, colorSplash);
        }

        [TestMethod]
        public void TestConstructorFixedLengthEqualsLengthOfOriginalText()
        {
            // This should NOT throw an exception.
            FullTextColorSplash colorSplash = new FullTextColorSplash("", Console.ForegroundColor, _colors);
            _ = new ColorSplashTextProvider("Test", Alignment.Left, 4, colorSplash);
        }

        [TestMethod]
        public void TestAllCharactersColorSet()
        {
            DefaultTextProvider p = new DefaultTextProvider("Test", Alignment.Left, 5);
            Assert.IsFalse(p.AllCharactersColorSet());
        }

        //[TestMethod]
        //public void TestGetFullTextColorSplashProvider()
        //{
        //    ColorSplashTextProvider provider = new ColorSplashTextProvider("Test", Alignment.Left, 5, ColorSplashBehavior.FullText, Console.ForegroundColor, _colors, null);
        //    Assert.AreEqual(ColorSplashBehavior.FullText, provider.ColorSplash.Behavior);
        //}

        //[TestMethod]
        //public void TestGetSequentialColorSplashProvider()
        //{
        //    ColorSplashTextProvider provider = new ColorSplashTextProvider("Test", Alignment.Left, 5, ColorSplashBehavior.Sequential, Console.ForegroundColor, _colors, null);
        //    Assert.AreEqual(ColorSplashBehavior.Sequential, provider.ColorSplash.Behavior);
        //}

        //[TestMethod]
        //public void TestGetRandomColorSplashProvider()
        //{
        //    ColorSplashTextProvider provider = new ColorSplashTextProvider("Test", Alignment.Left, 5, ColorSplashBehavior.Random, Console.ForegroundColor, _colors, 2);
        //    Assert.AreEqual(ColorSplashBehavior.Random, provider.ColorSplash.Behavior);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void TestGetRandomColorSplashProviderWithoutTurnsParam()
        //{
        //    _ = new ColorSplashTextProvider("Test", Alignment.Left, 5, ColorSplashBehavior.Random, Console.ForegroundColor, _colors, null);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //public void TestGetRandomColorSplashProviderWithNegativeTurnsParam()
        //{
        //    ColorSplashTextProvider provider = new ColorSplashTextProvider("Test", Alignment.Left, 5, ColorSplashBehavior.Random, Console.ForegroundColor, _colors, -1);
        //    Assert.AreEqual(ColorSplashBehavior.Random, provider.ColorSplash.Behavior);
        //}

        #endregion

    }
}
