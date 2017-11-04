using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Komputer.Matematyczne.Figury;

namespace Komputer.Xna.Menu
{
    /// <summary>
    /// Klasa Powina być właczana ~ 20/sek
    /// </summary>
    public  class MenuPodstawa :IXnaPrzedmiot
    {
        public event EventHandler KoniecZlecenia;
        public Vector2 MiejsceDodanejPrzycisku;
        public Vector2 RużnicaDoPrzyciski;
        Texture2D Mysz;
        public static Vector2[] PozycjaMyszki;
        public List<XnaKontrolka> Lista = new List<XnaKontrolka>();
        List<IComponet> ListaKomponetów = new List<IComponet>();
        List<ZleceniaMenu> zlecenia;
        public int TabInedx = 0;
        Texture2D[] ListaObrazów;
        bool CzySąObrazyWTle = false;
        int NumerObrazuWTle = 0,CzasTrwaniaObrazu=0,IDoObrazówWtle = 0;
        public int CzasWMenu = 0;
        int NumerAktualnieRelizowanegoZlecenia =-1;
        bool  OdczytywanieNagrań = false;
        public bool BlokadaOdczytu = false;
        bool CzyByłDodanySczytywaczZdażeń = false;
        public void ZlećPolecenia(List<ZleceniaMenu> ZleceniaDlaMenu)
        {
            int ip = 0;
            CzasWMenu = 0;
            zlecenia = ZleceniaDlaMenu;
            NumerAktualnieRelizowanegoZlecenia = -1;
            zlecenia.Add(new ZleceniaMenu(1));
            foreach (var item in ZleceniaDlaMenu)
            {
                item.Poczotek = ip;
                ip += item.Długość;
            };
            if (!CzyByłDodanySczytywaczZdażeń)
            {
                CzyByłDodanySczytywaczZdażeń = true;
                Add(new WywołajZdażenia());

            }
            OdczytywanieNagrań = true;
        }
        class WywołajZdażenia:XnaKontrolka
        {
            public WywołajZdażenia()
            {
                BezWielkości = true;
            }
            public override bool UpDate(EventArgs e)
            {
                ZlecenieZdażenia zd = e as ZlecenieZdażenia;
                if (zd!=null&&zd.EH!=null)
                {
                    zd.EH(this, EventArgs.Empty);
                }
                return false;
            }
        }
        public void DodajPrzycisk(Przycisk ix)
        {
            ix.Miejsce = MiejsceDodanejPrzycisku;
            MiejsceDodanejPrzycisku += RużnicaDoPrzyciski;
            Add(ix);
        }
        public List<TworzenieRenderTarget> ListPobrańRTarget = new List<TworzenieRenderTarget>();
        public static GraphicsDevice GraphicDeviceDP;
        public TouchCollection MiejscaDotchnieć;
        public Color TłoGdyPoberaszZRenderTarget = Color.CornflowerBlue;
        void PobraniaRenderTarget(SpriteBatch sp)
        {
            sp.End();
            while(ListPobrańRTarget.Count!=0)
            {
                GraphicDeviceDP.SetRenderTarget(ListPobrańRTarget[0].Obraz);
                GraphicDeviceDP.Clear(ListPobrańRTarget[0].Kolor);
                sp.Begin();
                ListPobrańRTarget[0].Pobieracz(sp);
                sp.End();
                ListPobrańRTarget.RemoveAt(0);
            }

            GraphicDeviceDP.SetRenderTarget(null);
            GraphicDeviceDP.Clear(TłoGdyPoberaszZRenderTarget);
            sp.Begin();
        }
        public virtual void Draw(SpriteBatch sp)
        {
            if (ListPobrańRTarget.Count != 0)
            {
                PobraniaRenderTarget(sp);
            }
                Wyświetl(sp);
        }

