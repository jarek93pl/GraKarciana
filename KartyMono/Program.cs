using System;
using System.Linq;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;

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
        [STAThread]
        static void Main(string[] st)
        {
             Parallel.For(0, 3, (x) => RunDifrent(x));
        }
        

        [STAThread]
        private static void RunDifrent(int i)
        {
            AppDomain ap = AppDomain.CreateDomain(i.ToString());
            ap.DoCallBack(() =>
            {
            using (var game = new Game1())
                game.Run();
            }
            );
        }
        
    }
#endif
}
