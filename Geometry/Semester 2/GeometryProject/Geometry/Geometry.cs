using System.Runtime.CompilerServices;
using MathNet.Spatial.Euclidean;

namespace Geometry
{
    public static class Geometry
    {
        private static bool IsOnOneLine(Point3D A, Point3D B, Point3D C)
        {
            var xCord = (C.X - A.X) / (B.X - A.X);
            var yCord = (C.Y - A.Y) / (B.X - A.Y);
            var zCord = (C.Z - A.Z) / (B.Z - A.Z);

            return xCord == yCord && yCord == zCord;
        }

        private static Vector3D FindSecondVector(Vector3D AB, Vector3D AE)
        {
            return AB.Length * AE.CrossProduct(AB).Normalize();
        }

        private static Vector3D FindNormalToPlaneABC(Vector3D AB, Vector3D AC)
        {
            return AB.Length * AB.CrossProduct(AC).Normalize();
        }

        public static List<Edge>? GetVertexsCube(Point3D A, Point3D B, Point3D C)
        {
            if (IsOnOneLine(A, B, C))
            {
                return null;
            }

            var AB = B - A;
            var AC = C - A;

            var AE = FindNormalToPlaneABC(AB, AC);
            var AD = FindSecondVector(AB, AE);

            var points = new Point3D[]
            {
                A, A + AB, A + AD + AB, A + AD, A + AE, A + AB + AE, A + AD + AB + AE, A + AD + AE 
            };

            return new List<Edge>
            {
                new Edge(PointProjection(points[0]), PointProjection(points[1])),
                new Edge(PointProjection(points[1]), PointProjection(points[2])),
                new Edge(PointProjection(points[2]), PointProjection(points[3])),
                new Edge(PointProjection(points[3]), PointProjection(points[0])),
                new Edge(PointProjection(points[0]), PointProjection(points[4])),
                new Edge(PointProjection(points[1]), PointProjection(points[5])),
                new Edge(PointProjection(points[2]), PointProjection(points[6])),
                new Edge(PointProjection(points[3]), PointProjection(points[7])),
                new Edge(PointProjection(points[4]), PointProjection(points[5])),
                new Edge(PointProjection(points[5]), PointProjection(points[6])),
                new Edge(PointProjection(points[6]), PointProjection(points[7])),
                new Edge(PointProjection(points[4]), PointProjection(points[7]))
            };
        }

        public static List<Edge>? GetVertexsPyramid(Point3D A, Point3D B, Point3D C)
        {
            if (IsOnOneLine(A, B, C))
            {
                return null;
            }

            var AB = B - A;
            var AC = C - A;

            var AE = FindNormalToPlaneABC(AB, AC);
            var AD = FindSecondVector(AB, AE);

            var a = A;
            var b = A + AB;
            var c = A + AB + AD;
            var d = A + AD;
            var s = a + (d - a) / 2 + (Math.Pow(2, 1/2) / 2) * AE;

            return new List<Edge>
            {
                new Edge(PointProjection(a), PointProjection(b)),
                new Edge(PointProjection(b), PointProjection(c)),
                new Edge(PointProjection(c), PointProjection(d)),
                new Edge(PointProjection(a), PointProjection(d)),
                new Edge(PointProjection(a), PointProjection(s)),
                new Edge(PointProjection(b), PointProjection(s)),
                new Edge(PointProjection(c), PointProjection(s)),
                new Edge(PointProjection(d), PointProjection(s))
            };
        }

        public static List<Edge>? GetVertexsTetrahedron(Point3D A, Point3D B, Point3D C)
        {
            if (IsOnOneLine(A, B, C))
            {
                return null;
            }

            var AB = B - A;
            var AC = C - A;

            var AE = FindNormalToPlaneABC(AB, AC);
            var AD = FindSecondVector(AB, AE);

            var a = A;
            var b = A + AB;
            var c = A + AB / 2 + (Math.Sqrt(3) / 2) * AD;
            var d = A + AB / 2 + (Math.Sqrt(3) / 6) * AD + Math.Pow(2 / 3, 1 / 2) * AE;

            return new List<Edge>
            {
                new Edge(PointProjection(a), PointProjection(b)),
                new Edge(PointProjection(b), PointProjection(c)),
                new Edge(PointProjection(c), PointProjection(a)),
                new Edge(PointProjection(a), PointProjection(d)),
                new Edge(PointProjection(b), PointProjection(d)),
                new Edge(PointProjection(c), PointProjection(d))
            };
        }

        private static Point2D PointProjection(Point3D coord)
        {
            return new Point2D(coord.X, coord.Y);
        }

        public static void PrintEdges(string Name, List<Edge> edges)
        {
            Console.WriteLine(Name);

            foreach (var edge in edges)
            {
                Console.WriteLine($"Edge: ({edge.FirstPoint.X}, {edge.FirstPoint.Y})-({edge.SecondPoint.X}, {edge.SecondPoint.Y})");
            }
        }
    }
}