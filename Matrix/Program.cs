using System;

namespace Matrix
{
    public static class Program
    {
        public static bool ShouldRestart;
        [STAThread]
        public static void Main()
        {
            do
            {
                ShouldRestart = false;
                using (var game = new Game1())
                    game.Run();
            }
            while (ShouldRestart);
        }
    }
}