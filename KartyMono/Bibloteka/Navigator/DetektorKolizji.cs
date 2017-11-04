using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Komputer.Matematyczne.Figury;
using System.Drawing;
using Microsoft.Xna.Framework;
namespace Komputer.Matematyczne.Silnik
{
    public class PrzetrzeńKolizji
    {
        public bool WyjdźPoZaZakres = false;
        int IlośćPólX, IlośćPólY;
        int IndexZmiany = 0;
        float SzerokośćKratki, WysokośćKratki, NajdalszyX, NajdalszyY, NajbliszyX, NajbliszyY;
        Tab<List<ObiektKolizji>> Mapa;
        public PrzetrzeńKolizji(int IlośćX, int IlośćY, float SzerokośćKratki, float WysokośćKratki)
        {
            this.IlośćPólX = IlośćX;
            this.IlośćPólY = IlośćY;
            Mapa = new Tab<List<ObiektKolizji>>(IlośćX, IlośćY);
            for (int i = -IlośćX; i < IlośćX; i++)
            {
                for (int ii = -IlośćY; ii < IlośćY; ii++)
                {
                    Mapa[i, ii] = new List<ObiektKolizji>();
                }
            }
            this.SzerokośćKratki = SzerokośćKratki;
            this.WysokośćKratki = WysokośćKratki;
            NajdalszyX = SzerokośćKratki * IlośćX;
            NajdalszyY = WysokośćKratki * IlośćY;
            NajbliszyX = -NajdalszyX;
            NajbliszyY = -NajdalszyY;
        }
        /// <summary>
        /// z Kołem 
        /// </summary>
        /// <param name="Miejsce"></param>
        /// <param name="Odległość"></param>
        /// <returns></returns>
        public List<ObiektKolizji> ZnajdźWsztstkieKolidujące(Vector2 Miejsce, float Odległość)
        {
            float średnica=Odległość*2;
            float kwadratpromienia=Odległość*Odległość;
            var z = ZnajdźWsztstkieKolidujące(Miejsce.X - Odległość, Miejsce.Y - Odległość, średnica, średnica);
            for (int i = 0; i < z.Count; i++)
			{
                float zx=Miejsce.X-z[i].Miejsce.X;
                float zy=Miejsce.Y-z[i].Miejsce.Y;
                if (zy*zy+zx*zx>kwadratpromienia)
	            {
		            z.RemoveAt(i);
                    i--;
	            }
			}
            return z;
        }
        public void Dodaj(ObiektKolizji dodawany,out List<ObiektKolizji> Blokujące)
        {
            IndexZmiany++;
            Komputer.Matematyczne.Graf.Krawedz o = dodawany.Szkielet as Komputer.Matematyczne.Graf.Krawedz;
            if (o!=null)
            {
            }
            List<List<ObiektKolizji>> KwadratyDoKtórychNależy;
            if (dodawany==null)
            {
                Blokujące = null;
                return;
            }
            if (dodawany.Szkielet != null)
            {
                dodawany.PrzesóńOMiejsce();
                Blokujące = ZnajdźWsztstkieKolidujące(dodawany, out KwadratyDoKtórychNależy);
                foreach (List<ObiektKolizji> item in KwadratyDoKtórychNależy)
                {
                    item.Add(dodawany);
                }
                
            }
            else
            {
                Blokujące = null;
                Point MiejsceDodania= PrzydzielPunkt(dodawany.Miejsce);
                Mapa[MiejsceDodania.X, MiejsceDodania.Y].Add(dodawany);
                
            }

        }
        private List<ObiektKolizji> PobierzObieky()
        {
            List<ObiektKolizji> l = new List<ObiektKolizji>();
            foreach (List<ObiektKolizji> item in Mapa)
            {
                foreach (var item2 in item)
                {
                    l.Add(item2);
                }
            }
            return l;
        }
        List<ObiektKolizji> Pomocnicza_WszystkieObiekty;
        int Pomocnicza_WszystkieObiekty_IndexZmiany = -1;
        public IEnumerable<ObiektKolizji> WszystkieObiekty()
        {
            if (IndexZmiany == Pomocnicza_WszystkieObiekty_IndexZmiany)
                return Pomocnicza_WszystkieObiekty;
            else
            {
                Pomocnicza_WszystkieObiekty = PobierzObieky();
                Pomocnicza_WszystkieObiekty_IndexZmiany = IndexZmiany;
                return Pomocnicza_WszystkieObiekty;

            }
        }
        public void Dodaj(ObiektKolizji dodawany)
        {
            List<ObiektKolizji> o;
            Dodaj(dodawany, out o);

        }
        /// <summary>
        /// z szkieletem
        /// </summary>
        /// <param name="d"></param>
        /// <param name="Kwadraty"></param>
        /// <returns></returns>
        public List<ObiektKolizji> ZnajdźWsztstkieKolidujące(ObiektKolizji d, out List<List<ObiektKolizji>> Kwadraty)
        {
            d.PrzesóńOMiejsce();
            List<ObiektKolizji> zwracane = new List<ObiektKolizji>();
            Kwadraty=PrzydzielDoSzkieletuKwadratyObj(d);
            foreach (ObiektKolizji item in Kwadraty.ZwracajPodwójnąListe())
            {
                if (item.wrażliwośćKolizji.Kolizja(d) && d.Szkielet != null && item.Szkielet != null && d.Szkielet.Kolizja(item.Szkielet))
                {
                    
                    zwracane.Add(item);
                }
            }
            return zwracane;
        }
        public List<ObiektKolizji> ZnajdźWsztstkieKolidujące(ObiektKolizji d)
        {
            List<List<ObiektKolizji>> las;
            return ZnajdźWsztstkieKolidujące(d, out las);
        }
        /// <summary>
        /// z prostokątem
        /// </summary>
        /// <param name="PoczotekX"></param>
        /// <param name="PoczotekY"></param>
        /// <param name="Szerokość"></param>
        /// <param name="Wysokość"></param>
        /// <returns></returns>
        public List<ObiektKolizji> ZnajdźWsztstkieKolidujące(float PoczotekX, float PoczotekY, float Szerokość, float Wysokość)
        {

            List<ObiektKolizji> zwracana = new List<ObiektKolizji>();
            int PoczotekIX = -IlośćPólX ;
            if (PoczotekX > NajbliszyX)
                PoczotekIX = Convert.ToInt32((PoczotekX ) / SzerokośćKratki);

            int PoczotekIY = -IlośćPólY;
            if(PoczotekY>NajbliszyY)
                PoczotekIY = Convert.ToInt32((PoczotekY) / WysokośćKratki);
            float KoniecFX = PoczotekX + Szerokość;
            float KoniecFY = PoczotekY + Wysokość;
            int KoniecIX=IlośćPólX;
            if(KoniecFX<NajdalszyX)
                KoniecIX = Convert.ToInt32((KoniecFX ) / SzerokośćKratki);
            int KoniecIY = IlośćPólY;
            if(KoniecFY<NajdalszyY)
                KoniecIY = Convert.ToInt32((KoniecFY) / WysokośćKratki);
            int Dodatkowa = PoczotekIY;
            while (PoczotekIX<KoniecIX)
            {
                while (Dodatkowa < KoniecIY)
                {
                    foreach (ObiektKolizji item in Mapa[PoczotekIX,Dodatkowa])
                    {
                        if (PoczotekX<item.Miejsce.X&&item.Miejsce.X<KoniecFX&&PoczotekY<item.Miejsce.Y&&item.Miejsce.Y<KoniecFY)
                        {
                            zwracana.Add(item);
                        }
                    }
                    Dodatkowa++;
                }
                PoczotekIX++;
                Dodatkowa = PoczotekIY;
            }
            return zwracana;
        }
        private List<List<ObiektKolizji>> PrzydzielDoSzkieletuKwadratyObj(ObiektKolizji o)
        {
            return Mapa.WybierzWybraneElementy(PrzydzielDoSzkieletuKwadraty(o));
        }
        public void Usuń(ObiektKolizji ok)
        {
            foreach (List<ObiektKolizji> item in Mapa)
            {
                item.Remove(ok);
            }
        }
        public void Usuń(ObiektKolizji ok,Point p)
        {
            IndexZmiany++;
            Mapa[p.X, p.Y].Remove(ok);
        }
        
