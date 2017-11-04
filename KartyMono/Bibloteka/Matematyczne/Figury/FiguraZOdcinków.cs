using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace Komputer.Matematyczne.Figury
{

    public class FiguraZOdcinków : IList<Odcinek>
    {
        List<Odcinek> Od = new List<Odcinek>();
        public bool Kolizja(FiguraZOdcinków b)
        {
            Vector2 v;
            for (int i = 0; i < Count; i++)
            {
                for (int ii = 0; ii < b.Count; ii++)
                {
                    if (this[i].Kolizja(b[ii], out v))
                        return true;
                }
            }
            return false;
        }
        public void Przesóń(Vector2 v)
        {
            foreach (var item in this)
            {
                item.Koniec += v;
                item.Poczotek += v;
            }
        }
        public static FiguraZOdcinków operator *(FiguraZOdcinków a, Matrix x)
        {
            foreach (var item in a)
            {
                item.Poczotek = Vector2.Transform(item.Poczotek, x);
                item.Koniec = Vector2.Transform(item.Koniec, x);
            }
            a.WyznaczOdległość();
            return a;
        }

        public static FiguraZOdcinków operator +(FiguraZOdcinków a, Vector2 b)
        {
            foreach (var item in a)
            {
                item.Poczotek += b;
                item.Koniec += b;
            }
            a.WyznaczOdległość();
            return a;
        }
        float maksymalnyZasieng;

        public float MaksymalnyZasieng
        {
            get { return maksymalnyZasieng; }
        }
        /// <summary>
        /// Metoda działa dla obiektów mniejscych niż 32000
        /// </summary>
        private void WyznaczOdległość()
        {
            float maksymalnyZasiengDoK2 = 0;
            foreach (Odcinek item in Od)
            {
                float a = item.PoczotekX * item.PoczotekX + item.PoczotekY * item.PoczotekY;
                maksymalnyZasiengDoK2 = maksymalnyZasiengDoK2 < a ? a : maksymalnyZasiengDoK2;
                a = item.KoniecX * item.KoniecX + item.KoniecY * item.KoniecY;
                maksymalnyZasiengDoK2 = maksymalnyZasiengDoK2 < a ? a : maksymalnyZasiengDoK2;
            }
            maksymalnyZasieng = Convert.ToSingle(Math.Sqrt(maksymalnyZasiengDoK2));
        }
        public static FiguraZOdcinków operator -(FiguraZOdcinków a, Vector2 b)
        {
            foreach (var item in a)
            {
                item.Poczotek -= b;
                item.Koniec -= b;
            }
            a.WyznaczOdległość();
            return a;
        }
        public bool Kolizja(Odcinek b)
        {
            Vector2 c;
            foreach (var item in this)
            {
                if (item.Kolizja(b, out c))
                {

                    return true;
                }
            }

            return false;
        }


        public bool Kolizja(FiguraZOdcinków b, Vector2 Przesuniecie, out Vector2 PunktStyku)
        {
            b -= Przesuniecie;
            foreach (Odcinek item2 in b)
            {
                foreach (var item in this)
                {
                    if (item.Kolizja(item2, out PunktStyku))
                    {

                        b += Przesuniecie;
                        return true;
                    }
                }
            }
            b += Przesuniecie;
            PunktStyku = Vector2.Zero; ;
            return false;
        }
        #region DoListy
        public int IndexOf(Odcinek item)
        {
            return Od.IndexOf(item);
        }

        public void Insert(int index, Odcinek item)
        {
            Od.Insert(index, item);
            WyznaczOdległość();
        }

        public void RemoveAt(int index)
        {
            Od.RemoveAt(index);
            WyznaczOdległość();
        }

        public Odcinek this[int index]
        {
            get
            {
                return Od[index];
            }
            set
            {
                Od[index] = value;
                WyznaczOdległość();
            }
        }


        public void Add(Odcinek item)
        {
            Od.Add(item);
            WyznaczOdległość();
        }

        public void Clear()
        {
            Od.Clear();
            WyznaczOdległość();
        }

        public bool Contains(Odcinek item)
        {
            return Od.Contains(item);

        }

        public void CopyTo(Odcinek[] array, int arrayIndex)
        {
            Od.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Od.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Odcinek item)
        {
            bool b = Od.Remove(item);
            WyznaczOdległość();
            return b;
        }

        public IEnumerator<Odcinek> GetEnumerator()
        {
            return Od.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Od.GetEnumerator();
        }
        #endregion

    }

    public class Odcinek
    {
        public Odcinek()
        {
        }
        public Vector2 Skrucony()
        {
            Vector2 v = Koniec - Poczotek;
            v.Normalize();
            return v;
        }
        public Odcinek(float PX, float KX, float PY, float KY)
        {
            this.KoniecX = KX;
            this.KoniecY = KY;
            this.PoczotekX = PX;
            this.PoczotekY = PY;
        }
        public Odcinek(Vector2 Poczotek, Vector2 Koniec)
        {
            this.Poczotek = Poczotek;
            this.Koniec = Koniec;
        }
        float pX, pY, kX, kY, Pa, Pb;

        public float KoniecY
        {
            get { return kY; }
            set
            {
                kY = value;
                wyznaczoneAB = false;
            }
        }

        public float KoniecX
        {
            get { return kX; }
            set
            {
                kX = value;
                wyznaczoneAB = false;
            }
        }
        public Vector2 OdPoczotku(float f)
        {
            return Poczotek + Skrucony() * f;
        }
        public float Odległość(Vector2 V)
        {

            Vector2 v1 = Koniec - Poczotek;
            Vector2 v2 = Koniec - V;
            Vector2 v3 = Poczotek - V;
            float a = v1.Length(), b = v2.Length(), c = v3.Length();
            if (!((a * a) + (c * c) < (b * b) || (a * a) + (b * b) < (c * c)) && !(a < 0.1f && a > -0.1f))
            {
                float x = ((a * a) - (b * b) + (c * c)) / (2 * a);

                if (b > a)
                {
                    if (x < a - x)
                        x = a - x;

                }
                else
                {
                    if (x > a - x)
                        x = a - x;
                }
                return Convert.ToSingle(Math.Sqrt(Math.Abs((b * b) - (x * x))));
            }
            float xb = (b < c) ? b : c;
            return xb;
        }
        public float Odległość(Vector2 V, out float Końca)
        {

            Vector2 v1 = Koniec - Poczotek;
            Vector2 v2 = Koniec - V;
            Vector2 v3 = Poczotek - V;
            float a = v1.Length(), b = v2.Length(), c = v3.Length();
            Końca = b;
            if (!((a * a) + (c * c) < (b * b) || (a * a) + (b * b) < (c * c)) && !(a < 0.1f && a > -0.1f))
            {
                float x = ((a * a) - (b * b) + (c * c)) / (2 * a);

                if (b > a)
                {
                    if (x < a - x)
                        x = a - x;

                }
                else
                {
                    if (x > a - x)
                        x = a - x;
                }
                return Convert.ToSingle(Math.Sqrt(Math.Abs((b * b) - (x * x))));
            }
            float xb = (b < c) ? b : c;
            return xb;
        }
        public Vector2 OdPoczotkuProcent(float f)
        {

            Vector2 v = Koniec - Poczotek;
            return Poczotek + v * f;
        }

        public Vector2 Poczotek
        {
            get
            {
                return new Vector2(pX, pY);
            }
            set
            {
                PoczotekX = value.X;
                PoczotekY = value.Y;
                wyznaczoneAB = false;
            }
        }

        public Vector2 Koniec
        {
            get
            {
                return new Vector2(kX, kY);
            }
            set
            {
                KoniecX = value.X;
                wyznaczoneAB = false;
                KoniecY = value.Y;
            }
        }
        public float PoczotekY
        {
            get { return pY; }
            set
            {
                pY = value;
                wyznaczoneAB = false;
            }
        }

        public float PoczotekX
        {
            get { return pX; }
            set
            {
                pX = value;
                wyznaczoneAB = false;
            }
        }

        bool wyznaczoneAB = false;

        public bool WyznaczoneAB
        {
            get { return wyznaczoneAB; }
        }

        public bool Kolizja(Odcinek O, out Vector2 PunktStyku)
        {
            WyZnaczAB();
            O.WyZnaczAB();
            PunktStyku = Vector2.Zero;
            if (kX < O.pX || O.kX < pX)
                return false;
            float PDX = (PoczotekX > O.PoczotekX) ? PoczotekX : O.PoczotekX;
            float KDX = (KoniecX > O.KoniecX) ? O.KoniecX : KoniecX;
            float PDY1, KDY1;
            float PDY2, KDY2;
            if (wyznaczoneAB)
            {
                PDY1 = WartośćY(PDX);
                KDY1 = WartośćY(KDX);
            }
            else
            {
                PDY1 = pY;
                KDY1 = kY;
            }

            if (O.wyznaczoneAB)
            {
                KDY2 = O.WartośćY(KDX);
                PDY2 = O.WartośćY(PDX);
            }
            else
            {
                PDY2 = O.pY;
                KDY2 = O.kY;
            }

            if (KDY1 < KDY2)
            {
                if (PDY1 < PDY2)
                    return false;
                else
                {
                    WyznaczPunktStyku(ref O, out PunktStyku);
                    return true;
                }
            }
            else
            {
                if (PDY1 < PDY2)
                {
                    WyznaczPunktStyku(ref O, out PunktStyku);
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        public void UstawPoczotek()
        {
            if (pX > kX)
            {
                float zamiania = pX;
                pX = kX;
                kX = zamiania;
                zamiania = kY;
                kY = pY;
                pY = zamiania;
                wyznaczoneAB = false;
            }
        }

        public void WyznaczPunktStyku(ref Odcinek O, out Vector2 PunktStyku)
        {
            if (!wyznaczoneAB && !O.wyznaczoneAB)
            {
                PunktStyku = O.OdPoczotkuProcent(0.5f);
                return;
            }
            if (!this.wyznaczoneAB)
            {
                PunktStyku = new Vector2(KoniecX, O.WartośćY(KoniecX));
                return;
            }
            if (!O.wyznaczoneAB)
            {
                PunktStyku = new Vector2(O.KoniecX, WartośćY(O.KoniecX));
                return;
            }
            float A = a - O.a;
            float B = -(b - O.b);
            float x = B / a;
            PunktStyku = new Vector2(x, WartośćY(x));


        }
        public float a
        {
            get
            {
                if (!wyznaczoneAB)
                {
                    WyZnaczAB();
                    if (!wyznaczoneAB)
                    {
                        throw new Exception("nie da się wyznaczyć A i B");
                    }
                }

                return Pa;
            }
        }
        public float b
        {
            get
            {
                if (!wyznaczoneAB)
                {
                    WyZnaczAB();
                    if (!wyznaczoneAB)
                    {
                        throw new Exception("nie da się wyznaczyć A i B");
                    }
                }
                return Pb;
            }
        }
        public float WartośćY(float MiejsceX)
        {
            try
            {
                return a * MiejsceX + b;
            }
            catch
            {
                throw new Exception("wartość niewyznaczalna");
            }

        }

        public void WyZnaczAB()
        {
            UstawPoczotek();
            float A = KoniecX - PoczotekX;
            if (A < 0.0001f && A > -0.0001f)
            {
                wyznaczoneAB = false;
                return;
            }
            Pa = (KoniecY - PoczotekY) / A;
            Pb = PoczotekY - (PoczotekX * Pa);
            wyznaczoneAB = true;
        }
        private void WyZnaczABTr()
        {

            float A = KoniecX - PoczotekX;
            if (A == 0)
                throw new Exception("Fynkic nie da się zapiać za pomocą wzoró f(x)=ax+b");
            Pa = (KoniecY - PoczotekY) / A;
            Pb = PoczotekY - (PoczotekX * Pa);
        }
        public float Dłougość { get { return (Poczotek - Koniec).Length(); } }

        public float Kąt
        {
            get
            {
                double a = -PoczotekX + KoniecX, b = -PoczotekY + KoniecY;

                return Convert.ToSingle(Math.Atan2(b, a));
            }

        }


    }

}
