using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Fractiles
{
    //Problem D
    class Program
    {
        static void Main(string[] args)
        {
            int T = int.Parse(Console.ReadLine());
            
            int k = 1;
            while (k <= T)
            {
                var input = Console.ReadLine();
                var splits = input.Trim().Split(' ');

                var K = int.Parse(splits[0]);
                var C = int.Parse(splits[1]);
                var S = int.Parse(splits[2]);

                string result;

                if (!SolutionPossible(K, C, S))
                {
                    result = "IMPOSSIBLE";
                }
                else
                {
                    var samples = GenerateOptimalSamples(K, C);

                    result = string.Join(" ", samples);
                }

                var output = $"Case #{k}: {result}";
                Console.WriteLine(output);
                k++;
            }
        }

        private static bool SolutionPossible(int patternLength, int complexity, int samples)
        {
            //Each sample has the potential to inform about a number of
            // positions in the original pattern equal to the complexity.
            return samples >= patternLength / (double)complexity;
        }

        private static List<BigInteger> GenerateOptimalSamples(int originalLength, int fractalDepth)
        {
            var results = new List<BigInteger>();

            var allPositions = Enumerable.Range(1, originalLength);

            foreach (var subsetOfPositions in ChunkEnumerable(allPositions, fractalDepth))
            {
                var sampleLocation = GetLocationToSample(subsetOfPositions, originalLength, fractalDepth);
                results.Add(sampleLocation);
            }

            return results;
        }

        private static IEnumerable<List<int>> ChunkEnumerable(IEnumerable<int> source, int chunkSize)
        {
            var chunk = new List<int>(chunkSize);

            foreach (var element in source)
            {
                chunk.Add(element);

                if (chunk.Count == chunkSize)
                {
                    yield return chunk;
                    chunk = new List<int>(chunkSize);
                }
            }

            if (chunk.Any())
            {
                yield return chunk;
            }
        }

        private static BigInteger GetLocationToSample(List<int> originalPositionsToCover, int originalLength, int fractalDepth)
        {
            var majorGroupLength = BigInteger.Pow(originalLength, fractalDepth - 1);

            var locationOffset = BigInteger.Zero;
            var workingGroupLength = majorGroupLength;

            foreach (var positionToCover in originalPositionsToCover)
            {
                locationOffset += (positionToCover - 1)*majorGroupLength;

                majorGroupLength /= originalLength;
            }

            return 1 + locationOffset; //One-indexed in original
        }
    }
}