        public void Usuń(Predicate<ObiektKolizji> warunek)
        {
            IndexZmiany++;
            foreach (List<ObiektKolizji> item in Mapa)
            {
                item.RemoveAll(warunek);
            }
        }
        private List<Point> PrzydzielDoSzkieletuKwadraty(ObiektKolizji o)
        {
            List<Point> PunktyTabel = new List<Point>();
            foreach (Odcinek item in o.Szkielet)
            {
                item.UstawPoczotek();
                PunktyTabel.DodajNiePowtarzające(PrzydzielDoOdcinkaKwadraty(item));
            }
            return PunktyTabel;
        }
        private List<Point> PrzydzielDoOdcinkaKwadraty(Odcinek o)
        {
            List<Point> Zwracana = new List<Point>();
            Point Poczotek = new Point(Convert.ToInt32(o.PoczotekX / SzerokośćKratki), Convert.ToInt32(o.PoczotekY / WysokośćKratki));
            Point Koniec = new Point(Convert.ToInt32(o.KoniecX / SzerokośćKratki), Convert.ToInt32(o.KoniecY / WysokośćKratki));
            if (Poczotek == Koniec)
            {
                Zwracana.Add(Poczotek);
                return Zwracana;
            }
            o.WyZnaczAB();
            if (!o.WyznaczoneAB || Poczotek.X == Koniec.X)
            {
                PrzydzielajDoWartosciKwadraty(Poczotek.X, o.PoczotekY, o.KoniecY, Zwracana);
                return Zwracana;
            }

            int IX = Poczotek.X + 1;
            float KoniecPoprzedniego = o.WartośćY((IX) * SzerokośćKratki);
            PrzydzielajDoWartosciKwadraty(Poczotek.X, o.PoczotekY, KoniecPoprzedniego, Zwracana);
            PrzydzielajDoWartosciKwadraty(Koniec.X, o.KoniecY, o.WartośćY(Koniec.X * SzerokośćKratki), Zwracana);
            while (IX < Koniec.X)
            {
                PrzydzielajDoWartosciKwadraty(IX++, KoniecPoprzedniego, KoniecPoprzedniego = o.WartośćY((IX) * SzerokośćKratki), Zwracana);
            }

            return Zwracana;

        }
        private void PrzydzielajDoWartosciKwadraty(int x, float PoczotekY, float KoniecY, List<Point> Zwracana)
        {
            int p = Convert.ToInt32(PoczotekY / WysokośćKratki);
            int k = Convert.ToInt32(KoniecY / SzerokośćKratki);
            if (p>k)
            {
                int l = p;
                p = k;
                k = l;
            }

            for (;p <= k; p++)
            {
                Zwracana.Add(new Point(x, p));
            }
        }
        private Point PrzydzielPunkt(Vector2 o)
        {
            return new Point(Convert.ToInt32(o.X / SzerokośćKratki), Convert.ToInt32(o.Y / WysokośćKratki));
        }
        public class ObiektKolizji : IPrzestrzeniKolizji
        {
            bool Zmiana;
            public ObiektKolizji(Vector2 Miejsce)
            {
                this.Miejsce = Miejsce;
                wrażliwośćKolizji = new RelacjiNiesymetrycznej();
            }
            public RelacjiNiesymetrycznej wrażliwośćKolizji{get;set;}
            public void PrzesóńOMiejsce()
            {
                if (!Zmiana)
                {
                Szkielet += Miejsce;
                
                Zmiana = true;
                    
                }
            }
            public FiguraZOdcinków Szkielet { get; set; }
            public Vector2 Miejsce { get; private set; }
            public object Tag;
        }
        class Tab<T> : IEnumerable<T>
        {
            T[,] a;
            int ax, ay, ax2;
            public Tab(int X, int Y)
            {
                ax = X;
                ay = Y;
                a = new T[ax * 2, ay * 2];
            }
            public T this[int x, int y]
            {
                get {
                    if (x<0||y<0)
                    {
                        
                    }
                    return a[x + ax, y + ay]; }
                set {

                    if (x < 0 || y < 0)
                    {

                    }
                    a[x + ax, y + ay] = value; }
            }
            public List<T> WybierzWybraneElementy(IEnumerable<Point> ie)
            {
                List<T> ls = new List<T>();
                foreach (Point item in ie)
                {
                    ls.Add(a[item.X+ax, item.Y+ay ]);
                }
                return ls;
            }



