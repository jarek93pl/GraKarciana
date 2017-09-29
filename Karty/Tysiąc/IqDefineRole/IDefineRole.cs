using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;
namespace Karty
{
    interface IDefineRole
    {
        bool IsContext(StateGame1000 s);
        List<Karta> GetValidCards(List<StateGame1000> ls);
    }
}
