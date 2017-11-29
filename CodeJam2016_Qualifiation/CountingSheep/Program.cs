using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingSheep
{
    class Program
    {
        static void Main(string[] args)
        {
            int T = int.Parse(Console.ReadLine());

            int k = 1;
            while (k <= T)
            {
                var input = Console.ReadLine();
                var N = int.Parse(input);

                var result = GetCountingResult(N);

                var output = $"Case #{k}: {result}";
                Console.WriteLine(output);
                k++;
            }
        }

        private static string GetCountingResult(int N)
        {
            if (N == 0)
            {
                return "INSOMNIA";
            }

            var digitsToSee = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var currentN = 0;

            do
            {
                currentN += N;

                var digitsInN = EnumerateDigits(currentN);
                
                foreach (var digit in digitsInN)
                {
                    digitsToSee.Remove(digit);
                }
            } while (digitsToSee.Any());

            return currentN.ToString();
        }

        private static IEnumerable<int> EnumerateDigits(int number)
        {
            return number.ToString().ToCharArray().Select(c => (int)(c - '0'));
        } 
    }
}
