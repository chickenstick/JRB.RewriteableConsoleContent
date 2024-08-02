using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRB.RewriteableConsoleContent.Colorization
{
    public class SequentialColorSplash : ColorSplashBase
    {

        #region - Fields -

        private int _index;

        #endregion

        #region - Constructor -

        public SequentialColorSplash(string originalText, ConsoleColor initialColor, IEnumerable<ConsoleColor> colors)
            : base(originalText, initialColor, colors)
        {
            _index = 0;
        }

        #endregion

        #region - Properties -

        protected override bool TrackIndividualChars => true;
        public override ColorSplashBehavior Behavior => ColorSplashBehavior.Sequential;

        #endregion

        #region - Public Methods -

        public override IEnumerable<ConsoleText> GetMainTextParts()
        {
            if (_writtenOnce)
            {
                UpdateIndexColor();

                foreach (ConsoleText ct in GetConsoleTextByChar())
                {
                    yield return ct;
                }
            }
            else
            {
                _writtenOnce = true;
                yield return new ConsoleText(OriginalText, InitialColor);
            }
        }

        public override bool AllCharactersColorSet() => AllCharactersColorSetForIndividualTrackedChars();

        #endregion

        #region - Protected Methods -

        protected void UpdateIndexColor()
        {
            if (!IndexColors.Any())
                return;

            if (_index >= IndexColors.Count)
            {
                _index = 0;
            }

            IndexColors[_index].Color = GetNextColor();
            _index += 1;
        }

        #endregion

    }
}
