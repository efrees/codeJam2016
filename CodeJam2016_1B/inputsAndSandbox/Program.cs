using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemB
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
                var splits = S.Trim().Split(' ');

                cOriginal = splits[0];
                jOriginal = splits[1];

                string cLocal;
                string jLocal;

                FillToBeClosest(out cLocal, out jLocal);

                Console.WriteLine($"Case #{k}: {cLocal} {jLocal}");
                k++;
            }
        }

        private static string cOriginal;
        private static string jOriginal;

        private static void FillToBeClosest(out string C, out string J)
        {
            var cArray = cOriginal.ToCharArray();
            var jArray = jOriginal.ToCharArray();

            long cValue = long.MaxValue;
            long jValue = long.MaxValue;
            C = "No";
            J = "Solution";

            if (FillSoSecondIsLargerOrEqual(cArray, jArray))
            {
                C = new string(cArray);
                J = new string(jArray);

                cValue = long.Parse(C);
                jValue = long.Parse(J);
            }

            cArray = cOriginal.ToCharArray();
            jArray = jOriginal.ToCharArray();

            if (FillSoSecondIsLargerOrEqual(jArray, cArray))
            {
                if (long.Parse(new string(cArray)) < cValue || (long.Parse(new string(cArray)) == cValue && long.Parse(new string(jArray)) < jValue))
                {
                    C = new string(cArray);
                    J = new string(jArray);
                }
            }
        }

        private static bool FillSoSecondIsLargerOrEqual(char[] s1, char[] s2)
        {
            var directionEnsuredAtIndex = -1;
            var forcingSearchIndex = -1;

            //Look for a conflict in our desired inequality
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != '?' && s2[i] != '?')
                {
                    if (s2[i] > s1[i])
                    {
                        directionEnsuredAtIndex = i;
                        break;
                    }
                    else if (s1[i] > s2[i])
                    {
                        //Must be forced before this position
                        forcingSearchIndex = i;
                        break;
                    }
                    else
                    {
                        //equal. keep searching.
                    }
                }
            }

            if (forcingSearchIndex > -1)
            {
                var didOurJob = false;
                //look for least significant flexible digit before forcingIndex
                for (int j = forcingSearchIndex; j >= 0; j--)
                {
                    if ((s1[j] == '?' && s2[j] != '0') || (s2[j] == '?' && s1[j] != '9'))
                    {
                        //Found our flexibility
                        if (s1[j] == '?' && s2[j] == '?')
                        {
                            s1[j] = '0';
                            s2[j] = '1';
                        }
                        else if (s1[j] == '?')
                        {
                            s1[j] = (char)(s2[j] - 1);
                        }
                        else //s2[j] == '?'
                        {
                            s2[j] = (char)(s1[j] + 1);
                        }

                        directionEnsuredAtIndex = j;
                        didOurJob = true;
                        break;
                    }
                }

                if (!didOurJob)
                {
                    //We failed. Not possible this direction
                    return false;
                }
            }
            else
            {
                //We have flexibility all the way. We'll make them equal.
            }


            for (int i = 0; i < s1.Length; i++)
            {
                //If there's a question, copy or use default.
                if (directionEnsuredAtIndex == -1 || i < directionEnsuredAtIndex)
                {
                    //Always zero when neither constrained.
                    s2[i] = CharOrDefault(s2[i], CharOrDefault(s1[i], '0'));
                    s1[i] = CharOrDefault(s1[i], s2[i]);
                }
                else
                {
                    //s2 is larger based on higher order bits, so let's push them closer together
                    s2[i] = CharOrDefault(s2[i], '0');
                    s1[i] = CharOrDefault(s1[i], '9');
                }
            }

            return true;
        }

        private static char CharOrDefault(char c, char def)
        {
            if (c == '?')
            {
                c = def;
            }
            return c;
        }
    }
}
