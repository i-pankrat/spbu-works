using Coding;

namespace Task
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("The program show the result of the task 4.8\nArithmetic coding is implemented through integer arithmetic.\n");
            var source = "abcbbbbbacabbacddacdbbaccbbadadaddd abcccccbacabbacbbaddbdaccbbddadadcc bcabbcdabacbbacbbddcbbaccbbdbdadaac";

            var coding = new ArithmeticCoding();
            var temp = new Dictionary<int, string>();
            string encodedMessage = coding.Encode(source);
            string decodedMessage = coding.Decode(encodedMessage, coding.Frequency);

            Console.WriteLine("Initial message: " + source);
            Console.WriteLine("Encoded message: " + encodedMessage);
            Console.WriteLine("Decoded message: " + decodedMessage);
            Console.WriteLine($"Result of bool function checking that strings are same: {source == decodedMessage}");
        }
    }

}
