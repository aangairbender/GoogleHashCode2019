using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleHashCode2019.IO;
using GoogleHashCode2019.Model;

namespace GoogleHashCode2019.Solution.Grouper
{
    public class GrouperSolver : ISolver
    {
        int score(Photo a,Photo b)
        {
            int q = a.Tags.Count();
            int w = b.Tags.Count();
            int e = 0;
            foreach(var tg in a.Tags)
            {
                if (b.Tags.Contains(tg))
                    e++;
            }
            int ans = Math.Min(e, Math.Min(q - e, w - e));
            return ans;
        }
        public IEnumerable<Slide> Solve(InputData inputData)
        {
            LinkedList<Photo> line = new LinkedList<Photo>();
            foreach(var photo in inputData.Photos)
            {
                if (photo.Orientation == Orientation.Vertical)
                        continue;
               if(line.Count < 1)
                {
                    line.AddLast(photo);
                    continue;
                }else
                {
                    Photo q = line.First.Value;
                    Photo w = line.Last.Value;
                    if (score(q, photo) > score(w, photo))
                    {
                        line.AddFirst(photo);
                    }else
                    {
                        line.AddLast(photo);
                    }
                }
            }
            List<Slide> slides = new List<Slide>();
            foreach(var ph in line)
            {
                HorizontalSlide hs = new HorizontalSlide(ph);
                slides.Add(hs);

            }
            return slides;
            
        }
    }
}
