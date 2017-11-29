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

                var test = string.Join("", spellings).OrderBy(c => c);

                var list = GetDigitsFromString(S);
                list.Sort();

                var result = S;
                Console.WriteLine($"Case #{k}: {string.Join(string.Empty, list)}");
                k++;
            }
        }

        private static string[] spellings = {"ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE"};

        private static List<int> GetDigitsFromString(string jumbledLetters)
        {
            var digits = new List<int>();

            //Z => 0
            jumbledLetters = CountAndRemoveWordsForDigit(jumbledLetters, digits, 0, 'Z');
            //W => 2
            jumbledLetters = CountAndRemoveWordsForDigit(jumbledLetters, digits, 2, 'W');
            //U => 4
            jumbledLetters = CountAndRemoveWordsForDigit(jumbledLetters, digits, 4, 'U');
            //X => 6
            jumbledLetters = CountAndRemoveWordsForDigit(jumbledLetters, digits, 6, 'X');
            //G => 8
            jumbledLetters = CountAndRemoveWordsForDigit(jumbledLetters, digits, 8, 'G');
            //H => 8,3 (and eights were removed above)
            jumbledLetters = CountAndRemoveWordsForDigit(jumbledLetters, digits, 3, 'H');
            //F => 4,5 (and fours were removed above)
            jumbledLetters = CountAndRemoveWordsForDigit(jumbledLetters, digits, 5, 'F');
            //S => 6,7 (and sixes were removed above)
            jumbledLetters = CountAndRemoveWordsForDigit(jumbledLetters, digits, 7, 'S');
            //I => 5,6,8,9 (all but nine removed above)
            jumbledLetters = CountAndRemoveWordsForDigit(jumbledLetters, digits, 9, 'I');
            //any thing left is a 1
            jumbledLetters = CountAndRemoveWordsForDigit(jumbledLetters, digits, 1, 'O');
            
            return digits;
        }

        private static string CountAndRemoveWordsForDigit(string jumbledLetters, List<int> digits,  int targetDigit, char signalChar)
        {
            while (jumbledLetters.Contains(signalChar))
            {
                digits.Add(targetDigit);

                //remove word
                jumbledLetters = RemoveWord(jumbledLetters, spellings[targetDigit]);
            }

            return jumbledLetters;
        }

        private static string RemoveWord(string jumbledLetters, string word)
        {
            foreach (var c in word)
            {
                var firstIndex = jumbledLetters.IndexOf(c);
                if (firstIndex >= 0)
                {
                    jumbledLetters = jumbledLetters.Remove(firstIndex, 1);
                }
            }

            return jumbledLetters;
        }
    }
}
