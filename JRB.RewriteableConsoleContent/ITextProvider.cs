using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRB.RewriteableConsoleContent
{
    public interface ITextProvider
    {
        string OriginalText { get; }
        Alignment TextAlignment { get; }
        int FixedLength { get; }

        IEnumerable<ConsoleText> GetStringParts();
    }
}
