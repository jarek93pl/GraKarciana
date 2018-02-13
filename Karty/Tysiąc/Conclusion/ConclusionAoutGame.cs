using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;
namespace Karty
{
    public enum MoveContext1000 { Action,ChoseCards,Game};
    public class ConclusionAboutGame
    {
        public int WhoMove;
        public MoveContext1000 MoveContext;
        List<Karta> AvilibeCards;
        const int maxSwap = 10;
        const int maxRandom = 10;
        static readonly Swaper<ConclusionAbouttUserBehavior, Karta> swaper = new Swaper<ConclusionAbouttUserBehavior, Karta>(X => X.UserCards, (X, Y) => X.ValidateCard(Y), maxSwap, maxRandom);
        readonly int playerIndex;
        public List<ConclusionAbouttUserBehavior> PlayerConclusion = new List<ConclusionAbouttUserBehavior>();
        public ConclusionAboutGame(int AmountPlayers,int PlaeyrNumber,IEnumerable<GraKarciana.Karta> cardsPlayer)
        {
            playerIndex = PlaeyrNumber;
            AvilibeCards = ObsugaKart.WylousjMałąTalie();
            AvilibeCards.RemoveAll(cardsPlayer);
            for (int i = 0; i < AmountPlayers; i++)
            {
                ConclusionAbouttUserBehavior conclusion;
                if (PlaeyrNumber==i)
                {
                    conclusion = new PlayerConclusion(AmountPlayers, cardsPlayer);
                }
                else
                {

                    conclusion = new ConclusionAbouttUserBehavior(AmountPlayers);
                }
                PlayerConclusion.Add(conclusion);
            }
        }
        public PlayerConclusion PlayerObjectConclusion =>(PlayerConclusion) PlayerConclusion.First(X => X is PlayerConclusion);
        public bool WinAction;
        public void Active(bool WinAction)
        {
            this.WinAction = WinAction;
            if (PlayerConclusion.Count==3)
            {
                foreach (var item in PlayerConclusion)
                {
                    if (item is ConclusionAbouttUserBehavior ca)
                    {
                        ca.AmountCards =WinAction?7: 8;
                    }
                }
            }
        }

        internal int RelateReating(StateGame1000 state)
        {
            return state.RateStates(playerIndex);
        }
        public void TransferedCard(Karta karta,int numberPlayer)
        {
            if (PlayerConclusion[numberPlayer] is ConclusionAbouttUserBehavior c)
            {
                AvilibeCards.Remove(karta);
                c.TransferCards(karta);
            }
            if (PlayerConclusion[numberPlayer] is PlayerConclusion cd)
            {
                throw new InvalidOperationException("nie można przeksazać karty samemu sobie");
            }
        }

        internal int ReatingState(StateGame1000 item2) => item2.ScorePlayer(playerIndex);

        public ConclusionAboutGame(List<ConclusionAbouttUserBehavior> z)
        {
            PlayerConclusion = z;
        }
    
        public StateGame1000 GetStates()
        {
            StateGame1000 state = new StateGame1000(PlayerConclusion.Count);
            BindConclusionWitchState(state);
            RandCards();
            state.Player = GetUserWhoMove();
            return state;
        }

        private int GetUserWhoMove()
        {
            switch (MoveContext)
            {
                case MoveContext1000.Action:
                case MoveContext1000.ChoseCards:
                    return playerIndex;
                case MoveContext1000.Game:
                    return WhoMove;
                default:
                    throw new NotImplementedException();
            }
        }

        private void RandCards()
        {
            List<Karta> dontRandomCards = AvilibeCards.Select(X => X).ToList();
            PlayerConclusion.Forech(X => X.RandomCards(dontRandomCards, MoveContext, WinAction));
            swaper.Run(PlayerConclusion);
        }

        private void BindConclusionWitchState(StateGame1000 state)
        {
            for (int i = 0; i < state.cardOnTable.Length; i++)
            {
                PlayerConclusion[i].UserCards = state.cards[i];
            }
        }
    }
}
