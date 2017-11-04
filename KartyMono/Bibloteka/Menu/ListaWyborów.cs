 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Komputer.Xna.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Komputer.Xna.Menu
{
    class ListaWyborów:PrzyciskZZaznaczeniem
    {
        Przycisk Wyświetlanie = new Przycisk(Vector2.Zero);
        MenuPodstawa MP;
        public SpriteFont Trzcionka;
        Vector2 StaraMiejsce = Vector2.Zero;
        public Vector2 WielkośćElementu;
        public void DodajText(string text, EventHandler eh)
        {
            PrzyciskText p = new PrzyciskText(WielkośćElementu, Trzcionka) { Text = text };
            p.Klikniecie += eh;
            p.Wielkość = WielkośćElementu;
            Add(p);
        }
        List<Przycisk> kolekcja = new List<Przycisk>();
        public ListaWyborów(Vector2 WielkośćElementu,MenuPodstawa menu)
            : base(WielkośćElementu)
        {
            BezWielkości = true;
            MP = menu;
            this.WielkośćElementu = WielkośćElementu;
            PobieranieNomalnegoObrazu();
            ObrazZaznaczony = ObrazNieZaznaczony;
            ZmianaWielkości += ListaWyborów_ZmianaWielkości;
            ZmianaZazanczenia += Zmiana;
        }

        void ListaWyborów_ZmianaWielkości(object sender, EventArgs e)
        {
            Forechan((XnaKontrolka x) => { x.Miejsce = x.Miejsce - StaraMiejsce + Miejsce; });
            StaraMiejsce = Miejsce;
        }
        void PobieranieNomalnegoObrazu()
        {
            Texture2D t = new Texture2D(MenuPodstawa.GraphicDeviceDP,Convert.ToInt32( WielkośćElementu.X),Convert.ToInt32(  WielkośćElementu.Y));
            Color[] ck = new Color[t.Width * t.Height];
            for (int i = 0; i < ck.Length; i++)
            {
                ck[i] = Color.White;
            }
            t.SetData(ck);
            ObrazNieZaznaczony = t;
        }

        void PobieranieRozwinietegoObrazu()
        {

            Texture2D t = new Texture2D(MenuPodstawa.GraphicDeviceDP, Convert.ToInt32(WielkośćElementu.X), Convert.ToInt32(WielkośćElementu.Y)*kolekcja.Count);
            Color[] ck = new Color[t.Width * t.Height];
            for (int i = 0; i < ck.Length; i++)
            {
                ck[i] = Color.White;
            }
            t.SetData(ck);
            ObrazZaznaczony = t;
        }
        public void Add(Przycisk p)
        {
            MP.Remove(this);
            p.PrzynależnośćDoInejFigury = true;
            MP.Add(p);
            if (kolekcja.Count == 0)
                p.Miejsce =Miejsce;
            else
            {
                p.Miejsce = kolekcja[kolekcja.Count - 1].Miejsce + new Vector2(0, WielkośćElementu.Y);
            }
            p.Niewyświtlanie = true;
            kolekcja.Add(p);
            p.Klikniecie += p_Klikniecie;
            PobieranieRozwinietegoObrazu();
            MP.Add(this);

        }

        void p_Klikniecie(object sender, EventArgs e)
        {
           ZamieńZPierwszym((Przycisk)sender);
        }
        public void Forechan(PrzekażKontrolki pk)
        {
            foreach (XnaKontrolka item in kolekcja)
            {
                pk(item);
            }
        }
        public void Remove(Przycisk p)
        {
            bool Znaleziony = false;
            for (int i = 0; i < kolekcja.Count; i++)
            {
                if (Znaleziony)
                {
                    kolekcja[i].Miejsce -= new Vector2(0, p.Wielkość.Y);
                    foreach (XnaKontrolka item in Przesówane)
                    {
                        item.Miejsce += PrzesónieciaGdyOtwierasz;
                    }
                }
                else
                {
                    if (p == kolekcja[i])
                    {
                        Znaleziony = true;
                    }
                    foreach (XnaKontrolka item in Przesówane)
                    {
                        item.Miejsce -= PrzesónieciaGdyOtwierasz;
                    }
                }
            }
            kolekcja.Remove(p);
            MP.Remove(p);
            PobieranieRozwinietegoObrazu();
        }
        public override bool UpDate(EventArgs e)
        {
            EventKlikniety ek=(EventKlikniety)e;
            if (KolizjaZawsze(ek.Miejsce))
            {
             base.UpDate(e);
             return true;
            }
            Zaznaczenie = false;
            return false;
        }
        public override void Draw(SpriteBatch pezel)
        {
            base.Draw(pezel);
            foreach (XnaKontrolka item in kolekcja)
            {
                if (!item.Ukryty)
                {
                item.Draw(pezel);
                }
            }
        }
        public void ZamieńZPierwszym(Przycisk par)
        {
            int i = 0;
            for (; i < kolekcja.Count; i++)
            {
                if (kolekcja[i] == par)
                {
                    break;
                }
            }
            ZamieńZPierwszym(i);
        }
        public void ZamieńZPierwszym(int i)
        {
            if (i==0)
            {
                return;
            }
            Vector2 Mj = kolekcja[i].Miejsce;
            kolekcja[i].Miejsce = kolekcja[0].Miejsce;
            kolekcja[0].Miejsce = Mj;
            Przycisk p = kolekcja[i];
            kolekcja[i] = kolekcja[0];
            kolekcja[0] = p;
            kolekcja[i].Zablokowany = true;
        }
        public void PrzesóńDalej(MenuPodstawa mp)
        {
            bool CzyKontynułuj = true;
            Zaznaczenie=true;
            PrzesónieciaGdyOtwierasz=new Vector2 (0, WielkośćElementu.Y) *( kolekcja.Count-1);
            XnaKontrolka OstatniPrzesówany = this, Nadpsiany=null;
            while (CzyKontynułuj)
            {
                CzyKontynułuj = false;
                foreach (XnaKontrolka item in mp.Lista)
                {
                    if (item.PrzynależnośćDoInejFigury)
                    {
                        continue;
                    }
                    if (item!=OstatniPrzesówany &&item.Krawedzie!=null&&OstatniPrzesówany.Krawedzie!=null&&OstatniPrzesówany.Krawedzie.Kolizja(item.Krawedzie))
                    {
                        CzyKontynułuj = true;
                        item.Miejsce += PrzesónieciaGdyOtwierasz;
                        if (!Przesówane.Exists((XnaKontrolka x) => { return x==item; }))
                            Przesówane.Add(item);
                        Nadpsiany = item;
                    }
                }
                OstatniPrzesówany = Nadpsiany;
            }
            ZmianaZazanczenia += ObsógaPRzesóniećInychMenu;
            Zaznaczenie = false;
        }

        private void ObsógaPRzesóniećInychMenu(object sender, EventArgs e)
        {
            if (Zaznaczenie)
            {
                foreach (XnaKontrolka item in Przesówane)
                {
                    item.Miejsce += PrzesónieciaGdyOtwierasz;
                }
                
            }
            else
            {
                foreach (XnaKontrolka item in Przesówane)
                {
                    item.Miejsce -= PrzesónieciaGdyOtwierasz;
                }
            }
        }
        Vector2 PrzesónieciaGdyOtwierasz;
        List<XnaKontrolka> Przesówane = new List<XnaKontrolka>();
        private void Zmiana(object sender, EventArgs e)
        {
            if (Zaznaczenie)
            {
                for (int i = 1; i < kolekcja.Count; i++)
                {
                    kolekcja[i].Ukryty = false;
                    kolekcja[i].Zablokowany = false;
                }
            }
            else
            {
                for (int i = 1; i < kolekcja.Count; i++)
                {
                    kolekcja[i].Ukryty = true;
                }

            }
        }
    }

}