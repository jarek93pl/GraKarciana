using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty
{
    public interface IStateGame<T,PlayerG,Move>: IEquatable<T>, IComparable<T> where T: struct,IStateGame<T, PlayerG, Move> 
    {
        IEnumerable<Tuple<Move,T>> GetStates();
        int RateStates(PlayerG p);
        PlayerG Player { get; }
    }
}
