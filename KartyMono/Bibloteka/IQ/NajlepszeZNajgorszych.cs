using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
namespace Komputer.IQ
{

    public enum RuchE { Komputer, Gracz };
    public abstract class NajlepszeZNajgorszych<T> where T : IPorównawczy<T>
    {
        public delegate T[] PobieraniePlanszZRuchem<P>(T Mapa, out P[] Ruchy);
        public delegate T OdczytPlik(BinaryReader br);
        public delegate void ZapisPlik(BinaryWriter bw,T dana);
        /// <summary>
        /// Metoda Przechowywana w tym delegacje zwraca możlwości nastepne stany aplikacji
        /// </summary>
        /// <param name="Mapa">Bierzocy stan aplikacji</param>
        /// <returns>Stany aplikacje nastepujoce z bierzocego stanu aplikacji</returns>
        protected abstract T[] PobieraniePlansz(T Mapa, RuchE GraczCzyKomputer);
        /// <summary>
        /// Suży do oceniania Bierzocego stanu aplikacji
        /// </summary>
        /// <param name="Mapa">Bierzocy stan aplkacji</param>
        /// <returns>Zwraca ocene aplikacji miedzy przegraną a wygraną dla tych wartości skrajnych kończy symulowanie </returns>
        protected abstract int OcanaPlanszy(T Mapa);
        /// <summary>
        /// Sprawdza Czy dany stan gry nie skończył trwania aplikacji
        /// </summary>
        /// <param name="Mapa">Bierzocy stan aplikacji</param>
        /// <returns>Dla true gra może trwać dalej</returns>
        protected abstract bool TrwanieGry(T Mapa);
        Random Los = new Random();
        int IlośćRuchówDoPrzodu;
        public const int Wygrana = 2147483647, Przegrana = -2147483648;
        /// <summary>
        /// Konstruktor Inteligencji
        /// </summary>
        /// <param name="Poczotek">Stan aplikacji(mapa,plansza) od którego aplikacja zaczyna</param>
        /// <param name="IlośćRuchówDoPrzodu">Ilość ruchów symulowanych do przodu</param>
        /// <param name="DPP">Delegat tworzocy wszystkie możliwie lub sensowne stany aplikacji bedące nastepsten danego</param>
        /// <param name="DOP">Ocenianie stanu aplikacji pod wzgledem możlwosci wygranej przez komputer</param>
        /// <param name="DTG">Metoda Kończoca Gre</param>
        public NajlepszeZNajgorszych(int IlośćRuchówDoPrzodu)
        {
            this.IlośćRuchówDoPrzodu = IlośćRuchówDoPrzodu;
        }
        List<int> IlośćMożliwościNiePrzegranych = new List<int>();
        public List<T> KolejneWybory = new List<T>();
        bool czyUlepszaSieInteligencja = false, InteligencjaPrzegrała = false;

