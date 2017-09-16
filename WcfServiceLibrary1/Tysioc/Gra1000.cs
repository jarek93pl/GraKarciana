using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraKarciana
{
    public class Gra1000 : Gra<Tysioc>
    {
        int Nr = 0;
        PodsumowanieTysioc WynikiGry = new PodsumowanieTysioc(3);
        int[] IlośćPunktów;
        RozgrywkaTysioc3 AktulanaRozgrywka;
        public Gra1000(int Ilo) : base(Ilo)
        {
            IlośćPunktów = new int[Ilo];
        }
        public override void ZnalezioneGraczy()
        {
            AktulanaRozgrywka = new RozgrywkaTysioc3(this,WynikiGry,Nr++);
         
        }

        public override void ZwonioneMiejsce(int Nr)
        {
            throw new NotImplementedException();
        }
        

        internal void Licytuj(int pk, Tysioc tysioc)
        {
            AktulanaRozgrywka.Licytuj(pk, gracze.FindIndex(tysioc));
        }

        internal void WyślijKarte(Karta k, Tysioc tysioc,bool melduj)
        {

             AktulanaRozgrywka.WyślijKarte(k, gracze.FindIndex(tysioc),melduj);
            if (AktulanaRozgrywka.Zakończona)
            {
                if (WynikiGry.CzyktośWygrał)
                {
                    foreach (var item in gracze)
                    {
                        item.DoOdpowiedz.KoniecGry(WynikiGry);
                    }
                }
                else
                {
                    foreach (var item in gracze)
                    {
                        item.DoOdpowiedz.PodsumowanieRozgrywki(WynikiGry);
                    }
                    AktulanaRozgrywka = new RozgrywkaTysioc3(this, WynikiGry,( Nr++) % 3);
                }
            }
        }

        public override void PrzedstawGraczowi(Tysioc gracz, Urzytkownik[] tb)
        {
            gracz.DoOdpowiedz.ZnalezionoNowychGraczy(tb);
        }
        

        internal void WyślijMusek(Karta[] k, Tysioc tysioc)
        {
            AktulanaRozgrywka.WyślijMusek(k, gracze.FindIndex(tysioc));
        }
    }
}