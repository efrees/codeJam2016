using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace JamCoins
{
    class Program
    {
        static void Main(string[] args)
        {
            int T = int.Parse(Console.ReadLine());

            //Just a number quick and easy to generate and that gives us
            // a good chance of finding a factor for the candidate numbers.
            GenerateEarlyPrimes(100000);

            int k = 1;
            while (k <= T)
            {
                var input = Console.ReadLine();
                var splits = input.Split(' ');

                var N = int.Parse(splits[0]);
                var J = int.Parse(splits[1]);

                var output = $"Case #{k}:";
                Console.WriteLine(output);

                var generator = new JamCoinCandidateGenerator(N);

                foreach (var candidate in generator.EnumerateCandidates())
                {
                    var factorsForEvidence = GetFactorsInEachBaseInOrder(candidate);

                    if (factorsForEvidence.Count == 9)
                    {
                        Console.WriteLine($"{candidate} {string.Join(" ", factorsForEvidence)}");
                        //One less remaining
                        J--;
                    }

                    if (J == 0) break;
                }

                k++;
            }
        }

        private static List<long> GetFactorsInEachBaseInOrder(string candidate)
        {
            var factors = new List<long>();

            for (var tryBase = 2; tryBase <= 10; tryBase++)
            {
                var numberInCurrentBase = ParseStringAsBase(candidate, tryBase);

                foreach (var prime in earlyPrimes)
                {
                    //primes are generated in order
                    if (prime >= numberInCurrentBase)
                    {
                        break;
                    }

                    if (numberInCurrentBase % prime == 0)
                    {
                        factors.Add(prime);
                        break;
                    }
                }
            }

            return factors;
        }

        private static BigInteger ParseStringAsBase(string numberString, int useBase)
        {
            BigInteger value = 0;
            BigInteger currentPlaceValue = 1;

            foreach (var digit in numberString.Reverse())
            {
                if (digit == '1')
                {
                    value += currentPlaceValue;
                }

                currentPlaceValue *= useBase;
            }

            return value;
        }


        static List<int> earlyPrimes = new List<int> { 2 };

        private static void GenerateEarlyPrimes(int upperBound = 1000)
        {
            for (int n = 3; n < upperBound; n += 2)
            {
                if (!earlyPrimes.Any(p => n % p == 0))
                {
                    earlyPrimes.Add(n);
                }
            }
        }
    }
}
