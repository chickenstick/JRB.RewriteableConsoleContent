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
    public class FullTextColorSplashTests
    {

        #region - Fields -

        private List<ConsoleColor> _colors;

        #endregion

        #region - Constructor -

        public FullTextColorSplashTests()
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
            _ = new FullTextColorSplash(null, ConsoleColor.White, Enumerable.Empty<ConsoleColor>());
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestColorsEmpty()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = new FullTextColorSplash("", ConsoleColor.White, Enumerable.Empty<ConsoleColor>());
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestColorsNull()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = new FullTextColorSplash("", ConsoleColor.White, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        public void TestColorChanging()
        {
            const string SLUG_TEXT = "Test";

            ConsoleColor originalColor = Console.ForegroundColor;
            FullTextColorSplash x = new FullTextColorSplash(SLUG_TEXT, originalColor, _colors);

            ConsoleText cText = x.GetMainTextParts().First();
            Assert.AreEqual(SLUG_TEXT, cText.Text);
            Assert.AreEqual(originalColor, cText.Color);

            foreach (ConsoleColor color in _colors)
            {
                cText = x.GetMainTextParts().First();
                Assert.AreEqual(SLUG_TEXT, cText.Text);
                Assert.AreEqual(color, cText.Color);
            }
        }

        [TestMethod]
        public void TestAllCharactersColorSet()
        {
            const string SLUG_TEXT = "Test";

            ConsoleColor originalColor = Console.ForegroundColor;
            FullTextColorSplash x = new FullTextColorSplash(SLUG_TEXT, originalColor, _colors);

            Assert.IsFalse(x.AllCharactersColorSet());

            _ = x.GetMainTextParts().First();
            Assert.IsTrue(x.AllCharactersColorSet());
        }

        #endregion

    }
}
