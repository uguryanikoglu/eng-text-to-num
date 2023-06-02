using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngTextToNum.Utils
{
    public class Constants
    {
        public static readonly Dictionary<string, WordNumberRepresentation> NumberWords = new()
        {
            {"zero", new(0,1)},
            {"one", new(1,1)},
            {"two", new(2,1)},
            {"three", new(3,1)},
            {"four", new(4,1)},
            {"five", new(5,1)},
            {"six", new(6,1)},
            {"seven", new(7,1)},
            {"eight", new(8,1)},
            {"nine", new(9,1)},
            {"ten", new(10,2)},
            {"eleven", new(11,2)},
            {"twelve", new(12,2)},
            {"thirteen", new(13,2)},
            {"fourteen", new(14,2)},
            {"fifteen", new(15,2)},
            {"sixteen", new(16,2)},
            {"seventeen", new(17,2)},
            {"eighteen", new(18,2)},
            {"nineteen", new(19,2)},
            {"twenty", new(20,2)},
            {"thirty", new(30,2)},
            {"forty", new(40,2)},
            {"fifty", new(50,2)},
            {"sixty", new(60,2)},
            {"seventy", new(70,2)},
            {"eighty", new(80,2)},
            {"ninety", new(90,2)},
            {"hundred", new(100,3)},
            {"thousand", new(1000,4)},
            {"million", new(1000000,5)},
            {"billion", new(1000000000,6)},
            {"trillion", new(1000000000000,7)}
        };

        public static readonly List<string> PointRepresentatives = new() { "and", "point" };

        public static readonly List<string> NumberSeperators = new() { " ", "-" };

        public static readonly List<long> PointFinisherPreNumbers = new() { 10, 100 };
    }
}
