using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngTextToNum.Utils
{
    public class WordNumberRepresentation
    {
        public long Value { get; set; }

        public int Level { get; set; }

        public WordNumberRepresentation(long value,int level)
        {
            Value = value;
            Level = level;
        }
    }
}
