namespace Coding
{
    public class ArithmeticCoding
    {
        public Dictionary<string, int> Frequency { get; private set; }
        private int counter;

        public ArithmeticCoding()
        {
            Frequency = new Dictionary<string, int>();
            counter = 0;
        }

        public string Encode(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                string symbol = source[i].ToString();

                if (!Frequency.ContainsKey(symbol))
                {
                    Frequency.Add(symbol, 0);
                }

                counter++;
                Frequency[symbol]++;
            }

            Frequency.Add("<EOF>", 1);
            counter++;
            Frequency = Frequency.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            /*Frequency = new Dictionary<string, int>();
            Frequency.Add("<EOF>", 1);
            Frequency.Add("_", 1);
            Frequency.Add("M", 1);
            Frequency.Add("I", 2);
            Frequency.Add("W", 1);
            Frequency.Add("S", 5);
            counter = 11;*/

            // NewHigh = OldLow + (OldHigh-OldLow)*HighRange(X),

            // NewLow = OldLow + (OldHigh - OldLow) * LowRange(X),

            // Code = (Code-LowRange(X))/(HighRange(X)-LowRange(X))


            string result = String.Empty;
            int oldHigh = 1000000000;
            int oldLow = 0;
            
            for (int i = 0; i < source.Length + 1; i++)
            {
                string symbol = i == source.Length ? "<EOF>" : source[i].ToString();
                double[] range = GetRange(symbol);
                double lowRange = range[0];
                double highRange = range[1];

                int tempNewHigh = (int)(oldLow + (oldHigh - oldLow) * highRange);
                int newHigh = tempNewHigh - 1;
                int tempNewLow = (int)(oldLow + (oldHigh - oldLow) * lowRange);
                int newLow = tempNewLow;

                while (newHigh / 100000000 == newLow / 100000000)
                {
                    result += (newHigh / 100000000).ToString();
                    newHigh = newHigh % 100000000 * 10 + 9;
                    newLow = newLow % 100000000 * 10;
                }

                newHigh++;
                oldHigh = newHigh;
                oldLow = newLow;
            }

            result += oldLow.ToString();
            /*
            byte[] bytes = Encoding.ASCII.GetBytes(result);
            result = String.Empty;

            foreach (var element in bytes)
            {
                string temp = Convert.ToString(element, 2);

                while (temp.Length < 8)
                {
                    temp = "0" + temp;
                }

                result +=  temp;
            }
            */
            return result;
        }

        public string Decode(string source, Dictionary<string, int> frequency)
        {
            Frequency = frequency;
            counter = 0;

            foreach (var pair in Frequency)
            {
                counter += pair.Value;
            }

            string result = String.Empty;
            /*byte[] symbolCodes = new byte[source.Length / 8];

            for (int i = 0; i < source.Length; i += 8)
            {
                string temp = source.Substring(i, 8);
                symbolCodes[i / 8] = Convert.ToByte(temp, 2);
            }

            source = Encoding.ASCII.GetString(symbolCodes);*/
            int code = Convert.ToInt32(source.Substring(0, 9));
            source = source.Substring(9);

            // Code = ((Code-LowRange) * 10 - 1)/(HighRange-LowRange + 1)

            int oldHigh = 1000000000;
            int oldLow = 0;
            string symbol = String.Empty;

            while (result.Length != counter - 1)
            {

                if (result.Length == counter - 2)
                {
                    Console.WriteLine("I'm here!");
                }

                double index = ((double)(code - oldLow)) / (oldHigh - oldLow);
                symbol = GetSymbol(oldLow, oldHigh, index);

                if (symbol == "<EOF>")
                {
                    break;
                }
                

                result += symbol;
                double[] range = GetRange(symbol);

                double lowRange = range[0];
                double highRange = range[1];
                int newHigh = (int)(oldLow + (oldHigh - oldLow) * highRange);
                int newLow = (int)(oldLow + (oldHigh - oldLow) * lowRange);

                
                newHigh--;

                while (newHigh / 100000000 == newLow / 100000000)
                {
                    newHigh = newHigh % 100000000 * 10 + 9;
                    newLow = newLow % 100000000 * 10;
                    code = code % 100000000 * 10 + Convert.ToInt32(source.Substring(0, 1));
                    source = source.Substring(1);
                }

                newHigh++;

                oldHigh = newHigh;
                oldLow = newLow;
            }


            return result;
        }
        private double[] GetRange(string symbol)
        {
            double sum = 0;
            double[] result = new double[2];

            foreach (var pair in Frequency)
            {
                if (!pair.Key.Equals(symbol))
                {
                    sum += pair.Value;
                }
                else
                {
                    break;
                }
            }

            result[0] = sum / counter;
            result[1] = (sum + Frequency[symbol]) / counter;

            return result;
        }
        // index 0.45370369684405149 end 0.45370370370370372
        private string GetSymbol(int low, int high, double index)
        {
            double sum = 0;
            string result = string.Empty;

            foreach (var pair in Frequency)
            {
                double currentEnd = (sum + pair.Value) / counter;
                // int newHigh = (int)(low + (high - low) * currentEnd);
                // int newLow = (int)(low + (high - low) * sum / counter);

                // newHigh > index && newLow < index
                double temp = (index - low) / (high - low);
                if (sum / counter <= index && index < currentEnd)
                {
                    result = pair.Key;
                    break;
                }

                sum += pair.Value;
            }

            return result;
        }
    }
}