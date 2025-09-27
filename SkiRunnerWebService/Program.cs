using SkiRunnerWebService.Models;
using SkiRunnerWebService.Models.Enums;

List<ResortEntity> resortEntities = [
    new Lift() {
        Start = 1,
        End = 2
    },
    new Run() {
        Start = 2,
        End = 3,
        Difficulty = RunDifficulty.Beginner
    },
    new Run() {
        Start = 2,
        End = 4,
        Difficulty = RunDifficulty.Intermediate
    },
    new Lift() {
        Start = 3,
        End = 5
    },
    new Run() {
        Start = 5,
        End = 1,
        Difficulty = RunDifficulty.Beginner
    },
    new Run() {
        Start = 5,
        End = 6,
        Difficulty = RunDifficulty.Expert
    }
];

List<Node> nodes = [];

foreach (ResortEntity entity in resortEntities) {
    nodes.Add(new(entity.Start, entity.End, entity is Run r ? r.Difficulty : null));
}

Node start = nodes[0];
Node end = nodes[4];

var path = AStar.FindPath(nodes, start, end);

if (path.Count > 0)
{
    Console.WriteLine("Path found:");
    foreach (var node in path)
    {
        Console.WriteLine($"({node.Start}, {node.End})");
    }
}
else
{
    Console.WriteLine("No path found.");
}