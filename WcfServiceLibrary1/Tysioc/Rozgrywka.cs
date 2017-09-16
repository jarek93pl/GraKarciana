#define KontrolaKart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
namespace GraKarciana
{
    public class RozgrywkaTysioc3
    {
        public Tysioc GraczWygrajocyLicytacje;
        public int WartośćWylicytowana = 0;

        private Gra1000 gra1000;
        Karta[] Musek = new Karta[3];
        List<Karta>[] KartyGracza = new List<Karta>[3];
        int Nr = 0;
        PodsumowanieTysioc Podsumowanie;
        const int IlośćGraczy = 3;
        public RozgrywkaTysioc3(Gra1000 gra1000, PodsumowanieTysioc pt, int Nr)
        {
            Podsumowanie = pt;
            pt.NowaTura();
            this.Nr = Nr;
            this.gra1000 = gra1000;
            Losuj();
            WyślijKary();
            PowiadomienieLicytacyjne();

        }

        private bool PowiadomienieLicytacyjne()
        {
            int Index = Nr % IlośćGraczy;

            if (Index == NajwyżejLicytujący)
            {
                KoniecLicytacji();
                return true;
            }
            else
            {
                gra1000.gracze[Index].DoOdpowiedz.TwojaLicytacja();
            }
            Nr++;
            return false;
        }

        private void WyślijKary()
        {
            for (int i = 0; i < 3; i++)
            {
                gra1000.gracze[i].DoOdpowiedz.OdbierzKarty(KartyGracza[i]);
            }
        }

        private void Losuj()
        {
            var TaliaKart = ObsugaKart.WylousjMałąTalie();
            Musek = TaliaKart.Wylosuje(3).ToArray();
            for (int i = 0; i < 3; i++)
            {
                KartyGracza[i] = TaliaKart.Wylosuje(7);
            }
        }

        internal void WyślijMusek(Karta[] k, int v)
        {
            int R = 0;
            for (int i = 0; i < IlośćGraczy; i++)
            {
                if (i!=v)
                {
                    Gracze[i].DoOdpowiedz.OdbierzKarty(new List<Karta>() { k[R] });
                    KartyGracza[i].Add(k[R]);
                    R++;
                }
            }
            KartyGracza[v].RemoveAll(k);
            Gracze[v].DoOdpowiedz.Twojruch();
        }
        
        int NajwyżejLicytujący = -1;
        internal void Licytuj(int pk, int v)
        {
            
            if (pk > WartośćWylicytowana)
            {
                NajwyżejLicytujący = v;
                RozgłośLicytacje(Gracze[v].Gracz.Nazwa, v);
                WartośćWylicytowana = pk;
                GraczWygrajocyLicytacje = Gracze[v];

            }
            PowiadomienieLicytacyjne();
        }

        private void KoniecLicytacji()
        {
            KartyGracza[NRGrającego].AddRange(Musek);
            TenGracz.DoOdpowiedz.OdbierzKarty(Musek.ToList());
          
        }

        public IList<Tysioc> Gracze { get => gra1000.gracze; }
        private void RozgłośLicytacje(string nazwa, int v) => Gracze.Forech(X => X.DoOdpowiedz.KtosZalicytowal(nazwa, v));
        public int PozycjaGracza => Nr % IlośćGraczy;
        public Tysioc TenGracz => Gracze[PozycjaGracza];
        Karta[] stół = new Karta[3];
        Karta Kozera;
        bool KozeraAktywna;
        int IlośćKartNaStole = 0;
        public bool Zakończona { get; private set; }
        internal void WyślijKarte(Karta k, int v,bool Melduj)
        {
            if (Melduj)
            {
                if (ObsugaKart.IstniejeMeldunek(k, KartyGracza[v])&&IlośćKartNaStole==0)
                {
                    Kozera = k.Kolor();
                    Podsumowanie.OdznaczMeldunek(Kozera,v);
                    KozeraAktywna = true;
                }
                else
                {
                    throw new InvalidOperationException("nie masz krola i damy by zamedowac ");
                }
            }
            RozgłośWysłanieKarty(k,v,Melduj);
            stół[v] = k;
            IlośćKartNaStole++;
            KartyGracza[v].Remove(k);
            Nr++;
            if (IlośćKartNaStole==IlośćGraczy)
            {
                Nr = PobierzZwyciensce();
                Podsumowanie.PrzydzielPunkty(Nr, stół);
                if (SprawdźCzyKoniec())
                {
                    Podsumowanie.Koniec(NajwyżejLicytujący, WartośćWylicytowana);
                }
                else
                {
                    IlośćKartNaStole = 0;

                }
                
            }
            TenGracz.DoOdpowiedz.Twojruch();
        }

        private void RozgłośWysłanieKarty(Karta k, int v,bool Zamledował)
        {
            foreach (var item in Gracze)
            {
                item.DoOdpowiedz.KtosWyslalKarte(k, TenGracz.Gracz.Nazwa,Zamledował);
            }
        }

        private int NRGrającego { get => Nr % IlośćGraczy; }

        private int PobierzZwyciensce()
        {
            ComparerTysioc porówjnaj = null;
            if (KozeraAktywna)
            {
                porówjnaj = new ComparerTysioc(stół[NRGrającego],Kozera);
            }
            else
            {
                porówjnaj = new ComparerTysioc(stół[NRGrającego]);
            }
            var Kopiastołu = (Karta[])stół.Clone();
            Array.Sort(Kopiastołu, porówjnaj);
            var maxkarta = Kopiastołu.Last();
            return stół.FindIndex( maxkarta);

        }

        private bool SprawdźCzyKoniec()
        {
            if (KartyGracza.All(X=>X.Count==0))
            {
                Zakończona = true;
                return true;
            }
            return false;
        }
    }
}