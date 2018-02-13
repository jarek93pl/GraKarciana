using GraKarciana;
using Karty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSerwis
{
    public class Iq1000Klient
    {
        public int Tick
        {
            get
            {
                return 5;
            }
        }
        public int AmountPlaeyr
        {
            get
            {
                return 3;
            }
        }
        KontrolerTysioc controler;
        ITysioc comunication;
        public int AmountPlayer;
        ConclusionAboutGame InGame;
        public int BindSum;
        public string Name;
        public Iq1000Klient(ITysioc comunication, KontrolerTysioc kontroler,int AmountPlayer,string Name,int IdPlayer)
        {
            controler = kontroler;
            controler.Initialize(comunication);
            controler.OdbieranieKart += Controler_OdbieranieKart;
            controler.OdbierzMusek += Controler_OdbierzMusek;
            controler.TwojaLicytacjaEv += Controler_TwojaLicytacjaEv;
            controler.KtośWysłałKarte += Controler_KtośWysłałKarte;
            controler.TwójRuchEv += Controler_TwójRuchEv;
            comunication.CzekajNaGracza(IdPlayer);
            this.comunication = comunication;
            this.AmountPlayer = AmountPlayer;
            this.Name = Name;
        }

        private void Controler_KtośWysłałKarte(object sender, Tuple<Urzytkownik, Karta> e)
        {
            if (InGame==null)
            {
                InGame = new ConclusionAboutGame(AmountPlaeyr, controler.IndexPlayer(Name), controler.TwojeKarty);
                InGame.Active(false);
            }
            InGame.PlayerConclusion[controler.IndexPlayer(e.Item1.Nazwa)].ConclusionAboutBehavior(controler.Stół, controler.AktywnaKozera, controler.Kozera);
        }

        private async void Controler_TwójRuchEv(object sender, EventArgs e)
        {
            IQ1000Game iq = GetIq();
            InGame.WhoMove = controler.IndexPlayer(Name);
            var ConclusionMovingPlaeyr = InGame.PlayerObjectConclusion;
            ConclusionMovingPlaeyr.haveCards = controler.DostępneKarty;
            Move1000 m= iq.CalculateMove(InGame);
            await controler.WyslijKarteAsync(m.card, m.Marriage);

        }

        private async void Controler_TwojaLicytacjaEv(object sender, EventArgs e)
        {
            IQ1000Game iq = GetIq();
            ConclusionAboutGame CAG = new ConclusionAboutGame(AmountPlaeyr, controler.IndexPlayer(Name), controler.TwojeKarty);
            await controler.LicytujAsync(BindSum =GetIq().CalculateBidAmount(CAG));
        }

        private async void Controler_OdbierzMusek(object sender, GraKarciana.Karta[] e)
        {
            IQ1000Game iq = GetIq();
            InGame = new ConclusionAboutGame(AmountPlaeyr, controler.IndexPlayer(Name), controler.TwojeKarty);
            InGame.Active(true);
            List<Karta> SnedCard = iq.GetWorstCard(controler.TwojeKarty, AmountPlaeyr);
            await controler.WysyłanieMuskuAsync(SnedCard);
            int j = 0;
            for (int i = 0; i < AmountPlaeyr; i++)
            {
                if (i!= controler.IndexPlayer(Name))
                {
                    InGame.TransferedCard(SnedCard[j++], i);
                }
            }

        }
        
        private IQ1000Game GetIq()
        {
            return new IQ1000Game(Tick, 0.6f);
        }

        private async void Controler_OdbieranieKart(object sender, GraKarciana.Karta[] e)
        {
        }
    }
}
