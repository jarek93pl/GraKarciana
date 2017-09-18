using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karty
{
    public class PlayerGame1000
    {
        public int DeclareScore = 0;
        public int Score = 0;
        public int ExpectedResult(int earnedScore)
        {
            if (DeclareScore==0)
            {
                return (earnedScore/10)*10;
            }
            else
            {
                if (earnedScore<DeclareScore)
                {
                    return -DeclareScore;
                }
                else
                {
                    return DeclareScore;
                }
            }
        }
    }
}
