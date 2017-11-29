using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                N = int.Parse(Console.ReadLine());

                missingListPlaced = false;
                missingList = new List<int>();
                foreach (var i in Enumerable.Range(1, N))
                {
                    missingList.Add(-1);
                }

                var listOfLists = new List<List<int>>();

                foreach (var i in Enumerable.Range(1, 2*N - 1))
                {
                    var splits = Console.ReadLine().Trim().Split(' ');

                    var list = splits.Select(s => int.Parse(s)).ToList();
                    listOfLists.Add(list);
                }

                var result = GetMissingList(listOfLists);
                Console.WriteLine($"Case #{k}: {string.Join(" ", result)}");
                k++;
            }
        }

        private static int N;
        private static List<List<int>> rows;
        private static List<List<int>> cols;
        private static List<int> missingList;
        private static bool missingListPlaced = false;

        private static List<int> GetMissingList(List<List<int>> listOfLists)
        {
            listOfLists.Sort(new FirstElementComparer());
            
            rows = new List<List<int>>();
            cols = new List<List<int>>();

            var firstList = listOfLists.First();

            rows.Add(firstList);

            if (PlaceListsRecursive(1, listOfLists))
            {
                FillInMissingList();
            }
            else
            {
                throw new Exception("No solution found.");
            }

            return missingList;
        }

        private static bool PlaceListsRecursive(int index, List<List<int>> listOfLists)
        {
            if (index == listOfLists.Count)
            {
                return true;
            }

            var listToAdd = listOfLists[index];

            if (CanAddAsRow(listToAdd))
            {
                rows.Add(listToAdd);

                if (PlaceListsRecursive(index + 1, listOfLists))
                {
                    return true;
                }

                //backtrack
                rows.RemoveAt(rows.Count - 1);
            }

            if (CanAddAsColumn(listToAdd))
            {
                cols.Add(listToAdd);

                if (PlaceListsRecursive(index + 1, listOfLists))
                {
                    return true;
                }

                //backtrack
                cols.RemoveAt(cols.Count - 1);
            }

            if (!missingListPlaced)
            {
                missingListPlaced = true;

                if (rows.Count <= N)
                {
                    rows.Add(missingList);

                    if (PlaceListsRecursive(index, listOfLists))
                    {
                        return true;
                    }

                    rows.RemoveAt(rows.Count - 1);
                }

                if (cols.Count <= N)
                {
                    cols.Add(missingList);

                    if (PlaceListsRecursive(index, listOfLists))
                    {
                        return true;
                    }

                    cols.RemoveAt(cols.Count - 1);
                }

                missingListPlaced = false;
            }

            return false;
        }

        private static bool CanAddAsRow(List<int> rowCandidate)
        {
            var nextRowIndex = rows.Count;

            if (nextRowIndex >= N)
            {
                return false;
            }

            if (rows.Any())
            {
                for (int i = 0; i < rowCandidate.Count; i++)
                {
                    if (rows.Last()[i] >= rowCandidate[i])
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < cols.Count; i++)
            {
                if (cols[i][nextRowIndex] != -1 && cols[i][nextRowIndex] != rowCandidate[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CanAddAsColumn(List<int> colCandidate)
        {
            var nextColIndex = cols.Count;

            if (nextColIndex >= N)
            {
                return false;
            }

            if (cols.Any())
            {
                for (int i = 0; i < colCandidate.Count; i++)
                {
                    if (cols.Last()[i] >= colCandidate[i])
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i][nextColIndex] != -1 && rows[i][nextColIndex] != colCandidate[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static void FillInMissingList()
        {
            if (!missingListPlaced)
            {
                if (rows.Count < cols.Count)
                {
                    rows.Add(missingList);
                }
                else
                {
                    cols.Add(missingList);
                }
            }

            var index = rows.IndexOf(missingList);

            if (index >= 0)
            {
                //it's a row
                for (int i = 0; i < cols.Count; i++)
                {
                    missingList[i] = cols[i][index];
                }
            }
            else
            {
                //it's a column
                index = cols.IndexOf(missingList);
                for (int i = 0; i < rows.Count; i++)
                {
                    missingList[i] = rows[i][index];
                }
            }
        }
    }

    public class FirstElementComparer : IComparer<List<int>>
    {
        public int Compare(List<int> x, List<int> y)
        {
            return x[0].CompareTo(y[0]);
        }
    }
}
