using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Komputer.Xna.Menu
{
    class PrzyciskZZaznaczeniem:Przycisk
    {
        
        private bool zaznaczenie = false;
        public event EventHandler ZmianaZazanczenia;
        public Color kolorZaznaczony = Color.White, kolorNieZaznaczony = Color.White;
        public float ScalaZaznaczona = 1, ScalaNieZaznaczona = 1;
        public bool Zaznaczenie
        {
            get { return zaznaczenie; }
            set
            {
                if(zaznaczenie != value)
                {
                    zaznaczenie = value;
                    if (ZmianaZazanczenia != null)
                    {
                        ZmianaZazanczenia(this, EventArgs.Empty);
                    }
                    if (zaznaczenie)
                    {
                        Zdjecie = ObrazZaznaczony;
                        Scala = ScalaZaznaczona;
                        Kolor = kolorZaznaczony;
                    }
                    else
                    {
                        Zdjecie = ObrazNieZaznaczony;
                        Scala = ScalaNieZaznaczona;
                        Kolor = kolorNieZaznaczony;
                    }
                }
            }
        }
        public Texture2D obrazNieZaznaczony, obrazZaznaczony;

        public Texture2D ObrazZaznaczony
        {
            get { return obrazZaznaczony; }
            set { obrazZaznaczony = value;

            if (Zdjecie == null && zaznaczenie)
                Zdjecie = value;
            }
        }

        public Texture2D ObrazNieZaznaczony
        {
            get { return obrazNieZaznaczony; }
            set {
                if (Zdjecie == null && !zaznaczenie)
                    Zdjecie = value;
                obrazNieZaznaczony = value; }
        }
        public PrzyciskZZaznaczeniem(Texture2D ZaznaczonyObraz, Texture2D NieZaznzaczonyObraz):base(NieZaznzaczonyObraz)
        {
            obrazNieZaznaczony=NieZaznzaczonyObraz;
            obrazZaznaczony=ZaznaczonyObraz;
            Klikniecie += new EventHandler((object o, EventArgs e) => { Zaznaczenie = !Zaznaczenie; });
            ZmianaKoloru += PrzyciskZZaznaczeniem_ZmianaKoloru;
        }

        void PrzyciskZZaznaczeniem_ZmianaKoloru(object sender, EventArgs e)
        {
            kolorNieZaznaczony = Kolor;
            kolorZaznaczony = Kolor;
        }
        public PrzyciskZZaznaczeniem(Vector2 Wielkosc):base(Wielkosc)
        {
            Klikniecie += new EventHandler((object o, EventArgs e) => { Zaznaczenie = !Zaznaczenie; });
            ZmianaKoloru += PrzyciskZZaznaczeniem_ZmianaKoloru;
        }
    }
}
