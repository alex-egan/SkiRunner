namespace SkiRunnerWebService.Models;
using SkiRunnerWebService.Models.Enums;

public class ResortEntity {
    public Guid Id { get; set; }
    public string Name {get; set;} = "";
    public ResortEntityDifficulty Difficulty { get; set; }
    public ResortEntityType Type { get; set; }
    public bool HasMoguls { get; set; }

    public virtual ResortEntity ParentEntity { get; set; } = null!;
    public virtual Resort Resort { get; set; } = null!;
    public virtual List<ResortEntity> AccessibleEntities { get; set;} = [];
}