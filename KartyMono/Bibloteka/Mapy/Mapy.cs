using System;
using System.Collections.Generic;
namespace Komputer.Mapy
{
    public enum WybórPrzeszkody { Pusty, Start, Meta, Płotek };
    public enum Kierunek { Gura, Lewo, Dół, Prawo, LewoGura, LewoDół, PrawoGura, PrawoDół };
    public class Mapy
    {
        public static int SprawdźOdległość(Kierunek a)
        {
            int Zwracana = 0;
                if (a == Kierunek.Gura || a == Kierunek.Dół || a == Kierunek.Prawo || a == Kierunek.Lewo)
                    Zwracana += 10;
                else
                    Zwracana += 14;
            return Zwracana;
        }
        public static int SprawdźOdległość(Kierunek[] Droga)
        {
            int Zwracana=0;
            foreach (Kierunek a in Droga)
            {
                if (a == Kierunek.Gura || a == Kierunek.Dół || a == Kierunek.Prawo || a == Kierunek.Lewo)
                    Zwracana += 10;
                else
                    Zwracana += 14;
                
            }
            return Zwracana;
        }
        public static Kierunek[] DrogaZMaxOdległością(Kierunek[] Droga,int Max)
        {
            int IlośćKierunkówNastepnej = 0, AktualnaDługość=0;
            for (int i = 0; i < Droga.Length; i++)
            {
                AktualnaDługość += SprawdźOdległość(Droga[i]);
                if (AktualnaDługość <= Max)
                {
                    IlośćKierunkówNastepnej++;
                }
                else
                {
                    break;
                }
            }
            Kierunek[] Zwracana = new Kierunek[IlośćKierunkówNastepnej];
            for (int i = 0; i <IlośćKierunkówNastepnej; i++)
            {
                Zwracana[i] = Droga[i];
            }
            return Zwracana;
        }
        public static WybórPrzeszkody[,] Przemiana(char[,] Wejście, int x, int y)
        {
            WybórPrzeszkody[,] p = new WybórPrzeszkody[x, y];
            for (int ii = 0; ii < y; ii++)
            {
                for (int i = 0; i < x; i++)
                {
                    if (Wejście[ii, i] == ' ')
                        p[i, ii] = WybórPrzeszkody.Pusty;
                    if (Wejście[ii, i] == 'x')
                        p[i, ii] = WybórPrzeszkody.Płotek;
                    if (Wejście[ii, i] == 's')
                        p[i, ii] = WybórPrzeszkody.Start;
                    if (Wejście[ii, i] == 'f')
                        p[i, ii] = WybórPrzeszkody.Meta;
                }
            }
            return p;
        }

