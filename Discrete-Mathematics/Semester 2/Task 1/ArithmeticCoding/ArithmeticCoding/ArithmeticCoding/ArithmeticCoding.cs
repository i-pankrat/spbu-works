namespace Coding
{
    public class ArithmeticCoding
    {
        private const int uppedBond = 1000000000;
        private const int roundBond = uppedBond / 10;
        private const int roundingAccuracy = 5;
        private const int len = 9; // number  of zeros in upperBond
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

            string result = String.Empty;
            int oldHigh = uppedBond;
            int oldLow = 0;
            
            for (int i = 0; i < source.Length + 1; i++)
            {
                string symbol = i == source.Length ? "<EOF>" : source[i].ToString();

                double[] range = GetRange(symbol);
                double lowRange = range[0];
                double highRange = range[1];

                int newHigh = (int)(oldLow + (oldHigh - oldLow) * highRange);
                int newLow = (int)(oldLow + (oldHigh - oldLow) * lowRange);
                newHigh--;

                while (newHigh / roundBond == newLow / roundBond)
                {
                    result += (newHigh / roundBond).ToString();
                    newHigh = newHigh % roundBond * 10 + 9;
                    newLow = newLow % roundBond * 10;
                }

                newHigh++;
                oldHigh = newHigh;
                oldLow = newLow;
            }

            result += oldLow.ToString();
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
            int code = Convert.ToInt32(source.Substring(0, len));
            source = source.Substring(len);

            int oldHigh = uppedBond;
            int oldLow = 0;
            string symbol = String.Empty;

            while (result.Length != counter - 1 && symbol !=  "<EOF>")
            {
                double index = Math.Round((double)(code - oldLow - 1) / (oldHigh - oldLow + 1), roundingAccuracy);
                symbol = GetSymbol(oldLow, oldHigh, index);
                result += symbol;

                double[] range = GetRange(symbol);
                double lowRange = range[0];
                double highRange = range[1];

                int newHigh = (int)(oldLow + (oldHigh - oldLow) * highRange);
                int newLow = (int)(oldLow + (oldHigh - oldLow) * lowRange);

                newHigh--;

                while (newHigh / roundBond == newLow / roundBond)
                {
                    newHigh = newHigh % roundBond * 10 + 9;
                    newLow = newLow % roundBond * 10;
                    code = code % roundBond * 10 + Convert.ToInt32(source.Substring(0, 1));
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

            result[0] = Math.Round(sum / counter, roundingAccuracy);
            result[1] = Math.Round((sum + Frequency[symbol]) / counter, roundingAccuracy);

            return result;
        }

        private string GetSymbol(int low, int high, double index)
        {
            double sum = 0;
            string result = string.Empty;

            foreach (var pair in Frequency)
            {
                double currentEnd = Math.Round((sum + pair.Value) / counter, roundingAccuracy);

                if (Math.Round(sum / counter, roundingAccuracy) <= index && index < currentEnd)
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