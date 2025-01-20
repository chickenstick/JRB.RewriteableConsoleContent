using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRB.RewriteableConsoleContent.Colorization
{
    public class RandomColorSplash : ColorSplashBase
    {

        #region - Fields -

        protected int _charsPerTurn;
        protected Queue<List<IndexColor>> _queueOfIndexColors;

        #endregion

        #region - Constructor -

        public RandomColorSplash(string originalText, ConsoleColor initialColor, IEnumerable<ConsoleColor> colors, int turnsUntilAllHaveColor)
            : base(originalText, initialColor, colors)
        {
            if (turnsUntilAllHaveColor <= 0)
                throw new ArgumentOutOfRangeException(nameof(turnsUntilAllHaveColor), "Must be greater than 0.");

            _charsPerTurn = IndexColors.Count / turnsUntilAllHaveColor;
            if (IndexColors.Count % turnsUntilAllHaveColor > 0)
            {
                _charsPerTurn += 1;
            }

            _queueOfIndexColors = GetQueue();
        }

        #endregion

        #region - Properties -

        protected override bool TrackIndividualChars => true;
        public override ColorSplashBehavior Behavior => ColorSplashBehavior.Random;

        #endregion

        #region - Public Methods -

        public override IEnumerable<ConsoleText> GetMainTextParts()
        {
            if (_writtenOnce)
            {
                UpdateIndexColors();

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

        protected Queue<List<IndexColor>> GetQueue()
        {
            if (!IndexColors.Any())
                return new Queue<List<IndexColor>>();

            IndexColor[] array = [.. IndexColors];
            Random.Shared.Shuffle(array);

            Queue<List<IndexColor>> queue = new Queue<List<IndexColor>>();
            for (int i = 0; i < array.Length; i += _charsPerTurn)
            {
                queue.Enqueue(array.Skip(i).Take(_charsPerTurn).ToList());
            }
            return queue;
        }

        protected void UpdateIndexColors()
        {
            if (!IndexColors.Any())
                return;

            if (!_queueOfIndexColors.Any())
            {
                _queueOfIndexColors = GetQueue();
            }
            List<IndexColor> list = _queueOfIndexColors.Dequeue();

            ConsoleColor nextColor = GetNextColor();
            foreach (IndexColor ic in list)
            {
                ic.Color = nextColor;
            }
        }

        #endregion

    }
}