        public static void NajkrutszaDrogaMapa(WybórPrzeszkody[,] Mapa, int x, int y, out int[,] TabLidrzb)
        {
            TabLidrzb = new int[x, y];
            int xy = x * y;

            int  StartX, startY;
            if (!PoprawnośćMapkiBK(Mapa, x, y, out StartX, out startY))
                return;

            for (int i = 0; i < x; i++)
            {
                for (int ii = 0; ii < y; ii++)
                {
                    TabLidrzb[i, ii] = xy;
                }
            }
            TabLidrzb[StartX, startY] = 0;
            ////////////////////////////////////////////////////////////////////////
            int ilerazy = 1;
            bool Pozytywne = false;
            while (ilerazy != 0 && !Pozytywne)
            {
                ilerazy = 0;
                for (int i = 0; i < x; i++)
                {
                    for (int ii = 0; ii < y; ii++)
                    {
                        if (Mapa[i, ii] == WybórPrzeszkody.Płotek || Mapa[i, ii] == WybórPrzeszkody.Start) { continue; }
                        else
                        {
                            if (TabLidrzb[i, ii] != xy)
                                continue;
                            else
                            {
                                if (i > 0 && TabLidrzb[i - 1, ii] < TabLidrzb[i, ii]) { TabLidrzb[i, ii] = TabLidrzb[i - 1, ii] + 1; ilerazy++; }
                                if (i < x - 1 && TabLidrzb[i + 1, ii] < TabLidrzb[i, ii]) { TabLidrzb[i, ii] = TabLidrzb[i + 1, ii] + 1; ilerazy++; }
                                if (ii > 0 && TabLidrzb[i, ii - 1] < TabLidrzb[i, ii]) { TabLidrzb[i, ii] = TabLidrzb[i, ii - 1] + 1; ilerazy++; }
                                if (ii < y - 1 && TabLidrzb[i, ii + 1] < TabLidrzb[i, ii]) { TabLidrzb[i, ii] = TabLidrzb[i, ii + 1] + 1; ilerazy++; }

                            }
                        }
                    }
                }
            }


        }
        public static int SprawdźOdległośćDoPunkt(int[,] TabLidrzbX10, int x, int y, Punkt P)
        {
            int MIEJSCEX = P.X;
            int MIEJSCEY = P.Y;
            int Wartośc=x*y*10;
            if (MIEJSCEY + 1 < y)
                if (TabLidrzbX10[MIEJSCEX, MIEJSCEY + 1] + 10 <Wartośc)
                {
                    Wartośc = TabLidrzbX10[MIEJSCEX, MIEJSCEY + 1]+10;
                }
            if (MIEJSCEX + 1 < x)
                if (TabLidrzbX10[MIEJSCEX+1, MIEJSCEY] + 10 < Wartośc)
                {
                    Wartośc = TabLidrzbX10[MIEJSCEX+1, MIEJSCEY]+10;
                }
            if (MIEJSCEY>0)
                if (TabLidrzbX10[MIEJSCEX, MIEJSCEY - 1] + 10 < Wartośc)
                {
                    Wartośc = TabLidrzbX10[MIEJSCEX, MIEJSCEY - 1]+10;
                }
            if (MIEJSCEX >0)
                if (TabLidrzbX10[MIEJSCEX - 1, MIEJSCEY] + 10 < Wartośc)
                {
                    Wartośc = TabLidrzbX10[MIEJSCEX-1, MIEJSCEY]+10;
                }
            if (MIEJSCEY + 1 < y && MIEJSCEX + 1 < x)
                if (TabLidrzbX10[MIEJSCEX+1, MIEJSCEY + 1] + 14 < Wartośc)
                {
                    Wartośc = TabLidrzbX10[MIEJSCEX+1, MIEJSCEY + 1]+14;
                }
            if (MIEJSCEX + 1 < x&&MIEJSCEY>0)
                if (TabLidrzbX10[MIEJSCEX + 1, MIEJSCEY-1] + 14 < Wartośc)
                {
                    Wartośc = TabLidrzbX10[MIEJSCEX + 1, MIEJSCEY-1]+14;
                }
            if (MIEJSCEX > 0 && MIEJSCEY + 1 < y)
                if (TabLidrzbX10[MIEJSCEX - 1, MIEJSCEY+1] + 14 < Wartośc)
                {
                    Wartośc = TabLidrzbX10[MIEJSCEX - 1, MIEJSCEY+1]+14;
                }
            if (MIEJSCEX > 0&&MIEJSCEY>0)
                if (TabLidrzbX10[MIEJSCEX - 1, MIEJSCEY-1] + 14 < Wartośc)
                {
                    Wartośc = TabLidrzbX10[MIEJSCEX - 1, MIEJSCEY-1]+14;
                }
            return Wartośc;
        }
        public static bool PusteNaOkoło(WybórPrzeszkody[,] TabLidrzbX10, int x, int y,Punkt Miejsce,out Punkt Pusty)
        {
            Pusty = Miejsce;
            int MIEJSCEX = Miejsce.X;
            int MIEJSCEY = Miejsce.Y;
            if (MIEJSCEY + 1 < y)
                if (TabLidrzbX10[MIEJSCEX, MIEJSCEY + 1] == WybórPrzeszkody.Pusty)
                {
                    Pusty.Y++;
                    return true;
                }
            if (MIEJSCEX + 1 < x)
                if (TabLidrzbX10[MIEJSCEX + 1, MIEJSCEY] == WybórPrzeszkody.Pusty)
                {
                    Pusty.X++;
                    return true;
                }
            if (MIEJSCEY > 0)
                if (TabLidrzbX10[MIEJSCEX, MIEJSCEY - 1] == WybórPrzeszkody.Pusty)
                {
                    Pusty.Y--;
                    return true;
                }
            if (MIEJSCEX > 0)
                if (TabLidrzbX10[MIEJSCEX - 1, MIEJSCEY] == WybórPrzeszkody.Pusty)
                {
                    Pusty.X--;
                    return true;
                }
            if (MIEJSCEY + 1 < y && MIEJSCEX + 1 < x)
                if (TabLidrzbX10[MIEJSCEX + 1, MIEJSCEY + 1] == WybórPrzeszkody.Pusty)
                {
                    Pusty.X++;
                    Pusty.Y++;
                    return true;
                }
            if (MIEJSCEX + 1 < x && MIEJSCEY > 0)
                if (TabLidrzbX10[MIEJSCEX + 1, MIEJSCEY - 1] == WybórPrzeszkody.Pusty)
                {
                    Pusty.X++;
                    Pusty.Y--;
                }
            if (MIEJSCEX > 0 && MIEJSCEY + 1 < y)
                if (TabLidrzbX10[MIEJSCEX - 1, MIEJSCEY + 1] == WybórPrzeszkody.Pusty)
                {
                    Pusty.X--;
                    Pusty.Y++;
                    return true;
                }
            if (MIEJSCEX > 0 && MIEJSCEY > 0)
                if (TabLidrzbX10[MIEJSCEX - 1, MIEJSCEY - 1] == WybórPrzeszkody.Pusty)
                {
                    Pusty.X--;
                    Pusty.Y--;
                    return true;
                }
            return false;
        }
      
