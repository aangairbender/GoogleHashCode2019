namespace GoogleHashCode2019.Model
{
    public class HorizontalSlide : Slide
    {
        public Photo Photo { get; }

        public HorizontalSlide(Photo photo)
        {
            Photo = photo;
        }

        public override string ToString()
        {
            return Photo.ToString();
        }
    }
}