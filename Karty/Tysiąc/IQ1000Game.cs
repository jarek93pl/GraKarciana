using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karty;
using GraKarciana;
namespace Karty
{
    public class IQ1000Game
    {
        List<Karta> usedCards;
        int stepsForowad;
        public IQ1000Game(int stepsForowad,List<Karta> usedCards)
        {
            this.stepsForowad = stepsForowad;
            this.usedCards = usedCards;
        }
        public int CalculateBidAmount()
        {
            throw new NotImplementedException();
        }
    }
}