        public static bool NajkrutszaDrogaKierunkiZSkosami(WybórPrzeszkody[,] Mapa, int x, int y, out Kierunek[] Droga)
        {
            int[,] TabLidrzbX10 = new int[x, y];
            Droga = new Kierunek[0];
            int xy = x * y * 10;

            int KoniecX, KoniecY, StartX, startY;
            if (!PoprawnośćMapki(Mapa, x, y, out KoniecX, out KoniecY, out StartX, out startY))
                return false;

            for (int i = 0; i < x; i++)
            {
                for (int ii = 0; ii < y; ii++)
                {
                    TabLidrzbX10[i, ii] = xy;
                }
            }
            TabLidrzbX10[StartX, startY] = 0;
            ////////////////////////////////////////////////////////////////////////
            int ilerazy = 1;
            bool Pozytywne = false;
            while (ilerazy != 0 && !Pozytywne)
            {
                ilerazy = 0;
                for (int i = 0; i < x; i++)
                {
                    for (int ii = 0; ii < y; ii++)
                    {
                        if (Mapa[i, ii] == WybórPrzeszkody.Płotek || Mapa[i, ii] == WybórPrzeszkody.Start) { continue; }
                        else
                        {
                            if (i > 0 && TabLidrzbX10[i - 1, ii] < TabLidrzbX10[i, ii] - 10) { TabLidrzbX10[i, ii] = TabLidrzbX10[i - 1, ii] + 10; ilerazy++; }
                            if (i < x - 1 && TabLidrzbX10[i + 1, ii] < TabLidrzbX10[i, ii] - 10) { TabLidrzbX10[i, ii] = TabLidrzbX10[i + 1, ii] + 10; ilerazy++; }
                            if (ii > 0 && TabLidrzbX10[i, ii - 1] < TabLidrzbX10[i, ii] - 10) { TabLidrzbX10[i, ii] = TabLidrzbX10[i, ii - 1] + 10; ilerazy++; }
                            if (ii < y - 1 && TabLidrzbX10[i, ii + 1] < TabLidrzbX10[i, ii] - 10) { TabLidrzbX10[i, ii] = TabLidrzbX10[i, ii + 1] + 10; ilerazy++; }


                            if ((i > 0 && ii > 0) && TabLidrzbX10[i - 1, ii - 1] < TabLidrzbX10[i, ii] - 14) { TabLidrzbX10[i, ii] = TabLidrzbX10[i - 1, ii - 1] + 14; ilerazy++; }
                            if ((ii < y - 1) && (i < x - 1) && TabLidrzbX10[i + 1, ii + 1] < TabLidrzbX10[i, ii] - 14) { TabLidrzbX10[i, ii] = TabLidrzbX10[i + 1, ii + 1] + 14; ilerazy++; }
                            if (i > 0 && ii < y - 1 && TabLidrzbX10[i - 1, ii + 1] < TabLidrzbX10[i, ii] - 14) { TabLidrzbX10[i, ii] = TabLidrzbX10[i - 1, ii + 1] + 14; ilerazy++; }
                            if (ii > 0 && i < x - 1 && TabLidrzbX10[i + 1, ii - 1] < TabLidrzbX10[i, ii] - 14) { TabLidrzbX10[i, ii] = TabLidrzbX10[i + 1, ii - 1] + 14; ilerazy++; }

                        }
                    }
                }
            }

            if (TabLidrzbX10[KoniecX, KoniecY] == xy)
                return false;

            List<Kierunek> ListaDroga = new List<Kierunek>();
            int MIEJSCEX = KoniecX, MIEJSCEY = KoniecY;
            int Wartośc = TabLidrzbX10[MIEJSCEX, MIEJSCEY];
            int xi = 0;
            while(Wartośc!=0)
            {
                xi++;
                if (MIEJSCEY + 1 < y)
                    if (TabLidrzbX10[MIEJSCEX, MIEJSCEY + 1] == Wartośc-10)
                    {
                        Wartośc -= 10;
                        MIEJSCEY++;
                        ListaDroga.Add(Kierunek.Gura);
                        continue;
                    }
                if (MIEJSCEY > 0 )
                    if (TabLidrzbX10[MIEJSCEX, MIEJSCEY - 1] == Wartośc-10)
                    {
                        Wartośc -= 10;
                        ListaDroga.Add(Kierunek.Dół);
                        MIEJSCEY--;
                        continue;
                    }
                if (MIEJSCEX + 1 < x)
                    if (TabLidrzbX10[MIEJSCEX + 1, MIEJSCEY] == Wartośc-10)
                    {
                        Wartośc -= 10;
                        MIEJSCEX += 1;
                        ListaDroga.Add(Kierunek.Lewo);
                        continue;
                    }
                if (MIEJSCEX > 0)
                    if (TabLidrzbX10[MIEJSCEX - 1, MIEJSCEY] == Wartośc-10)
                    {
                        MIEJSCEX -= 1;
                        Wartośc -= 10;
                        ListaDroga.Add(Kierunek.Prawo);
                        continue;
                    }

                if (MIEJSCEY + 1 < y && MIEJSCEX + 1 < x)
                    if (TabLidrzbX10[MIEJSCEX+1, MIEJSCEY + 1] == Wartośc - 14)
                    {
                        Wartośc -= 14;
                        MIEJSCEY++;
                        MIEJSCEX++;
                        ListaDroga.Add(Kierunek.LewoGura);
                        continue;
                    }
                if (MIEJSCEY > 0 && MIEJSCEX + 1 < x)
                    if (TabLidrzbX10[MIEJSCEX+1, MIEJSCEY - 1] == Wartośc - 14)
                    {
                        Wartośc -= 14;
                        ListaDroga.Add(Kierunek.LewoDół);
                        MIEJSCEY--;
                        MIEJSCEX++;
                        continue;
                    }
                if (MIEJSCEY + 1 < y && MIEJSCEX >0)
                    if (TabLidrzbX10[MIEJSCEX - 1, MIEJSCEY + 1] == Wartośc - 14)
                    {
                        Wartośc -= 14;
                        MIEJSCEY++;
                        MIEJSCEX--;
                        ListaDroga.Add(Kierunek.PrawoGura);
                        continue;
                    }
                if (MIEJSCEY > 0 && MIEJSCEX >0)
                    if (TabLidrzbX10[MIEJSCEX - 1, MIEJSCEY - 1] == Wartośc - 14)
                    {
                        Wartośc -= 14;
                        ListaDroga.Add(Kierunek.PrawoDół);
                        MIEJSCEY--;
                        MIEJSCEX--;
                        continue;
                    }
            }
            Droga = new Kierunek[xi];
            for (int i = 0; i<xi; i++)
            {
                Droga[i] = ListaDroga[xi - (i+1)];
            }
            return true;

        }
        public static void NajkrutszaDrogaMapaZSkosami(WybórPrzeszkody[,] Mapa, int x, int y, out int[,] TabLidrzbX10)
        {
            TabLidrzbX10 = new int[x, y];
            int xy = x * y*10;

            int  StartX, startY;
            if (!PoprawnośćMapkiBK(Mapa, x, y, out StartX, out startY))
                return;

            for (int i = 0; i < x; i++)
            {
                for (int ii = 0; ii < y; ii++)
                {
                    TabLidrzbX10[i, ii] = xy;
                }
            }
            TabLidrzbX10[StartX, startY] = 0;
            ////////////////////////////////////////////////////////////////////////
            int ilerazy = 1;
            bool Pozytywne = false;
            while (ilerazy != 0 && !Pozytywne)
            {
                ilerazy = 0;
                for (int i = 0; i < x; i++)
                {
                    for (int ii = 0; ii < y; ii++)
                    {
                        if (Mapa[i, ii] == WybórPrzeszkody.Płotek || Mapa[i, ii] == WybórPrzeszkody.Start) { continue; }
                        else
                        {
                                if (i > 0 && TabLidrzbX10[i - 1, ii] < TabLidrzbX10[i, ii]-10) { TabLidrzbX10[i, ii] = TabLidrzbX10[i - 1, ii] + 10; ilerazy++; }
                                if (i < x - 1 && TabLidrzbX10[i + 1, ii] < TabLidrzbX10[i, ii]-10) { TabLidrzbX10[i, ii] = TabLidrzbX10[i + 1, ii] + 10; ilerazy++; }
                                if (ii > 0 && TabLidrzbX10[i, ii - 1] < TabLidrzbX10[i, ii]-10) { TabLidrzbX10[i, ii] = TabLidrzbX10[i, ii - 1] + 10; ilerazy++; }
                                if (ii < y - 1 && TabLidrzbX10[i, ii + 1] < TabLidrzbX10[i, ii]-10) { TabLidrzbX10[i, ii] = TabLidrzbX10[i, ii + 1] + 10; ilerazy++; }


                                if ((i > 0&&ii>0 )&& TabLidrzbX10[i - 1, ii-1] < TabLidrzbX10[i, ii]-14) { TabLidrzbX10[i, ii] = TabLidrzbX10[i - 1, ii-1] + 14; ilerazy++; }
                                if ((ii < y - 1) && (i < x - 1) && TabLidrzbX10[i + 1, ii + 1] < TabLidrzbX10[i, ii]-14) { TabLidrzbX10[i, ii] = TabLidrzbX10[i + 1, ii+1] + 14; ilerazy++; }
                                if (i > 0 && ii < y - 1 && TabLidrzbX10[i - 1, ii + 1] < TabLidrzbX10[i, ii]-14) { TabLidrzbX10[i, ii] = TabLidrzbX10[i-1, ii + 1] + 14; ilerazy++; }
                                if (ii > 0 && i < x - 1 && TabLidrzbX10[i + 1, ii - 1] < TabLidrzbX10[i, ii]-14) { TabLidrzbX10[i, ii] = TabLidrzbX10[i + 1, ii - 1] + 14; ilerazy++; }
                            
                        }
                    }
                }
            }



        }
        public static bool NajkrutszaDrogaKierunk(WybórPrzeszkody[,] Mapa, int x, int y, out Kierunek[] tab)
        {
            tab=new Kierunek[0];
            int xy = x * y;
            int DługośćDrogi;
            
            int KoniecX, KoniecY, StartX, startY;
            if(! PoprawnośćMapki(Mapa, x, y,out KoniecX,out KoniecY,out StartX,out startY))
                return false;
            
            int[,] TabLidrzb = new int[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int ii = 0; ii < y; ii++)
                {
                     TabLidrzb[i, ii] = xy;
                }
            }
            TabLidrzb[StartX, startY] = 0;
            ////////////////////////////////////////////////////////////////////////
            int ilerazy = 1;
            bool Pozytywne = false;
            while (ilerazy != 0 && !Pozytywne)
            {
                ilerazy = 0;
                for (int i = 0; i < x; i++)
                {
                    for (int ii = 0; ii < y; ii++)
                    {
                        if (Mapa[i, ii] == WybórPrzeszkody.Płotek || Mapa[i, ii] == WybórPrzeszkody.Start) { continue; }
                        else
                        {
                            if (TabLidrzb[i, ii] != xy)
                                continue;
                            else
                            {
                                if (i > 0 && TabLidrzb[i - 1, ii] < TabLidrzb[i, ii]) { TabLidrzb[i, ii] = TabLidrzb[i - 1, ii] + 1; ilerazy++; }
                                if (i < x - 1 && TabLidrzb[i + 1, ii] < TabLidrzb[i, ii]) { TabLidrzb[i, ii] = TabLidrzb[i + 1, ii] + 1; ilerazy++; }
                                if (ii > 0 && TabLidrzb[i, ii - 1] < TabLidrzb[i, ii]) { TabLidrzb[i, ii] = TabLidrzb[i, ii - 1] + 1; ilerazy++; }
                                if (ii < y - 1 && TabLidrzb[i, ii + 1] < TabLidrzb[i, ii]) { TabLidrzb[i, ii] = TabLidrzb[i, ii + 1] + 1; ilerazy++; }

                            }
                        }
                    }
                }
            }

