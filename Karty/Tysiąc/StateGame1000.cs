using GraKarciana;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Karty
{
    public class StateGame1000 :  IStateGame<StateGame1000, int, Move1000>
    {
        int HashValue = 0;
        public Karta Kozera;
        public bool EnebleKozera;
        public int[] scoreInCurentGame;
        public int NumberCardInTable = 0;
        public Karta[] cardOnTable;
        public List<Karta>[] cards;
        public PlayerGame1000[] players;//nie musi być tworzona głeboka kopia

        Lazy<bool> LazyGameOne;
        Lazy<long[]> LazyTableToCheckEquality;
        readonly int amountPlayer;
        public StateGame1000(int amountPlayer):this()
        {
            this.amountPlayer = amountPlayer;
            scoreInCurentGame = new int[this.amountPlayer];
            cardOnTable = new Karta[this.amountPlayer];
            cards = new List<Karta>[this.amountPlayer];
            players=new PlayerGame1000[3];
            for (int i = 0; i < this.amountPlayer; i++)
            {
                cards[i] = new List<Karta>();
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
            zw.cards = (List<Karta>[])zw.cards.Clone();
            zw.cardOnTable = (Karta[])zw.cardOnTable.Clone();
            for (int i = 0; i < amountPlayer; i++)
            {
                zw.cards[i] = zw.cards[i].Select(X => X).ToList();
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
        public StateGame1000 SetTable(IEnumerable<Karta> kartas)
        {
            var zw = DeapCopy();
            int tmp = 0;
            foreach (var item in kartas)
            {
                zw.cardOnTable[tmp++] = item;
            }
            zw.NumberCardInTable = kartas.Count();
            return zw;
        }
        public List<Tuple<Move1000, StateGame1000>> GetStates()
        {
            List<Tuple<Move1000, StateGame1000>> zw = new List<Tuple<Move1000, StateGame1000>>();
            if (!GameOn)
            {
                return zw;
            }
            List<Karta> availableCard = ObsugaTysiąc.ZaładujDostepneKarty(cards[Player], cardOnTable.Take(NumberCardInTable).ToList(), EnebleKozera, Kozera);
            for (int i = 0; i < availableCard.Count; i++)
            {
                zw.Add( GetMove(availableCard[i]));
            }
            return zw;
        }

        public Tuple<Move1000, StateGame1000> GetMove(Karta card)
        {
            StateGame1000 state = DeapCopy();
            Move1000 move= state.LoadMove(card);
            return new Tuple<Move1000, StateGame1000>(move,state);

        }

        public override int GetHashCode()
        {
            var tmp = LazyTableToCheckEquality.Value;
            return HashValue;
        }
        public int RateStates(int p)
        {
            int returned = 0;
            for (int i = 0; i < amountPlayer; i++)
            {
                returned += players[i].ExpectedResult(scoreInCurentGame[i]) *( p == i ? (players.Length-1) : -1);
            }
            return returned;
        }
        private bool gameOnLazyM()
        {
            for (int i = 0; i < amountPlayer; i++)
            {
                if (cards[i].Count!=0)
                {
                    return true;
                }
            }
            return false;
        }

        private Move1000 LoadMove(Karta card)
        {
            cards[Player].Remove(card);
            bool wontMarriage = true;// później będzie można dodać decyzje
            //czy inteligencja chce kozery
            bool marriage = false;
            if (ObsugaTysiąc.IstniejeMeldunek(card, cards[Player])&&wontMarriage)
            {
                marriage = true;
                scoreInCurentGame[Player] += ObsugaTysiąc.WartościMeldunków(card);
            }
            cardOnTable[NumberCardInTable++] = card;
            if (NumberCardInTable == amountPlayer)
            {
                int IndexWiner= ObsugaTysiąc.FindWinner(EnebleKozera, Kozera, Player, cardOnTable);
                scoreInCurentGame[IndexWiner] = ObsugaTysiąc.ScoreInTable(cardOnTable);
                NumberCardInTable = 0;
                Player = IndexWiner;
            }
            return new Move1000() { card = card, Marriage = marriage };
        }
        private void Initialize()
        {
            LazyTableToCheckEquality = new Lazy<long[]>(DetermineComareArrey);
            LazyGameOne = new Lazy<bool>(gameOnLazyM);
        }
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
            zw[zw.Length - 1] = date;
        }

        private long[] GetArrey()
        {
            int AmountCard = cards.Sum(X => X.Count);
            int LenghtLongArrey = AmountCard / CardsInOneLong + 2;
            // jeden it jest dodany z powodu zaokrągleń a drugi na resze danych
            long[] zw = new long[LenghtLongArrey];
            List<Karta> list = new List<Karta>();

            //operacje związane ze stołem
            cards.Forech(X => list.AddRange(X.OrderBy(Y => (int)Y)));
            AmountCard += NumberCardInTable;

             
            list.AddRange(GetCardInTable());
            int NrLong = 0;
            while (true)
            {
                checked
                {
                    for (int i = 0; i < CardsInOneLong; i++)
                    {
                        zw[NrLong] += ((int)list[--AmountCard]) << OfsetCard * i;
                        if (AmountCard == 0)
                        {
                            return zw;
                        }
                    }
                }
                NrLong++;
            }
            
        }

        private IEnumerable<Karta> GetCardInTable()
        {
            for (int i = 0; i < NumberCardInTable; i++)
            {
                yield return cardOnTable[i];
            }
        }

        private void DonwloadHash(long[] zw)
        {
            foreach (var item in zw)
            {
                HashValue ^= item.GetHashCode();
            }
        }

    }
}
