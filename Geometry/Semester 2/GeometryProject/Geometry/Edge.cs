using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace Geometry
{
    public class Edge
    {
        public Point2D FirstPoint { get; private set; }
        public Point2D SecondPoint { get; private set; }

        public Edge(Point2D first, Point2D second)
        {
            FirstPoint = first;
            SecondPoint = second;
        }
    }
}
