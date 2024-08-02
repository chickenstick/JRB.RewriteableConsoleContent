using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRB.RewriteableConsoleContent.Colorization
{
    public abstract class ColorSplashBase
    {

        #region - Fields -

        protected bool _writtenOnce;
        private int _colorIndex;

        private IReadOnlyList<IndexColor> _indexColors;

        #endregion

        #region - Constructor -

        public ColorSplashBase(string originalText, ConsoleColor initialColor, IEnumerable<ConsoleColor> colors)
        {
            if (originalText == null)
                throw new ArgumentNullException(nameof(originalText));

            if (colors == null)
                throw new ArgumentNullException(nameof(colors));

            List<ConsoleColor> lstColors = colors.ToList();
            if (!lstColors.Any())
                throw new ArgumentException(nameof(colors));

            _writtenOnce = false;
            this.InitialColor = initialColor;
            this.Colors = lstColors;
            _colorIndex = 0;
            OriginalText = originalText;

            if (TrackIndividualChars)
            {
                _indexColors = GetNonWhitespaceIndexColors().ToList();
            }
            else
            {
                _indexColors = new List<IndexColor>();
            }
        }

        #endregion

        #region - Properties -

        public ConsoleColor InitialColor { get; private set; }
        public IReadOnlyList<ConsoleColor> Colors { get; private set; }
        public string OriginalText { get; private set; }

        protected abstract bool TrackIndividualChars { get; }
        protected IReadOnlyList<IndexColor> IndexColors
        {
            get
            {
                if (!TrackIndividualChars)
                    throw new NotSupportedException("This type does not track individual characters.");

                return _indexColors;
            }
        }

        public abstract ColorSplashBehavior Behavior { get; }

        #endregion

        #region - Public Methods -

        public abstract IEnumerable<ConsoleText> GetMainTextParts();

        public abstract bool AllCharactersColorSet();

        #endregion

        #region - Protected Methods -

        protected ConsoleColor GetNextColor()
        {
            if (!_writtenOnce)
            {
                _writtenOnce = true;
                return InitialColor;
            }

            ConsoleColor color = Colors[_colorIndex];
            _colorIndex += 1;
            if (_colorIndex >= Colors.Count)
            {
                _colorIndex = 0;
            }
            return color;
        }

        protected IEnumerable<ConsoleText> GetConsoleTextByChar()
        {
            for (int i = 0; i < OriginalText.Length; i++)
            {
                string s = OriginalText[i].ToString();
                ConsoleColor color = FindConsoleColor(i);
                yield return new ConsoleText(s, color);
            }
        }

        protected ConsoleColor FindConsoleColor(int index)
        {
            IndexColor? indexColor = IndexColors.Where(ic => ic.Index == index).FirstOrDefault();
            return indexColor?.Color ?? InitialColor;
        }

        protected bool AllCharactersColorSetForIndividualTrackedChars()
        {
            if (!_writtenOnce)
                return false;

            return _indexColors.All(c => c.Color != InitialColor);
        }

        #endregion

        #region - Private Methods -

        private IEnumerable<IndexColor> GetNonWhitespaceIndexColors()
        {
            for (int i = 0; i < OriginalText.Length; i++)
            {
                char c = OriginalText[i];
                if (!char.IsWhiteSpace(c))
                {
                    yield return new IndexColor(i, InitialColor);
                }
            }
        }

        #endregion

        #region - Nested Classes -

        protected class IndexColor
        {

            public IndexColor(int index, ConsoleColor color)
            {
                this.Index = index;
                this.Color = color;
            }

            public int Index { get; private set; }
            public ConsoleColor Color { get; set; }

        }

        #endregion

    }
}
