using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleHashCode2019.Model;

namespace GoogleHashCode2019.IO
{
    public class IO
    {
        private readonly TextReader _textReader;
        private readonly TextWriter _textWriter;

        public IO(TextReader textReader, TextWriter textWriter)
        {
            _textReader = textReader;
            _textWriter = textWriter;
        }

        public InputData Read()
        {
            var line = _textReader.ReadLine();
            int photosCount;
            int.TryParse(line, out photosCount );

            var photos = new List<Photo>();
            var tags = new HashSet<Tag>();

            for (int i = 0; i < photosCount; ++i)
            {
                line = _textReader.ReadLine();
                var items = line.Split();

                var orientation = items[0] == "H" ? Orientation.Horizontal : Orientation.Vertical;

                var currentTags = new List<Tag>();
                for (int j = 2; j < items.Length; ++j)
                {
                    var tag = new Tag(items[j]);
                    currentTags.Add(tag);
                    if (!tags.Contains(tag))
                        tags.Add(tag);
                }

                photos.Add(new Photo(i, orientation, currentTags));
            }

            return new InputData(photos, tags);
        }

        public void Write(IEnumerable<Slide> slides)
        {
            var list = slides.ToList();
            _textWriter.WriteLine(list.Count);
            foreach (var slide in list)
            {
                _textWriter.WriteLine(slide.ToString());
            }
            _textWriter.Flush();
            _textWriter.Close();
        }
    }
}