            class raz : IEnumerator<T>
            {

                T[,] a;
                int ax, ay, ax2;
                
                public raz(Tab<T> a,int X,int Y)
                {
                    this.a = a.a;
                    ax = X;
                    ay = Y;
                    ax2 = (ax * 2);
                    licznikMax = ((ax * 2) * (ay * 2) - 1);
                }
                int licznik = -1, licznikMax;
                public T Current
                {
                    get { return a[licznik % ax2, licznik / ax2]; }
                }

                public void Dispose()
                {
                    licznik = licznikMax - 1;
                }

                object System.Collections.IEnumerator.Current
                {
                    get { return a[licznik % ax2, licznik / ax2]; }
                }

                public bool MoveNext()
                {
                    licznik++;
                    return licznik != licznikMax;
                }

                public void Reset()
                {
                    licznik = -1;
                }
            }


            public IEnumerator<T> GetEnumerator()
            {
                return new raz(this, ax, ay);
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return new raz(this, ax, ay);
            }
        }

        
    }
    public static class Roz
    {
        public static List<T> DodajNiePowtarzające<T>(this List<T> obiektWywołujący,params T[] lab)
        {
            return DodajNiePowtarzające(obiektWywołujący,lab);
        }
        public static List<T> DodajNiePowtarzające<T>(this List<T> obiektWywołujący, IEnumerable<T> doawanie)
        {
            foreach (T item2 in doawanie)
            {
                
                foreach (T item in obiektWywołujący)
                {

                    if (item.Equals(item2))
                    {
                        goto Koniec;
                    }
                }
                obiektWywołujący.Add(item2);
            Koniec:;
            }
            return obiektWywołujący;
        }
        public static List<T> WybierzWybraneElementy<T>(this T[,] t,IEnumerable<Point> ie)
        {
            List<T> ls = new List<T>();
            foreach (Point item in ie)
            {
                ls.Add(t[item.X, item.Y]);
            }
            return ls;
        }
        public static List<T> WybierzWybraneElementy<T>(this IList<T> t, IEnumerable<int> ie)
        {
            List<T> ls = new List<T>();
            foreach (int item in ie)
            {
                ls.Add(t[item]);
            }
            return ls;
        }
        public static IEnumerable<T> ZwracajPodwójnąListe<T>(this List<List<T>> wywołujący)
        {
            foreach (List<T> item in wywołujący)
            {
                foreach (T item2 in item)
                {
                    yield return item2;
                }
            }
        }

    }
    public struct Point
    {
        public int X, Y;
        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Point a, Point b)
        {
            return a.X != b.X && a.Y != b.Y;
        }
        public Point(int p1, int p2)
        {
            X = p1;
            Y = p2;
        }
    }
}
