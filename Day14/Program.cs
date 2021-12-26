// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Numerics;

namespace Day14 // Note: actual namespace depends on the project name.
{
    class PolymerSpinner
    {
        struct InsertionResult
        {
            public int PairA;
            public int PairB;
            public int MoleculeIndex;
        }

        InsertionResult[] InsertionRules = new InsertionResult[0];
        public ulong[] Pairs = new ulong[0];
        public ulong[] MoleculeCount = new ulong[0];
        public Dictionary<char, int> MoleculeLookup = new();
        public Dictionary<int, char> MoleculeInverseLookup = new();


        public PolymerSpinner(string[] bin)
        {
            // Translate polymer into pairs
            string polymer = bin[0];
            for (int x = 0; x < polymer.Length - 1; x++)
            {
                int moleculeA = PerformLookup(polymer[x]);
                int moleculeB = PerformLookup(polymer[x + 1]);
                Pairs[GetInsertionIndex(moleculeA, moleculeB)]++;
            }

            // Translate rules into fixed table
            for (int x = 2; x < bin.Length; x++)
            {
                var parts = bin[x].Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
                string combination = parts[0];
                string result = parts[1];

                int moleculeA = PerformLookup(combination[0]);
                int moleculeB = PerformLookup(combination[1]);
                int moleculeC = PerformLookup(result[0]);

                InsertionRules[GetInsertionIndex(moleculeA, moleculeB)] = new InsertionResult()
                {
                    PairA = GetInsertionIndex(moleculeA, moleculeC),
                    PairB = GetInsertionIndex(moleculeC, moleculeB),
                    MoleculeIndex = BitOperations.TrailingZeroCount(moleculeC)
                };
            }

            foreach (char m in polymer)
            {
                // Shove all molecules into the counter
                MoleculeCount[BitOperations.TrailingZeroCount(PerformLookup(m))]++;
            }
        }

        int GetInsertionIndex(int moleculeA, int moleculeB)
        {
            // Return simple OR, and +1 if flipped
            return (moleculeA | moleculeB) * 2 + ((moleculeB > moleculeA) ? 1 : 0);
        }

        int PerformLookup(char c)
        {
            if (!MoleculeLookup.TryGetValue(c, out int i))
            {
                i = 1 << MoleculeLookup.Count;
                MoleculeLookup[c] = i;
                MoleculeInverseLookup[i] = c;

                // Resize if needed
                int requiredInsertionLength = 2 << MoleculeLookup.Count;
                if (InsertionRules.Length < requiredInsertionLength)
                {
                    InsertionResult[] newInsertionRules = new InsertionResult[requiredInsertionLength * 4];
                    InsertionRules.CopyTo(newInsertionRules, 0);
                    InsertionRules = newInsertionRules;

                    ulong[] newPairs = new ulong[newInsertionRules.Length];
                    Pairs.CopyTo(newPairs, 0);
                    Pairs = newPairs;

                    ulong[] newMoleculeCount = new ulong[MoleculeLookup.Count];
                    MoleculeCount.CopyTo(newMoleculeCount, 0);
                    MoleculeCount = newMoleculeCount;
                }
            }

            return i;
        }

        public void Spin()
        {
            ulong[] newPairs = new ulong[Pairs.Length];

            // Re-solve every combination
            foreach (var moleculeA in MoleculeLookup)
            {
                foreach (var moleculeB in MoleculeLookup)
                {
                    // One pair turns into two new pairs
                    // Since we do a full iteration over all molecules, we hit both AB and BA, so just do one here
                    int indexLesser = GetInsertionIndex(moleculeA.Value, moleculeB.Value);
                    InsertionResult combinationLesser = InsertionRules[indexLesser];
                    ulong lesserPairs = Pairs[indexLesser];
                    newPairs[combinationLesser.PairA] += lesserPairs;
                    newPairs[combinationLesser.PairB] += lesserPairs;
                    MoleculeCount[combinationLesser.MoleculeIndex] += lesserPairs;
                }
            }

            Pairs = newPairs;
        }

        public ulong GetAnswers()
        {
            KeyValuePair<char, ulong> least = new KeyValuePair<char, ulong>('?', ulong.MaxValue);
            KeyValuePair<char, ulong> most = new KeyValuePair<char, ulong>('?', ulong.MinValue);

            foreach (var molecule in MoleculeLookup)
            {
                ulong c = MoleculeCount[BitOperations.TrailingZeroCount(molecule.Value)];
                Console.WriteLine($"Molecule {molecule.Key} was found {c} times");

                if (c > most.Value)
                {
                    most = new KeyValuePair<char, ulong>(molecule.Key, c);
                }

                if (c < least.Value)
                {
                    least = new KeyValuePair<char, ulong>(molecule.Key, c);
                }
            }

            ulong result = most.Value - least.Value;
            Console.WriteLine($"Most ({most.Key}, {most.Value}) - ({least.Key}, {least.Value}) = {result}");

            return result;
        }
    }

    public class Program
    {
        static ulong SpinPolymer(string[] bin, int iterations)
        {
            Console.WriteLine($"Going to spin polymer {iterations} times");

            var spinner = new PolymerSpinner(bin);

            for (int x = 0; x < iterations; x++)
            {
                spinner.Spin();

                Console.WriteLine($"---- Spin {x} complete:");
            }

            return spinner.GetAnswers();
        }

        public static ulong Part1(string[] bin)
        {
            Console.WriteLine("Beginning polymer init (part 1)");

            return SpinPolymer(bin, 10);
        }

        public static ulong Part2(string[] bin)
        {
            Console.WriteLine("Beginning polymer init (part 1)");

            return SpinPolymer(bin, 40);
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string[] bin = File.ReadAllLines("input14.txt");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Part1(bin);
            sw.Stop();
            Console.WriteLine($"Part 1 complete: {sw.ElapsedMilliseconds}ms");

            sw.Start();
            Part2(bin);
            sw.Stop();
            Console.WriteLine($"Part 2 complete: {sw.ElapsedMilliseconds}ms");
        }
    }
}