        private void Wyświetl(SpriteBatch sp)
        {
            if (CzySąObrazyWTle)
                sp.Draw(ListaObrazów[NumerObrazuWTle], Vector2.Zero, Color.White);
            foreach (IComponet item in ListaKomponetów)
            {
                item.Draw(sp);
            }
            for (int i = 0; i < Lista.Count; i++)
            {
                if (!Lista[i].Ukryty&&!Lista[i].Niewyświtlanie)
                    Lista[i].Draw(sp);
            }
            if (MyszkaAktywna && WyświetlanieMyszki)
                sp.Draw(Mysz, MiejsceMyszki, Color.White);
            if (OdczytywanieNagrań && NumerAktualnieRelizowanegoZlecenia != -1 && zlecenia[NumerAktualnieRelizowanegoZlecenia].ListaWyświetleń != null)
            {
                foreach (var item in zlecenia[NumerAktualnieRelizowanegoZlecenia].ListaWyświetleń)
                {
                    sp.Draw(item.Obraz, item.Pozycja, Color.White);
                }
            }
        }
        bool MyszkaAktywna = false;
        static bool WyświetlanieMyszki=false;
        List<EventHandler> ZdażeniaDoNatepnegoUpdate = new List<EventHandler>();
        public void DodajZdażenieDoNastepnejUpdate(EventHandler e)
        {
            ZdażeniaDoNatepnegoUpdate.Add(e);
        }
        public virtual void ObsugaBack()
        {
        }
        public MenuPodstawa()
        {

        }
        public MenuPodstawa(int Wspułczynik, params Texture2D[] Obrazy)
            : this()
        {
            CzySąObrazyWTle = true;
            CzasTrwaniaObrazu = Wspułczynik;
            ListaObrazów = Obrazy;
        }
        public MenuPodstawa(Texture2D WyglądKursora,int Wspułczynik,params Texture2D[] Obrazy):this()
        {
            CzySąObrazyWTle = true;
            CzasTrwaniaObrazu = Wspułczynik;
            ListaObrazów = Obrazy;
            MyszkaAktywna=true;
            Mysz = WyglądKursora;
        }
        public MenuPodstawa(Texture2D Kursor)
        {
            MyszkaAktywna = true;
            Mysz = Kursor;
        }
        public virtual void UpDate(GameTime GT)
        {
            while (ZdażeniaDoNatepnegoUpdate.Count>0)
            {
                ZdażeniaDoNatepnegoUpdate[0](this, EventArgs.Empty);
                ZdażeniaDoNatepnegoUpdate.RemoveAt(0);
            }
            System.Diagnostics.Debug.WriteLine("U1");
            if (!WyświetlanieMyszki)
            {
                MouseState s = Mouse.GetState();
                if (!(s.Y == 0 && s.X==0))
                    WyświetlanieMyszki = true;
            }
            if (OdczytywanieNagrań)
            {
                if (zlecenia[NumerAktualnieRelizowanegoZlecenia+1].Poczotek == CzasWMenu)
                {
                    NumerAktualnieRelizowanegoZlecenia++;
                    if (zlecenia[NumerAktualnieRelizowanegoZlecenia].ListaZdażeń != null)
                        Zleć();
                }
                if(zlecenia.Count==NumerAktualnieRelizowanegoZlecenia+1)
                {
                    OdczytywanieNagrań=false;
                    if (KoniecZlecenia != null)
                        KoniecZlecenia(this, EventArgs.Empty);
                    goto wyjdź;
                }

            }
            CzasWMenu++;
            wyjdź:
            if (!BlokadaOdczytu)
                OdczytZTothPad();

            if (MyszkaAktywna)
                MyszkaAktywna = ObsługaMyszki();

            for (int i = 0; i < Lista.Count; i++)
			{

                if (!Lista[i].Ukryty && Lista[i].CzyUżywaUpdate)
                    Lista[i].UżycieUpdate(GT);
			}

            System.Diagnostics.Debug.WriteLine("U2");

            for (int i = 0; i < ListaKomponetów.Count; i++)
            {
                if (ListaKomponetów[i].UpDate(GT))
                {
                    ListaKomponetów.RemoveAt(i);
                    i--;
                }
            }
            if (CzySąObrazyWTle)
            {
                if (IDoObrazówWtle < CzasTrwaniaObrazu)
                {
                    IDoObrazówWtle++;
                }
                else
                {
                    IDoObrazówWtle = 0;
                    if (NumerObrazuWTle+1 < ListaObrazów.Length)
                        NumerObrazuWTle++;
                    else
                        NumerObrazuWTle = 0;

                }
            }
            System.Diagnostics.Debug.WriteLine("U3");
        }
        void Zleć()
        {
            Vector2 MinZleć=Vector2.Zero,MaxZleć=Vector2.Zero;
            for (int ii = 0; ii < zlecenia[NumerAktualnieRelizowanegoZlecenia].ListaZdażeń.Count; ii++)
            {
                EventArgs EventDoListy = zlecenia[NumerAktualnieRelizowanegoZlecenia].ListaZdażeń[ii];
                if (zlecenia[NumerAktualnieRelizowanegoZlecenia].ListaZdażeń[ii] is EventKlikniety)
                {
                    EventKlikniety ek=(EventKlikniety)zlecenia[NumerAktualnieRelizowanegoZlecenia].ListaZdażeń[ii];
                    MinZleć = ek.Miejsce;
                    MaxZleć = ek.Miejsce;
                    EventDoListy=ek;
                }
                else if (zlecenia[NumerAktualnieRelizowanegoZlecenia].ListaZdażeń[ii] is EventPrzesuniecie)
                {
                    EventPrzesuniecie ek = (EventPrzesuniecie)zlecenia[NumerAktualnieRelizowanegoZlecenia].ListaZdażeń[ii];
                    MinZleć = ek.Miejsce;
                    MaxZleć = ek.Miejsce1;
                    EventDoListy=ek;
                } 
                UżywanieKotrolekPoKwadracie(EventDoListy, MinZleć, MaxZleć);
            }
        }
        public void Zleć(EventArgs a,bool DlaWszytkichBezMiejsca,Vector2 MiejsceZdażenie)
        {
            for (int i = 0; i < Lista.Count; i++)
            {
                if (DlaWszytkichBezMiejsca||Lista[i].Kolizja(MiejsceZdażenie))
                {
                    Lista[i].UpDate(a);
                }
            }
        }
        private static void UstawPunktyRosnoco(ref Vector2 MinZleć, ref Vector2 MaxZleć)
        {
            Vector2 Przełóż = Vector2.Zero;
            if (MinZleć.X > MaxZleć.X)
            {
                Przełóż=MinZleć;
                MinZleć.X = MaxZleć.X;
                MaxZleć.X = Przełóż.X;
            }

            if (MinZleć.Y > MaxZleć.Y)
            {
                Przełóż = MinZleć;
                MinZleć.Y = MaxZleć.Y;
                MaxZleć.Y = Przełóż.Y;
            }
        }
        private void UżywanieKotrolekPoKwadracie(EventArgs EventDoListy, Vector2 MinZleć, Vector2 MaxZleć)
        {

            Odcinek o=new Odcinek(MinZleć.X,MaxZleć.X,MinZleć.Y,MaxZleć.Y);

            for (int i = 0; i < Lista.Count; i++)
            {
                if (!Lista[i].Ukryty && Lista[i].KolizjaKwadratu(o))
                {

                    if (Lista[i].UpDate(EventDoListy))
                    {
                        break;
                    }

                }
            }
        }
        private void UżywanieKotrolekPoPunkcie(EventArgs EventDoListy, Vector2 MinZleć)
        {

            for (int i = 0; i < Lista.Count; i++)
            {
                if (!Lista[i].Ukryty && Lista[i].Kolizja(MinZleć))
                {
                    if (Lista[i].UpDate(EventDoListy))
                        break;
                }
            }
        }
        void OdczytZTothPad()
        {
            
            MiejscaDotchnieć = TouchPanel.GetState();
            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.FreeDrag;
            Vector2 min = Vector2.Zero, max = Vector2.Zero;
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gestureSample = TouchPanel.ReadGesture();

                EventArgs Zdażenie = EventArgs.Empty;
                switch (gestureSample.GestureType)
                {
                    case GestureType.FreeDrag:
                        Zdażenie = new EventPrzesuniecie(gestureSample.Position.X, gestureSample.Position.Y, gestureSample.Position.X + gestureSample.Delta.X, gestureSample.Position.Y + gestureSample.Delta.Y);
                        
                            min.X = gestureSample.Position.X;
                            min.Y = gestureSample.Position.Y;
                            max.X = gestureSample.Position.X+gestureSample.Delta.X;
                            max.Y = gestureSample.Position.Y + gestureSample.Delta.Y;
                        break;
                    case GestureType.Tap:
                        Zdażenie = new EventKlikniety(gestureSample.Position.X, gestureSample.Position.Y);
                        min = gestureSample.Position;
                        max = gestureSample.Position;
                        break;
                    default:
                        break;
                }
                UżywanieKotrolekPoKwadracie(Zdażenie, min, max);

            }
            if(ObsugaWysyłańPojedyczychDotchnieć)
            {
            foreach (TouchLocation item in MiejscaDotchnieć)
            {
                EventWysłanieDotchniecia m = new EventWysłanieDotchniecia();
                m.Dotchniecie = item;
                UżywanieKotrolekPoKwadracie(m, item.Position, item.Position);
            }
            }
        }
        Vector2 MiejsceDawnegoKlikniecia;
        public Vector2 MiejsceMyszki;
        bool ByłKlikniety = false, ByłPrzesówany = false;
        XnaKontrolka ZaznaczonaKontrolkaMyszką;
        public bool ObsugaWysyłańPojedyczychDotchnieć;
        bool ObsługaMyszki()
        {
            if (BlokadaOdczytu)
            {
                return true;
            }

            MouseState m = Mouse.GetState();
            MiejsceMyszki = new Vector2(m.X, m.Y);
            Vector2 Min = Vector2.Zero, Max = Vector2.Zero;
            if (m.LeftButton == ButtonState.Pressed)
            {
                if (ByłKlikniety)
                {
                    Vector2 c = MiejsceDawnegoKlikniecia;
                    c.X -= m.X;
                    c.Y -= m.Y;
                    float Długośc = c.Length();
                    if (c.Length() > 2)
                    {
                        Min = MiejsceDawnegoKlikniecia;
                        Max = MiejsceMyszki;
                        try
                        {
                            UżywanieKotrolekPoKwadracie(new EventPrzesuniecie(MiejsceDawnegoKlikniecia.X, MiejsceDawnegoKlikniecia.Y, MiejsceMyszki.X, MiejsceMyszki.Y), Min, Max);
                        }
                        catch (Exception e)
                        {
                        }
                        MiejsceDawnegoKlikniecia = new Vector2(m.X, m.Y);
                        ByłPrzesówany = true;
                    }
                }
                else
                {
                    ByłKlikniety = true;
                    MiejsceDawnegoKlikniecia=new Vector2(m.X, m.Y);
                }
            }
            else
            {
                if (ByłKlikniety)
                {
                    if (!ByłPrzesówany)
                    {
                        Vector2 c = MiejsceDawnegoKlikniecia;
                        c.X -= m.X;
                        c.Y -= m.Y;
                        if (c.Length() < 6)
                        {
                            try
                            {
                                UżywanieKotrolekPoPunkcie(new EventKlikniety(MiejsceDawnegoKlikniecia.X, MiejsceDawnegoKlikniecia.Y), MiejsceDawnegoKlikniecia);
                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }
                    ByłKlikniety = false;
                }
                else
                {
                    bool b = true;
                    foreach (XnaKontrolka item in Lista)
                    {
                        if (item.KolizjaZawsze(MiejsceMyszki)&&!item.BezWielkości)
                        {
                            if (item==ZaznaczonaKontrolkaMyszką)
                            {
                                b = false;
                                break;
                            }
                            b = false;
                            if (ZaznaczonaKontrolkaMyszką!=null)
                            {
                                ZaznaczonaKontrolkaMyszką.Puść(MiejsceMyszki);
                            }
                            item.PoczotekTrzymanaMyszką(MiejsceMyszki);
                            ZaznaczonaKontrolkaMyszką = item;
                        }
                    }
                    if (ZaznaczonaKontrolkaMyszką!=null&&b)
                    {
                        ZaznaczonaKontrolkaMyszką.Puść(MiejsceMyszki);
                        ZaznaczonaKontrolkaMyszką = null;
                    }
                    ByłKlikniety = false;
                    ByłPrzesówany = false;
                }
            }
                
            return true;
        }
        public void Add(XnaKontrolka Kontrolka)
        {
            Lista.Add(Kontrolka);
            Kontrolka.Dodawanie();
        }
        public void RemoveAt(int Index)
        {
            Lista[Index].Usówanie();
            Lista.RemoveAt(Index);

        }
        public void Remove(XnaKontrolka x)
        {
            Lista.Remove(x);
            x.Usówanie();
        }
        protected void Forechan(PrzekażKontrolki e)
        {
            foreach (XnaKontrolka item in Lista)
            {
                e(item);
            }
        }
        protected void AddKomponet(IComponet Kontrolka)
        {
            ListaKomponetów.Add(Kontrolka);
        }
        protected void RemoveAtKomponet(int Index)
        {
            ListaKomponetów.RemoveAt(Index);
        }
    }
}
