using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Komputer.Xna.Menu;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Komputer.Xna;
using Komputer;
using Microsoft.Xna.Framework.Input.Touch;
namespace Komputer.Xna.Menu
{
     public class ObiektZChwyceniem:XnaKontrolka
     {
         public delegate void WyślijVektor(object o, Vector2 Vektor);
         public event WyślijVektor Upuszczony;
         public bool CzyMożnaPrzesówać
         {
             get;
             set;
         }
         bool WyświetlanieBezTrzymania = true;
         int IdDotchniecia = 0;
         Vector2 MiejsceTrzymania;
         MenuPodstawa menu;
         bool Trzymany=false;
         Texture2D prznoszony;
         Vector2 PołowaObrazu;
         public Color KolorUniesionego;
         public Texture2D PrznoszonyObraz
         {
             get { return prznoszony; }
             set { prznoszony = value;
             Wielkość = new Vector2(value.Width, value.Height);
             }
         }
        public ObiektZChwyceniem( MenuPodstawa menu)
        {
            CzyUżywaUpdate = true;
            this.menu = menu;
            CzyMożnaPrzesówać = true;
            BezWielkości = true;
            ZmianaWielkości += ObiektZChwyceniem_ZmianaWielkości;
        }

        void ObiektZChwyceniem_ZmianaWielkości(object sender, EventArgs e)
        {
            PołowaObrazu = Wielkość / 2;
        }
        public override void Draw(SpriteBatch pezel)
        {
            base.Draw(pezel);
            if (PrznoszonyObraz != null)
            {
                if (Trzymany)
                {
                        pezel.Draw(PrznoszonyObraz, MiejsceTrzymania - PołowaObrazu, null, KolorUniesionego, 0, Vector2.Zero, Scala, SpriteEffects.None, 0);
                }
                else if(WyświetlanieBezTrzymania)
                {
                    pezel.Draw(PrznoszonyObraz, Miejsce, null,Kolor , 0, Vector2.Zero, Scala, SpriteEffects.None, 0);
                }
            }
        }
        public override void UżycieUpdate(GameTime gt)
        {
            if (menu.BlokadaOdczytu)
            {
                return;
            }
                for (int i = 0; i < menu.MiejscaDotchnieć.Count; i++)
                {
                    if (Trzymany)
                    {
                        if (IdDotchniecia == menu.MiejscaDotchnieć[i].Id)
                        {
                            switch (menu.MiejscaDotchnieć[i].State)
                            {
                                case TouchLocationState.Invalid:
                                    Trzymany = false;
                                    break;
                                case TouchLocationState.Moved:
                                    MiejsceTrzymania = menu.MiejscaDotchnieć[i].Position;
                                    break;
                                case TouchLocationState.Released:
                                    if (Upuszczony!=null)
                                    {
                                        Upuszczony(this, menu.MiejscaDotchnieć[i].Position);
                                    }
                                    Trzymany = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (CzyMożnaPrzesówać && menu.MiejscaDotchnieć[i].State == TouchLocationState.Pressed && Kolizja(menu.MiejscaDotchnieć[i].Position))
                    {
                        IdDotchniecia = menu.MiejscaDotchnieć[i].Id;
                        Trzymany = true;
                    }
                }

            base.UżycieUpdate(gt);
            
        }

        public override bool UpDate(EventArgs b)
        {
            ZlećChwycenie e=b as ZlećChwycenie;
            if (e!=null)
            {
                switch (e.Stan)
                {
                    case TouchLocationState.Moved:
                        if (Trzymany)
                        {
                            MiejsceTrzymania = e.Miejsce;
                        }
                        break;
                    case TouchLocationState.Pressed:
                        if (Kolizja(e.Miejsce))
                        {
                            Trzymany = true;
                            MiejsceTrzymania = e.Miejsce;
                        }
                        break;
                    case TouchLocationState.Released:
                        if (Trzymany)
                        {
                            Trzymany = false;
                            MiejsceTrzymania = e.Miejsce;
                            Upuszczony(this, MiejsceTrzymania);
                        }
                        break;
                    default:
                        break;
                }
            }
            return false;
        }
    }
     public class ZlećChwycenie:EventArgs
     {
         public Vector2 Miejsce;
         public TouchLocationState Stan;
         public static void ZlećKolejke(List<ZleceniaMenu> Lista, int Długość,Texture2D Obraz,Vector2 Przesóniecie, params Vector2[] Punkty)
         {
             ZleceniaMenu z=new ZleceniaMenu(Długość+10);
             z.ListaWyświetleń =new List<ZlecenieWyświet>(){ new ZlecenieWyświet(Obraz, Punkty[0] - Przesóniecie)};
             ZlećChwycenie Zh=new ZlećChwycenie();
             Zh.Miejsce=Punkty[0];
             Zh.Stan=TouchLocationState.Pressed;
             z.ListaZdażeń =new List<EventArgs>(){ Zh};
             Lista.Add(z);
             for (int i = 1; i < Punkty.Length-1; i++)
             {
                 z = new ZleceniaMenu(Długość);
                 Zh = new ZlećChwycenie();
                 Zh.Miejsce = Punkty[i];
                 Zh.Stan = TouchLocationState.Moved;
                 z.ListaWyświetleń = new List<ZlecenieWyświet>() { new ZlecenieWyświet(Obraz, Punkty[i] - Przesóniecie) };
                 z.ListaZdażeń = new List<EventArgs>() { Zh };
                 Lista.Add(z);
             }
             z = new ZleceniaMenu(Długość+5);
             Zh = new ZlećChwycenie();
             Zh.Miejsce = Punkty[Punkty.Length-1];
             Zh.Stan = TouchLocationState.Released;
             z.ListaWyświetleń = new List<ZlecenieWyświet>() { new ZlecenieWyświet(Obraz, Punkty[Punkty.Length-1] - Przesóniecie) };
             z.ListaZdażeń = new List<EventArgs>() { Zh };
             Lista.Add(z);
         }
     }
}
