using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProblemC
{
    class Program
    {
        static void Main(string[] args)
        {
            var T = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= T)
            {
                var N = int.Parse(Console.ReadLine());
                var splits = Console.ReadLine().Trim().Split(' ');

                var BFFs = splits.Select(s => int.Parse(s)).ToList();

                var result = SizeOfLargestCircle(BFFs);
                Console.WriteLine($"Case #{k}: {result}");
                k++;
            }
        }

        private static int SizeOfLargestCircle(List<int> BFFs)
        {
            var cycleRecords = new CycleRecord[BFFs.Count];

            for (int i = 0; i < BFFs.Count; i++)
            {
                cycleRecords[i] = GroupStartingFrom(i, BFFs);
            }

            var largestClosed = cycleRecords.Where(r => r.isSuccess).Max(r => r.GetSize());

            currentSubset = new List<CycleRecord>();
            var largestOpenCombination = GetLargestCompatibleSubSet(cycleRecords.Where(r => r.isSuccess && r.isOpen).ToList());

            return largestClosed > largestOpenCombination ? largestClosed : largestOpenCombination;
        }

        private static List<CycleRecord> currentSubset; 
        private static int GetLargestCompatibleSubSet(List<CycleRecord> openGroups)
        {
            //var skippedGroups = new List<CycleRecord>();
            if (openGroups.Count == 0)
            {
                return currentSubset.Sum(r => r.GetSize());
            }

            var firstGroup = openGroups.First();
            openGroups.RemoveAt(0);

            var largestGroup = GetLargestCompatibleSubSet(openGroups);

            if (IsCompatible(firstGroup))
            {
                currentSubset.Add(firstGroup);

                var largestWithIt = GetLargestCompatibleSubSet(openGroups);
                if (largestWithIt > largestGroup)
                {
                    largestGroup = largestWithIt;
                }

                currentSubset.RemoveAt(currentSubset.Count - 1);
            }

            openGroups.Insert(0, firstGroup);

            return largestGroup;
        }

        private static bool IsCompatible(CycleRecord record)
        {
            foreach (var current in currentSubset)
            {
                if (!current.IsCompatibleWith(record))
                    return false;
            }

            return true;
        }

        private static CycleRecord GroupStartingFrom(int start, List<int> BFFs)
        {
            var visited = new bool[BFFs.Count];

            var previous = -1;
            var current = start;
            visited[current] = true;

            while (!visited[BFFs[current]])
            {
                previous = current;
                current = BFFs[current];
                visited[current] = true;
            }

            var openGroup = BFFs[current] == previous;

            if (BFFs[current] != start && !openGroup)
            {
                return new CycleRecord { isSuccess = false };
            }

            var record = new CycleRecord()
            {
                visited = visited,
                isOpen = openGroup,
                isSuccess = true,
            };

            return record;
        }
    }

    public class CycleRecord
    {
        public bool[] visited;
        public bool isOpen;
        public bool isSuccess;

        private int size = -1;
        public int GetSize()
        {
            if (size == -1)
            {
                size = visited.Count(v => v);
            }
            return size;
        }

        public bool IsCompatibleWith(CycleRecord other)
        {
            if (visited.Length != other.visited.Length)
                return false;

            for (int i = 0; i < visited.Length; i++)
            {
                if (other.visited[i] && visited[i])
                    return false;
            }
            return true;
        }
    }
}
