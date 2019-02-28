using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleHashCode2019.Model;

namespace GoogleHashCode2019.IO
{
    public class InputData
    {
        public IEnumerable<Photo> Photos { get; }
        public IEnumerable<Tag> Tags { get; }

        public InputData(IEnumerable<Photo> photos, IEnumerable<Tag> tags)
        {
            Photos = photos;
            Tags = tags;
        }
    }
}
