using ClientSerwis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KartyMono.Menu;
using KartyMono.Common.UI;
using ks = KartyMono.ServiceReference1;
using System.ServiceModel;

namespace KartyMono.Game1000
{
    class Proxy
    {
        internal ITysioc comunication;
        Menu1000Game menu;
        Table LastTable = Table.Empty();

        internal KontrolerTysioc controler;
        public Proxy(ITysioc comunication,Menu1000Game mg, KontrolerTysioc controler)
        {
            this.comunication = comunication;
            controler.Initialize(comunication);
            controler.KtośWysłałKarte += Controler_KtośWysłałKarte;
            controler.KtośZalicytował += Controler_KtośZalicytował;
            controler.OdbieranieKart += Controler_OdbieranieKart;
            controler.OdbierzKartęOdGracza += Controler_OdbierzKartęOdGracza;
            controler.OdbierzMusek += Controler_OdbierzMusek;
            controler.TwojaLicytacjaEv += Controler_TwojaLicytacjaEv;
            controler.TwójRuchEv += Controler_TwójRuchEv;
            controler.ZmianaStołu += Controler_ZmianaStołu;
            mg.ConditonSetCardToTable = ConditonSetCardToTable;
            menu = mg;

        }

        internal static Proxy Activate(Menu1000Game mg)
        {
            KontrolerTysioc kontroler = new KontrolerTysioc();
            InstanceContext instance = new InstanceContext(kontroler);
            var client = new TysiocClient(instance);
            kontroler.Initialize(client);

            return new Proxy(client, mg, kontroler) { comunication = client, controler = kontroler };
        }

        private bool ConditonSetCardToTable(CardUI arg1, CardSocketUI arg2)
        {
            return true;
        }

        public void Controler_ZmianaStołu(object sender, EventArgs e)
        {
            Table table = new Table(controler,menu);
            table.Execute(LastTable);
            LastTable = table;
        }

        private void Controler_TwójRuchEv(object sender, EventArgs e)
        {
        }

        private void Controler_TwojaLicytacjaEv(object sender, EventArgs e)
        {
        }

        private void Controler_OdbierzMusek(object sender, GraKarciana.Karta[] e)
        {
        }

        private void Controler_OdbierzKartęOdGracza(object sender, GraKarciana.Karta[] e)
        {
        }

        private void Controler_OdbieranieKart(object sender, GraKarciana.Karta[] e)
        {
        }

        private void Controler_KtośZalicytował(object sender, Tuple<Urzytkownik, int> e)
        {
        }

        private void Controler_KtośWysłałKarte(object sender, Tuple<Urzytkownik, GraKarciana.Karta> e)
        {
        }
    }
}
