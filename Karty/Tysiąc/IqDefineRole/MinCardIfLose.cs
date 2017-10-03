using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;
using Karty;

namespace Karty.Tysiąc.IqDefineRole
{
    class RoleMinCardIfLose : IDefineRole
    {
        const int ValueIfCardsDontExist =int.MaxValue;
        public bool IsEnded => false;

        public List<Karta> GetValidCards(List<Karta> ls, StateGame1000 s)
        {
            int[] max = BasicTools.InitializeTable(4, ValueIfCardsDontExist);
            List<Karta> zw = new List<Karta>(ls.Count);
            GetMinCards(max, ls);
            zw.AddRange(IntToCard(max));
            return zw;

        }
        private IEnumerable<Karta> IntToCard(int[] max)
        {
            foreach (var item in max)
            {
                if (ValueIfCardsDontExist != item)
                {
                    yield return (Karta)item;
                }
            }
        }
        private static void GetMinCards(int[] max, List<Karta> zw)
        {
            for (int i = 0; i < zw.Count; i++)
            {
                Karta car = zw[i];
                BasicTools.SetMin(ref max[(int)car.Kolor()], (int)car);
            }
        }
        public bool IsContext(StateGame1000 s, ResultMoveGame mk) => ((s.NumberCardInTable != 0)&&mk==ResultMoveGame.Lose||(mk== ResultMoveGame.Win&&(s.NumberCardInTable+1==s.amountPlayer)));
    }
}
