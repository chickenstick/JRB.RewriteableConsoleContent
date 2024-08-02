using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRB.RewriteableConsoleContent
{
    public class DefaultTextProvider : TextProviderBase
    {

        #region - Constructor -

        public DefaultTextProvider(string originalText, Alignment textAlignment, int fixedLength)
            : base(originalText, textAlignment, fixedLength)
        {
        }

        #endregion

        #region - Public Methods -

        public override bool AllCharactersColorSet() => false;

        #endregion

        #region - Protected Methods -

        protected override IEnumerable<ConsoleText> GetMainTextParts()
        {
            yield return new ConsoleText(OriginalText, base.DefaultTextColor);
        }

        #endregion

    }
}
