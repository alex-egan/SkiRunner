namespace SkiRunnerWebService.Models;

public class Lift {
    public Guid Id {get; set;}
    public string Name {get; set;} = "";
    public Guid ResortId { get; set; }

    public virtual Resort Resort { get; set; } = null!;
    public virtual List<Run> AccessibleRuns { get; set; } = [];
}