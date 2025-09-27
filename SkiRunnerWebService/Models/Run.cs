using SkiRunnerWebService.Models.Enums;

namespace SkiRunnerWebService.Models;

public class Run : ResortEntity {
    public RunDifficulty? Difficulty { get; set; }
    public RunTerrain Terrain { get; set; }
}