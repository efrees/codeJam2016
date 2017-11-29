using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamCoins
{
    public class JamCoinCandidateGenerator
    {
        private int targetLength;

        public JamCoinCandidateGenerator(int N)
        {
            targetLength = N;
        }

        public IEnumerable<string> EnumerateCandidates()
        {
            var rawPermutations = GenerateRawPermutationsRecursive(targetLength - 2);
            foreach (var raw in rawPermutations)
            {
                yield return "1" + raw + "1";
            }
        }

        private IEnumerable<string> GenerateRawPermutationsRecursive(int substringLength)
        {
            if (substringLength == 1)
            {
                yield return "1";
                yield return "0";
            }

            var recursiveResults = GenerateRawPermutationsRecursive(substringLength - 1);

            foreach (var subResult in recursiveResults)
            {
                yield return subResult + "1";
                yield return subResult + "0";
            }
        }
    }
}
