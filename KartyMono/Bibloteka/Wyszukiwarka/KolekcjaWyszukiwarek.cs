using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Komputer.Sortowanie;
namespace Komputer.Przeszukiwacz
{
    public class KolekcjaWyszukiwarek<L> : KolekcjaWyszukiwarek<object, L>
    {

    }
    public class KolekcjaWyszukiwarek<T,L>
    {
        List<Wyszukiwarka<T, L>> ListaWyszukiwarek=new List<Wyszukiwarka<T,L>>();
        List<DoSortowania< Wyszukiwarka<T, L>>> ListaWyszukiwarekPosegregowana=new List<DoSortowania<Wyszukiwarka<T,L>>>();
        public void DodajWyszukawerke(Wyszukiwarka<T,L> w,int Pryorytet)
        {
            ListaWyszukiwarek.Add(w);
            ListaWyszukiwarekPosegregowana.Add(new DoSortowania<Wyszukiwarka<T, L>>() { Obiekt = w, Wartość = Pryorytet });
            ListaWyszukiwarekPosegregowana.Sort();
        }
        public void UsuńIndex(int r)
        {
            Wyszukiwarka<T, L> w = ListaWyszukiwarek[r];
            Usuń(w);
        }
        public void Wyczyść(int r)
        {
            foreach ( Wyszukiwarka<T, L> item in ListaWyszukiwarek)
            {
                item.Wyczyść();
            }
        }
        public void Usuń(Wyszukiwarka<T,L> w)
        {
            for (int i = 0; i < ListaWyszukiwarekPosegregowana.Count; i++)
            {
                if (w==ListaWyszukiwarekPosegregowana[i].Obiekt)
                {
                    ListaWyszukiwarekPosegregowana.RemoveAt(i);
                    ListaWyszukiwarek.Remove(w);
                    return;
                }
            }
        }
        public List<Wyszukiwarka<T, L>.WynikZMiejscem> ZnajdźWszystieNieNakładojceSię(IEnumerable<L> Dane,T GdyNieZnalezionoŻadnego)
        {
            List<Wyszukiwarka<T, L>.WynikZMiejscem> Zwracany = new List<Wyszukiwarka<T, L>.WynikZMiejscem>();
            List<Wyszukiwarka<T, L>.WynikZMiejscem> Zwrócony = ZnajdźWszystieNieNakładojceSię(Dane);
            Wyszukiwarka<T, L>.Wynik GdyNieZnaleziono = new Wyszukiwarka<T, L>.Wynik(GdyNieZnalezionoŻadnego, new List<L>());
            int Kon = 0;//koniec nastepnego mósi być poczotkiem nastepnego
            foreach (Wyszukiwarka<T, L>.WynikZMiejscem item in Zwrócony)
            {
                if (Kon != item.Poczotek)
                {
                    Zwracany.Add(new Wyszukiwarka<T, L>.WynikZMiejscem(Kon,item.Poczotek) { Odnaleziony = GdyNieZnaleziono});
                }
                Kon = item.Koniec;
                Zwracany.Add(item);
            }

            return Zwracany;

        }
        public List<Wyszukiwarka<T, L>.WynikZMiejscem> ZnajdźWszystieNieNakładojceSię(IEnumerable<L> Dane)
        {
            List<Wyszukiwarka<T, L>.WynikZMiejscem> Zwracany = new List<Wyszukiwarka<T, L>.WynikZMiejscem>();
            List<Wyszukiwarka<T, L>.WynikZMiejscem> Zwrócony = new List<Wyszukiwarka<T, L>.WynikZMiejscem>();

            for (int i = ListaWyszukiwarekPosegregowana.Count - 1; i >= 0; i--)
            {
                Zwrócony = ListaWyszukiwarekPosegregowana[i].Obiekt.ZnajdźWszystieNieNakładojceSię(Dane);
                for (int ixx = 0; ixx < Zwrócony.Count; ixx++)
                {
                    bool Znalezionyb = false;
                    for (int ix = 0; ix < Zwracany.Count; ix++)
                    {
                        if (Zwrócony[ixx].Kolizja(Zwracany[ix]))
                        {
                            Znalezionyb = true;
                            break;
                        }
                    }
                    if (!Znalezionyb)
                    {
                        Zwracany.Add(Zwrócony[ixx]);
                    }

                }
            }
            if (Zwracany.Count > 0)
                Zwracany.Sort(Zwracany[0]);
            return Zwracany;

        }
        public List<T> ZnajdźWszystieNieNakładojceSięObiekt(IEnumerable<L> Dane)
        {
            List<T> Zwracan;
            List<Wyszukiwarka<T, L>.WynikZMiejscem> x = ZnajdźWszystieNieNakładojceSię(Dane);
            Zwracan = new List<T>(x.Count);
            foreach (Wyszukiwarka<T, L>.WynikZMiejscem item in x)
            {
                Zwracan.Add(item);
            }
            return Zwracan;
        }
         public Wyszukiwarka<T, L>.Wynik  Odnajdź(L Litera )
         {
             Wyszukiwarka<T, L>.Wynik w = null;
             foreach (DoSortowania< Wyszukiwarka<T, L>> item in ListaWyszukiwarekPosegregowana)
             {
                 w=item.Obiekt.Odnajdź(Litera);
                 if(w!=null )
                 {
                     return w;
                 }
             }
             return w;
         }
         public Wyszukiwarka<T, L>.Wynik Odnajdź(IEnumerable<L> Litera)
         {
             Wyszukiwarka<T, L>.Wynik w;
             foreach (DoSortowania<Wyszukiwarka<T, L>> item in ListaWyszukiwarekPosegregowana)
             {
                  w=item.Obiekt.Odnajdź(Litera);
                 if (w!=null)
                 {
                     return w;
                 }
             }
             return null;
         }
        public T OdnajdźObiekt(IEnumerable<L> Litera)
         {
             return Odnajdź(Litera);
         }
        public T OdnajdźObiekt(L Litera)
        {
            return Odnajdź(Litera);
        }
    }
}
