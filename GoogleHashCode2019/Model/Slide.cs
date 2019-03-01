using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleHashCode2019.Model
{
    public abstract class Slide
    {
        public abstract IEnumerable<Tag> Tags { get; }

        public int CalculateInterest(Slide neighborSlide)
        {
            var tags1 = Tags.ToList();
            var tags2 = neighborSlide.Tags.ToList();

            var intersection = tags1.Intersect(tags2).ToList();

            var s1 = tags1.Except(intersection);
            var s3 = tags2.Except(intersection);

            return Math.Min(
                intersection.Count,
                Math.Min(s1.Count(), s3.Count()));
        }
    }
}
