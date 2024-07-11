namespace SkiRunnerWebService.Models;

// Node represents a cell in the grid with properties like coordinates, whether it's walkable, and pathfinding-related costs.
// AStar contains the implementation of the A* algorithm.
// FindPath method searches for the path from the start node to the end node.
// GetLowestFScoreNode, GetNeighbors, GetHeuristicCost, and ReconstructPath are helper methods used within the FindPath method.
// Main method demonstrates how to set up a grid, define start and end points, and call the FindPath method to get the path.

public class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsWalkable { get; set; }
    public Node Parent { get; set; } = null!;
    public int G { get; set; } // Cost from start to current node
    public int H { get; set; } // Heuristic cost from current node to end
    public int F => G + H; // Total cost

    public Node(int x, int y, bool isWalkable)
    {
        X = x;
        Y = y;
        IsWalkable = isWalkable;
    }
}

public class AStar
{
    private static readonly int[] DX = [ -1, 1, 0, 0 ];
    private static readonly int[] DY = [ 0, 0, -1, 1 ];

    public static List<Node> FindPath(Node[,] grid, Node start, Node end)
    {
        var openList = new List<Node> { start };
        var closedList = new HashSet<Node>();

        while (openList.Count > 0)
        {
            var current = GetLowestFScoreNode(openList);
            if (current == end)
            {
                return ReconstructPath(current);
            }

            openList.Remove(current);
            closedList.Add(current);

            foreach (var neighbor in GetNeighbors(grid, current))
            {
                if (!neighbor.IsWalkable || closedList.Contains(neighbor))
                {
                    continue;
                }

                var tentativeGScore = current.G + 1;
                if (!openList.Contains(neighbor))
                {
                    neighbor.Parent = current;
                    neighbor.G = tentativeGScore;
                    neighbor.H = GetHeuristicCost(neighbor, end);
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

    private static Node GetLowestFScoreNode(List<Node> openList)
    {
        Node lowest = openList[0];
        foreach (var node in openList)
        {
            if (node.F < lowest.F)
            {
                lowest = node;
            }
        }
        return lowest;
    }

    private static List<Node> GetNeighbors(Node[,] grid, Node node)
    {
        List<Node> neighbors = [];
        for (int i = 0; i < DX.Length; i++)
        {
            int newX = node.X + DX[i];
            int newY = node.Y + DY[i];

            if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1))
            {
                neighbors.Add(grid[newX, newY]);
            }
        }
        return neighbors;
    }

    private static int GetHeuristicCost(Node a, Node b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

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

// Example usage:
public class TestProgram
{
    public static void Main()
    {
        int width = 5;
        int height = 5;
        var grid = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new Node(x, y, true);
            }
        }

        // Add some obstacles
        grid[1, 2].IsWalkable = false;
        grid[2, 2].IsWalkable = false;
        grid[3, 2].IsWalkable = false;

        var start = grid[0, 0];
        var end = grid[4, 4];

        var path = AStar.FindPath(grid, start, end);

        if (path.Count > 0)
        {
            Console.WriteLine("Path found:");
            foreach (var node in path)
            {
                Console.WriteLine($"({node.X}, {node.Y})");
            }
        }
        else
        {
            Console.WriteLine("No path found.");
        }
    }
}