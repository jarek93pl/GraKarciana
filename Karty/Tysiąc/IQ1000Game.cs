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
        const int MaxScore = 500;
        int Tick;
        readonly int minPositywe;
        public IQ1000Game(int Tick,float percentCertainty)
        {
            this.Tick = Tick;
            minPositywe =(int) (percentCertainty * Tick);
        }
        public int CalculateBidAmount(ConclusionAboutGame game)
        {
            var hist = donwloadHistogram(game);
            return getScore(hist);
        }
        public Move1000 CalculateMove(ConclusionAboutGame game)
        {
            var hist = donwloadHistogramFigure(game);
            int MaxIndex = hist.FindMaxIndex();
            Move1000 m = new Move1000();
            m.Marriage =1==(MaxIndex/ 52);
            m.card =(Karta) (MaxIndex % 52);
            return m;
        }

        private int getScore(int[] hist)
        {
            int sum= 0;
            for (int i = 0; i < hist.Length; i++)
            {
                sum += hist[i];
                if (sum>=minPositywe)
                {
                    return sum - MaxScore;
                }
            }
            throw new NotImplementedException("współczynik percentCertainty musi być wartością miedzy 0 a 1");
        }

        private int[] donwloadHistogram(ConclusionAboutGame game)
        {
            int[] hist = new int[MaxScore * 2];
            for (int i = 0; i < Tick; i++)
            {
                var state = GetIqState(game);
                int tmp;
                hist[MaxScore+(tmp= game.ReatingState(state.Item2))]++;
            }
            return hist;
        }
        private int[] donwloadHistogramFigure(ConclusionAboutGame game)
        {
            int[] hist = new int[104];//104 czyl dwie talie, miejsce na zapisanie meldunku
            for (int i = 0; i < Tick; i++)
            {
                var state = GetIqState(game);
                hist[(int) state.Item1.card+(state.Item1.Marriage?52:0)]++;
            }
            return hist;
        }
        public static Tuple<Move1000, StateGame1000> GetIqState(ConclusionAboutGame game)
        {
            var state = game.GetStates();
            RelatingIq<StateGame1000, Move1000, int> iq = new RelatingIq<StateGame1000, Move1000, int>(30,true,true);
            var zw= iq.Run(state);
            return zw;
        }
    }
}
