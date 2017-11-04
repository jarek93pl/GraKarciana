using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompilator.Wyszukiwarka;

namespace Komputer.Przeszukiwacz
{
    

    public class Wyszukiwarka<T> : Wyszukiwarka<object, T> 
    {

    }
    /// <summary>
    /// uwaga Maksymalna długość chasła to 10000
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="L"></typeparam>
    public class Wyszukiwarka<T, L>
    {
        internal int IndexZmiany=0;
        public int IlośćSłów = 0;
        public Odniesienie Kożeń = new Odniesienie();

    
        public void Dodaj(IEnumerable<L> s)
        {
            Dodaj(s,0,default(T));
        }
        public void Dodaj(IEnumerable<L> s,T Obiekt)
        {
            Dodaj(s, 0, Obiekt);
        }
       public void Dodaj(IEnumerable<L> s,long Adres,T Obiekt)
        {
            IndexZmiany++;///do ienemurator przy dokonaniu jakiej kolwiek zmiany trzeba zmienić
            Odniesienie brany = Kożeń;
            foreach (L item in s)
            {
                int lp = brany.Odniesienia.FindIndex((Odniesienie o) => { return o.Litera.Equals(item); });
                if(lp==-1)
                {
                    Odniesienie x = new Odniesienie();
                    x.Litera=item;
                    brany.Odniesienia.Add(x);
                    x.Poprzednie = brany;
                    brany = x;
                }
                else
                {
                    brany = brany.Odniesienia[lp];
                }
            }
            brany.index = IlośćSłów;
            brany.Adres.Add(Adres);
            brany.ZaładujObiekt(Obiekt);
            IlośćSłów++;
        }
        public List<WynikZMiejscem> ZnajdźWszystieNieNakładojceSię(IEnumerable<L> Dane)
        {
            int i = 0;
            List<WynikZMiejscem> Zwracana = new List<WynikZMiejscem>();

            Wynik w;
            foreach (L item in Dane)
            {
                i++;
                w=Odnajdź(item);
                if (w!=null)
                {
                    Zwracana.Add(new WynikZMiejscem(i - w.Lista.Count, i) { Odnaleziony = w });
                }
            }

            i++;
            w = Odnajdź(default(L));
            if (w!=null)
            {
                Zwracana.Add(new WynikZMiejscem(i - w.Lista.Count, i));
            }
            if (Zwracana.Count>0)
            {
            Zwracana.Sort(Zwracana[0]);
            }  
            for (int iw = 0; iw < Zwracana.Count-1; iw++)
            {
                if (Zwracana[iw+1].Kolizja(Zwracana[iw]))
                {
                    Zwracana.RemoveAt(iw + 1);
                    iw--;
                }
            }
            return Zwracana;
        }
        
