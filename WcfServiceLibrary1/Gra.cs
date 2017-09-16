using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraKarciana
{
    public abstract class Gra<T> where T:InstancjaGry
    {
        public Gra(int Ilo)
        {
            gracze = new T[Ilo];
        }
        public T[] gracze;
        public bool MiejsceWolne(out int Nr)
        {
            Nr = -1;
            for (int i = 0; i < gracze.Length; i++)
            {
                if (gracze[i] == null)
                {
                    if (Nr!=-1)
                    {
                        return true;
                    }
                    Nr = i;
                }

            }
            return false;
        }
        public bool DodajGracza(T uk)
        {
            uk.Zamykany += Uk_Zamykany;
            int NrWolny;
            if (MiejsceWolne(out NrWolny))
            {
                gracze[NrWolny] = uk;
                
                return true;
            }
            else
            {
                gracze[NrWolny] = uk;
                WywołajPrzedstawienie();
                ZnalezioneGraczy();
                return false;
            }
        }
        public abstract void ZwonioneMiejsce(int Nr);
        public abstract void PrzedstawGraczowi(T gracz, Urzytkownik[] tb);
        public abstract void ZnalezioneGraczy();
        public void WywołajPrzedstawienie()
        {
            List<Urzytkownik> uk = new List<Urzytkownik>();
            foreach (var item in gracze)
            {
                uk.Add(item.Gracz);
            }
            var z = uk.ToArray();
            foreach (var item in gracze)
            {
                PrzedstawGraczowi(item,z);
            }
        }
        private void Uk_Zamykany(object sender, EventArgs e)
        {
            InstancjaGry x = (InstancjaGry)sender;
            x.Zamykany -= Uk_Zamykany;
            for (int i = 0; i < gracze.Length; i++)
            {
                if (gracze[i].Equals(sender))
                {
                    gracze[i] = default(T);
                    ZwonioneMiejsce(i);
                    return;
                }
            }
        }
        
    }
    public interface InstancjaGry
    {
        event EventHandler Zamykany;
        Urzytkownik Gracz { get; set; }
    }
}