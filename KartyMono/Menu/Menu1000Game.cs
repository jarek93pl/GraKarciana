using ks = KartyMono.ServiceReference1;
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
using Microsoft.Xna.Framework;
using Komputer.Xna.Menu;

namespace KartyMono.Menu
{
    partial class Menu1000Game : MenuPlayerAndTable
    {
        public Menu1000Game(ContentManager cm) : base(cm)
        {
            ConstructorAction();
        }
        Proxy proxy;
        TysiocClient client;
        int IdConection;

        public Vector2 startPosytionCard { get; internal set; }
        public CardSocketUI SocketEmpty { get; internal set; }

        public override void UpDate(GameTime GT)
        {
            Game1.SetTitle((proxy?.controler?.Stan ?? Stan.CzekajNaGracza).ToString());
            UpdateAuction(GT);
            base.UpDate(GT);
        }

        #region Load
        private void LoadConection()
        {
            proxy = Proxy.Activate(this);
            client = (TysiocClient)proxy.comunication;
            ks.DoKontaClient dk = new ks.DoKontaClient();
            IdConection = dk.Rejestruj(new ks.Urzytkownik() { Nazwa = Guid.NewGuid().ToString(), Haslo = "bardzo trudne" });
        }
        private void PreperTable()
        {
            PrepareTable prepare = new PrepareTable(this);
            prepare.GetTable();
        }
        private async void Activate()
        {
            await client.CzekajNaGraczaAsync(IdConection);
        }


        public override IEnumerable<Action> Load()
        {
            yield return PreperTable;
            yield return LoadConection;
            yield return Activate;
        }
        #endregion
    }
}
