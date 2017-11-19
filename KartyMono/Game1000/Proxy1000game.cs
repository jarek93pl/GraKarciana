﻿using ClientSerwis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KartyMono.Menu;
namespace KartyMono.Game1000
{
    class Proxy
    {
        KontrolerTysioc controler;
        public Proxy(ITysioc comunication,Menu1000Game mg)
        {
            controler = new KontrolerTysioc();
            controler.Initialize(comunication);
            controler.KtośWysłałKarte += Controler_KtośWysłałKarte;
            controler.KtośZalicytował += Controler_KtośZalicytował;
            controler.OdbieranieKart += Controler_OdbieranieKart;
            controler.OdbierzKartęOdGracza += Controler_OdbierzKartęOdGracza;
            controler.OdbierzMusek += Controler_OdbierzMusek;
            controler.TwojaLicytacjaEv += Controler_TwojaLicytacjaEv;
            controler.TwójRuchEv += Controler_TwójRuchEv;
            controler.ZmianaStołu += Controler_ZmianaStołu;

        }

        private void Controler_ZmianaStołu(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Controler_TwójRuchEv(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Controler_TwojaLicytacjaEv(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Controler_OdbierzMusek(object sender, GraKarciana.Karta[] e)
        {
            throw new NotImplementedException();
        }

        private void Controler_OdbierzKartęOdGracza(object sender, GraKarciana.Karta[] e)
        {
            throw new NotImplementedException();
        }

        private void Controler_OdbieranieKart(object sender, GraKarciana.Karta[] e)
        {
            throw new NotImplementedException();
        }

        private void Controler_KtośZalicytował(object sender, Tuple<Urzytkownik, int> e)
        {
            throw new NotImplementedException();
        }

        private void Controler_KtośWysłałKarte(object sender, Tuple<Urzytkownik, GraKarciana.Karta> e)
        {
            throw new NotImplementedException();
        }
    }
}
