using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode2019.Model
{
    public class VerticalSlide : Slide
    {
        public IReadOnlyCollection<Photo> Photos { get; }

        public VerticalSlide(Photo firstPhoto, Photo secondPhoto)
        {
            Photos = new HashSet<Photo>
            {
                firstPhoto,
                secondPhoto
            };
        }

        public override string ToString()
        {
            return string.Join(" ", Photos.Select(p => p.ToString()));
        }

        public override IEnumerable<Tag> Tags => Photos.SelectMany(p => p.Tags, (photo, tag) => tag).Distinct();
    }
}