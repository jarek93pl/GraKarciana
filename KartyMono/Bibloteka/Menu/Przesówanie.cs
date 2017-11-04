using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Komputer.Xna.Menu
{
    public class Sówak:XnaKontrolka
    {
        const int DługoścWyświetlaniaWskaźnika = 60;
        float  SzybkośćPrzesówania =0;
        public Vector2 Przerwa = new Vector2(10, 10), WielkoścPrzycisku, Miejsce_Przycisku, MiejscePrzesunieca = Vector2.Zero, MiejsceWyświetlania = Vector2.Zero;

        List<Przycisk> ListaPrzycisk = new List<Przycisk>();
        bool PiewszyPrzycisk = true;
        float MaksymalnaDługość = 0;
        Texture2D Wskaźnik = null;
        int WyświtlanieWskaźnika = -1;
        bool DziałainieWskaźnika = false;
        public Sówak()
        {
        }
        public void TwórzWskaźnik(GraphicsDevice GD, int Szerokość, Color KolorWskaźnika)
        {
            if (Szerokość == 0)
            {
                Wskaźnik = null;
                return;
            }
            if (MaksymalnaDługość<0)
            {
                return;
            }
            Wskaźnik = new Texture2D(GD,(int)(Wielkość.X/ (MaksymalnaDługość+Wielkość.X)*Wielkość.X), Szerokość);
            Color[] cro=new Color[(int)(Wskaźnik.Width * Wskaźnik.Height)];
            for (int i = 0; i < cro.Length; i++)
            {
                cro[i] = KolorWskaźnika;
            }
            Wskaźnik.SetData(cro);
            DziałainieWskaźnika = true;
        }
        public void Dodaj(Przycisk p)
        {
            if (PiewszyPrzycisk)
            {
                WielkoścPrzycisku = p.Wielkość;
                if (p.Wielkość.X + Przerwa.X > Wielkość.X || p.Wielkość.Y + Przerwa.Y > Wielkość.Y)
                    throw new Exception("przycisk jest wiekszy od okna");
                Miejsce_Przycisku = Przerwa;
                MaksymalnaDługość =-Wielkość.X+2*(WielkoścPrzycisku.X+Przerwa.X);
            }
            else
            {
                if (WielkoścPrzycisku != p.Wielkość)
                    throw new Exception("Przycisk o złym rozmiaże");
            }
            
            p.Miejsce = Miejsce_Przycisku;
            ListaPrzycisk.Add(p);

            Miejsce_Przycisku.Y += Przerwa.Y + WielkoścPrzycisku.Y;
            if (Miejsce_Przycisku.Y + WielkoścPrzycisku.Y > Wielkość.Y)
            {
                MiejsceWyświetlania = new Vector2(0, Miejsce_Przycisku.Y);
                Miejsce_Przycisku = new Vector2(WielkoścPrzycisku.X + Miejsce_Przycisku.X + Przerwa.X, Przerwa.Y);
                MaksymalnaDługość += WielkoścPrzycisku.X +Przerwa.X;
            }
            PiewszyPrzycisk = false;
            DziałainieWskaźnika = false;
            WyświtlanieWskaźnika = -1;
        }
        public override bool UpDate(EventArgs e)
        {
            EventKlikniety kl = e as EventKlikniety;
            if (kl != null)
            {
                foreach (Przycisk item in ListaPrzycisk)
                {
                    if (!item.Ukryty && item.Kolizja(new Vector2(kl.Miejsce.X - Miejsce.X + MiejscePrzesunieca.X, kl.Miejsce.Y)))
                    {
                        if(item.UpDate(e))
                        break;
                    }
                }
            }
            EventPrzesuniecie d = e as EventPrzesuniecie;
            if (d==null)
            {
                return false;
            }
            SzybkośćPrzesówania = d.Miejsce.X - d.Miejsce1.X;
            if (DziałainieWskaźnika)
            {
                WyświtlanieWskaźnika = DługoścWyświetlaniaWskaźnika;
                CzyUżywaUpdate = true;
            }
            MiejscePrzesunieca.X += SzybkośćPrzesówania;
            MiejsceWyświetlania.X = (MiejscePrzesunieca.X / (Wielkość.X + MaksymalnaDługość)) * Wielkość.X;
            if (MiejscePrzesunieca.X < 0)
            {
                MiejscePrzesunieca.X = 0;
                SzybkośćPrzesówania = 0;
                MiejsceWyświetlania.X = (MiejscePrzesunieca.X / (Wielkość.X + MaksymalnaDługość)) * Wielkość.X;
            }
            if (MiejscePrzesunieca.X - Wielkość.X > MaksymalnaDługość)
            {
                MiejscePrzesunieca.X = MiejscePrzesunieca.X - Wielkość.X;
                SzybkośćPrzesówania = 0;
                MiejsceWyświetlania.X = (MiejscePrzesunieca.X / (Wielkość.X + MaksymalnaDługość)) * Wielkość.X;
            }
            if (MiejscePrzesunieca.X > MaksymalnaDługość)
            {
                if (MaksymalnaDługość < 0)
                {
                    MiejscePrzesunieca.X = 0;
                    WyłączWskaźnik();
                    MiejsceWyświetlania.X = (MiejscePrzesunieca.X / (Wielkość.X + MaksymalnaDługość)) * Wielkość.X;
                }
                else
                {
                    MiejscePrzesunieca.X = MaksymalnaDługość;
                    MiejsceWyświetlania.X = (MiejscePrzesunieca.X / (Wielkość.X + MaksymalnaDługość)) * Wielkość.X;
                }
            }
            return false;
        }
        void WyłączWskaźnik()
        {
            WyświtlanieWskaźnika = -1;
            CzyUżywaUpdate = false;
        }

        public void PokażWskaźnik()
        {
            WyświtlanieWskaźnika = 150;
            CzyUżywaUpdate = true;
        }
        public override void UżycieUpdate(GameTime gt)
        {
            WyświtlanieWskaźnika--;
            if (WyświtlanieWskaźnika < 0)
                WyłączWskaźnik();
        }
        public override void Draw(SpriteBatch pezel)
        {
            Vector2 v = new Vector2(MiejscePrzesunieca.X, 0);
            if (WyświtlanieWskaźnika>0&&DziałainieWskaźnika)
            {
                pezel.Draw(Wskaźnik, MiejsceWyświetlania, Kolor);
            }
            foreach (var item in ListaPrzycisk)
            {
                if (item.Miejsce.X - MiejscePrzesunieca.X + WielkoścPrzycisku.X < 0 || -MiejscePrzesunieca.X + item.Miejsce.X > Wielkość.X)
                    continue;
                item.Draw(pezel, Miejsce - v);
            }
        }
    }
}
