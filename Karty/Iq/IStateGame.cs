using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty
{
    public interface IStateGame< T,PlayerG,Move>: IEquatable<T> where T:IStateGame<T, PlayerG, Move> 
    {
        /// <summary>
        /// w Wielu grach nie może być zwracane gdy jedna ze stron wygrała
        /// </summary>
        /// <returns></returns>
        List<Tuple<Move,T>> GetStates();
        int RateStates(PlayerG p);
        PlayerG Player { get; }
        bool GameOn { get; }
    }
}
