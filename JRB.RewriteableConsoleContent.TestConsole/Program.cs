using JRB.RewriteableConsoleContent;

namespace JRB.RewriteableConsoleContent.TestConsole
{
    internal class Program
    {

        static void Main(string[] args)
        {
            const int FIELD_LENGTH = 3;
            List<IncrementDisplay> displays = new List<IncrementDisplay>()
            {
                new IncrementDisplay(1, FIELD_LENGTH),
                new IncrementDisplay(2, FIELD_LENGTH),
                new IncrementDisplay(3, FIELD_LENGTH),
                new IncrementDisplay(4, FIELD_LENGTH),
                new IncrementDisplay(5, FIELD_LENGTH)
            };

            displays.ForEach(d =>
            {
                d.CreateHere();
                Console.WriteLine();
            });

            Console.WriteLine("Press any key to increment the multipliers.");
            while (true)
            {
                Console.ReadKey(true);
                displays.ForEach(d => d.Increment());
            }
        }

        class IncrementDisplay
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

            private void UpdateSlugText(string text, bool returnCursorPosition)
            {
                string s = (text.Length > _slug?.FixedLength) ? text.Substring(text.Length - _slug.FixedLength) : text;
                _slug?.Write(s, Alignment.Right, returnCursorPosition);
            }

        }

    }
}
