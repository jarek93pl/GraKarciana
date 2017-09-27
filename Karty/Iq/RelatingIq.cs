//#define IndexRe
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty
{
    public class RelatingIq<StateT,MoveT,PlayerT> where StateT: IStateGame<StateT,PlayerT,MoveT>
    {
        
        int SteptsToforward;
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
                BasicTools.SetMax(ref BestState,new Tuple<MoveT, StateT>(item.Item1 ,TmpState.Item2), ref RatingBestState, TmpRating);
            }
            return BestState;
        }
        Dictionary<StateT, CasheResult> Cashe = new Dictionary<StateT, CasheResult>();
        struct CasheResult
        {
            public int level;
            public Tuple<MoveT, StateT> tuple;
            public static explicit operator Tuple<MoveT, StateT>(CasheResult c)=>c.tuple;


        }
        public Tuple<MoveT, StateT> GetState(Tuple<MoveT, StateT> state,int Index)
        {
            if (Index>SteptsToforward)
            {
                return state;
            }
            CasheResult casheRow;
            if (Cashe.ContainsKey(state.Item2)&& (casheRow=Cashe[state.Item2]).level<=Index)
            {
                return (Tuple<MoveT, StateT>)casheRow;
            }
            var Best = GetBestState(state.Item2,out bool Find);
            if (Find)
            {
                if (Cashe.ContainsKey(state.Item2))
                {
                    Cashe.Remove(state.Item2);//nie tszeba sprawdzać bo miedzy wcze
                }
                Cashe.Add(state.Item2, new CasheResult() { level = Index, tuple = Best });
                
                return Best;

            }
            else
            {
                Cashe.Add(state.Item2, new CasheResult() { level = Index, tuple = state });
                return state;
            }
        }

    }
}
