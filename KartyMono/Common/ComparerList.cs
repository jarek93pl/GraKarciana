using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartyMono.Common
{
    public class ComparerList<T,L>  where T:IEquatable<T>
    {
        
        public class Transition
        {
            public L From;
            public L To;
            public T target;
        }
        public Func<L, IEnumerable<T>> convert;
        public ComparerList( Func<L, IEnumerable<T>> con)
        {
            convert = con;
        }
        public List<Transition> Comparer(IList<L> Base,IList<L> to)
        {
            if (Base.Count != to.Count)
            {
                throw new InvalidOperationException("długości list sie róźnią");
            }
            List<Transition> returned;
            Dictionary<T, L> help;
            GetDiffrent(Base, to, out returned, out help);
            GetTransaction(returned,help);
            return returned;
        }

        private void GetTransaction(List<Transition> returned, Dictionary<T, L> help)
        {
            foreach (var item in returned)
            {
                L zw;
                if (help.TryGetValue(item.target,out zw))
                {
                    item.From=zw;
                    help.Remove(item.target);
                }
            }
            foreach (var item in help)
            {
                returned.Add(new Transition() { From=item.Value,target=item.Key});
            }
        }

        private void GetDiffrent(IList<L> Base, IList<L> to, out List<Transition> returned, out Dictionary<T, L> help)
        {
            returned = new List<Transition>();
            help = new Dictionary<T, L>();
            for (int i = 0; i < Base.Count; i++)
            {
                L ThisBase = Base[i], ThisTo = to[i];
                HashSet<T> BastToHashet = new HashSet<T>(convert( ThisBase));
                HashSet<T> toToHashet = new HashSet<T>(convert( ThisTo));
                returned.AddRange(convert( ThisTo).Where(X => !BastToHashet.Contains(X)).Select(X => new Transition() {  To = ThisTo, target = X }));
                foreach (var item in convert( ThisBase).Where(X => !toToHashet.Contains(X)))
                {
                    help.Add(item, ThisBase);
                }
            }
        }
    }
}
