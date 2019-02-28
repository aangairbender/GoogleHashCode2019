using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleHashCode2019.Model
{
    public class Tag : IEquatable<Tag>
    {
        private readonly int _id;

        public string Value { get; }

        public Tag(string value)
        {
            Value = value;
            _id = NextId();
        }

        public bool Equals(Tag other)
        {
            if (other == null)
                return false;
            return _id == other._id;
        }

        public override int GetHashCode()
        {
            return _id;
        }

        private static int _tagCounter = 0;

        private static int NextId()
        {
            return _tagCounter++;
        }
    }
}
