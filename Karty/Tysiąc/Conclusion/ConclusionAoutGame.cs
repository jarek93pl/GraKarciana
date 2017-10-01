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

        internal int RatingState(StateGame1000 state)
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
        public ConclusionAboutGame(List<ConclusionAbouttUserBehavior> z)
        {
            PlayerConclusion = z;
        }
    
        public StateGame1000 GetStates()
        {
            StateGame1000 state = new StateGame1000(PlayerConclusion.Count);
            BindConclusionWitchState(state);
            RandCards();
            return state;
        }

        private void RandCards()
        {
            List<Karta> dontRandomCards = AvilibeCards.Select(X => X).ToList();
            PlayerConclusion.Forech(X => X.RandomCards(dontRandomCards, MoveContext));
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
