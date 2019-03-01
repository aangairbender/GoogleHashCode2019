using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleHashCode2019.Model
{
    public class Tag : IEquatable<Tag>
    {
        public string Value { get; }

        public Tag(string value)
        {
            Value = value;
        }

        public bool Equals(Tag other)
        {
            if (other == null)
                return false;
            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
