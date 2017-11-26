using System;
using System.Linq;
using System.Collections;
namespace KartyMono
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Run();
        }
        

        [STAThread]
        private static void Run()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
