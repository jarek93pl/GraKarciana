using GraKarciana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Karty
{
    public class StateGame1000 :  IStateGame<StateGame1000, int, Move1000>
    {
        public Karta Kozera;
        public bool EnebleKozera;
        public int[] scoreInCurentGame;
        public List<Karta>[] card;
        public PlayerGame1000[] players;//nie musi być tworzona głeboka kopia

        Lazy<bool> LazyGameOne;
        Lazy<long[]> LazyTableToCheckEquality;
        readonly int amountPlayer;
        public StateGame1000(int Index):this()
        {
            amountPlayer = Index;
            scoreInCurentGame = new int[amountPlayer];
            card = new List<Karta>[amountPlayer];
            for (int i = 0; i < amountPlayer; i++)
            {
                card[i] = new List<Karta>();
                players[i] = new PlayerGame1000();
            }
        }
        
        public StateGame1000()
        {
            Initialize();
        }


        public StateGame1000 DeapCopy()
        {
            StateGame1000 zw = (StateGame1000)MemberwiseClone();
            zw.Initialize();
            zw.scoreInCurentGame = (int[])zw.scoreInCurentGame.Clone();
            zw.card = (List<Karta>[])zw.card.Clone();
            for (int i = 0; i < amountPlayer; i++)
            {
                zw.card[i] = zw.card[i].Select(X => X).ToList();
            }
            return zw;
        }
        public int Player { get; set; }
        public bool GameOn => LazyGameOne.Value;

        public bool Equals(StateGame1000 other)
        {
            return LazyTableToCheckEquality.Value.SequenceEqual(other.LazyTableToCheckEquality.Value);
        }

        public override bool Equals(object obj)
        {
            return Equals((StateGame1000)obj);
        }
        public List<Tuple<Move1000, StateGame1000>> GetStates()
        {
            List<Tuple<Move1000, StateGame1000>> zw = new List<Tuple<Move1000, StateGame1000>>();
            if (GameOn)
            {
                return zw;
            }
            int Enemy = (Player + 1) % amountPlayer;
            for (int i = 0; i < card[Player].Count; i++)
            {
                var Added = DeapCopy();
                Added.Player = Enemy;
                Added.card[Player].RemoveAt(i);
                
                Move1000 m
                zw.Add( new Tuple<Move1000, StateGame1000>());
            }
            return zw;
        }

        public int RateStates(int p)
        {
            int returned = 0;
            for (int i = 0; i < amountPlayer; i++)
            {
                returned += players[i].ExpectedResult(scoreInCurentGame[i]) * p == i ? 1 : -1;
            }
            return returned;
        }
        private bool gameOnLazyM()
        {
            for (int i = 0; i < amountPlayer; i++)
            {
                if (card[i].Count!=0)
                {
                    return true;
                }
            }
            return false;
        }

        private void Initialize()
        {
            LazyTableToCheckEquality = new Lazy<long[]>(DetermineComareArrey);
            LazyGameOne = new Lazy<bool>(gameOnLazyM);
        }
        int HashValue = 0;
        const int CardsInOneLong = 10;
        const int OfsetCard = 6;
        private long[] DetermineComareArrey()
        {
            long[] zw = GetArrey();
            DonwloadElseDate(zw);
            DonwloadHash(zw);
            return zw;
        }

        private void DonwloadElseDate(long[] zw)
        {
            long date = 0;
            date += (int)Kozera;//6 bit
            date += (EnebleKozera?1:0)<<7;//1 bit
            date += Player << 8;//4 bit na wszeli wypadek
            for (int i = 0; i < amountPlayer; i++)
            {
                date +=scoreInCurentGame[i]<< (amountPlayer * 10 + 12);
            }
        }

        private long[] GetArrey()
        {
            int AmountCard = card.Sum(X => X.Count);
            int LenghtLongArrey = AmountCard / CardsInOneLong + 2;
            // jeden it jest dodany z powodu zaokrągleń a drugi na resze danych
            long[] zw = new long[LenghtLongArrey];
            List<Karta> list = new List<Karta>();
            card.Forech(X => list.AddRange(X.OrderBy(Y => (int)Y)));
            int NrLong = 0;
            AmountCard--;
            while (true)
            {
                checked
                {
                    for (int i = 0; i < CardsInOneLong; i++)
                    {
                        zw[NrLong] += ((int)zw[AmountCard--]) << OfsetCard * i;
                        if (AmountCard == 0)
                        {
                            return zw;
                        }
                    }
                }
                NrLong++;
            }
            
        }

        private void DonwloadHash(long[] zw)
        {
            foreach (var item in zw)
            {
                HashValue ^= item.GetHashCode();
            }
        }

        public override int GetHashCode()
        {
            var tmp= LazyTableToCheckEquality.Value;
            return HashValue;
        }
    }
}
