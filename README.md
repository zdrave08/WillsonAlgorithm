# WillsonAlgorithm

Wilson's algorithm can be used to solve the Traveling Salesman Problem (TSP). TSP is a well-known problem in combinatorial optimization where the goal is to find the shortest travel route that visits each city once and returns to the starting point.

This example uses a coarse-grained approach to solving the TSP, which means it examines all possible permutations of routes to find the shortest one. **The number of possibilities grows factorially** with the number of cities, so the algorithm quickly becomes infeasible as the problem size increases. It is only practical for small sets of cities.

A dynamic-programming based alternative is provided in `DynamicProgrammingSalesman`. While it still has exponential complexity, it is significantly faster than brute force and demonstrates another way to approach the problem.
