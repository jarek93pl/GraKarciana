using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty
{
    public interface IStateGame<T,PlayerG>: IEquatable<T>, IComparable<T> where T:IStateGame<T, PlayerG>
    {

    }
}
