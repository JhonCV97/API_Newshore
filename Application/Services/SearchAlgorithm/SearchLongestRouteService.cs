using Application.Cqrs.Journey.Queries;
using Application.DTOs.Flight;
using Application.Interfaces.SearchAlgorithm;

namespace Application.Services.SearchAlgorithm
{
    public class SearchLongestRouteService : ISearchLongestRouteService
    {
        public List<FlightDto> FindLongestRoute(List<FlightDto> routes, GetRouteQuery request)
        {
            var graph = routes.GroupBy(r => r.Origin).ToDictionary(g => g.Key, g => g.ToList());
            var visited = new HashSet<string>();
            var result = new List<FlightDto>();

            if (DFS(graph, request.Origin, request.Destination, visited, result))
            {
                return result;
            }

            return null;
        }

        private bool DFS(Dictionary<string, List<FlightDto>> graph, string currentStation, string destination, HashSet<string> visited, List<FlightDto> result)
        {
            visited.Add(currentStation);

            if (currentStation == destination)
            {
                return true;
            }

            if (graph.ContainsKey(currentStation))
            {
                foreach (var nextRoute in graph[currentStation])
                {
                    if (!visited.Contains(nextRoute.Destination))
                    {
                        result.Add(nextRoute);
                        if (DFS(graph, nextRoute.Destination, destination, visited, result))
                        {
                            return true;
                        }
                        result.Remove(nextRoute);
                    }
                }
            }

            return false;
        }
    }
}
