namespace JRB.RewriteableConsoleContent
{
    /// <summary>
    /// The re-writeable portion of text.
    /// </summary>
    public sealed class Slug
    {

        #region - Constructors -

        public Slug(ConsolePosition startPosition, int fixedLength)
        {
            if (fixedLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(fixedLength), "The fixed length must be greater than zero.");

            this.StartPosition = startPosition;
            this.FixedLength = fixedLength;
        }

        #endregion

        #region - Static Methods -

        public static Slug CreateHere(int fixedLength)
        {
            ConsolePosition here = ConsolePosition.CreateHere();
            return new Slug(here, fixedLength);
        }

        #endregion

        #region - Properties -

        public ConsolePosition StartPosition { get; set; }
        public int FixedLength { get; set; }

        #endregion

        #region - Public Methods -

        public void Write(string text, Alignment alignment)
        {
            DefaultTextProvider textProvider = new DefaultTextProvider(text, alignment, FixedLength);
            Write(textProvider);
        }

        public void Write(string text, Alignment alignment, bool returnCursorPosition)
        {
            DefaultTextProvider textProvider = new DefaultTextProvider(text, alignment, FixedLength);
            Write(textProvider, returnCursorPosition);
        }

        public void Write(ITextProvider textProvider)
        {
            if (textProvider.OriginalText.Length > FixedLength)
                throw new ArgumentOutOfRangeException(nameof(textProvider.OriginalText), "The length of the text is greater than the fixed length.");

            Console.SetCursorPosition(StartPosition.Left, StartPosition.Top);

            ConsoleColor original = Console.ForegroundColor;
            foreach (ConsoleText cText in textProvider.GetStringParts())
            {
                Console.ForegroundColor = cText.Color;
                Console.Write(cText.Text);
            }
            Console.ForegroundColor = original;
        }

        public void Write(ITextProvider textProvider, bool returnCursorPosition)
        {
            if (returnCursorPosition)
            {
                var originalPos = Console.GetCursorPosition();
                Write(textProvider);
                Console.SetCursorPosition(originalPos.Left, originalPos.Top);
            }
            else
            {
                Write(textProvider);
            }
        }

        #endregion

    }
}
