using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
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
        public IQ1000Game(int Tick, float percentCertainty)
        {
            this.Tick = Tick;
            minPositywe = (int)(percentCertainty * Tick);
        }
        public int CalculateBidAmount(ConclusionAboutGame game)
        {
            game.MoveContext = MoveContext1000.Action;
            var hist = donwloadHistogram(game);
            int Tmp;
            return (Tmp = getScore(hist)) < 100 ? 100 : Tmp;
        }
        public List<Karta> GetWorstCard(IEnumerable<Karta> AllCard, int Amount)
        {
            SortedDictionary<int, List<Karta>> retuned = new SortedDictionary<int, List<Karta>>();
            var List = AllCard.ToList();
            foreach (var item in GetAllCombinateList(List, Amount))
            {
                int Rate = RatingStateWorstCard(item.Item1);
                if (!retuned.ContainsKey(Rate))
                {
                    retuned.Add(Rate, item.Item2.ToList());
                }
            }
            return retuned.Last().Value;

        }

        private int RatingStateWorstCard(List<Karta> cards)
        {
            int Rating = 0;
            var table = ObsugaKart.GetAmountInColor(cards);
            foreach (var item2 in cards)
            {
                int intvalueCard = (int)item2.PobierzKarte();
                int ivcto2 = intvalueCard * intvalueCard;
                Rating += table[(int)item2.Kolor()] * ivcto2;
            }
            return Rating;
        }

        private IEnumerable<Tuple<List<Karta>, HashSet<Karta>>> GetAllCombinateList(List<Karta> list, int Amount)
        {
            foreach (var item in Matematyka.Wariancja.WarjancjaJakaśTam(Amount, list.Count))
            {
                HashSet<Karta> RandomedDontUse = new HashSet<Karta>(item.Select(X => list[X]));
                List<Karta> zw = new List<Karta>(list.Where(X => !RandomedDontUse.Contains(X)));
                yield return new Tuple<List<Karta>, HashSet<Karta>>(zw, RandomedDontUse);

            }
        }

        public Move1000 CalculateMove(ConclusionAboutGame game)
        {
            game.MoveContext = MoveContext1000.Game;
            var hist = donwloadHistogramFigure(game);
            int MaxIndex = hist.FindMaxIndex();
            Move1000 m = GetMove(MaxIndex);
            return m;
        }

        private static Move1000 GetMove(int MaxIndex)
        {
            Move1000 m = new Move1000();
            m.Marriage = 1 == (MaxIndex / 52);
            m.card = (Karta)(MaxIndex % 52);
            return m;
        }

        private int getScore(int[] hist)
        {
            int sum = 0;
            for (int i = 0; i < hist.Length; i++)
            {
                sum += hist[i];
                if (sum >= minPositywe)
                {
                    return i - MaxScore;
                }
            }
            throw new NotImplementedException("współczynik percentCertainty musi być wartością miedzy 0 a 1");
        }

        private int[] donwloadHistogram(ConclusionAboutGame game)
        {
            int[] hist = new int[MaxScore * 2];
            List<StateGame1000> state = GetState(game);
            var cmp = ComputeMove(state);

            foreach (var item in cmp)
            {
                int tmp;
                hist[MaxScore + (tmp = game.ReatingState(item.Item2))]++;
                System.Diagnostics.Debug.WriteLine($"wartość kalkulowana to {tmp}");
            }
            return hist;
        }
        private int[] donwloadHistogramFigure(ConclusionAboutGame game)
        {
            int[] hist = new int[104];//104 czyl dwie talie, miejsce na zapisanie meldunku
            List<StateGame1000> state = GetState(game);
            var cmp = ComputeMove(state);

            foreach (var item in cmp)
            {
                hist[(int)item.Item1.card + (item.Item1.Marriage ? 52 : 0)]++;
            }
            return hist;
        }

        private static List<Tuple<Move1000, StateGame1000>> ComputeMove(List<StateGame1000> state)
        {
            return state.AsParallel().Select(X => GetIqState(X)).ToList();
        }

        private List<StateGame1000> GetState(ConclusionAboutGame game)
        {
            return Enumerable.Range(0, Tick).Select(X => game.GetStates()).ToList();
        }

        public static Tuple<Move1000, StateGame1000> GetIqState(StateGame1000 game)
        {
            RelatingIq<StateGame1000, Move1000, int> iq = new RelatingIq<StateGame1000, Move1000, int>(30);
            var zw = iq.Run(game);
            return zw;
        }
    }
}
