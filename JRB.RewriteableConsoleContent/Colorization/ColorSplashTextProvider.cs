using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRB.RewriteableConsoleContent.Colorization
{
    public class ColorSplashTextProvider : TextProviderBase
    {

        #region - Fields -

        protected ColorSplashBase _colorSplash;

        #endregion

        #region - Constructors -

        public ColorSplashTextProvider(string originalText, Alignment textAlignment, int fixedLength, ColorSplashBehavior behavior, ConsoleColor initialColor, 
            IEnumerable<ConsoleColor> colors, int? turnsUntilAllHaveColor)
            : base(originalText, textAlignment, fixedLength)
        {
            _colorSplash = ColorSplashFactory.Create(originalText, behavior, initialColor, colors, turnsUntilAllHaveColor);
        }

        public ColorSplashTextProvider(string originalText, Alignment textAlignment, int fixedLength, ColorSplashBase colorSplash)
            : base(originalText, textAlignment, fixedLength)
        {
            if (colorSplash == null)
                throw new ArgumentNullException(nameof(colorSplash));

            _colorSplash = colorSplash;
        }

        #endregion

        #region - Properties -

        public ColorSplashBase ColorSplash => _colorSplash;

        #endregion

        #region - Public Methods -

        public override bool AllCharactersColorSet() => _colorSplash.AllCharactersColorSet();

        #endregion

        #region - Protected Methods -

        protected override IEnumerable<ConsoleText> GetMainTextParts() => _colorSplash.GetMainTextParts();

        #endregion

    }
}
