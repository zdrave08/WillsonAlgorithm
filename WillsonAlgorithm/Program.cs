using WillsonAlgorithm;

int[,] distanceMatrix = {
            { 0, 10, 15, 20 },
            { 10, 0, 35, 25 },
            { 15, 35, 0, 30 },
            { 20, 25, 30, 0 }
        };

TravelingSalesman tsp = new TravelingSalesman(distanceMatrix);
tsp.Solve();
tsp.PrintBestPath();
