using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Komputer.Xna.Menu;
namespace Komputer.Matematyczne.Silnik
{
    public class Silnik:XnaKontrolka, ICollection<ObiektFizyczny>
    {
        #region Lista
        List<ObiektFizyczny> ob = new List<ObiektFizyczny>();
        public void Add(ObiektFizyczny item)
        {
            ob.Add(item);
        }

        public void Clear()
        {
            ob.Clear();
        }

        public bool Contains(ObiektFizyczny item)
        {
            return ob.Contains(item);
        }

        public void CopyTo(ObiektFizyczny[] array, int arrayIndex)
        {
            ob.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return ob.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ObiektFizyczny item)
        {
            return ob.Remove(item);
        }

        public IEnumerator<ObiektFizyczny> GetEnumerator()
        {
            return ob.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ob.GetEnumerator();
        }
        #endregion
        List<ObiektFizyczny>[,] Pola;
        private Point WiekośćPola;
        public bool WystająceZaokroglij = false;
        public bool ObiektyWiekszeNiżPole;
        private Point IlośćPól;
        private Point IlośćPólNa2;
        public bool ZUjemnymiPozycjami = true;
        public Silnik(Point IlośćPól,Point WiekośćPola,int DomyślnaIlość)
        {
            Pola = new List<ObiektFizyczny>[IlośćPól.X, IlośćPól.Y];
            this.IlośćPól = IlośćPól;
            this.WiekośćPola = WiekośćPola;
            for (int i = 0; i < IlośćPól.X; i++)
            {
                for (int ii = 0; ii < IlośćPól.Y; ii++)
                {
                    Pola[i, ii] = new List<ObiektFizyczny>(DomyślnaIlość);
                }
            }
            IlośćPólNa2.X = IlośćPól.X / 2;
            IlośćPólNa2.Y = IlośćPól.Y / 2;
            CzyUżywaUpdate = true;
        }
        public override void UżycieUpdate(GameTime gt)
        {
            WyczyśćPola();
            PrzydzielDoPól();
            SprawdźKolozje();
            WywołajUpDate(gt);
            base.UżycieUpdate(gt);
        }

        private void WywołajUpDate(GameTime gt)
        {
            foreach (ObiektFizyczny item in ob)
            {
                item.UpDate(gt);
            }
        }

        private void SprawdźKolozje()
        {
            foreach (List<ObiektFizyczny> item in Pola)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    for (int ii = i+1; ii < item.Count; ii++)
                    {
                            Vector2 PunkStyku;
                            if (item[i].Kolizja(item[ii], out PunkStyku))
                            {
                                Kolzją(item[i], item[ii], PunkStyku);
                            }
                    }
                    
                }
            }
        }

        int iss = 0;
        private void Kolzją(ObiektFizyczny obiektFizyczny1, ObiektFizyczny obiektFizyczny2, Vector2 PunkStyku)
        {
            System.Diagnostics.Debug.WriteLine("Kolizja"+iss.ToString());
            Vector2 WzglednośćSierodków = obiektFizyczny2.Miejsce - obiektFizyczny1.Miejsce;
            Vector2 WzglednośćPredkości = obiektFizyczny2.KierunekWektor - obiektFizyczny1.KierunekWektor;
            float MMs = obiektFizyczny1.Masa + obiektFizyczny2.Masa;

            obiektFizyczny1.Przesóń(2 * obiektFizyczny1.Masa / MMs, obiektFizyczny2, PunkStyku, WzglednośćSierodków, WzglednośćPredkości);
            obiektFizyczny2.Przesóń(2 * obiektFizyczny2.Masa / MMs, obiektFizyczny1, PunkStyku - WzglednośćSierodków, -WzglednośćSierodków, -WzglednośćPredkości);

        }

        private void PrzydzielDoPól()
        {
            try
            {
                foreach (ObiektFizyczny item in ob)
                {
                    Vector2 v = item.Miejsce;
                    float f = item.Szkielet.MaksymalnyZasieng;
                    int MinX = Convert.ToInt32(v.X - f) / WiekośćPola.X, MaxX = Convert.ToInt32(v.X + f) / WiekośćPola.X, MinY = Convert.ToInt32(v.Y - f) / WiekośćPola.Y, MaxY = Convert.ToInt32(v.Y + f) / WiekośćPola.Y;
                    if (ZUjemnymiPozycjami)
                    {
                        MinX += IlośćPólNa2.X;
                        MinY += IlośćPólNa2.Y;
                    }
                    if (WystająceZaokroglij)
                    {
                        
                        MinX = (MinX < 0) ? 0 : MinX;
                        MinX = (MinX < IlośćPól.X) ? MinX : IlośćPól.X - 1;
                        MinY = (MinY < 0) ? 0 : MinY;
                        MinY = (MinY < IlośćPól.Y) ? MinY : IlośćPól.Y - 1;
                        MaxX = (MaxX < 0) ? 0 : MaxX;
                        MaxX = (MaxX < IlośćPól.X) ? MaxX : IlośćPól.X - 1;
                        MaxY = (MaxY < 0) ? 0 : MaxY;
                        MaxY = (MaxY < IlośćPól.Y) ? MaxY : IlośćPól.Y - 1;
                    }
                    for (int i = MinX; i <= MaxX; i++)
                    {
                        for (int ii = MinY; ii <= MaxY; ii++)
                        {
                            Pola[i, ii].Add(item);
                        }
                    }

                }
            }
            catch (IndexOutOfRangeException e)
            {

                throw new IndexOutOfRangeException("Mapa Jest zbyt mała by pomieścieć tyle Obiektów");
            }
        }

        private void WyczyśćPola()
        {
            foreach (List<ObiektFizyczny> item in Pola)
            {
                item.Clear();
            }
        }

        public override bool UpDate(EventArgs e)
        {

            return false;
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch pezel)
        {
            foreach (ObiektFizyczny item in ob)
            {
                item.Draw(pezel);
            }
            base.Draw(pezel);
        }
    }
}
