using System.Linq;
using System.Collections.Generic;
using GoogleHashCode2019.IO;
using GoogleHashCode2019.Model;

namespace GoogleHashCode2019.Solution.Same_unique
{
    public class Same_unique : ISolver
    {
        private int _deep = 0;
        private readonly ICollection<Tag> _usedTags = new List<Tag>();

        public IEnumerable<Slide> Solve(InputData inputData)
        {
            var slides = new List<Slide>();
            var verticals = inputData.Photos.Where(photo => photo.Orientation == Orientation.Vertical);
            for (int i = 0; i < verticals.Count(); i+=2)
            {
                if (i + 1 < verticals.Count())
                    slides.Add(new VerticalSlide(verticals.ElementAt(i), verticals.ElementAt(i + 1)));
            }
            inputData.Photos.Where(photo => photo.Orientation == Orientation.Horizontal).ToList().ForEach(photo =>
                slides.Add(new HorizontalSlide(photo)));
            return GroupSlides(slides);
        }

        private IEnumerable<Slide> GroupSlides(IEnumerable<Slide> slides)
        {
            if (slides.Count() < 3)
                return slides;
            _deep++;
            var tagMap = new Dictionary<Tag, int>();
            slides.ToList().ForEach(slide =>
            {
                switch (slide)
                {
                    case HorizontalSlide horizontalSlide:
                        ExtractTags(tagMap, horizontalSlide.Photo);
                    break;
                    case VerticalSlide verticalSlide:
                        verticalSlide.Photos.ToList().ForEach(photo =>
                            ExtractTags(tagMap, photo));
                    break;
                }
            });
            var maxTag = tagMap.Values.Max();
            var sameTag = tagMap.First(pair => pair.Value == maxTag).Key;
            _usedTags.Add(sameTag);
            var leftGroup = slides.Where(slide => ExtractTags(slide).Contains(sameTag));
            var result = RearrangeSlides(leftGroup).Concat(RearrangeSlides(slides.Except(leftGroup)));
            _deep--;
            _usedTags.Remove(sameTag);
            return result;
        }

        private IEnumerable<Slide> RearrangeSlides(IEnumerable<Slide> slides)
        {
            if (slides.Count() < 3)
                return slides;
            var tagMap = new Dictionary<Tag, int>();
            var slidesList = slides.ToList();
            slidesList.ForEach(slide =>
            {
                switch (slide)
                {
                    case HorizontalSlide horizontalSlide:
                        ExtractTags(tagMap, horizontalSlide.Photo);
                        break;
                    case VerticalSlide verticalSlide:
                        verticalSlide.Photos.ToList().ForEach(photo =>
                            ExtractTags(tagMap, photo));
                        break;
                }
            });
            var minTag = tagMap.Values.Min();
            var uniqueTag = tagMap.First(pair => pair.Value == minTag).Key;
            var uniqueSlides = slidesList.Where(slide => ExtractTags(slide).Contains(uniqueTag));
            int divider = slidesList.Count() / (uniqueSlides.Count() + 1);
            int position = 1;
            uniqueSlides.ToList().ForEach(slide =>
            {
                var tempSlide = slidesList[position * divider];
                var index = slidesList.IndexOf(slide);
                slidesList[position * divider] = slide;
                slidesList[index] = tempSlide;
            });
            var result = new List<Slide>();
            for (int i = 0; i < uniqueSlides.Count() + 1; i++)
            {
                result.AddRange(GroupSlides(slidesList.Skip(i * divider).Take(divider)));
            }
            return result;
        }

        private IEnumerable<Tag> ExtractTags(Slide slide)
        {
            switch (slide)
            {
                case HorizontalSlide horizontalSlide:
                    return horizontalSlide.Photo.Tags;
                case VerticalSlide verticalSlide:
                    return verticalSlide.Photos.First().Tags.Concat(verticalSlide.Photos.Last().Tags);
                default:
                    return new List<Tag>();
            }
        }

        private void ExtractTags(Dictionary<Tag, int> tagMap, Photo photo)
        {
            photo.Tags.ToList().ForEach(tag =>
            {
                if (_usedTags.Contains(tag))
                    return;
                if (!tagMap.ContainsKey(tag))
                    tagMap.Add(tag, 1);
                else
                    tagMap[tag]++;
            });
        }        
    }
}
