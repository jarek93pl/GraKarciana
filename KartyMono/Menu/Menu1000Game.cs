using ks= KartyMono.ServiceReference1;
using KartyMono.Common.UI;
using KartyMono.Game1000;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ClientSerwis;
namespace KartyMono.Menu
{
    class Menu1000Game: MenuGame
    {



        public Menu1000Game(ContentManager cm):base(cm)
        {

        }
        Proxy proxy;
        TysiocClient client;
        int IdConection;
#region Load
        private void LoadConection()
        {
            ks.DoKontaClient dk = new ks.DoKontaClient();
            var kontroler = new KontrolerTysioc();
            InstanceContext instance = new InstanceContext(kontroler);
            client = new TysiocClient(instance);
            kontroler.Initialize(client);
            IdConection = dk.Rejestruj(new ks.Urzytkownik() { Nazwa = Guid.NewGuid().ToString(), Haslo = "bardzo trudne" });
        }
        private void PreperTable()
        {
            PrepareTable prepare = new PrepareTable(this);
            prepare.GetTable();
        }
        private void LoadProxy()
        {
            proxy = new Proxy(client,this);
        }
        private async void Activate()
        {
            await client.CzekajNaGraczaAsync(IdConection);
        }
        public override IEnumerable<Action> Load()
        {
            yield return LoadConection;
            yield return PreperTable;
            yield return LoadProxy;
            yield return Activate;
        }
#endregion
    }
}
