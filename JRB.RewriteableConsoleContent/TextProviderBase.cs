using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRB.RewriteableConsoleContent
{
    public abstract class TextProviderBase : ITextProvider
    {

        #region - Constructor -

        public TextProviderBase(string originalText, Alignment textAlignment, int fixedLength)
        {
            if (originalText == null)
                throw new ArgumentNullException(nameof(originalText));

            if (fixedLength < originalText.Length)
                throw new ArgumentOutOfRangeException(nameof(fixedLength), $"The value of {nameof(fixedLength)} ({fixedLength}) is less than the length of the {nameof(originalText)} parameter ({originalText.Length}).");

            OriginalText = originalText;
            TextAlignment = textAlignment;
            FixedLength = fixedLength;
        }

        #endregion

        #region - Properties -

        public string OriginalText { get; private set; }
        public Alignment TextAlignment { get; private set; }
        public int FixedLength { get; private set; }
        public ConsoleColor DefaultTextColor => ConsoleColor.Gray;

        #endregion

        #region - Public Methods -

        public IEnumerable<ConsoleText> GetStringParts()
        {
            int diff = FixedLength - OriginalText.Length;
            if (TextAlignment == Alignment.Right && diff > 0)
            {
                yield return GetWhitespace(diff);
            }

            foreach (ConsoleText ct in GetMainTextParts())
            {
                yield return ct;
            }

            if (TextAlignment == Alignment.Left && diff > 0)
            {
                yield return GetWhitespace(diff);
            }
        }

        public abstract bool AllCharactersColorSet();

        #endregion

        #region - Protected Methods -

        protected abstract IEnumerable<ConsoleText> GetMainTextParts();

        #endregion

        #region - Private Methods -

        private ConsoleText GetWhitespace(int diff)
        {
            string whitespace = new string(' ', diff);
            return new ConsoleText(whitespace, DefaultTextColor);
        }

        #endregion

    }
}
