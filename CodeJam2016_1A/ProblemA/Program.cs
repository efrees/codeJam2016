using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemA
{
    class Program
    {
        static void Main(string[] args)
        {
            var T = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= T)
            {
                var S = Console.ReadLine();

                var result = GetLastWord(S);
                Console.WriteLine($"Case #{k}: {result}");
                k++;
            }
        }

        private static string GetLastWord(string input)
        {
            string result = "" + input[0];

            for (int i = 1; i < input.Length; i++)
            {
                char c = input[i];
                if (c >= result[0])
                {
                    result = c + result;
                }
                else
                {
                    result = result + c;
                }
            }

            return result;
        }
    }
}
