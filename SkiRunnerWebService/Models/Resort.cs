namespace SkiRunnerWebService.Models;

public class Resort {
    public Guid? Id {get; set;}
    public string Name {get; set;} = "";

    public virtual HashSet<ResortEntity> ResortEntities { get; set; } = [];
}