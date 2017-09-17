#define IndexRe
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty
{
    public class RelatingIq<StateT,MoveT,PlayerT> where StateT:struct, IStateGame<StateT,PlayerT,MoveT>
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

#if IndexRe
        public static int IndexRecurent = 0;
#endif
        private Tuple<MoveT, StateT> GetBestState(StateT state,out bool Exist,int index=0)
        {
#if IndexRe
            IndexRecurent++;
            int CurentIndexRecurent = IndexRecurent;
#endif
            Exist = false;
            Tuple<MoveT, StateT> BestState = default(Tuple<MoveT, StateT>);
            int RatingBestState = int.MinValue;
            List<Tuple<MoveT, StateT>> tst;
            foreach (var item in tst= state.GetStates())
            {
                Exist = true;
                var TmpState = item.Item2.GameOn? GetState(item,index+1):item;
                int TmpRating = TmpState.Item2.RateStates(state.Player);
                BasicTools.Swap(ref BestState,new Tuple<MoveT, StateT>(item.Item1 ,TmpState.Item2), ref RatingBestState, TmpRating);
            }
            return BestState;
        }

        public Tuple<MoveT, StateT> GetState(Tuple<MoveT, StateT> state,int Index)
        {
            if (Index>SteptsToforward)
            {
                return state;
            }
            var Best = GetBestState(state.Item2,out bool Find);
            if (Find)
            {
                return Best;

            }
            {
                return state;
            }
        }

    }
}
