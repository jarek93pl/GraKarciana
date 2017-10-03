#define performance
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
        const int sizeCashe = 100000;
        bool UsingCashe;
        bool IgnoreLevel;
        int SteptsToforward;
        public RelatingIq(int steptsToforward,bool UsingCashe=false,bool ignoreLevel=false)
        {
#if performance
            ExecutionInLevel = new int[steptsToforward+2];
#endif
            IgnoreLevel = ignoreLevel;
            this.UsingCashe = UsingCashe;
            SteptsToforward = steptsToforward;
        }
        public Tuple<MoveT,StateT> Run(StateT state)
        {
            var Best= GetBestState(state, out var z);
            return Best;
        }
        #region ToDiagnostic
#if IndexRe
        public static int IndexRecurent = 0;
#endif
#if performance
         public int UsedCashCount = 0;
        int[] ExecutionInLevel;
#endif
#endregion
        private Tuple<MoveT, StateT> GetBestState(StateT state,out bool Exist,int index=0)
        {
            #region ToDiagnostic
#if IndexRe
            IndexRecurent++;
            int CurentIndexRecurent = IndexRecurent;
#endif
#if performance
            ExecutionInLevel[index]++;
#endif
            #endregion
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
        Dictionary<StateT, CasheResult> Cashe = new Dictionary<StateT, CasheResult>(sizeCashe);
        struct CasheResult
        {
            public int level;
            public Tuple<MoveT, StateT> tuple;
            public static explicit operator Tuple<MoveT, StateT>(CasheResult c)=>c.tuple;


        }
        public Tuple<MoveT, StateT> GetState(Tuple<MoveT, StateT> state,int Index)
        {
            if (Index > SteptsToforward)
            {
                return state;
            }
            if (UsingCashe)
            {
                return GetCasheValue(state, Index);
            }
            else
            {
                return DontUsingCashe(state,Index);
            }
        
        }

        private Tuple<MoveT, StateT> DontUsingCashe(Tuple<MoveT, StateT> state,int Index)
        {
            var Best = GetBestState(state.Item2, out bool Find, Index);
            if (Find)
            {
                return Best;
            }
            else
            {
                return state;
            }
        }

        private Tuple<MoveT, StateT> GetCasheValue(Tuple<MoveT, StateT> state, int Index)
        {
            CasheResult casheRow;
            if (Cashe.ContainsKey(state.Item2) && ((casheRow = Cashe[state.Item2]).level <= Index||IgnoreLevel))
            {
#if performance
                UsedCashCount++;
#endif
                return (Tuple<MoveT, StateT>)casheRow;
            }
            var Best = GetBestState(state.Item2, out bool Find, Index);
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