            if (TabLidrzb[KoniecX, KoniecY] == xy)
                return false;

                DługośćDrogi = TabLidrzb[KoniecX,KoniecY] ;
                tab = new Kierunek[DługośćDrogi];
                int MIEJSCEX=KoniecX,MIEJSCEY=KoniecY;
           
                for (int i = DługośćDrogi; 0 < i; i--)
                {
                    bool p = true;
                    if (MIEJSCEY + 1 < y && p)
                        if (TabLidrzb[MIEJSCEX, MIEJSCEY + 1] == i)
                        {
                            MIEJSCEY++ ;
                            p = false;
                            tab[i] = Kierunek.Gura;
                        }
                    if (MIEJSCEY > 0 && p)
                        if (TabLidrzb[MIEJSCEX, MIEJSCEY - 1] == i)
                        {
                            tab[i] = Kierunek.Dół;
                            MIEJSCEY--; 
                            p = false; 
                        }
                    if (MIEJSCEX + 1 < x && p)
                        if (TabLidrzb[MIEJSCEX + 1, MIEJSCEY] == i)
                        {
                            MIEJSCEX += 1;
                            p = false;
                            tab[i] = Kierunek.Lewo;
                        }
                    if (MIEJSCEX > 0 && p)
                        if (TabLidrzb[MIEJSCEX - 1, MIEJSCEY] == i)
                        {
                            MIEJSCEX -= 1;
                            p = false;
                            tab[i] = Kierunek.Prawo;
                        }

                }

