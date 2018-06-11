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
using GraKarciana;

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
            mg.TookCard += Mg_TookCard;
            menu = mg;

        }
        private  void Mg_TookCard(object sender, CardUI e)
        {
            switch (controler.Stan)
            {
                case Stan.CzekajNaGracza:
                case Stan.CzekajNaLicytacje:
                case Stan.TwojaLicytacja:
                case Stan.CzekanieNaMusek:
                case Stan.CzekanieNaRuch:
                    throw new InvalidOperationException("nie można przyjąć karty w tym stanie");
                case Stan.TwójRuch:
                    SendCardToTable(sender, e);
                    break;
                case Stan.WysylanieMusku:
                    SendCardToEnemy(sender,e);
                    break;
                default:
                    break;
            }
        }

        private async void SendCardToTable(object sender, CardUI e)
        {
            await controler.WyslijKarteMeldującAsync(e.Card);
        }

        List<Karta> CardToSend = new List<Karta>();
        private async void SendCardToEnemy(object sender, CardUI e)
        {
            CardToSend.Add(e.Card);
            if (CardToSend.Count==2)
            {
               await controler.WysyłanieMuskuAsync(CardToSend);
            }
        }

        internal static Proxy Activate(Menu1000Game mg)
        {
            KontrolerTysioc kontroler = new KontrolerTysioc();
            kontroler.LisenAboutSelfMove = false;
            InstanceContext instance = new InstanceContext(kontroler);
            var client = new TysiocClient(instance);
            kontroler.Initialize(client);
            return new Proxy(client, mg, kontroler) { comunication = client, controler = kontroler };
        }
        private bool ConditonSetCardToTable(CardUI arg1, CardSocketUI arg2)
        {
            switch (controler.Stan)
            {
                case Stan.CzekanieNaRuch:
                case Stan.CzekajNaGracza:
                case Stan.CzekajNaLicytacje:
                case Stan.TwojaLicytacja:
                case Stan.CzekanieNaMusek:
                    return false;
                case Stan.WysylanieMusku:
                    return true;
                case Stan.TwójRuch:
                    return controler.DostępneKarty.Contains(arg1.Card);
                default:
                    throw new NotImplementedException();
                    
            }
        }

        public void Controler_ZmianaStołu(object sender, EventArgs e)
        {
            TableChange();
        }

        private void TableChange()
        {
            Table table = new Table(controler, menu);
            Table thitable = new Table(menu);
            table.Execute(thitable);
            LastTable = table;
        }

        private void Controler_TwójRuchEv(object sender, EventArgs e)
        {
            TableChange();
        }

        private void Controler_TwojaLicytacjaEv(object sender, EventArgs e)
        {
            menu.YourAction();
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
