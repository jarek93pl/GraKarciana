using GraKarciana;
using Karty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSerwis
{
    class Iq1000Klient
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
        public int PlayerIndex;
        ConclusionAboutGame InGame;
        public int BindSum;
        public string Name;
        public Iq1000Klient(ITysioc comunication,int AmountPlayer,string Name)
        {
            controler = new KontrolerTysioc();
            controler.Initialize(comunication);
            controler.OdbieranieKart += Controler_OdbieranieKart;
            controler.OdbierzMusek += Controler_OdbierzMusek;
            controler.TwojaLicytacjaEv += Controler_TwojaLicytacjaEv;
            controler.KtośWysłałKarte += Controler_KtośWysłałKarte;
            controler.TwójRuchEv += Controler_TwójRuchEv;
            this.comunication = comunication;
            this.AmountPlayer = AmountPlayer;
            this.Name = Name;
            PlayerIndex = controler.IndexPlayer(Name);
        }

        private void Controler_KtośWysłałKarte(object sender, Tuple<Urzytkownik, Karta> e)
        {
            InGame.PlayerConclusion[controler.IndexPlayer(e.Item1.Nazwa)].ConclusionAboutBehavior(controler.Stół, controler.AktywnaKozera, controler.Kozera);
        }

        private async void Controler_TwójRuchEv(object sender, EventArgs e)
        {
            IQ1000Game iq = GetIq();
            InGame.WhoMove = PlayerIndex;
            Move1000 m= iq.CalculateMove(InGame);
            var ConclusionMovingPlaeyr = InGame.PlayerObjectConclusion;
            ConclusionMovingPlaeyr.haveCards = controler.DostępneKarty;
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
            InGame = new ConclusionAboutGame(AmountPlaeyr, PlayerIndex, controler.TwojeKarty);
            List<Karta> SnedCard = iq.GetWorstCard(controler.TwojeKarty, AmountPlaeyr);
            await controler.WysyłanieMuskuAsync(SnedCard);
            int j = 0;
            for (int i = 0; i < AmountPlaeyr; i++)
            {
                if (i!=PlayerIndex)
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