                return true;
                
        }

        public static int LidrzbaPustychKratek(WybórPrzeszkody[,] Mapa,int x,int y)
        {
            int IlośćPustych = 0;
            for (int i = 0; i <x; i++)
            {
                for (int ii = 0; ii <y; ii++)
                {
                    if (Mapa[i,ii] == WybórPrzeszkody.Pusty)
                        IlośćPustych++;
                }
            }
            return IlośćPustych;

        }
        public static bool PoprawnośćMapki(WybórPrzeszkody[,] Mapa, int x, int y,out int KoniecX,out int KoniecY,out int StartX,out int startY)
        {
            bool bs = false, bk = false;
            KoniecX = 0; KoniecY = 0; StartX = 0; startY = 0;
            for (int i = 0; i < x; i++)
            {
                for (int ii = 0; ii < y; ii++)
                {
                    if (Mapa[i, ii] == WybórPrzeszkody.Start)
                    {
                        if (bs)
                            throw new ArgumentNullException("2 starty");
                        StartX = i;
                        startY = ii;
                        bs = true;
                    }
                    if (Mapa[i, ii] == WybórPrzeszkody.Meta)
                    {
                        if (bk)
                            throw new ArgumentNullException("2 mety");
                        KoniecX = i;
                        KoniecY = ii;
                        bk = true;
                    }
                }
            }
            return bs&&bk;

        }
        public static bool PoprawnośćMapkiBK(WybórPrzeszkody[,] Mapa, int x, int y, out int StartX, out int startY)
        {
            bool bs = false;
            StartX = 0; startY = 0;
            for (int i = 0; i < x; i++)
            {
                for (int ii = 0; ii < y; ii++)
                {
                    if (Mapa[i, ii] == WybórPrzeszkody.Start)
                    {
                        if (bs)
                            throw new ArgumentNullException("2 starty");
                        StartX = i;
                        startY = ii;
                        bs = true;
                    }
                }
            }
            return bs;

        }

    }
    public struct Punkt
    {
        public Punkt(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public int X, Y;
    }
}
