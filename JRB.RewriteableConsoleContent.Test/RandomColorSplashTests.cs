using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using JRB.RewriteableConsoleContent;
using JRB.RewriteableConsoleContent.Colorization;

namespace JRB.RewriteableConsoleContent.Test
{
    [TestClass]
    public class RandomColorSplashTests
    {

        #region - Fields -

        private List<ConsoleColor> _colors;

        #endregion

        #region - Constructor -

        public RandomColorSplashTests()
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
        public void TestOriginalTextNull()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = new RandomColorSplash(null, ConsoleColor.White, Enumerable.Empty<ConsoleColor>(), 2);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestColorsEmpty()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = new RandomColorSplash("", ConsoleColor.White, Enumerable.Empty<ConsoleColor>(), 2);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestColorsNull()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = new RandomColorSplash("", ConsoleColor.White, null, 2);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestTurnsAtZero()
        {
            _ = new RandomColorSplash("Test", ConsoleColor.White, _colors, 0);
        }

        [TestMethod]
        public void TestAllCharactersColorSet()
        {
            const string SLUG_TEXT = "Test";
            const int TURNS = 2;

            ConsoleColor originalColor = Console.ForegroundColor;
            RandomColorSplash x = new RandomColorSplash(SLUG_TEXT, originalColor, _colors, TURNS);

            ConsoleText cText = x.GetMainTextParts().First();
            Assert.AreEqual(SLUG_TEXT, cText.Text);
            Assert.AreEqual(originalColor, cText.Color);

            for (int i = 0; i < TURNS; i++)
            {
                _ = x.GetMainTextParts().First();
                if (i == TURNS - 1)
                {
                    Assert.IsTrue(x.AllCharactersColorSet());
                }
                else
                {
                    Assert.IsFalse(x.AllCharactersColorSet());
                }
            }
        }

        #endregion

    }
}
