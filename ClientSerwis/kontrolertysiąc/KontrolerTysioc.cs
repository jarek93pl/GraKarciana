using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using static GraKarciana.ObsugaKart;
using GraKarciana;
namespace ClientSerwis
{
    public enum Stan { CzekajNaGracza, CzekajNaLicytacje, TwojaLicytacja, WysylanieMusku, CzekanieNaMusek, CzekanieNaRuch, TwójRuch };
    public class KontrolerTysioc : ITysiocCalback
    {
        readonly int IlośćGraczy;
        public ReadOnlyCollection<Karta> Stół { get => stół?.AsReadOnly(); }
        public ReadOnlyCollection<Karta> TwojeKarty { get => twojeKarty?.AsReadOnly(); }
        List<Karta> twojeKarty;
        List<Karta> stół = new List<Karta>();
        public Urzytkownik[] ListaUrzytkowników;
        ITysioc tk;
        #region zdażenia
        public int IndexPlayer(string u) => ListaUrzytkowników.FindIndex(X => X.Nazwa == u);
        static readonly object KeyZmianaStołu = new object();//snipet desing
        public event EventHandler ZmianaStołu
        {
            add
            {
                DzienikZdarzeń.AddHandler(KeyZmianaStołu, value);
            }
            remove => DzienikZdarzeń.RemoveHandler(KeyZmianaStołu, value);
        }


        readonly EventHandlerList DzienikZdarzeń = new EventHandlerList();

        static readonly object KeyKtośZalicytował = new object();//snipet desing
        public event EventHandler<Tuple<Urzytkownik, int>> KtośZalicytował
        {

            add => DzienikZdarzeń.AddHandler(KeyKtośZalicytował, value);
            remove => DzienikZdarzeń.RemoveHandler(KeyKtośZalicytował, value);
        }
        static readonly object KeyTwójRuch = new object();//snipet desing
        public event EventHandler TwójRuchEv
        {
            add => DzienikZdarzeń.AddHandler(KeyTwójRuch, value);
            remove => DzienikZdarzeń.RemoveHandler(KeyTwójRuch, value);
        }

        static readonly object KeyTwojaLicytacja = new object();//snipet desing
        public event EventHandler TwojaLicytacjaEv
        {
            add => DzienikZdarzeń.AddHandler(KeyTwojaLicytacja, value);
            remove => DzienikZdarzeń.RemoveHandler(KeyTwojaLicytacja, value);
        }

        static readonly object KeyOdbieranieKart = new object();//snipet desing
        public event EventHandler<Karta[]> OdbieranieKart
        {
            add => DzienikZdarzeń.AddHandler(KeyOdbieranieKart, value);
            remove => DzienikZdarzeń.RemoveHandler(KeyOdbieranieKart, value);
        }

        static readonly object KeyOdbierzMusek = new object();//snipet desing
        public event EventHandler<Karta[]> OdbierzMusek
        {
            add => DzienikZdarzeń.AddHandler(KeyOdbierzMusek, value);
            remove => DzienikZdarzeń.RemoveHandler(KeyOdbierzMusek, value);
        }
        static readonly object KeyOdbierzKartęOdGracza = new object();//snipet desing
        public event EventHandler<Karta[]> OdbierzKartęOdGracza
        {
            add => DzienikZdarzeń.AddHandler(KeyOdbierzKartęOdGracza, value);
            remove => DzienikZdarzeń.RemoveHandler(KeyOdbierzKartęOdGracza, value);
        }
        static readonly object KeyKtośWysłałKarte = new object();//snipet desing
        public event EventHandler<Tuple<Urzytkownik, Karta>> KtośWysłałKarte
        {
            add => DzienikZdarzeń.AddHandler(KeyKtośWysłałKarte, value);
            remove => DzienikZdarzeń.RemoveHandler(KeyKtośWysłałKarte, value);
        }
        static readonly object KeyZmianaStanu = new object();//snipet desing
        public event EventHandler ZmianaStanu
        {
            add
            {
                DzienikZdarzeń.AddHandler(KeyZmianaStanu, value);
            }
            remove => DzienikZdarzeń.RemoveHandler(KeyZmianaStanu, value);
        }
        #endregion
        Stan _stan;
        public Stan Stan
        {
            get
            {
                return _stan;
            }
            protected set
            {
                _stan = value;
                (DzienikZdarzeń[KeyZmianaStanu] as EventHandler)?.Invoke(this, EventArgs.Empty);
            }
        }

        public KontrolerTysioc()
        {

            IlośćGraczy = 3;
            Stan = Stan.CzekajNaGracza;
        }
        public void Initialize(ITysioc tk)
        {
            this.tk = tk;
        }
        public Task CzekajNaGraczaAync(int nr) => tk.CzekajNaGraczaAsync(nr);


