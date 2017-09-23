using Karty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
     struct TikTacToe : IStateGame<TikTacToe, int, int>
    {
        const int LenghtTable = 9;
        const int LenghtVin = 3;
        //012
        //345
        //678
        static int[][] TableViners =new int[][] {
            new int[] { 0, 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 6, 7, 8 } ,
            new int[] { 0, 3, 6 }, new int[] { 1, 4, 7 }, new int[] { 2, 5, 8 }
            ,new int[] { 0, 4, 8 }, new int[] { 2, 4, 6 }

        };
        
        public static TikTacToe GetClear()
        {
            TikTacToe tikTac=new TikTacToe();
            tikTac.Table = new int[9];
            tikTac.PlayerIndex = 1;
            return tikTac;
        }
        public int PlayerIndex;
        public int[] Table;
        public int Player => PlayerIndex;
        public int Winer()
        {
            var tmp = this;
            foreach (var item in TableViners)
            {
                int[] Histogram = new int[3];
                foreach (var item2 in item)
                {
                    Histogram[tmp.Table[item2]]++;
                }

                for (int i = 0; i < Histogram.Length; i++)
                {
                    if (Histogram[i] == LenghtVin)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }
        public int CompareTo(TikTacToe other)
        {
            return RateStates(PlayerIndex) - other.RateStates(PlayerIndex);
        }

        public bool Equals(TikTacToe other)
        {
            if (other.Player!=Player)
            {
                return false;
            }
            TikTacToe z = this;
            for (int i = 0; i < LenghtTable; i++)
            {
                if (other.Table[i]!=z.Table[i])
                {
                    return false;
                }
            }
            return true;
        }
        public string Text => ToString();

        public bool GameOn => Winer() == 0;

        public override int GetHashCode()
        {
            return 0;
        }
        public override bool Equals(object obj)
        {
           return Equals((TikTacToe)obj);
        }
        public List<Tuple<int, TikTacToe>> GetStates()
        {
            
            List<Tuple<int, TikTacToe>> zw = new List<Tuple<int, TikTacToe>>();
            if (GameOn)
            {
                int Enemy = PlayerIndex == 1 ? 2 : 1;
                TikTacToe t = this;
                for (int i = 0; i < 9; i++)
                {
                    if (t.Table[i] == 0)
                    {
                        TikTacToe curent = Copy();
                        curent.Table[i] = Player;
                        curent.PlayerIndex = Enemy;
                        zw.Add(new Tuple<int, TikTacToe>(i, curent));
                    }
                }
            }
            return zw;
            
        }

        public int RateStates(int p)
        {
            int Result = Winer();
            if (Result==0)
            {
                return 0;
            }
            else
            {
                if (Result==p)
                {
                    return 1;
                }
                {
                    return -1;
                }
            }
            
        }
        public TikTacToe Copy()
        {
            TikTacToe z = (TikTacToe)MemberwiseClone();
            z.Table = (int[])Table.Clone();
            return z;
        }
        public override string ToString()
        {
            string zw = "";
            TikTacToe tmp = this;
            for (int i = 0; i < LenghtTable; i++)
            {
                if (i % 3 == 0)
                {
                    zw += ";";
                }
                zw += tmp.Table[i];
            }
            return zw;
        }
    }
}
