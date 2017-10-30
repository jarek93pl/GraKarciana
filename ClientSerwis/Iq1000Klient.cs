using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSerwis
{
    class Iq1000Klient
    {
        KontrolerTysioc controler;
        ITysioc comunication;
        public int AmountPlayer;
        public Iq1000Klient(ITysioc comunication,int AmountPlayer)
        {
            controler = new KontrolerTysioc();
            controler.Initialize(comunication);
            controler.OdbieranieKart += Controler_OdbieranieKart;
            controler.OdbierzMusek += Controler_OdbierzMusek;
            controler.TwojaLicytacjaEv += Controler_TwojaLicytacjaEv;
            controler.TwójRuchEv += Controler_TwójRuchEv;
            this.comunication = comunication;
            this.AmountPlayer = AmountPlayer;
        }

        private async void Controler_TwójRuchEv(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void Controler_TwojaLicytacjaEv(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void Controler_OdbierzMusek(object sender, GraKarciana.Karta[] e)
        {
            throw new NotImplementedException();
        }

        private async void Controler_OdbieranieKart(object sender, GraKarciana.Karta[] e)
        {
            throw new NotImplementedException();
        }
    }
}
