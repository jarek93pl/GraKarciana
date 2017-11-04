using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komputer.Przeszukiwacz
{
    public class WyszykiwarkaEnum<T, L> : IEnumerator<Wyszukiwarka<T, L>.Wynik>, IEnumerable<Wyszukiwarka<T, L>.Wynik>
    {
        public WyszykiwarkaEnum(int IndexZmiany, Wyszukiwarka<T, L>.Odniesienie korzeńWstepny)
        {
            this.IndexZmiany=IndexZmiany;
            this.KorzeńWstepny = korzeńWstepny;
            this.KorzeńPrzeglądany = korzeńWstepny;
            Stos.Push(0);
        }
        Wyszukiwarka<T, L>.Odniesienie KorzeńWstepny;


        Stack<int> Stos = new Stack<int>();
        Wyszukiwarka<T, L>.Odniesienie KorzeńPrzeglądany;
        public Wyszukiwarka<T, L> Przeszókiwany { get; set; }
        readonly  int IndexZmiany;
        

        public Wyszukiwarka<T, L>.Wynik Current
        {
            get 
            {
                return Wyszukiwarka<T, L>.WyodrebnikWynik(KorzeńPrzeglądany);
            }
        }

        public void Dispose()
        {
            KorzeńWstepny = null;
            Przeszókiwany = null;
        }


        public bool MoveNext()
        {
            bool NBlokada = false;
            while (true)
            {
                for (; Stos.Peek() < KorzeńPrzeglądany.Odniesienia.Count;)
                {
                    if (NBlokada && KorzeńPrzeglądany.Adres.Count > 0)
                    {
                        return true;
                    }
                    NBlokada = true;
                    KorzeńPrzeglądany = KorzeńPrzeglądany.Odniesienia[Stos.Peek()];
                    Stos.Zmień(1);
                    Stos.Push(0);
                    
                }
                if (NBlokada && KorzeńPrzeglądany.Adres.Count > 0)
                {
                    return true;
                }
                Stos.Pop();
                KorzeńPrzeglądany = KorzeńPrzeglądany.Poprzednie;
                if (Stos.Count==0)
                {
                    return false;
                }
            }
            throw new NotImplementedException("błąd");
        }
        public void Reset()
        {
            Stos.Clear();
            Stos.Push(0);
        }

        object System.Collections.IEnumerator.Current
        {
            get { return (object)Current; }
        }


        public IEnumerator<Wyszukiwarka<T, L>.Wynik> GetEnumerator()
        {
            return this;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
    public static class DodatniStos
    {
        public static void Zmień(this Stack<int> l, int Zmiana)
        {
            l.Push(l.Pop() + Zmiana);
        }
    }
    
}
