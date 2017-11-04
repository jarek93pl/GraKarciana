using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Komputer.Matematyczne.Figury;
using Komputer.Matematyczne.Silnik;
using Microsoft.Xna.Framework;
namespace Komputer.Matematyczne.Graf
{
    public class TworzenieGrafu
    {
        public Krawedz.Kolejność KolejnoścWyznaczaniaTrasy = Krawedz.Kolejność.Bez;
        int IndexZmiany = 0;
        Dictionary<object, PrzetrzeńKolizji.ObiektKolizji> Szkielety = new Dictionary<object, PrzetrzeńKolizji.ObiektKolizji>();
        List<Punkt> Punky = new List<Punkt>();
        List<Krawedz> Krawedzie = new List<Krawedz>();
        RelacjiNiesymetrycznej relacjaOdcinekAktywny, relacjaOdcinekMartwy, relacjaFiguraAktywna, relacjaFiguraZnikająca;
        internal PrzetrzeńKolizji Przestrzeń;
        private TworzenieGrafu()
        {
            PrzypiszRelacje();   
        }
        public TworzenieGrafu(int IlośćX,int IlośćY,int SzerokośćKratki,int WysokośćKratki):this()
        {
            Przestrzeń = new PrzetrzeńKolizji(IlośćX, IlośćY, SzerokośćKratki, WysokośćKratki);
             
        }

