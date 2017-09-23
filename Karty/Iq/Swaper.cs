using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty
{
    public class Swaper<T,K>
    {
        const int RotAttempt= 100;
        private int MaxRandom;
        private int MaxSwap;
        private Func<T, IList<K>> Tranform;
        private Func<T,K, bool> Validate;

        public Swaper( Func<T, IList<K>> Tranform,Func<T,K,bool> Validate,int MaxRandom,int MaxSwap)
        {
            this.MaxSwap = MaxSwap;
            this.MaxRandom = MaxRandom;
            this.Tranform = Tranform;
            this.Validate = Validate;
        }
        public void Run(List<T> ValidationColection)
        {
            MultiList<T, K> multiList = new MultiList<T, K>(Tranform, ValidationColection);
            for (int i = 0; i < multiList.Count; i++)
            {
                if (!ValidIn(multiList,i,i))
                {
                    MultiSwape(i, multiList);
                }
            }
        }

        private void MultiSwape(int First, MultiList<T, K> multiList)
        {
            int[] ArreySwap = new int[MaxSwap];
            ArreySwap[0] = First;
            for (int i = 0; i < RotAttempt; i++)
            {
                if (MultiSwapeRecurence(ArreySwap, 1, multiList))
                {
                    return;
                }
            }
            throw new TimeoutException($"dont find corect state int {RotAttempt}");

        }
        private bool MultiSwapeRecurence(int[] level,int LevelCount, MultiList<T, K> multiList)
        {
            if (++LevelCount >= level.Length)
            {
                return false;
            }
            for (int i = 0; i < MaxRandom; i++)
            {
                int TrySwapIndex = Rand(multiList, level, LevelCount);
                level[LevelCount - 1] = TrySwapIndex;

                if (ValidNext(level, LevelCount, multiList))
                {
                    if (ValidInFirst(level, LevelCount, multiList))
                    {
                        SetDate(multiList, level, LevelCount);
                        return true;
                    }
                    else
                    {
                        if (MultiSwapeRecurence(level, LevelCount, multiList))
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }

        private bool ValidInFirst(int[] level, int LevelCount, MultiList<T, K> multiList)
        {
            return ValidIn(multiList, level[LevelCount - 1], level[0]);
        }

        private bool ValidNext(int[] level, int LevelCount, MultiList<T, K> multiList)
        {
            return ValidIn(multiList, level[LevelCount - 2], level[LevelCount - 1]);
        }

        private void SetDate(MultiList<T, K> multiList, int[] level, int levelCount)
        {
            K tmp = default(K), z = multiList[ level[0]];
            for (int i = 0; i < levelCount-1; i++)
            {
                tmp = multiList[level[i+1]];
                multiList[level[i + 1]] =z;
                z = tmp;
            }
            multiList[level[0]] = tmp;
        }

        private static int Rand(MultiList<T, K> multiList, int[] level, int LevelCount)
        {
            HashSet<int> t = new HashSet<int>(level.Skip(1).Take(LevelCount));
            while (true)
            {
                int tmp= GraKarciana.RandCards.Random.Next(multiList.Count);
                if (!t.Contains(tmp))
                {
                    return tmp;
                }
            }
        }
        
        private bool ValidIn(MultiList<T, K> multiList, int Index, int CheckIndex)
        {
            var listElement = multiList[Index];
            var ListParent = multiList.GetParrent(CheckIndex);
            return Validate(ListParent, listElement);
        }
    }
}
