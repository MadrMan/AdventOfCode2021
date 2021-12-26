// See https://aka.ms/new-console-template for more information

namespace Day3 // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static bool IsBitPositive(string[] bin, int index)
        {
            int positive = 0;

            foreach (string b in bin)
            {
                if (b[index] == '1')
                {
                    positive++;
                }
            }

            if (positive * 2 >= bin.Length)
            {
                return true;
            }

            return false;
        }

        public static int Part1(string[] bin)
        {
            bool[] positive = new bool[bin[0].Length];

            for (int n = 0; n < positive.Length; n++)
            {
                positive[n] = IsBitPositive(bin, n);
            }

            int gamma = 0;
            int epsilon = 0;

            foreach (bool p in positive)
            {
                gamma <<= 1;
                epsilon <<= 1;

                if (p)
                {
                    gamma++;
                }
                else
                {
                    epsilon++;
                }
            }

            Console.WriteLine($"Gamma: {gamma}");
            Console.WriteLine($"Epsilon: {epsilon}");
            Console.WriteLine($"Power consumption: {gamma * epsilon}");

            return gamma * epsilon;
        }

        public static int Part2(string[] bin)
        {
            string[] oxygen = bin.ToArray();
            int index = 0;

            // Eliminate
            while (oxygen.Length > 1)
            {
                char c = IsBitPositive(oxygen, index) ? '1' : '0';
                oxygen = oxygen.Where(b => b[index] == c).ToArray();
                index = (index + 1) % bin[0].Length;
            }

            string[] co2 = bin.ToArray();
            index = 0;

            while (co2.Length > 1)
            {
                char c = IsBitPositive(co2, index) ? '0' : '1';
                var newco2 = co2.Where(b => b[index] == c).ToArray();
                if (newco2.Length > 0) co2 = newco2;
                index = (index + 1) % bin[0].Length;
            }

            int oxygenLevel = Convert.ToInt32(oxygen[0], 2);
            int co2Level = Convert.ToInt32(co2[0], 2);

            Console.WriteLine($"Oxygen: {oxygenLevel}");
            Console.WriteLine($"Co2: {co2Level}");
            Console.WriteLine($"Power consumption: {oxygenLevel * co2Level}");

            return oxygenLevel * co2Level;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string[] bin = File.ReadAllLines("input3.txt");

            Part1(bin);
            Part2(bin);
        }
    }
}