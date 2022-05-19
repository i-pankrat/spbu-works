using MathNet.Spatial.Euclidean;

namespace MyNamespace
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Input 3 points");

            var input = Console.ReadLine().Trim().Split(" ");
            var A = new Point3D(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]), Convert.ToInt32(input[2]));

            input = Console.ReadLine().Trim().Split(" ");
            var B = new Point3D(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]), Convert.ToInt32(input[2]));

            input = Console.ReadLine().Trim().Split(" ");
            var C = new Point3D(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]), Convert.ToInt32(input[2]));

            if (IsOnOneLine(A, B, C))
            {
                Console.WriteLine("GG");
                return;
            }

            var AB = B - A;
            var AC = C - A;

            var AD = FindSecondVector(AB, AC, A, B, C);
            var AE = FindNormalToPlaneABC(AB, AD);

            var cube = GetVertexsCube(A, AB, AD, AE);
            var pyramid = GetVertexsPyramid(A, AB, AD, AE);
            var tetrahedron = GetVertexsTetrahedron(A, AB, AD, AE);

            PrintCoords("Cube", ProjectionOnXoY(cube));
            PrintCoords("Pyramid", ProjectionOnXoY(pyramid));
            PrintCoords("Tetrahedron", ProjectionOnXoY(tetrahedron));
        }

        
    }
}