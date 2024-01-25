using Application.Cqrs.Journey.Queries;
using Application.DTOs.Flight;
using Application.DTOs.PriorityQueue;
using Application.Interfaces.SearchAlgorithm;

namespace Application.Services.SearchAlgorithm
{
    public class SearchShortRouteService : ISearchShortRouteService
    {
        public List<FlightDto> FindShortestRoute(List<FlightDto> flights, GetRouteQuery request)
        {
            var graph = flights.GroupBy(f => f.Origin).ToDictionary(g => g.Key, g => g.ToList());
            var distances = new Dictionary<string, int>();
            var previousNodes = new Dictionary<string, string>();
            var visited = new HashSet<string>();

            foreach (var node in graph.Keys)
            {
                distances[node] = int.MaxValue;
                previousNodes[node] = null;
            }

            distances[request.Origin] = 0;

            var priorityQueue = new PriorityQueue<string>();
            priorityQueue.Enqueue(request.Origin, 0);

            while (priorityQueue.Count > 0)
            {
                var currentNode = priorityQueue.Dequeue();

                if (currentNode == request.Destination)
                {
                    return ReconstructPath(previousNodes, currentNode, flights);
                }

                if (!visited.Contains(currentNode))
                {
                    visited.Add(currentNode);

                    if (graph.ContainsKey(currentNode))
                    {
                        foreach (var nextFlight in graph[currentNode])
                        {
                            var newDistance = distances[currentNode] + nextFlight.Price;

                            if (newDistance < distances[nextFlight.Destination])
                            {
                                distances[nextFlight.Destination] = (int)newDistance;
                                previousNodes[nextFlight.Destination] = currentNode;
                                priorityQueue.Enqueue(nextFlight.Destination, newDistance);
                            }
                        }
                    }
                }
            }

            return null;
        }

        private List<FlightDto> ReconstructPath(Dictionary<string, string> previousNodes, string destination, List<FlightDto> flights)
        {
            var path = new List<FlightDto>();
            string current = destination;

            while (current != null)
            {
                string previous = previousNodes[current];
                if (previous != null)
                {
                    var flight = flights.First(f => f.Origin == previous && f.Destination == current);
                    path.Insert(0, flight);
                }

                current = previous;
            }

            return path;
        }
    }
}
