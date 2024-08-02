using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRB.RewriteableConsoleContent.Colorization
{
    public static class ColorSplashFactory
    {

        #region - Public Methods -

        public static ColorSplashBase Create(string originalText, ColorSplashBehavior behavior, ConsoleColor initialColor, IEnumerable<ConsoleColor> colors, int? turnsUntilAllHaveColor = null)
        {
            return behavior switch
            {
                ColorSplashBehavior.FullText => new FullTextColorSplash(originalText, initialColor, colors),
                ColorSplashBehavior.Sequential => new SequentialColorSplash(originalText, initialColor, colors),
                ColorSplashBehavior.Random => turnsUntilAllHaveColor switch
                {
                    not null => new RandomColorSplash(originalText, initialColor, colors, turnsUntilAllHaveColor.Value),
                    _ => throw new ArgumentNullException(nameof(turnsUntilAllHaveColor))
                },
                _ => throw new ArgumentOutOfRangeException(nameof(behavior), $"{nameof(behavior)} has value {behavior}, which is not supported.")
            };
        }

        #endregion

    }
}
