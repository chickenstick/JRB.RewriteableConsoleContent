using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRB.RewriteableConsoleContent.Colorization
{
    public class FullTextColorSplash : ColorSplashBase
    {

        #region - Fields -

        #endregion

        #region - Constructor -

        public FullTextColorSplash(string originalText, ConsoleColor initialColor, IEnumerable<ConsoleColor> colors)
            : base(originalText, initialColor, colors)
        {
        }

        #endregion

        #region - Properties -

        protected override bool TrackIndividualChars => false;
        public override ColorSplashBehavior Behavior => ColorSplashBehavior.FullText;

        #endregion

        #region - Public Methods -

        public override IEnumerable<ConsoleText> GetMainTextParts()
        {
            ConsoleColor color = GetNextColor();
            yield return new ConsoleText(OriginalText, color);
        }

        public override bool AllCharactersColorSet()
        {
            return _writtenOnce;
        }

        #endregion

    }
}
