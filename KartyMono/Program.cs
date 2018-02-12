using System;
using System.Linq;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using ClientSerwis;
using System.ServiceModel;
using System.Threading;
using ks = KartyMono.ServiceReference1;
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
            Task.Factory.StartNew(() => RunDifrent(0));
            GetIQClinet();
            RunDifrent(2);
        }

        private static void GetIQClinet()
        {
           
            Task.Factory.StartNew(() =>
            {
                AppDomain ap = AppDomain.CreateDomain("Serwis");
                ap.DoCallBack(() =>
                {
                    KontrolerTysioc kontroler = new KontrolerTysioc();
                    kontroler.LisenAboutSelfMove = false;
                    InstanceContext instance = new InstanceContext(kontroler);
                    var client = new TysiocClient(instance);
                    kontroler.Initialize(client); ks.DoKontaClient dk = new ks.DoKontaClient();
                    int IdConection = dk.Rejestruj(new ks.Urzytkownik() { Nazwa = Guid.NewGuid().ToString(), Haslo = "bardzo trudne" });

                    Iq1000Klient iq = new Iq1000Klient(client, kontroler, 3, "zap",IdConection);
                });
            });
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
