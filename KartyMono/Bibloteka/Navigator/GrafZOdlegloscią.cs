using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Komputer.Matematyczne.Figury;
namespace Komputer.Matematyczne.Graf
{
  public delegate void EventHandlerWolność<T>(object o,T a);

    public class Punkt
    {
        /// <summary>
        /// możesz za pomocą tego określić jednostroność grafu.
        /// Dla zera graf zawsze działa
        /// </summary>
        public int Kolejność = 0;
        public float MaksymalneSzukanie;
        internal EventHandlerWolność<Punkt[]> KoniecEvent;
        public Vector2 Miejsce;
        internal float NajkrutszaOdległośćDoKońca=float.MaxValue;
        internal List<Krawedz> ListaKrawedzi = new List<Krawedz>();
        public object Tag;
        public void ProwadźDo(Punkt Koniec)
        {
            Krawedz k = new Krawedz(this);
            k.Prowadź(Koniec);
        }


        internal void ZlikwidujOdniesienia()
        {
            for (int i = 0; i < ListaKrawedzi.Count; i++)
            {
                Punkt p = ListaKrawedzi[i].PoczotekKrawdzi == this ? ListaKrawedzi[i].KoniecKrawdzi : ListaKrawedzi[i].PoczotekKrawdzi;
                p.ListaKrawedzi.Remove(ListaKrawedzi[i ]);
                
            }
            ListaKrawedzi = null;
        }
    }
    public class Krawedz:FiguraZOdcinków
    {
        public enum Kolejność { OdMalejocych,OdRosnocych,Bez}
        public static int MaxksymalnaDługośćDrogi = 400;
        public readonly Punkt KoniecKrawdzi, PoczotekKrawdzi;
        public bool Aktywny = false;
        public float DługośćKrawedzi = 1;
        internal Krawedz(Punkt k)
        {
            Aktywny = true;
            KoniecKrawdzi = k;
        }
        public Krawedz(Punkt Poczotek,Punkt Koniec)
        {
            this.PoczotekKrawdzi = Poczotek;
            this.KoniecKrawdzi = Koniec;
            Add(new Odcinek(Poczotek.Miejsce, Koniec.Miejsce));
        }
        internal void Prowadź(float Odległość,Punkt Koniec,ref Punkt[] tabW,int l,Kolejność kolejnosc=Kolejność.Bez,bool ZaczyniaeZKońca=false)
        {
            Punkt p = PoczotekKrawdzi, k = KoniecKrawdzi;
            if (ZaczyniaeZKońca)
            {
                p = KoniecKrawdzi;
                k = PoczotekKrawdzi;
            }
            
            float dystans = Odległość + DługośćKrawedzi;
            if (k.NajkrutszaOdległośćDoKońca < dystans || Koniec.NajkrutszaOdległośćDoKońca < dystans)
            {
                return;
            }
            k.NajkrutszaOdległośćDoKońca = dystans;
            if (kolejnosc!=Kolejność.Bez)
            {
                if (kolejnosc==Kolejność.OdRosnocych)
                {
                    if (p.Kolejność < k.Kolejność)
                    {
                        return;
                    }
                }
                else
                {

                    if (p.Kolejność > k.Kolejność)
                    {
                        return;
                    }
                }
            }
            if (tabW.Length<=l)
            {
                return;
            }
            tabW[l++] = k;
            if (k==Koniec)
            {
                if (Koniec.KoniecEvent!=null)
                {
                  Punkt[] kar=(Punkt[])tabW.Clone();
                    
                  Array.Resize(ref kar,l);
                  Koniec.KoniecEvent(dystans, kar);
                }
                return;
            }
            foreach (Krawedz item in k.ListaKrawedzi)
            {
                if (item.Aktywny)
                {
                     item.Prowadź(dystans, Koniec, ref tabW, l,kolejnosc,item.KoniecKrawdzi==k);
                }
            }
        }
        public void Prowadź(Punkt Koniec)
        {
            Punkt[] pk = new Punkt[MaxksymalnaDługośćDrogi];
            if (Aktywny)
                Prowadź(0, Koniec, ref pk, 0);
        }
        public void Prowadź(Punkt Koniec,Punkt Wywołujocy,Kolejność kor=Kolejność.Bez)
        {
            Punkt[] pk = new Punkt[MaxksymalnaDługośćDrogi];
            if (Aktywny)
                Prowadź(0, Koniec, ref pk, 0,kor,Wywołujocy==Koniec);
        }
        public void DodajSię()
        {
            Aktywny = true;
           

        }
        public void UsuńSię()
        {
            Aktywny = false;
        }
    }
}
