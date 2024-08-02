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
    public class SequentialColorSplashTests
    {

        #region - Fields -

        private List<ConsoleColor> _colors;

        #endregion

        #region - Constructor -

        public SequentialColorSplashTests()
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
            _ = new SequentialColorSplash(null, ConsoleColor.White, Enumerable.Empty<ConsoleColor>());
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestColorsEmpty()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = new SequentialColorSplash("", ConsoleColor.White, Enumerable.Empty<ConsoleColor>());
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestColorsNull()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = new SequentialColorSplash("", ConsoleColor.White, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        public void TestColorChanging()
        {
            const string SLUG_TEXT = "Test";

            ConsoleColor originalColor = Console.ForegroundColor;
            SequentialColorSplash x = new SequentialColorSplash(SLUG_TEXT, originalColor, _colors);

            ConsoleText cText = x.GetMainTextParts().First();
            Assert.AreEqual(SLUG_TEXT, cText.Text);
            Assert.AreEqual(originalColor, cText.Color);

            for (int i = 0; i < SLUG_TEXT.Length; i++)
            {
                List<ConsoleText> texts = x.GetMainTextParts().ToList();
                Assert.AreEqual(1, texts[i].Text.Length);
                Assert.AreEqual(SLUG_TEXT[i], texts[i].Text[0]);
                Assert.AreEqual(_colors[i], texts[i].Color);
            }
        }

        [TestMethod]
        public void TestAllCharactersColorSet()
        {
            const string SLUG_TEXT = "Test";

            ConsoleColor originalColor = Console.ForegroundColor;
            SequentialColorSplash x = new SequentialColorSplash(SLUG_TEXT, originalColor, _colors);

            Assert.IsFalse(x.AllCharactersColorSet());
            _ = x.GetMainTextParts().First();
            Assert.IsFalse(x.AllCharactersColorSet());

            for (int i = 0; i < SLUG_TEXT.Length; i++)
            {
                _ = x.GetMainTextParts().First();
            }
            Assert.IsTrue(x.AllCharactersColorSet());
        }

        #endregion

    }
}
