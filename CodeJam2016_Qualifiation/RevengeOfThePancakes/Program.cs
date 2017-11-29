using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevengeOfPancakes
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

                //I have the makings of a proof that this is correct
                var pancakeStack = input.Trim();

                var sections = CountContiguousSections(pancakeStack);

                if (pancakeStack.Last() == '+')
                    sections -= 1;

                var output = $"Case #{k}: {sections}";
                Console.WriteLine(output);
                k++;
            }
        }

        public static int CountContiguousSections(string pancakeStack)
        {
            if (pancakeStack.Length == 0)
                return 0;

            var currentChar = pancakeStack[0];
            var count = 1;
            for (int i = 1; i < pancakeStack.Length; i++)
            {
                if (pancakeStack[i] != currentChar)
                {
                    currentChar = pancakeStack[i];
                    count++;
                }
            }

            return count;
        }
    }
}
