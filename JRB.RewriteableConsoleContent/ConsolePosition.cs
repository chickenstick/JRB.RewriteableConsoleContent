using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRB.RewriteableConsoleContent
{
    public struct ConsolePosition
    {

        #region - Constructor -

        public ConsolePosition(int top, int left)
        {
            if (top < 0)
                throw new ArgumentOutOfRangeException(nameof(top), "Must be greater than or equal to zero.");

            if (left < 0)
                throw new ArgumentOutOfRangeException(nameof(left), "Must be greater than or equal to zero.");

            this.Top = top;
            this.Left = left;
        }

        #endregion

        #region - Properties -

        public int Top { get; private set; }
        public int Left { get; private set; }

        #endregion

        #region - Static Methods -

        public static ConsolePosition CreateHere()
        {
            var pos = Console.GetCursorPosition();
            return new ConsolePosition(pos.Top, pos.Left);
        }

        #endregion

    }
}