        public bool CzyUlepszaSieInteligencja
        {
            get { return czyUlepszaSieInteligencja; }
        }
        ObsugaPołączenia<T> Komunikacja;
        public void DodajUlepszanieInteligencji(ObsugaPołączenia<T> Obiekt)
        {
            Komunikacja = Obiekt;
            czyUlepszaSieInteligencja = true;
        }
        public void Zapisz(BinaryWriter Br,ZapisPlik zp)
        {
            Br.Write(KolejneWybory.Count);
            for (int i = 0; i < KolejneWybory.Count; i++)
            {
                Br.Write(IlośćMożliwościNiePrzegranych[i]);
                zp(Br,KolejneWybory[i]);
            }
        }
        public void Odczyt(BinaryReader br,OdczytPlik op)
        {
            int x = br.ReadInt32();
            for (int i = 0; i < x; i++)
            {
                IlośćMożliwościNiePrzegranych.Add(br.ReadInt32());
                KolejneWybory.Add(op(br));
            }
        }
        public T WybórKomputeraStan(T Mapa, out int Max, out bool Jeden, bool Pierwszy = false)
        {
            Max = Przegrana;
            Jeden = false;
            if (WielkośćTabeli > 0)
            {
                ListaRóchów = new List<RuchE>[WielkośćTabeli];
                ListaOcenaPlańszy = new List<int>[WielkośćTabeli];
                ListaIlośćRuchów = new List<int>[WielkośćTabeli];
                ListaPlansz = new List<T>[WielkośćTabeli];
                for (int i = 0; i < WielkośćTabeli; i++)
                {
                    ListaRóchów[i] = new List<RuchE>();
                    ListaOcenaPlańszy[i] = new List<int>();
                    ListaIlośćRuchów[i] = new List<int>();
                    ListaPlansz[i] = new List<T>();

                }
            }
            List<T> DoWyboru = new List<T>();
            T[] TablicaObiektów = PobieraniePlansz(Mapa, RuchE.Komputer);
            if (TablicaObiektów.Length == 1)
            {
                Jeden = true;
                KolejneWybory.Add(TablicaObiektów[0]);
                IlośćMożliwościNiePrzegranych.Add(1);
                return TablicaObiektów[0];
            }

            IlośćMożliwościNiePrzegranych.Add(0);
            if (TablicaObiektów.Length == 0)
            {
                throw new Exception("nie ma możliwości Ruchu");
            }
            for (int i = 0; i < TablicaObiektów.Length; i++)
            {
                int g = Załaduj(TablicaObiektów[i], IlośćRuchówDoPrzodu, RuchE.Gracz);
                if (CzyObsugiwanaJestIq(Pierwszy))
                {
                    if (Komunikacja.SprawdźCzyJest(TablicaObiektów[i]))
                    {
                        g = Przegrana;
                    }
                    else if (Przegrana != g)
                    {
                        IlośćMożliwościNiePrzegranych[IlośćMożliwościNiePrzegranych.Count - 1]++;
                    }
                }
                if (Max < g)
                {
                    DoWyboru.Clear();
                    DoWyboru.Add(TablicaObiektów[i]);
                    Max = g;
                }
                else if (Max == g)
                {
                    DoWyboru.Add(TablicaObiektów[i]);
                }
            }
            T zwracana = DoWyboru[Los.Next(0, DoWyboru.Count)];
            if (CzyObsugiwanaJestIq(Pierwszy))
            {
                KolejneWybory.Add(zwracana);
                SprawdźDrzewoRozwiozań(KolejneWybory.Count - 1);

                if (Max == Przegrana)
                    InteligencjaPrzegrała = true;
            }
            if (TablicaObiektów.Length == 0)
                throw new BrakMożliwośćiRuchu("Nie znaleziono żadnej możliwosci ruchu");
            return zwracana;
        }

