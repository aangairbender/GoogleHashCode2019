using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoogleHashCode2019.Model
{
    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    public class Photo
    {
        public int Id { get; }
        public Orientation Orientation { get; }
        public IEnumerable<Tag> Tags { get; }

        public Photo(int id, Orientation orientation, IEnumerable<Tag> tags)
        {
            Id = id;
            Orientation = orientation;
            Tags = tags;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
