using GraKarciana;
using KartyMono.Common.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartyMono.Common
{
    class BaseTable
    {
        List<KeyValuePair<List<Karta>, List<CardSocketUI>>> list = new List<KeyValuePair<List<Karta>, List<CardSocketUI>>>();

        public List<Karta> AddCardCollection(List<CardSocketUI> cd)
        {
            var tmp = cd.ToListCard();
            list.Add(new KeyValuePair<List<Karta>, List<CardSocketUI>>( tmp, cd));
            return tmp;
        }
        public void Execute(BaseTable date,Vector2 From,CardSocketUI empty)
        {
            ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>> com = new ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>>(X=>X.Key.Cast<byte>());
            var diff = com.Comparer(date.list, list);
            foreach (var item in diff)
            {
                ExecuteMove(item, From, empty);
            }
        }

        private void ExecuteMove(ComparerList<byte, KeyValuePair<List<Karta>, List<CardSocketUI>>>.Transition item, Vector2 from, CardSocketUI empty)
        {
        }
    }
}
