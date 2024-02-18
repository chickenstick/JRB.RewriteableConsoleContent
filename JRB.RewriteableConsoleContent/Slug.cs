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
            if (text.Length > FixedLength)
                throw new ArgumentOutOfRangeException(nameof(text), "The length of the text is greater than the fixed length.");

            Console.SetCursorPosition(StartPosition.Left, StartPosition.Top);
            foreach (string s in GetStringParts(text, alignment))
            {
                Console.Write(s);
            }
        }

        public void Write(string text, Alignment alignment, bool returnCursorPosition)
        {
            if (returnCursorPosition)
            {
                var originalPos = Console.GetCursorPosition();
                Write(text, alignment);
                Console.SetCursorPosition(originalPos.Left, originalPos.Top);
            }
            else
            {
                Write(text, alignment);
            }
        }

        #endregion

        #region - Private Methods -

        private IEnumerable<string> GetStringParts(string text, Alignment alignment)
        {
            int diff = FixedLength - text.Length;
            if (alignment == Alignment.Right && diff > 0)
            {
                foreach (string s in Enumerable.Repeat(" ", diff))
                {
                    yield return s;
                }
            }

            yield return text;

            if (alignment == Alignment.Left && diff > 0)
            {
                foreach (string s in Enumerable.Repeat(" ", diff))
                {
                    yield return s;
                }
            }
        }

        #endregion

    }
}
