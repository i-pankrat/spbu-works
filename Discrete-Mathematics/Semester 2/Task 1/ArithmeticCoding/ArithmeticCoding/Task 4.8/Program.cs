using Coding;

namespace task
{
    public static class Program
    {
        public static void Main()
        {
            var source = "abcbbbbbacabbacddacdbbaccbbadadaddd abcccccbacabbacbbaddbdaccbbddadadcc bcabbcdabacbbacbbddcbbaccbbdbdadaac";
            var test = new ArithmeticCoding();
            Dictionary<int, string> temp = new Dictionary<int, string>();
            string secondSource = test.Encode(source);
            string result = test.Decode(secondSource, test.Frequency);
            Console.WriteLine(source == result);
            Console.WriteLine("Initial: " + source);
            Console.WriteLine("End: " + result);
            Console.ReadLine();
        }
    }

}
