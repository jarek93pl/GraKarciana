using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komputer.Matematyczne
{
    /// <summary>
    /// algorytm nie dokończony
    /// komurka= miejsce na daną
    /// >jeżeli przekierowuje nawias po nim zostawia wolne miejsca(nie używana komurka)
    /// >nie szuka optymalnej kolejności która by pozwoliła jak najszypciej przestać używać jak najwiekszjej ilości komórek
    /// >tworzy nowe a nie wykorzystuje starek komurki
    /// </summary>
    public class Funkcje<T>
    {
        public delegate T RachunekSybmolu(params T[] Dane);
        public delegate T PrzekształcenieNaDaną(string s);
        PrzekształcenieNaDaną MojePrzekształcenie;
        List<T> WartościStałe = new List<T>();
        List<string> WartościStałeString = new List<string>();
        private List<string> ListaNawiasów = new List<string>();
        List<Komeda> DostepneSymole = new List<Komeda>();
        int AdresNajbliszy = 0;
        public readonly List<char> ListaZmienych = new List<char>();
        char[] AkceptowaneZnakiLidrzby;
        string text;
        List<int> Polecenia = new List<int>();
        /// <summary>
        /// Wpisz scieżke poleceń
        /// </summary>
        /// <param name="s">Scieżka uwaga !!tylko małe Litery do oznaczania zmienych i zabronione jest kożystanie ze znaku #</param>
        public Funkcje(char[] AkceptowaneZnaki,PrzekształcenieNaDaną StringNaDaną)
        {
            this.AkceptowaneZnakiLidrzby = AkceptowaneZnaki;
            MojePrzekształcenie=StringNaDaną;
           
        }
        protected void Kompiluj(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {

                if (CzyLitera(s[i]))
                {
                    if (!CzyDużaLietera(s[i]))
                    {
                        if (SprawdźKomedy(s[i]))
                        {
                           
                            if (!CzyZnakLidrzba(s[i]))
                                throw new BłednyWpis("nie znaleziono komendy o znaku '" + s[i] + "'");
                        }
                    }

                }
                else
                {

                    if (s[i] == '(' || s[i] == ')' || CzyZnakLidrzba(s[i]))
                        continue;
                    if (!SprawdźKomedy(s[i]))
                    {
                       
                        if (!CzyZnakLidrzba(s[i]))
                            throw new BłednyWpis("nie znaleziono  komedy o znaku '" + s[i] + "'");
                    }
                }
            }
            text = s + "+";//by odrazu się nie kończyło
            ZamieńNaDane();
            text = text.Substring(0, text.Length - 1);/// powrót do stanu przed dodaniem

            ListaNawiasów.Add(text);
            NawiasyZMieńNaDane();
            GenerujKomendy();

        }
        protected void DodajSymbol(Komeda l)
        {
                if (CzyZnakLidrzba(l.znak))
                    throw new BłednyWpis("Nie możesz lidrzby jako sybol");
                if (SprawdźKomedy(l.znak))
                    throw new BłednyWpis("sybol komendy jest już używany");
                DostepneSymole.Add(l);
        }
        private string ZapiszAdres(int i)
        {
            string s = "#";
            if (i < 100)
            {
                s += "0";
                if (i < 10)
                {
                    s += "0";
                }
            }
            s += i.ToString();
            s += "#";
            return s;

        }
        /// <summary>
        /// sprawdza wcześniejsze odniesienie do tego adresu
        /// </summary>
        /// <param name="znak"></param>
        /// <returns></returns>
        private void NawiasyZMieńNaDane()
        {

            int PoczotkiNawiasów = 0, NrNawiasów = 0;
            for (int ii = 0; ii < ListaNawiasów.Count; ii++)
            {
                for (int i = 0; i < ListaNawiasów[ii].Length; i++)
                {
                    if (ListaNawiasów[ii][i] == '(')
                    {
                        if (NrNawiasów == 0)
                            PoczotkiNawiasów = i;
                        NrNawiasów++;
                        continue;
                    }
                    if (ListaNawiasów[ii][i] == ')')
                    {

                        NrNawiasów--;
                        if (NrNawiasów == 0)
                        {

                            if (PoczotkiNawiasów + 1 == i)
                                throw new BłednyWpis("nawias nie może być pusty");
                            if (NrNawiasów < 0)
                                throw new BłednyWpis("źle ustawiony nawias");


                            ListaNawiasów.Add(ListaNawiasów[ii].Substring(PoczotkiNawiasów + 1, i - PoczotkiNawiasów - 1));
                            ListaNawiasów[ii] = ListaNawiasów[ii].Substring(0, PoczotkiNawiasów) + ZapiszAdres(AdresNajbliszy) + ListaNawiasów[ii].Substring(i + 1, ListaNawiasów[ii].Length - i - 1);
                            i += 5 - i + PoczotkiNawiasów;
                            AdresNajbliszy++;
                        }

                    }
                }
            }
        }
        private bool SpradźIdetyczneStałe(string s, ref int danyAdres)
        {
            for (int i = 0; i < WartościStałeString.Count; i++)
            {
                if (WartościStałeString[i] == s)
                {
                    danyAdres = i + ListaZmienych.Count;
                    return true;
                }
            }
            return false;
        }
        private void ZamieńNaDane()
          {

              for (int i = 0; i < text.Length; i++)
              {
                  if (CzyLitera(text[i]) && !CzyDużaLietera(text[i]))
                  {
                      int DAdres = SprawdźCzyNiema(text[i]);
                      if (DAdres == -1)
                      {
                          DAdres = AdresNajbliszy;
                          ListaZmienych.Add(text[i]);
                          AdresNajbliszy++;
                      }
                      text = text.Substring(0, i) + ZapiszAdres(DAdres) + text.Substring(i + 1, text.Length - i - 1);


                      i += 4;
                  }

              }
              int PoczotekLidrzby = 0;

              bool CzyByłaLidzba = false;
              for (int i = 0; i < text.Length; i++)
              {
                  if (text[i] == '#')
                  {
                      i += 4;
                  }
                  else if (CzyZnakLidrzba(text[i]) && i < text.Length - 1 & !CzyByłaLidzba)
                  {
                      CzyByłaLidzba = true;
                      PoczotekLidrzby = i;

                  }
                  else if (CzyByłaLidzba && !CzyZnakLidrzba(text[i]))
                  {

                      string s = text.Substring(PoczotekLidrzby, i - PoczotekLidrzby);
                      int AdresDlaKomurki = AdresNajbliszy;
                      if (!SpradźIdetyczneStałe(s, ref AdresDlaKomurki))
                      {
                          WartościStałeString.Add(s);
                          text = text.Substring(0, PoczotekLidrzby) + ZapiszAdres(AdresDlaKomurki) + text.Substring(i, text.Length - i);
                          AdresNajbliszy++;
                          WartościStałe.Add(MojePrzekształcenie(s));
                      }
                      else
                      {
                          text = text.Substring(0, PoczotekLidrzby) + ZapiszAdres(AdresDlaKomurki) + text.Substring(i, text.Length - i);
                      }
                      i += 5 - i + PoczotekLidrzby;
                      CzyByłaLidzba = false;
                  }

              }
          }
        private void GenerujKomendy()
        {
            int NumerScieżkiNawiasu = AdresNajbliszy - 1;
            for (int i = ListaNawiasów.Count - 1; i >= 0; i--, NumerScieżkiNawiasu--)
            {
                bool DanaWystepowała = true;
                for (int OZnaczenieKolejności = 5; OZnaczenieKolejności >= 1; OZnaczenieKolejności--)
                {
                    for (int ii = 0; ii < ListaNawiasów[i].Length; ii++)
                    {
                        if (ListaNawiasów[i][ii] == '#' || CzyZnakLidrzba(ListaNawiasów[i][ii]))
                            continue;
                        int NrKomendy = SprawdźNumerKomedy(ListaNawiasów[i][ii]);
                        if (DostepneSymole[NrKomendy].PoziomPierszeństwaKomedy != OZnaczenieKolejności)
                            continue;
                        if (DostepneSymole[NrKomendy].ilośćAgumentów == 1)
                        {
                            int AdresDanej1 = AdresNaInt(ii, ListaNawiasów[i]);
                            int AdresWyniku = AdresNajbliszy;
                            if (!SzukajIdetycznejWartosci(NrKomendy, AdresDanej1, -1, ref AdresWyniku))
                            {
                                DanaWystepowała = false;
                                Polecenia.Add(NrKomendy);
                                Polecenia.Add(AdresDanej1);
                                Polecenia.Add(AdresWyniku);
                                AdresNajbliszy++;
                            }
                            else
                                DanaWystepowała = true;

                            ListaNawiasów[i] = ListaNawiasów[i].Substring(0, ii) + ZapiszAdres(AdresWyniku) + ListaNawiasów[i].Substring(ii + 6, ListaNawiasów[i].Length - ii - 6);
                        }
                        else
                        {
                            int AdresDanej1 = AdresNaInt(ii - 6, ListaNawiasów[i]), AdresDanej2 = AdresNaInt(ii, ListaNawiasów[i]);
                            int AdresWyniku = AdresNajbliszy;
                            if (!SzukajIdetycznejWartosci(NrKomendy, AdresDanej1, AdresDanej2, ref AdresWyniku))
                            {
                                DanaWystepowała = false;
                                Polecenia.Add(NrKomendy);
                                Polecenia.Add(AdresDanej1);
                                Polecenia.Add(AdresDanej2);
                                Polecenia.Add(AdresWyniku);
                                AdresNajbliszy++;
                            }
                            else
                                DanaWystepowała = true;
                            ListaNawiasów[i] = ListaNawiasów[i].Substring(0, ii - 5) + ZapiszAdres(AdresWyniku) + ListaNawiasów[i].Substring(ii + 6, ListaNawiasów[i].Length - ii - 6);

                            ii -= 5;
                        }

                    }
                }
                if (ListaNawiasów[i].Length == 5)
                {
                    if (i != 0)
                    {
                        if (!DanaWystepowała)
                        {
                            Polecenia[Polecenia.Count - 1] = NumerScieżkiNawiasu;
                            AdresNajbliszy--;
                        }
                        else
                        {
                            ZamieńOdnieśienia(ZapiszAdres(NumerScieżkiNawiasu), ListaNawiasów[i], i);
                        }
                    }
                    else
                    {
                        
                        Polecenia[Polecenia.Count - 1] = AdresNajbliszy - 1;
                    }
                }
                else
                {
                    throw new BłednyWpis("zły wzór");
                }
            }
        }
        /// <summary>
        /// działą tylko dla liter
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private bool CzyDużaLietera(char r)
        {
            return r.ToString().ToUpper() == r.ToString();
        }
        private void ZamieńOdnieśienia(string Zamieniane, string Przykrywające, int ograniczenie)
        {
            for (int i = 0; i < ograniczenie; i++)
            {
                for (int ii = 0; ii < ListaNawiasów[i].Length; ii++)
                {
                    if (ListaNawiasów[i][ii] == '#' && ii + 1 < ListaNawiasów[i].Length && CzyZnakLidrzba(ListaNawiasów[i][ii + 1]))
                    {
                        if (ListaNawiasów[i].Substring(ii, 5) == Zamieniane)
                        {
                            ListaNawiasów[i] = ListaNawiasów[i].Substring(0, ii) + Przykrywające + ListaNawiasów[i].Substring(ii + 5, ListaNawiasów[i].Length - 5 - ii);
                        }
                    }
                    ii += 5;
                }
            }
        }
        public static bool CzyLitera(char r)
        {
            return !(r.ToString().ToUpper() == r.ToString().ToLower());
        }
        public bool CzyZnakLidrzba(char r)
        {
            foreach (char c in AkceptowaneZnakiLidrzby)
                if (c == r)
                    return true;
            return false;
        }
        private int SprawdźCzyNiema(char znak)
        {
            for (int i = 0; i < ListaZmienych.Count; i++)
            {
                if (ListaZmienych[i] == znak)
                    return i;
            }
            return -1;

        }
        /// <summary>
        /// odnajduje czy idetyczna zmiena nie została użyta wcześniej
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private bool SzukajIdetycznejWartosci(int NrKomedy, int wartość1, int wartość2, ref int DanyAdres)
        {
            for (int i = 0; i < Polecenia.Count; i++)
            {

                if (DostepneSymole[Polecenia[i]].ilośćAgumentów == 2)
                {
                    if (Polecenia[i] == NrKomedy && wartość1 == Polecenia[i + 1] && wartość2 == Polecenia[i + 2])
                    {
                        DanyAdres = Polecenia[i + 3];
                        return true;
                    }
                    i += 3;
                }
                else if (DostepneSymole[Polecenia[i]].ilośćAgumentów == 1)
                {
                    if (Polecenia[i] == NrKomedy && wartość1 == Polecenia[i + 1])
                    {
                        DanyAdres = Polecenia[i + 2];
                        return true;
                    }
                    i += 2;
                }
            }
            return false;
        }
        public T Wywołaj(T[] TabDaneWejscia)
        {
            if (TabDaneWejscia.Length != ListaZmienych.Count)
                throw new BłednyWpis(string.Format("lista zmienych to {0} a podałeś {1}", ListaZmienych.Count, TabDaneWejscia.Length));
            T[] tab = new T[AdresNajbliszy];
            for (int i = 0; i < TabDaneWejscia.Length; i++)
                tab[i] = TabDaneWejscia[i];
            for (int i = 0; i < WartościStałe.Count; i++)
                tab[i + TabDaneWejscia.Length] = WartościStałe[i];
            for (int i = 0; i <Polecenia.Count; i++)
            {
                if (DostepneSymole[Polecenia[i]].ilośćAgumentów == 1)
                {
                    tab[Polecenia[i + 2]] =DostepneSymole[Polecenia[i]].zdarzenie(tab[Polecenia[i + 1]]);
                    i += 2;
                }
                else
                {
                    tab[Polecenia[i + 3]] = DostepneSymole[Polecenia[i]].zdarzenie(tab[Polecenia[i + 1]], tab[Polecenia[i + 2]]);
                    i += 3;
                }
            }
            return tab[tab.Length - 1];


        }
        private int AdresNaInt(int i, string s)
        {
            return Convert.ToInt32(s.Substring(i + 2, 3));
        }
        private int SprawdźNumerKomedy(char r)
        {
            int i = 0;
            foreach (Komeda d in DostepneSymole)
            {
                if (d.znak == r)
                    return i;

                i++;
            }
            throw new BłednyWpis("nie znaleziono komendy");
        }
        private bool SprawdźKomedy(char r)
        {
            foreach (Komeda d in DostepneSymole)
            {
                if (d.znak == r)
                    return true;
            }
            return false;
        }
        public interface Komeda
        {
            int ilośćAgumentów { get; }
            char znak { get; }
            int PoziomPierszeństwaKomedy { get; }
            RachunekSybmolu zdarzenie { get; }
        }
        public class Komeda1Argumetowa : Komeda
        {

            char a;
            int b = 0;
            RachunekSybmolu c;
            public Komeda1Argumetowa(RachunekSybmolu Zdażenie, int Poziom, char Znak)
            {
                if (Znak == '#')
                    throw new BłednyWpis("Nie możesz użyć znaku '#' jako symbol");
                if (!(Poziom < 6 && Poziom > 0))
                    throw new BłednyWpis("poziom kolejnosci działań musi być miedzy 1 a 5");
                this.c = Zdażenie;
                this.b = Poziom;
                this.a = Znak;
            }

            public char znak
            {
                get { return a; }
            }
            public int ilośćAgumentów
            {
                get { return 1; }
            }
            public int PoziomPierszeństwaKomedy
            {
                get { return b; }
            }
            public RachunekSybmolu zdarzenie
            {
                get { return c; }
            }
        }
        public class Komeda2Argumetowa : Komeda
        {
            char a;
            int b = 0;
            RachunekSybmolu c;
            public Komeda2Argumetowa(RachunekSybmolu Zdażenie, int Poziom, char Znak)
            {
                if (Znak == '#')
                    throw new BłednyWpis("Nie możesz użyć znaku '#' jako symbol");
                if (!(Poziom < 6 && Poziom > 0))
                    throw new BłednyWpis("poziom kolejnosci działań musi być miedzy 1 a 5");
                this.c = Zdażenie;
                this.b = Poziom;
                this.a = Znak;
            }

            public int ilośćAgumentów
            {
                get { return 2; }
            }
            public char znak
            {
                get { return a; }
            }
            public int PoziomPierszeństwaKomedy
            {
                get { return b; }
            }
            public RachunekSybmolu zdarzenie
            {
                get { return c; }
            }
        }
    }
    
    public class BłednyWpis : Exception
    {
        public BłednyWpis(string s)
            : base(s)
        {
        }
    }
}
