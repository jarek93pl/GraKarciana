using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Komputer.Matematyczne.Figury;
namespace Komputer.Xna.Menu
{
    public abstract class XnaKontrolka
    {

        public bool PrzynależnośćDoInejFigury = false;
        public string Nazwa = "";
        float scal = 1;
        public bool CzyUżywaUpdate = false;
        Vector2 Max;
        Vector2 miejsce, wielkość;
        Texture2D obraz;
        Color kolor = Color.White;
        public event PrzekażMiejsca Zaznaczony;
        public event PrzekażMiejsca Puszczony;
        public event EventHandler ZmianaKoloru;
        public event EventHandler ZmianaWielkości;
        public Color Kolor
        {
            get { return kolor; }
            set
            {
                kolor = value;
                if (ZmianaKoloru != null)
                    ZmianaKoloru(this, EventArgs.Empty);
            }
        }
        FiguraZOdcinków krawedzie;

        public FiguraZOdcinków Krawedzie
        {
            get { return krawedzie; }
        }
        public SpriteEffects RodzajWyświetlania = SpriteEffects.None;
        public bool BezWielkości;
        public Vector2 Miejsce
        {
            get
            {
                return miejsce;
            }
            set
            {
                miejsce = value;
                Wielkość = Wielkość / scal;
            }
        }

        public Texture2D Zdjecie
        {
            get { return this.obraz; }
            set
            {
                this.obraz = value;
                Wielkość = new Vector2(obraz.Width, obraz.Height);
            }
        }
        public Vector2 Wielkość
        {
            get
            {
                return wielkość;
            }
            set
            {
                Max = value * scal + miejsce;
                wielkość = value * scal;
                krawedzie = new FiguraZOdcinków();
                krawedzie.Add(new Odcinek(miejsce.X, Max.X, miejsce.Y, Miejsce.Y));
                krawedzie.Add(new Odcinek(miejsce.X, Max.X, Max.Y, Max.Y));
                krawedzie.Add(new Odcinek(miejsce.X, miejsce.X, miejsce.Y, Max.Y));
                krawedzie.Add(new Odcinek(Max.X, Max.X, miejsce.Y, Max.Y));
                if (ZmianaWielkości != null)
                {
                    ZmianaWielkości(this, EventArgs.Empty);
                }
            }
        }
        public float Scala
        {
            get { return scal; }
            set
            {
                Wielkość = Wielkość / scal * value;
                scal = value;
            }
        }
        /// <summary>
        /// Przycisk nie wrażliwy na klikniecia
        /// </summary>
        public bool Zablokowany = false;
        public bool Kolizja(Vector2 v)
        {
            return (!Zablokowany && (v.X < Max.X && v.Y < Max.Y) && (v.X > miejsce.X && v.Y > miejsce.Y)) || BezWielkości;
        }
        public bool KolizjaZawsze(Vector2 v)
        {
            return ((v.X < Max.X && v.Y < Max.Y) && (v.X > miejsce.X && v.Y > miejsce.Y));
        }
        public bool KolizjaKwadratu(Odcinek o)
        {
            if (BezWielkości || Kolizja(o.Poczotek) || Kolizja(o.Koniec))
                return true;

            return Krawedzie.Kolizja(o);


        }
        /// <summary>
        /// Metoda sprawdza obliczenia w kotrolnec
        /// </summary>
        /// <param name="Pozycja">Pozycja Myszki</param>
        /// <returns>Zwraca true gdy kotrolka chce zablokować działanie innych</returns>
        public abstract bool UpDate(EventArgs e);
        public virtual void Draw(SpriteBatch pezel)
        {
            if (obraz != null)
                pezel.Draw(obraz, miejsce, null, Kolor, 0, Vector2.Zero, scal, RodzajWyświetlania, 0);
        }

        public virtual void Draw(SpriteBatch pezel, Vector2 Wzgledne)
        {
            if (obraz != null)
                pezel.Draw(obraz, miejsce + Wzgledne, null, Kolor, 0, Vector2.Zero, scal, RodzajWyświetlania, 0);
        }
        public virtual void UżycieUpdate(GameTime gt)
        {
        }
        public bool Ukryty { get; set; }
        public bool Niewyświtlanie = false;
        internal void Puść(Vector2 md)
        {

            Puszczony?.Invoke(this, new EventKlikniety(md.X, miejsce.Y));
        }
        internal void PoczotekTrzymanaMyszką(Vector2 md)
        {
            Zaznaczony?.Invoke(this, new EventKlikniety(md.X, miejsce.Y));
        }
        public virtual void Dodawanie() { }

        public virtual void Usówanie()
        {
        }

        public float LenghtToObject(XnaKontrolka X)
        {
            return (X.Miejsce - Miejsce).Length();
        }
    }
}