        private void PrzypiszRelacje()
        {
            relacjaFiguraAktywna = new RelacjiNiesymetrycznej();
            relacjaFiguraZnikająca = new RelacjiNiesymetrycznej();
            relacjaOdcinekAktywny = new RelacjiNiesymetrycznej();
            relacjaOdcinekMartwy = new RelacjiNiesymetrycznej();
            relacjaOdcinekAktywny.DodajWraźliwości(0);
            relacjaOdcinekMartwy.DodajWraźliwości(1);
            relacjaFiguraAktywna.DodajWraźliwości(2);
            relacjaFiguraZnikająca.DodajWraźliwości(3);

            relacjaFiguraAktywna.DodajWykrywania(0);

            relacjaFiguraZnikająca.DodajWykrywania(1);

            relacjaOdcinekMartwy.DodajWykrywania(2);
        }
        public Punkt[] ZnajdźNajkrutszy(Punkt Start,Punkt Meta,out float Dystans, float MaksymalnaOdległość)
        {
            Dystans = -1;

            foreach (Punkt item in Punky)
            {
                item.NajkrutszaOdległośćDoKońca = MaksymalnaOdległość;
            }
            Punkt[] Zwracana = new Punkt[0];
            EventHandlerWolność<Punkt[]> Zdażenie = new EventHandlerWolność<Punkt[]>((object o, Punkt[] p) =>
                {
                        Zwracana=p;
                }
                );
            Meta.KoniecEvent += Zdażenie;
            Start.ProwadźDo(Meta);
            if (Zwracana.Length != 0)
            {
                Dystans = Meta.NajkrutszaOdległośćDoKońca;
            }
            return Zwracana;
        }
        public void DodajFigureBlokującą(FiguraZOdcinków fz,Vector2 Miejsce,object klucz)
        {
            PrzetrzeńKolizji.ObiektKolizji dodawany = new PrzetrzeńKolizji.ObiektKolizji(Miejsce);
            dodawany.wrażliwośćKolizji = relacjaFiguraAktywna;
            dodawany.Szkielet = fz;
            Szkielety.Add(klucz, dodawany);
            List<PrzetrzeńKolizji.ObiektKolizji> pom;
            Przestrzeń.Dodaj(dodawany,out pom);
            foreach (PrzetrzeńKolizji.ObiektKolizji item in pom)
            {
                item.wrażliwośćKolizji = relacjaOdcinekMartwy;
                Krawedz k = (Krawedz)item.Szkielet;
                k.UsuńSię();
            }
        }
        public void UsuńFigureBlokującą( object klucz)
        {
            PrzetrzeńKolizji.ObiektKolizji Usuwany = Szkielety[klucz];
            Usuwany.wrażliwośćKolizji = relacjaFiguraZnikająca;
            Przestrzeń.Usuń(Usuwany);
            foreach (PrzetrzeńKolizji.ObiektKolizji item in Przestrzeń.ZnajdźWsztstkieKolidujące(Usuwany))
            {
                DodajOdcinek(item, true);
            }
        }
        public List<Vector2> WyznaczTrase(Vector2 Poczotek,Vector2 Koniec,out float Dystans,float MaksymalnaOdległość)
        {
            Punkt poczotek = new Punkt() { Miejsce = Poczotek, Kolejność = KolejnoścWyznaczaniaTrasy == Krawedz.Kolejność.OdRosnocych ? int.MaxValue : int.MinValue,MaksymalneSzukanie=float.MaxValue };
            Punkt koniec = new Punkt() { Miejsce = Koniec, Kolejność = KolejnoścWyznaczaniaTrasy == Krawedz.Kolejność.OdRosnocych ? int.MinValue : int.MaxValue, MaksymalneSzukanie = float.MaxValue };
            DodajPunkt(poczotek);
            DodajPunkt(koniec);
            Punkt[] p = ZnajdźNajkrutszy(poczotek, koniec, out Dystans,MaksymalnaOdległość);
            List<Vector2> v = new List<Vector2>(p.Length);
             foreach (Punkt item in p)
            {
                v.Add(item.Miejsce);
            }
            UsuńPunkt(poczotek);
            UsuńPunkt(koniec);
            return v;

        }
        private List<Krawedz> ZnajdźPoloczenia(Punkt punkt)
        {
            List<Krawedz> zwracana = new List<Krawedz>();
            foreach (PrzetrzeńKolizji.ObiektKolizji item in Przestrzeń.ZnajdźWsztstkieKolidujące(punkt.Miejsce, punkt.MaksymalneSzukanie))
            {
                Punkt p = item.Tag as Punkt;
                if(p!=null&&p!=punkt)
                { 
                float odl = (p.Miejsce - punkt.Miejsce).Length();
                Krawedz kr = new Krawedz(p, punkt) { DługośćKrawedzi = odl };
                p.ListaKrawedzi.Add(kr);
                punkt.ListaKrawedzi.Add(kr);
                zwracana.Add(kr);
                }
            }
            return zwracana;
        }
        public void DodajPunkt(Punkt p)
        {
            Punky.Add(p);
            Przestrzeń.Dodaj(new PrzetrzeńKolizji.ObiektKolizji(p.Miejsce) { Tag=p});
            foreach (Krawedz item in ZnajdźPoloczenia(p))
            {
                PrzetrzeńKolizji.ObiektKolizji DodawanyOdcinek = new PrzetrzeńKolizji.ObiektKolizji(Vector2.Zero);
                DodawanyOdcinek.Szkielet = item; 
                DodajOdcinek(DodawanyOdcinek);
            }
            
        }

       private void DodajOdcinek(PrzetrzeńKolizji.ObiektKolizji k,bool WPrzestrzeni=false)
       {
           bool b = false;
           if (!WPrzestrzeni)
           {
               List<PrzetrzeńKolizji.ObiektKolizji> opk;
               k.wrażliwośćKolizji = relacjaOdcinekMartwy;
               Przestrzeń.Dodaj(k, out opk);
               
               b = opk.Count == 0;
           }
           else
           {
               b = Przestrzeń.ZnajdźWsztstkieKolidujące(k).Count == 0;
           }
           
           if (b)
           {
               k.wrażliwośćKolizji = relacjaOdcinekAktywny;
               Krawedz kr = (Krawedz)k.Szkielet;
               kr.DodajSię();

           }
       }
        public void UsuńPunkt(Punkt p)
       {
           Punky.Remove(p);
           Przestrzeń.Usuń((PrzetrzeńKolizji.ObiektKolizji o) => { return o.Tag == p; });
           p.ZlikwidujOdniesienia();
       }
    }
}
