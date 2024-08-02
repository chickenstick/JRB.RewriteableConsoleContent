using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRB.RewriteableConsoleContent
{
    public sealed class ConsoleText
    {

        #region - Constructor -

        public ConsoleText(string text, ConsoleColor color)
        {
            Text = text;
            Color = color;
        }

        #endregion

        #region - Properties -

        public string Text { get; private set; }
        public ConsoleColor Color { get; private set; }

        #endregion

    }
}