        private bool CzyObsugiwanaJestIq(bool Pierwszy)
        {
            return CzyUlepszaSieInteligencja && Pierwszy && !InteligencjaPrzegrała;
        }
        void SprawdźDrzewoRozwiozań(int i)
        {
            if (!Komunikacja.SprawdźCzyJest(KolejneWybory[i]))
                Komunikacja.Dodaj(KolejneWybory[i]);
            if (IlośćMożliwościNiePrzegranych[i] == 0)
            {
                if (i > 0)
                {
                    IlośćMożliwościNiePrzegranych[i - 1]--;
                    if (IlośćMożliwościNiePrzegranych[i - 1] < 0)
                        IlośćMożliwościNiePrzegranych[i - 1] = 0;
                    SprawdźDrzewoRozwiozań(i - 1);
                }

            }
        }
        public Task<T> WybórKomputeraStanAsync(T Mapa)
        {
            return Task.Factory.StartNew(() => { return WybórKomputeraStan(Mapa); });
        }
        public T WybórKomputeraStan(T Mapa)
        {
            bool Jeden;
            int L;

            T Wynik = WybórKomputeraStan(Mapa, out L, out Jeden, true);
            if (L != Wygrana || Jeden)
                return Wynik;
            int Zap = IlośćRuchówDoPrzodu;
            for (int i = 1; i < Zap; i++)
            {
                IlośćRuchówDoPrzodu = i;
                Wynik = WybórKomputeraStan(Mapa, out L, out Jeden);
                if (L == Wygrana)
                {
                    IlośćRuchówDoPrzodu = Zap;
                    return Wynik;
                }

            }
            return Wynik;

        }
        List<RuchE>[] ListaRóchów;
        List<int>[] ListaOcenaPlańszy;
        List<int>[] ListaIlośćRuchów;
        List<T>[] ListaPlansz;
        int Załaduj(T Mapa, int i, RuchE R)
        {

            int X, Y, Z;
            if (Przeszukajaj(Mapa, out X, out Y, out Z, i, R))
                return Z;
            if (!TrwanieGry(Mapa))
            {
                int m = OcanaPlanszy(Mapa);
                ListaOcenaPlańszy[X][Y] = m;
                return m;
            }
            i--;
            if (i != 0)
            {

                bool b = true;
                int Max = Przegrana, Min = Wygrana;
                T[] Los = PobieraniePlansz(Mapa, R);
                foreach (T ID in Los)
                {
                    b = false;
                    int Wspułczynik;
                    if (R == RuchE.Gracz)
                    {
                        Wspułczynik = Załaduj(ID, i, RuchE.Komputer);
                        if (Wspułczynik < Min)
                            Min = Wspułczynik;
                        if (Min == Przegrana)
                        {
                            ListaOcenaPlańszy[X][Y] = Min;
                            return Min;
                        }

                    }
                    else
                    {
                        Wspułczynik = Załaduj(ID, i, RuchE.Gracz);
                        if (Wspułczynik > Max)
                            Max = Wspułczynik;

                        if (Max == Wygrana)
                        {
                            ListaOcenaPlańszy[X][Y] = Max;
                            return Max;
                        }
                    }
                }

                if (b)
                {
                    int m = OcanaPlanszy(Mapa); ;
                    ListaOcenaPlańszy[X][Y] = m;
                    return m;
                }
                if (R == RuchE.Gracz)
                {
                    ListaOcenaPlańszy[X][Y] = Min;
                    return Min;
                }
                if (R == RuchE.Komputer)
                {
                    ListaOcenaPlańszy[X][Y] = Max;
                    return Max;
                }
            }
            else
            {
                int m = OcanaPlanszy(Mapa); ;
                ListaOcenaPlańszy[X][Y] = m;
                return m;
            }
            return 0;

        }

        private bool Przeszukajaj(T obiekt, out int X, out int Y, out int IloścPunktów, int Numer, RuchE R)
        {
            IloścPunktów = 0;
            X = obiekt.NumerTabeli;
            for (Y = 0; Y < ListaRóchów[X].Count; Y++)
            {
                if (ListaRóchów[X][Y] == R && ListaPlansz[X][Y].Porównanie(obiekt))
                {
                    if (ListaIlośćRuchów[X][Y] < Numer)
                    {
                        ListaIlośćRuchów[X][Y] = Numer;
                        return false;
                    }
                    else
                    {
                        IloścPunktów = ListaOcenaPlańszy[X][Y];
                        return true;
                    }
                }
            }
            ListaPlansz[X].Add(obiekt);
            ListaOcenaPlańszy[X].Add(0);
            ListaRóchów[X].Add(R);
            ListaIlośćRuchów[X].Add(Numer);
            return false;
        }

        public int WielkośćTabeli { get; set; }
    }
    [Serializable]
    public class BrakMożliwośćiRuchu : Exception
    {
        public BrakMożliwośćiRuchu(string s)
            : base(s)
        {
        }
    }
    public interface IPorównawczy<T>
    {
        int NumerTabeli { get; }
        bool Porównanie(T a);

    }
}
