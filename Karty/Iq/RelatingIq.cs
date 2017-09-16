using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty
{
    class RelatingIq<StateT,MoveT,PlayerT> where StateT:struct, IStateGame<StateT,PlayerT,MoveT>
    {
        
        int SteptsToforward;
        //Dictionary<StateT, Tuple<StateT, int>> Cashe = new Dictionary<StateT, Tuple<StateT, int>>();
        public RelatingIq(int steptsToforward)
        {
            SteptsToforward = steptsToforward;
        }
        public Tuple<MoveT,StateT> Run(StateT state)
        {
            var Best= GetBestState(state, out var z);
            return Best;
        }

        private Tuple<MoveT, StateT> GetBestState(StateT state,out bool Exist)
        {
            Exist = false;
            Tuple<MoveT, StateT> BestState = default(Tuple<MoveT, StateT>);
            int RatingBestState = int.MinValue;
            foreach (var item in state.GetStates())
            {
                Exist = true;
                var TmpState = GetState(item.Item2);
                int TmpRating = TmpState.RateStates(state.Player);
                BasicTools.Swap(ref BestState, item, ref RatingBestState, TmpRating);
            }
            return BestState;
        }

        public StateT GetState(StateT state,int Index=0)
        {
            if (Index>SteptsToforward)
            {
                return state;
            }
            var Best = GetBestState(state,out bool Find);
            if (Find)
            {
                return Best.Item2;

            }
            {
                return state;
            }
        }

    }
}
