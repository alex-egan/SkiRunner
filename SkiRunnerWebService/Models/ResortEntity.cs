namespace SkiRunnerWebService.Models;

public abstract class ResortEntity {
    public Guid? Id { get; set; }
    public Guid ResortId { get; set; }
    public string Name { get; set; } = "";
    public int Start { get; set; }
    public int End { get; set; }
    public int Duration { get; set; }
}