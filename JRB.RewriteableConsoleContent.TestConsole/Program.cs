using JRB.RewriteableConsoleContent;
using JRB.RewriteableConsoleContent.Colorization;

namespace JRB.RewriteableConsoleContent.TestConsole
{
    internal class Program
    {

        static void Main(string[] args)
        {
            const int FIELD_LENGTH = 3;

            foreach (int i in Enumerable.Range(0, 12))
            {
                WriteAlphabet();
            }

            List<IPlaceholder> lstPlaceholders = new List<IPlaceholder>()
            {
                new IncrementDisplay(1, FIELD_LENGTH),
                new IncrementDisplay(2, FIELD_LENGTH),
                new IncrementDisplay(3, FIELD_LENGTH),
                new IncrementDisplay(4, FIELD_LENGTH),
                new IncrementDisplay(5, FIELD_LENGTH),
                new ColorSplashSlug("Test1"),
                new ColorSplashSlug("Test2"),
                new ColorSplashSlug("Test3"),
                new ColorSplashSlug("Test4"),
                new ColorSplashSlug("Test5")
            };
            
            for (int i = 0; i < lstPlaceholders.Count; i++)
            {
                var beforePosition = Console.GetCursorPosition();
                lstPlaceholders[i].CreateHere();
                Console.WriteLine();
                var afterPosition = Console.GetCursorPosition();

                if (beforePosition.Top < afterPosition.Top)
                    continue;

                for (int j = 0; j <= i; j++)
                {
                    lstPlaceholders[j].ModifyPosition(top: -1);
                }
            }

            while (true)
            {
                Console.ReadKey(true);
                lstPlaceholders.ForEach(f => f.Increment());
            }
        }

        static void WriteAlphabet()
        {
            for (int i = (int)'a'; i <= (int)'z'; i++)
            {
                Console.WriteLine((char)i);
            }
        }

        interface IPlaceholder
        {
            void CreateHere();
            void Increment();
            void ModifyPosition(int top = 0, int left = 0);
        }

        class IncrementDisplay : IPlaceholder
        {

            private int _multiplier;
            private int _fieldLength;
            private int _baseValue;
            private Slug? _slug;

            public IncrementDisplay(int multiplier, int fieldLength)
            {
                _multiplier = multiplier;
                _fieldLength = fieldLength;
                _baseValue = 0;
            }

            public void CreateHere()
            {
                if (_slug != null)
                    throw new InvalidOperationException("Cannot create more than once.");

                Console.Write($"{_multiplier}x:  ");
                _slug = Slug.CreateHere(_fieldLength);

                string text = (_baseValue * _multiplier).ToString();
                UpdateSlugText(text, false);
            }

            public void Increment()
            {
                if (_slug == null)
                    throw new InvalidOperationException("Must be created before it can be incremented.");

                _baseValue += 1;
                string text = (_baseValue * _multiplier).ToString();
                UpdateSlugText(text, true);
            }

            public void ModifyPosition(int top = 0, int left = 0)
            {
                if (_slug != null)
                {
                    _slug.ModifyCursorPosition(top, left);
                }
            }

            private void UpdateSlugText(string text, bool returnCursorPosition)
            {
                string s = (text.Length > _slug?.FixedLength) ? text.Substring(text.Length - _slug.FixedLength) : text;
                _slug?.Write(s, Alignment.Right, returnCursorPosition);
            }

        }

        sealed class ColorSplashSlug : IPlaceholder
        {

            private string _originalText;
            private ITextProvider _textProvider;
            private Slug? _slug;

            public ColorSplashSlug(string originalText)
            {
                _originalText = originalText;

                ColorSplashBase colorSplash = ColorSplashFactory.Create(originalText, ColorSplashBehavior.Random, Console.ForegroundColor, GetColors(), 5);
                ITextProvider textProvider = new ColorSplashTextProvider(originalText, Alignment.Left, originalText.Length, colorSplash);
                _textProvider = textProvider;
            }

            private static IEnumerable<ConsoleColor> GetColors()
            {
                //yield return ConsoleColor.Yellow;
                //yield return ConsoleColor.Green;
                //yield return ConsoleColor.Red;
                //yield return ConsoleColor.Blue;
                //yield return ConsoleColor.Cyan;
                yield return ConsoleColor.White;
            }

            public void CreateHere()
            {
                if (_slug != null)
                    throw new InvalidOperationException("Cannot create more than once.");

                _slug = Slug.CreateHere(_originalText.Length);
                _slug.Write(_textProvider, false);
            }

            public void Increment()
            {
                if (_slug == null)
                    throw new InvalidOperationException("Must be created before it can be incremented.");

                _slug.Write(_textProvider, true);
            }

            public void ModifyPosition(int top = 0, int left = 0)
            {
                if (_slug != null)
                {
                    _slug.ModifyCursorPosition(top, left);
                }
            }

        }

        #region - Nested Classes -

        public sealed class FacialPlaceholder
        {

            private ColorSplashTextProvider _textProvider;
            private Slug _slug;

            private FacialPlaceholder(string text, ColorSplashTextProvider textProvider, Slug slug, int totalSplashes)
            {
                this.Text = text;
                this.TotalSplashes = totalSplashes;

                _textProvider = textProvider;
                _slug = slug;
            }

            public string Text { get; private set; }
            public int TotalSplashes { get; private set; }

            public static FacialPlaceholder CreateHere(string ladyName, int totalSplashes)
            {
                ColorSplashTextProvider textProvider = GetTextProvider(ladyName, totalSplashes);
                Slug slug = Slug.CreateHere(ladyName.Length);
                slug.Write(textProvider, false);

                return new FacialPlaceholder(ladyName, textProvider, slug, totalSplashes);
            }

            private static ColorSplashTextProvider GetTextProvider(string ladyName, int totalSplashes)
            {
                ColorSplashBase colorSplash = ColorSplashFactory.Create(ladyName, ColorSplashBehavior.Random, ConsoleColor.Gray, GetColors(), totalSplashes);
                return new ColorSplashTextProvider(ladyName, Alignment.Left, ladyName.Length, colorSplash);
            }

            private static IEnumerable<ConsoleColor> GetColors()
            {
                yield return ConsoleColor.White;
            }

            public bool IsDone() => _textProvider.AllCharactersColorSet();

            public void Increment()
            {
                _slug.Write(_textProvider, true);
            }

        }

        #endregion

    }
}