        public List<Wyszukiwarka<T, L>.WynikZMiejscem> ZnajdźWszystieNieNakładojceSię(IEnumerable<L> Dane, T GdyNieZnalezionoŻadnego)
        {
            List<Wyszukiwarka<T, L>.WynikZMiejscem> Zwracany = new List<Wyszukiwarka<T, L>.WynikZMiejscem>();
            List<Wyszukiwarka<T, L>.WynikZMiejscem> Zwrócony = ZnajdźWszystieNieNakładojceSię(Dane);
            Wyszukiwarka<T, L>.Wynik GdyNieZnaleziono = new Wyszukiwarka<T, L>.Wynik(GdyNieZnalezionoŻadnego, new List<L>());
            int Kon = 0;//koniec nastepnego mósi być poczotkiem nastepnego
            foreach (Wyszukiwarka<T, L>.WynikZMiejscem item in Zwrócony)
            {
                if (Kon != item.Poczotek)
                {
                    Zwracany.Add(new Wyszukiwarka<T, L>.WynikZMiejscem(Kon, item.Poczotek) { Odnaleziony = GdyNieZnaleziono });
                }
                Kon = item.Koniec;
                Zwracany.Add(item);
            }

            return Zwracany;

        }
        public List<T> ZnajdźWszystieNieNakładojceSięObiekt(IEnumerable<L> Dane, T GdyNieZnalezionoŻadnego)
        {
            List<T> Zwracan;
            List<Wyszukiwarka<T, L>.WynikZMiejscem> x = ZnajdźWszystieNieNakładojceSię(Dane, GdyNieZnalezionoŻadnego);
            Zwracan = new List<T>(x.Count);
            foreach (Wyszukiwarka<T, L>.WynikZMiejscem item in x)
            {
                Zwracan.Add(item);
            }
            return Zwracan;
        }
        List<PrzechowyczZKolekcją> ZapytanieZPoprzedniego = new List<PrzechowyczZKolekcją>();
        List<PrzechowyczZKolekcją> Przeszókania = new List<PrzechowyczZKolekcją>();
        class PrzechowyczZKolekcją
        {
            public PrzechowyczZKolekcją(Odniesienie o, List<L> ListaZapytań)
            {
                ods = o;
                this.ListaZapytań = ListaZapytań;
            }
            public PrzechowyczZKolekcją(Odniesienie o)
            {
                ods = o;
                this.ListaZapytań = new List<L>();
            }
            public Odniesienie ods;
            public List<L> ListaZapytań;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Litera"></param>
        /// <param name="w">W wyniku znajdą się zadane zapytania</param>
        /// <returns></returns>
        public Wynik Odnajdź(L Litera)
        {
            Wynik w = null;
             Przeszókania = ZapytanieZPoprzedniego;
             ZapytanieZPoprzedniego = new List<PrzechowyczZKolekcją>();
            Przeszókania.Add(new PrzechowyczZKolekcją( Kożeń));
            for (int i = 0; i < Przeszókania.Count; i++)
			{
                Odniesienie o = Przeszókania[i].ods;
                foreach (Odniesienie item in o.Odniesienia)
                {
                        if (item.Litera.Equals(Litera))
                        {
                            Przeszókania[i].ListaZapytań.Add(Litera);
                            ZapytanieZPoprzedniego.Add(new PrzechowyczZKolekcją(item,Przeszókania[i].ListaZapytań));
                            if (item.Adres.Count != 0)
                            {
                                w = new Wynik(item.Obiekt, Przeszókania[i].ListaZapytań);
                            }
                        }
                    
                }
                
            }
            return w;
        }
        public T OdnajdźObiekt(L Litera)
        {
            return Odnajdź(Litera);
        }
        /// <summary>
        /// te odnajdywanie nie wiąże się wogóle z poprzednie wyszukiwanymi 
        /// </summary>
        /// <param name="Litera"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public Wynik Odnajdź(IEnumerable<L> Litera)
        {
            Odniesienie o = Kożeń;
            bool Znaleziono = false;
            List<L> ListaPrzejsciowa = new List<L>();
            foreach (L item in Litera)
            {
                foreach (Odniesienie item2 in o.Odniesienia)
                {
                    Znaleziono = false;
                    if (item2.Litera.Equals(item))
                    {
                        o = item2;
                        ListaPrzejsciowa.Add(item);
                        Znaleziono = true;
                        break;
                    }
                }
                if (!Znaleziono)
                {
                    return null;
                }
            }
            if (o.Adres.Count != 0)
            {
                return new Wynik(o.Obiekt, ListaPrzejsciowa);
            }
            return null;

        }
        public T OdnajdźObiekt(IEnumerable<L> Litera)
        {
            return Odnajdź(Litera);
        }
        /// <summary>
        /// </summary>
        /// <param name="Litera"></param>
        /// <returns>w liscie znajdują się progi</returns>
        public  List<Wynik> OdnajdźZWielomaPoprawnymi(IEnumerable<L> Litera) 
        {
            List<Wynik> Zwranana = new List<Wynik>();
            List<Odniesienie> OdList = new List<Odniesienie>();
            List<Odniesienie> OdList2 = new List<Odniesienie>();
            foreach (Odniesienie item in Kożeń.Odniesienia)
            {
                OdList2.Add(item);
            }
            foreach (L item in Litera)
            {
                OdList = OdList2;
                OdList2 = new List<Odniesienie>();
                foreach (Odniesienie item2 in OdList)
                {
                    foreach (Odniesienie item3 in item2.Odniesienia)
                    {
                        if (item2.Litera.Equals(item))
                        {
                            OdList2.Add(item3);
                        }
                        if (item3.Adres.Count > 0)
                        {
                            Zwranana.Add(WyodrebnikWynik(item3));
                        } 
                    }
                }
            }
            return Zwranana;
        }
        public  List<T> OdnajdźZWielomaPoprawnymiObiekt(IEnumerable<L> Litera)
        {
            List<T> Zwracan;
            List<Wyszukiwarka<T, L>.Wynik> x = OdnajdźZWielomaPoprawnymi(Litera);
            Zwracan = new List<T>(x.Count);
            foreach (Wyszukiwarka<T, L>.Wynik item in x)
            {
                Zwracan.Add(item);
            }
            return Zwracan;
        }
        public static Wynik WyodrebnikWynik(Odniesienie item)
        {
            List<L> PomL = new List<L>();
            Odniesienie Pom = item;
            while (Pom.Poprzednie != null)
            {
                PomL.Add(Pom.Litera);
                Pom = Pom.Poprzednie;
            }
            PomL.Reverse();
            return new Wynik(item.Obiekt, PomL);
        }
        /// <summary>
        /// </summary>
        /// <param name="Litera"></param>
        /// <returns>w liscie znajdują się progi</returns>
        public WyszykiwarkaEnum<T,L> OdnajdźPoPoczątku(IEnumerable<L> Litera )
        {
            Odniesienie o = Kożeń;
            foreach (L item2 in Litera)
            {
                foreach (Odniesienie item in o.Odniesienia)
                {
                    if (item2.Equals(item.Litera))
                    {
                        o = item;
                        goto Koniec;

                    }
                }
                return null;
            Koniec:
                ;
            }
            WyszykiwarkaEnum<T, L> t = new WyszykiwarkaEnum<T, L>(IndexZmiany, o);
            return t;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Litera"></param>
        /// <param name="Adres">Podajesz tylko wtedy jak jest wiele typów z takimi samyli literami potrzebnymi do wyszukania</param>
        /// <returns></returns>
        public bool Usuń(IEnumerable<L> Litera, int Adres = 0)
        {
            IndexZmiany++;
            Odniesienie o = Kożeń;
            Odniesienie OdKtóregoUsuwać = Kożeń;
            Odniesienie CoUsuówać = null;
            foreach (L item2 in Litera)
            {
                foreach (Odniesienie item in o.Odniesienia)
                {
                    if (item2.Equals(item.Litera))
                    {
                        if (o.Odniesienia.Count>1)
                        {
                            OdKtóregoUsuwać = o;
                            CoUsuówać = item;
                        }
                        o = item;
                        goto Koniec;

                    }
                }
                return false;
            Koniec:
                ;
            }
            if (CoUsuówać==o)
            {
                if (CoUsuówać.Odniesienia.Count==0)
                {
                    OdKtóregoUsuwać.Odniesienia.Remove(CoUsuówać);
                }
                else
                {
                    if (CoUsuówać.Adres.Count>1)
                    {
                        CoUsuówać.Adres.Remove(Adres);
                    }
                    else
                    {
                        CoUsuówać.Adres.Clear();
                        CoUsuówać.Wyczyść();
                    }
                }
            }
            else
            {
                OdKtóregoUsuwać.Odniesienia.Remove(CoUsuówać);
            }
            return true;
        }
         public class Odniesienie : IOdniesienie<T>
        {
            public long index;

            public long Index
            {
                get { return index; }
            }
            T obiekt;

            public T Obiekt
            {
                get { return obiekt; }
            }
             internal void ZaładujObiekt(T x)
            {
                this.obiekt = x;
            }
            internal void Wyczyść()
            {
                obiekt = default(T);
            }
            List<long> adres = new List<long>();

            public List<long> Adres
            {
                get { return adres; }
            }
            public L Litera;
            public Odniesienie Poprzednie;
            public List<Odniesienie> Odniesienia = new List<Odniesienie>();
        }
    public class Wynik
    {
        public readonly T Dane;
        public readonly IList<L> Lista;
        public static implicit operator T(Wynik w)
        {
            return w.Dane;
        }
        public Wynik(T dana,List<L> Kolekacja)
        {
            Dane = dana;
            Lista = Kolekacja;
        }
    }
    public class WynikZMiejscem:IComparer<WynikZMiejscem>
    {
        public Wynik Odnaleziony;
        public readonly int Poczotek,Koniec;
        public WynikZMiejscem(int pocz,int koniec)
        {
            this.Poczotek=pocz;
            this.Koniec=koniec;
        }
        public int Compare(WynikZMiejscem x, WynikZMiejscem y)
        {
            int Zwracana = 0;
            try
            {
                checked
                {
                    Zwracana -= (y.Poczotek - x.Poczotek) * 100000;//ponieważ poczotek jest ważniejszy
                }
            }
            catch
            {
            }
            Zwracana += y.Odnaleziony.Lista.Count - x.Odnaleziony.Lista.Count;
            return Zwracana;
        }
        public bool Kolizja(WynikZMiejscem wm)
        {
            return !(Koniec <= wm.Poczotek || wm.Koniec <= Poczotek);
        }
        public static implicit operator Wynik(WynikZMiejscem z)
        {
            return z.Odnaleziony;
        }
        public static implicit operator T(WynikZMiejscem w)
        {
            return w.Odnaleziony.Dane;
        }
    }
    public void Wyczyść()
    {
        ZapytanieZPoprzedniego = new List<PrzechowyczZKolekcją>();
    }
    public WyszykiwarkaEnum<T, L> Listuj
    {
        get { return new WyszykiwarkaEnum<T, L>(IndexZmiany, Kożeń); }
    }
    }
    public struct Znaleziony
    {
        public int Index;
        public int MiejsceZnalezienia;
    }
    public static class Rozszeżenia
    {
        public static List<T> ZmieńNaZwracanych<T,L>(this List<Wyszukiwarka<T,L>.WynikZMiejscem> x)
        {
            List<T> k = new List<T>();
            foreach (var item in x)
            {
                k.Add(item);
            }
            return k;
        }
        public static List<T> ZmieńNaZwracanych<T, L>(this List<Wyszukiwarka<T, L>.Wynik> x)
        {
            List<T> k = new List<T>();
            foreach (var item in x)
            {
                k.Add(item);
            }
            return k;
        }
    }
    
}