        public Task LicytujAsync(int pk)
        {
            if (pk < 100)
            {
                throw new Exception("wartość nie może być mniejsza od 100");
            }
            if (Stan == Stan.TwojaLicytacja)
            {
                Stan = Stan.CzekajNaLicytacje;
                return tk.LicytujAsync(pk);

            }
            throw new InvalidCastException("teraz nie jest twój ruch");
        }
        public Task WyslijKarteAsync(Karta k, bool Melduj)
        {
            Stan = Stan.CzekanieNaRuch;
            twojeKarty.Remove(k);
            (DzienikZdarzeń[KeyZmianaStołu] as EventHandler)?.Invoke(this, EventArgs.Empty);
            return tk.WyslijKarteAsync(k, Melduj);
        }


        public Task WyslijKarteMeldującAsync(Karta k)
        {

            bool CzyMożnaMeldować = k.PobierzKarte() == Karta.Dama && ObsugaTysiąc.IstniejeMeldunek(k, twojeKarty) && stół.Count == 0;

            return WyslijKarteAsync(k, CzyMożnaMeldować);
        }
        public ReadOnlyCollection<Karta> DostępneKarty { get; private set; }
        public void ZnalezionoNowychGraczy(Urzytkownik[] gracze) => ListaUrzytkowników = gracze;

        public void TwojaLicytacja()
        {
            Stan = Stan.TwojaLicytacja;
            (DzienikZdarzeń[KeyTwojaLicytacja] as EventHandler)?.Invoke(this, EventArgs.Empty);
        }

        public void Twojruch()
        {
            Stan = Stan.TwójRuch;
            DostępneKarty = ObsugaTysiąc.ZaładujDostepneKarty(twojeKarty, stół, aktywnaKozera, Kozera).AsReadOnly();
            (DzienikZdarzeń[KeyTwójRuch] as EventHandler)?.Invoke(this, EventArgs.Empty);
        }



        public void OdbierzKarty(List<Karta> k)
        {
            var karty = k.ToArray();
            if (karty.Length > 5)//odbieranie kart
            {
                Stan = Stan.CzekajNaLicytacje;
                twojeKarty = karty.ToList();
                (DzienikZdarzeń[KeyOdbieranieKart] as EventHandler<Karta[]>)?.Invoke(this, karty);
            }
            else if (karty.Length > 1)//odbieranie musku
            {
                Stan = Stan.WysylanieMusku;
                twojeKarty.AddRange(karty);
                (DzienikZdarzeń[KeyOdbierzMusek] as EventHandler<Karta[]>)?.Invoke(this, karty);
            }
            else
            {
                Stan = Stan.CzekanieNaRuch;
                twojeKarty.AddRange(karty);
                (DzienikZdarzeń[KeyOdbierzKartęOdGracza] as EventHandler<Karta[]>)?.Invoke(this, karty);
            }

            (DzienikZdarzeń[KeyZmianaStołu] as EventHandler)?.Invoke(this, EventArgs.Empty);
        }
        public int MinimalnaWartośćLicytacji { get; protected set; }
        public void KtosZalicytowal(string Login, int cena)
        {
            MinimalnaWartośćLicytacji = cena;
            (DzienikZdarzeń[KeyKtośZalicytował] as EventHandler<Tuple<Urzytkownik, int>>)?.Invoke(this, new Tuple<Urzytkownik, int>(WeźUrzytkownika(Login), cena));
        }

        private Urzytkownik WeźUrzytkownika(string login) => ListaUrzytkowników.First(X => X.Nazwa == login);
        public Task WysyłanieMuskuAsync(IList<Karta> KartyDoUsuniecia)
        {
            Stan = Stan.CzekanieNaRuch;
            var DoUsunieca = new HashSet<Karta>(KartyDoUsuniecia);
            twojeKarty.RemoveAll(X => DoUsunieca.Contains(X));
            (DzienikZdarzeń[KeyZmianaStołu] as EventHandler)?.Invoke(this, EventArgs.Empty);
            return tk.WyslijMusekAsync(KartyDoUsuniecia.ToArray());
        }
        public Karta Kozera { get; private set; }
        public bool AktywnaKozera { get => aktywnaKozera; }

        bool aktywnaKozera;
        public void KtosWyslalKarte(Karta k, string s, bool Melduj)
        {
            stół.Add(k);
            if (stół.Count == IlośćGraczy)
            {
                stół.Clear();
            }
            if (Melduj)
            {
                Kozera = k.Kolor();
                aktywnaKozera = true;
            }
            (DzienikZdarzeń[KeyZmianaStołu] as EventHandler)?.Invoke(this, EventArgs.Empty);
            (DzienikZdarzeń[KeyKtośWysłałKarte] as EventHandler<Tuple<Urzytkownik, Karta>>)?.Invoke(this, new Tuple<Urzytkownik, Karta>(WeźUrzytkownika(s), k));
        }

        public void KoniecGry(PodsumowanieTysioc pk)
        {
            Stan = Stan.CzekajNaLicytacje;
            //throw new NotImplementedException();
        }

        public void PodsumowanieRozgrywki(PodsumowanieTysioc pk)
        {
            aktywnaKozera = false;
            stół?.Clear();
            twojeKarty?.Clear();
        }

    }
}
