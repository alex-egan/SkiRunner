using SkiRunnerWebService.Models.Enums;

namespace SkiRunnerWebService.Models;

// Node represents a cell in the grid with properties like coordinates, whether it's walkable, and pathfinding-related costs.
// AStar contains the implementation of the A* algorithm.
// FindPath method searches for the path from the start node to the end node.
// GetLowestFScoreNode, GetNeighbors, GetHeuristicCost, and ReconstructPath are helper methods used within the FindPath method.
// Main method demonstrates how to set up a grid, define start and end points, and call the FindPath method to get the path.

public class Node
{
    public int Start { get; set; }
    public int End { get; set; }
    public RunDifficulty? Difficulty { get; set; }
    public Node Parent { get; set; } = null!;
    public int G { get; set; } // Cost from start to current node

    public Node(int start, int end, RunDifficulty? difficulty)
    {
        Start = start;
        End = end;
        Difficulty = difficulty;
    }
}

public class AStar
{
    public static List<Node> FindPath(List<Node> nodes, Node start, Node end)
    {
        RunDifficulty userProficiency = RunDifficulty.Beginner;

        var openList = new List<Node> { start };
        var closedList = new HashSet<Node>();

        while (openList.Count > 0)
        {
            Node current = GetLowestGScoreNode(openList);
            if (current == end)
            {
                return ReconstructPath(current);
            }

            openList.Remove(current);
            closedList.Add(current);

            foreach (Node neighbor in GetNeighbors(nodes, current.End))
            {
                if (!CanCompleteEntity(userProficiency, neighbor.Difficulty) || closedList.Contains(neighbor)) 
                    continue;

                int tentativeGScore = CalculateDistanceCost(current.End, neighbor.Start);
                if (!openList.Contains(neighbor))
                {
                    neighbor.Parent = current;
                    neighbor.G = tentativeGScore;
                    openList.Add(neighbor);
                }
                else if (tentativeGScore < neighbor.G)
                {
                    neighbor.Parent = current;
                    neighbor.G = tentativeGScore;
                }
            }
        }

        return []; // No path found
    }

    private static int CalculateDistanceCost(int currentLocation, int startLocation)
        => Math.Abs(currentLocation - startLocation);

    private static bool CanCompleteEntity(RunDifficulty userProficiency, RunDifficulty? runDifficulty)
        => runDifficulty is null || (int)userProficiency >= (int)runDifficulty;

    private static Node GetLowestGScoreNode(List<Node> openList)
    {
        Node lowest = openList[0];
        foreach (var node in openList)
        {
            if (node.G < lowest.G)
            {
                lowest = node;
            }
        }
        return lowest;
    }

    private static List<Node> GetNeighbors(List<Node> nodes, int currentLocation)
        => [.. nodes.Where(n => n.Start == currentLocation)];

    private static List<Node> ReconstructPath(Node current)
    {
        var path = new List<Node>();
        while (current != null)
        {
            path.Add(current);
            current = current.Parent;
        }
        path.Reverse();
        return path;
    }
}