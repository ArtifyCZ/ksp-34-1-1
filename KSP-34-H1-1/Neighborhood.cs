using System;
using System.Collections.Generic;
using System.Text;

namespace KSP_34_H1_1
{
    public class Neighborhood
    {
        public Point A { get; }

        public Point B { get; }

        public ulong Weight { get; }

        public Neighborhood(Point a, Point b, ulong weight)
        {
            this.A = a;
            this.B = b;
            this.Weight = weight;
        }

        public Point GetTheSecondPoint(Point first) => first == A ? B : A;
    }
}
