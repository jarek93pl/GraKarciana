using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using GraKarciana;
namespace Karty
{
    public class ConclusionAbouttUserBehavior
    {
        readonly int PlayerInGame;
        public int AmountCards;
        public List<Karta> UserCards;
        public Karta[] TheMostFigureInColor = new Karta[4];
        public Karta transferred;
        bool usedTransferedCard;
        public ConclusionAbouttUserBehavior(int PlayerInGame)
        {
            this.PlayerInGame = PlayerInGame;
            for (int i = 0; i < TheMostFigureInColor.Length; i++)
            {
                TheMostFigureInColor[i] = Karta.As + 4;
            }
        }
        public void ConclusionAboutBehavior(IList<Karta> Table,bool enebleAtute,Karta atute)
        {
            AmountCards--;
            ConclusionAboutTransferedCard(Table);
            ConclusionAboutMaxCards(Table,enebleAtute,atute);

        }

        private void ConclusionAboutMaxCards(IList<Karta> table, bool enebleAtute, Karta atute)
        {
            if (table.Count>1&&!ObsugaTysiąc.LastWin(table,enebleAtute,atute, out bool usingAtute, out bool usingSuit))
            {
                Karta last = table.Last();
                Karta first = table.First();
                if (!usingSuit)
                {
                    TheMostFigureInColor[(int)last.Kolor()] = Karta.trelf;//trefl to wartość 0
                    if (enebleAtute)
                    {
                        TheMostFigureInColor[(int)atute.Kolor()] = MaxAtutInTable(table,atute);
                    }
                }
                else
                {
                    TheMostFigureInColor[(int)first.Kolor()] = first;
                }

            }
        }

        private Karta MaxAtutInTable(IList<Karta> table, Karta atute) => (Karta)table.Where(X => X.Kolor() == atute).DefaultIfEmpty(Karta.trelf).Max(X => (int)X);

        private void ConclusionAboutTransferedCard(IList<Karta> Table)
        {
            if (Table.Any(X => X == transferred))
            {
                usedTransferedCard = true;
            }
        }
        public void TransferCards(Karta karta)
        {
            transferred = karta;
            usedTransferedCard = false;
        }
        internal bool ValidateCard(Karta karta) => TheMostFigureInColor[(int)karta.Kolor()] > karta;
        /// <summary>
        /// te losowanie nie zwraca uwagi na ilość kart ponieważ one jest uwzglednianie w 
        /// klasie conclusion about game 
        /// 
        /// </summary>
        /// <param name="dontRandCards"></param>
        public void RandomCards(List<Karta> dontRandCards, MoveContext1000 stateGame,bool WinAction)
        {
            UserCards.Clear();
            switch (PlayerInGame)
            {
                case 2:
                    RandomCards2Cards(dontRandCards,stateGame);
                    break;
                case 3:
                    RandomCards3Cards(dontRandCards, stateGame,WinAction);
                    break;
                case 4:
                    RandomCards4Cards(dontRandCards, stateGame);
                    break;
                default:
                    break;
                    throw new InvalidOperationException("w tysiąca można grać od 2 do 4 graczy");
            }
        }

        protected virtual void RandomCards4Cards(List<Karta> dontRandCards, MoveContext1000 stateGame)
        {
            throw new NotImplementedException();
        }

        protected virtual void RandomCards2Cards(List<Karta> dontRandCards, MoveContext1000 stateGame)
        {
            throw new NotImplementedException();
        }

        protected virtual void RandomCards3Cards(List<Karta> dontRandCards, MoveContext1000 stateGame,bool WinAction)
        {
            switch (stateGame)
            {
                case MoveContext1000.Action:
                    RandomCards3CardsAction(dontRandCards);
                    break;
                case MoveContext1000.ChoseCards:
                    RandomCards3CardsChoseCards(dontRandCards);
                    break;
                case MoveContext1000.Game:
                    RandomCards3CardsGame(dontRandCards,WinAction);
                    break;
                default:
                    break;
            }
        }

        private void RandomCards3CardsAction(List<Karta> dontRandCards)
        {
            UserCards.AddRange(dontRandCards.RandAndDelete(8));
        }

        private void RandomCards3CardsChoseCards(List<Karta> dontRandCards)
        {
            UserCards.Add(transferred);
            UserCards.AddRange(dontRandCards.RandAndDelete(7));
        }

        private void RandomCards3CardsGame(List<Karta> dontRandCards,bool WinAction)
        {
            if (usedTransferedCard||!WinAction)
            {
                UserCards.AddRange(dontRandCards.RandAndDelete(AmountCards));
            }
            else
            {
                UserCards.Add(transferred);
                UserCards.AddRange(dontRandCards.RandAndDelete(AmountCards-1));
            }
        }
    }
}
