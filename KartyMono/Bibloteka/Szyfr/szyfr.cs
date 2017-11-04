using System;
using System.Collections.Generic;
using System.Text;

namespace Komputer.Szyfr
{
    public class Szyfr
    {
        private int Numer = 0;
        private string Text = "";
        private void Kwadrat()
        {
            int I = 0;
            for (; (I * I) < Text.Length; I++)
                ;
            int p = 0;
            char[,] Tablica = new char[I, I];
            for (int i = 0; i < I; i++)
            {
                for (int i2 = 0; i2 < I; i2++)
                {
                    if (p < Text.Length)
                        Tablica[i2, i] = this.Text[p];
                    else
                        Tablica[i2, i] = ' ';
                    p++;
                   // Console.Write(Tablica[i2, i] + " ");
                }
                //Console.WriteLine();
            }

            string zwracana = "";
            for (int i = 0; i < I; i++)
            {
                for (int i2 = 0; i2 < I; i2++)
                {
                    zwracana += Tablica[i, i2];
                }
                this.Text = zwracana;
            }
        }
        public string Dokument
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        public void Dodaj(string text)
        {
            this.Text += (text + "#");
        }
        public string Wez()
        {
            string Zwracana = "";
            for (; Text[Numer] != '#'; Numer++)
            {
                Zwracana += Text[Numer];
            }
            Numer++;
            return Zwracana;
        }
        public char WezZnak()
        {
            char Byte = ' ';
            if (Numer < Text.Length)
                Byte = Text[Numer];
            Numer++;
            return Byte;
        }
        public void ZacznijOdPoczątku()
        {
            Numer = 0;
        }
        public int ZacznijOd
        {
            set { Numer = value; }
        }
        public void SzyfrujIOdszyfruj()
        {
            Kwadrat();
        }
    }
}
