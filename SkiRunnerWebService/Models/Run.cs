using SkiRunnerWebService.Models.Enums;

namespace SkiRunnerWebService.Models;

public class Run {
    public Guid Id {get; set;}
    public Guid ResortId { get; set; }
    public Guid? ParentRunId { get; set; }
    public Guid? LiftId { get; set; }
    public string Name {get; set;} = "";
    public RunDifficulty Difficulty {get; set;}
    public bool HasMoguls { get; set; }

    public virtual Resort Resort { get; set; } = null!;
    public virtual Lift Lift { get; set; } = null!;
    public virtual List<Run> AccessibleRuns { get; set;} = [];
}