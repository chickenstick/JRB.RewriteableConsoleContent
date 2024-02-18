using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JRB.RewriteableConsoleContent.Test
{
    [TestClass]
    public class ConsolePositionTest
    {

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void TestTopLessThanZero()
        {
            new ConsolePosition(-1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestLeftLessThanZero()
        {
            new ConsolePosition(0, -1);
        }

        [TestMethod]
        public void TestTopAndLeftAreZero()
        {
            new ConsolePosition(0, 0);
        }

    }
}