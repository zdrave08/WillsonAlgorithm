using System;
using System.Collections.Generic;

namespace WillsonAlgorithm
{
    /// <summary>
    /// Solves the Traveling Salesman Problem using a dynamic programming
    /// approach (Held-Karp algorithm).
    /// </summary>
    public class DynamicProgrammingSalesman
    {
        private readonly int[,] _distanceMatrix;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicProgrammingSalesman"/> class.
        /// </summary>
        /// <param name="distanceMatrix">Matrix with pairwise distances.</param>
        public DynamicProgrammingSalesman(int[,] distanceMatrix)
        {
            _distanceMatrix = distanceMatrix;
        }

        /// <summary>
        /// Computes the optimal path and its total distance using dynamic programming.
        /// </summary>
        /// <returns>Tuple containing the best path and its distance.</returns>
        public (List<int> Path, int Distance) Solve()
        {
            int n = _distanceMatrix.GetLength(0);
            var dp = new Dictionary<(int subset, int last), int>();
            var parent = new Dictionary<(int subset, int last), int>();

            // Initialize with paths that start at 0 and visit one city
            for (int city = 1; city < n; city++)
            {
                int subset = 1 << city;
                dp[(subset, city)] = _distanceMatrix[0, city];
                parent[(subset, city)] = 0;
            }

            int allVisitedMask = (1 << n) - 2; // Exclude start city (0)
            for (int subsetMask = 1; subsetMask <= allVisitedMask; subsetMask++)
            {
                // Skip subsets with only one bit set
                if ((subsetMask & (subsetMask - 1)) == 0) continue;

                for (int last = 1; last < n; last++)
                {
                    if ((subsetMask & (1 << last)) == 0) continue;

                    int subsetWithoutLast = subsetMask ^ (1 << last);
                    int best = int.MaxValue;
                    int bestPrev = -1;

                    for (int prev = 1; prev < n; prev++)
                    {
                        if ((subsetWithoutLast & (1 << prev)) == 0) continue;
                        if (!dp.TryGetValue((subsetWithoutLast, prev), out int prevCost))
                            continue;

                        int cost = prevCost + _distanceMatrix[prev, last];
                        if (cost < best)
                        {
                            best = cost;
                            bestPrev = prev;
                        }
                    }

                    if (bestPrev != -1)
                    {
                        dp[(subsetMask, last)] = best;
                        parent[(subsetMask, last)] = bestPrev;
                    }
                }
            }

            int bestDistance = int.MaxValue;
            int bestLastCity = -1;
            foreach (var kvp in dp)
            {
                var (subset, last) = kvp.Key;
                if (subset != allVisitedMask) continue;
                int cost = kvp.Value + _distanceMatrix[last, 0];
                if (cost < bestDistance)
                {
                    bestDistance = cost;
                    bestLastCity = last;
                }
            }

            var path = new List<int> { 0 };
            int mask = allVisitedMask;
            int cityCursor = bestLastCity;
            var stack = new Stack<int>();
            while (cityCursor != 0 && cityCursor != -1)
            {
                stack.Push(cityCursor);
                int prev = parent[(mask, cityCursor)];
                mask ^= 1 << cityCursor;
                cityCursor = prev;
            }
            while (stack.Count > 0)
            {
                path.Add(stack.Pop());
            }
            path.Add(0); // return to start

            return (path, bestDistance);
        }
    }
}
