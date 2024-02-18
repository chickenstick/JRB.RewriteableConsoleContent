using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JRB.RewriteableConsoleContent.Test
{

    [TestClass]
    public class SlugTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFixedLengthLessThanZer()
        {
            _ = new Slug(new ConsolePosition(0, 0), -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFixedLengthAtZero()
        {
            _ = new Slug(new ConsolePosition(0, 0), 0);
        }

        [TestMethod]
        public void TestFixedLengthGreaterThanZero()
        {
            _ = new Slug(new ConsolePosition(0, 0), 10);
        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentOutOfRangeException))]
        public void TestWriteTextLargerThan()
        {
            Slug slug = new Slug(new ConsolePosition(0, 0), 5);
            slug.Write("123456", Alignment.Left);
        }

    }

}
