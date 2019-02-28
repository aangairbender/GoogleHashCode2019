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
        public static int SwapIterations = 100;

        private readonly Random _rand = new Random();

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

            for (int i = 0; i < SwapIterations; ++i)
                TryMakeSwap(slidesAsArray);

            return slidesAsArray;
        }

        private IEnumerable<Slide> CreateSlidesFromVertical(IEnumerable<Photo> photos)
        {
            List<Slide> slides = new List<Slide>();
            var arr = photos.ToArray();
            for (int i = 0; i + 1< arr.Length; ++i)
            {
                slides.Add(new VerticalSlide(arr[i], arr[i + 1]));
            }

            return slides;
        }

        private void TryMakeSwap(Slide[] slides)
        {
            int i = _rand.Next(slides.Length);
            int j = _rand.Next(slides.Length);

            int cur = CalcTotalValue(slides);

            Swap(ref slides[i], ref slides[j]);

            int after = CalcTotalValue(slides);

            if (after < cur)
            {
                Swap(ref slides[i], ref slides[j]);
            }
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
