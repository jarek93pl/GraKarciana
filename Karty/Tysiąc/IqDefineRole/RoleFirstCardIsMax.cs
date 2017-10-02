using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;
using Karty;
namespace Karty.Tysiąc.IqDefineRole
{
    public class RoleFirstCardIsMax : IDefineRole
    {
        const int ValueIfCardsDontExist = -1;
        public bool IsEnded => true;

        public List<Karta> GetValidCards(List<Karta> ls)
        {
            int[] max = BasicTools.InitializeTable(4, ValueIfCardsDontExist);
            List<Karta> zw = new List<Karta>(ls.Count);
            GetMaxCards(max, ls);
            zw.AddRange(IntToCard(max));
            zw.AddRange(ObsugaTysiąc.GetAllQueenIfMariage(ls));
            return zw;

        }

        private IEnumerable<Karta> IntToCard(int[] max)
        {
            foreach (var item in max)
            {
                if (ValueIfCardsDontExist!=item)
                {
                    yield return (Karta)item;
                }
            }
        }

        private static void GetMaxCards(int[] max, List<Karta> zw)
        {
            for (int i = 0; i < zw.Count; i++)
            {
                Karta car = zw[i];
                BasicTools.SetMax(ref max[(int)car.Kolor()], (int)car);
            }
        }

        public bool IsContext(StateGame1000 s, ResultMoveGame mk) =>( s.NumberCardInTable==0);
    }
}
