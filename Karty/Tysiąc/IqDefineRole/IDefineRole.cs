using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;
namespace Karty
{
    public interface IDefineRole
    {
        bool IsContext(StateGame1000 s, ResultMoveGame mk);
        List<Karta> GetValidCards(List<Karta> ls, StateGame1000 s);
        bool IsEnded { get; }
    }
    
}
