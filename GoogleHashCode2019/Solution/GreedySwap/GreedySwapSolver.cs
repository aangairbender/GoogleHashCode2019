using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleHashCode2019.IO;
using GoogleHashCode2019.Model;

namespace GoogleHashCode2019.Solution.GreedySwap
{
    public class GreedySwapSolver : ISolver
    {
        public static int SwapIterations = 300000;

        private readonly Random _rand = new Random();

        private int _curValue;

        public IEnumerable<Slide> Solve(InputData inputData)
        {
            var vertical = inputData.Photos.Where(p => p.Orientation == Orientation.Vertical);
            var slides = inputData.Photos
                .Where(p => p.Orientation == Orientation.Horizontal)
                .Select(p => new HorizontalSlide(p))
                .Select(p => p as Slide)
                .ToList();

            slides.AddRange(CreateSlidesFromVertical(vertical));

            var slidesAsArray = slides.ToArray();

            MakeShuffle(slidesAsArray);
            _curValue = CalcTotalValue(slidesAsArray);

            for (int i = 0; i < SwapIterations; ++i)
                TryMakeSwap(slidesAsArray);

            return slidesAsArray;
        }

        private IEnumerable<Slide> CreateSlidesFromVertical(IEnumerable<Photo> photos)
        {
            List<Slide> slides = new List<Slide>();
            var arr = photos.ToArray();
            for (int i = 0; i + 1 < arr.Length; i += 2)
            {
                slides.Add(new VerticalSlide(arr[i], arr[i + 1]));
            }

            return slides;
        }

        private void MakeShuffle(Slide[] slides)
        {
            for (int i = 0; i < slides.Length; ++i)
            {
                int j = _rand.Next(slides.Length);
                Swap(ref slides[i], ref slides[j]);
            }
        }

        private void TryMakeSwap(Slide[] slides)
        {
            int i = _rand.Next(slides.Length);
            int j = _rand.Next(slides.Length);
            if (Math.Abs(i - j) < 2)
                return;

            int delta =
                -CalcValueAround(slides, i - 1, i, i + 1)
                - CalcValueAround(slides, j - 1, j, j + 1)
                + CalcValueAround(slides, i - 1, j, i + 1)
                + CalcValueAround(slides, j - 1, i, j + 1);

            if (delta > 0)
            {
                Swap(ref slides[i], ref slides[j]);
                _curValue += delta;
            }
        }

        private int CalcValueAround(Slide[] slides, int a, int b, int c)
        {
            int val = 0;
            if (a >= 0)
                val += slides[a].CalculateInterest(slides[b]);
            if (c < slides.Length)
                val += slides[c].CalculateInterest(slides[b]);
            return val;
        }

        private void Swap<T>(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }

        private int CalcTotalValue(Slide[] slides)
        {
            int val = 0;
            for (int i = 0; i + 1 < slides.Length; ++i)
                val += slides[i].CalculateInterest(slides[i + 1]);
            return val;
        }
    }
}
