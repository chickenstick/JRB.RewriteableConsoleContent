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
    public class DefaultTextProviderTests
    {

        #region - Tests -

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructorOriginalTextNull()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = new DefaultTextProvider(null, Alignment.Left, 1);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConstructorFixedLengthLessThanLengthOfOriginalText()
        {
            _ = new DefaultTextProvider("Test", Alignment.Left, 3);
        }

        [TestMethod]
        public void TestConstructorFixedLengthEqualsLengthOfOriginalText()
        {
            // This should NOT throw an exception.
            _ = new DefaultTextProvider("Test", Alignment.Left, 4);
        }

        [TestMethod]
        public void TestAllCharactersColorSet()
        {
            DefaultTextProvider p = new DefaultTextProvider("Test", Alignment.Left, 5);
            Assert.IsFalse(p.AllCharactersColorSet());
        }

        #endregion

    }
}
