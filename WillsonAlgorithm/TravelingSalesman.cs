namespace WillsonAlgorithm
{
    public class TravelingSalesman
    {
        private readonly int[,] _distanceMatrix;
        private readonly bool[] visited;
        private readonly List<int> bestPath;
        private int bestPathDistance;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="distanceMatrix"></param>
        public TravelingSalesman(int[,] distanceMatrix)
        {
            _distanceMatrix = distanceMatrix;
            visited = new bool[distanceMatrix.GetLength(0)];
            bestPath = new List<int>();
            bestPathDistance = int.MaxValue;
        }

        /// <summary>
        /// Solve traveling.
        /// </summary>
        public void Solve()
        {
            List<int> currentPath = new List<int> { 0 }; // Start from city 0
            visited[0] = true;
            SolveRecursive(currentPath, 0, 0);
        }

        /// <summary>
        /// Show the best path.
        /// </summary>
        public void PrintBestPath()
        {
            Console.WriteLine("Best Path:");
            foreach (int city in bestPath)
            {
                Console.Write(city + " ");
            }
            Console.WriteLine("\nDistance: " + bestPathDistance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPath"></param>
        /// <param name="currentCity"></param>
        /// <param name="currentDistance"></param>
        private void SolveRecursive(List<int> currentPath, int currentCity, int currentDistance)
        {
            if (currentPath.Count == _distanceMatrix.GetLength(0))
            {
                currentDistance += _distanceMatrix[currentCity, 0]; // Return to the starting city
                if (currentDistance < bestPathDistance)
                {
                    bestPathDistance = currentDistance;
                    bestPath.Clear();
                    bestPath.AddRange(currentPath);
                    bestPath.Add(0);
                }
                return;
            }

            for (int nextCity = 0; nextCity < _distanceMatrix.GetLength(0); nextCity++)
            {
                if (!visited[nextCity])
                {
                    visited[nextCity] = true;
                    currentPath.Add(nextCity);
                    SolveRecursive(currentPath, nextCity, currentDistance + _distanceMatrix[currentCity, nextCity]);
                    currentPath.RemoveAt(currentPath.Count - 1);
                    visited[nextCity] = false;
                }
            }
        }

    }
}
