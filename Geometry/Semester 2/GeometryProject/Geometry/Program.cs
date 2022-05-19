using MathNet.Spatial.Euclidean;

namespace Geometry
{
    public static class Program
    {
        public static void Main()
        {
            string[]? input;

            while (true)
            {
                Console.WriteLine("Enter 3 points\n");

                input = Console.ReadLine().Trim().Split(" ");
                if (input[0] == "exit") break;
                var A = new Point3D(int.Parse(input[0]), int.Parse(input[1]), int.Parse(input[2]));

                input = Console.ReadLine().Trim().Split(" ");
                if (input[0] == "exit") break;
                var B = new Point3D(int.Parse(input[0]), int.Parse(input[1]), int.Parse(input[2]));

                input = Console.ReadLine().Trim().Split(" ");
                if (input[0] == "exit") break;
                var C = new Point3D(int.Parse(input[0]), int.Parse(input[1]), int.Parse(input[2]));

                var cube = Geometry.GetVertexsCube(A, B, C);

                Console.Write("\n");

                if (cube != null)
                {
                    Geometry.PrintEdges("Cube", cube);
                }
                else
                {
                    Console.WriteLine("Points are on one line");
                }

                Console.Write("\n");

                var pyramid = Geometry.GetVertexsPyramid(A, B, C);

                if (pyramid != null)
                {
                    Geometry.PrintEdges("Pyramid", pyramid);
                }
                else
                {
                    Console.WriteLine("Points are on one line");
                }

                Console.Write("\n");

                var tetrahedron = Geometry.GetVertexsTetrahedron(A, B, C);

                if (tetrahedron != null)
                {
                    Geometry.PrintEdges("Tetrahedron", tetrahedron);
                }
                else
                {
                    Console.WriteLine("Points are on one line");
                }

                Console.Write("\n");
            }
        }
    }
}
