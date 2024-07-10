using Microsoft.EntityFrameworkCore;
using SkiRunnerWebService.Models;
using SkiRunnerWebService.Models.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/all-resorts", () => {
    using SkiRunnerContext context = new();
    context.Database.EnsureCreated();

    Resort resort = context.Resorts
                            .First();
    
    Console.WriteLine(resort);

    Console.WriteLine(resort.Lifts.First());

    Run run = resort.Lifts.First().AccessibleRuns.First();
    bool runner = true;
    while (runner) {
        if (run?.AccessibleRuns.Count > 0) {
            Console.WriteLine(run.Name);
            run = run.AccessibleRuns.First();
        }
        else {
            runner = false;
            break;
        }
    }
});

app.MapGet("/resorts", () =>
{
    using SkiRunnerContext context = new();
    context.Database.EnsureCreated();

    Resort resort = context.Resorts
                            .Include(l => l.Lifts)
                            .ThenInclude(l => l.AccessibleRuns)
                            .ThenInclude(r => r.AccessibleRuns)
                            .First();

    Run r = resort.Lifts[0].AccessibleRuns[0].AccessibleRuns[0];
    Run run3 = new()
    {
        Id = Guid.NewGuid(),
        ResortId = resort.Id,
        Name = "Test Run 3",
        HasMoguls = false,
        Difficulty = RunDifficulty.Expert,
        ParentRunId = r.Id
    };
    // Resort resort = new()
    // {
    //     Id = Guid.NewGuid(),
    //     Name = "Test Resort 3"
    // };

    // Lift lift = new()
    // {
    //     Id = Guid.NewGuid(),
    //     Name = "Test Lift 2",
    //     ResortId = resort.Id,
    //     Resort = resort
    // };

    // Run run1 = new()
    // {
    //     Id = Guid.NewGuid(),
    //     ResortId = resort.Id,
    //     Name = "Test Run 1",
    //     HasMoguls = true,
    //     Difficulty = RunDifficulty.Intermediate,
    //     Lift = lift,
    //     LiftId = lift.Id
    // };

    // Run run2 = new()
    // {
    //     Id = Guid.NewGuid(),
    //     ResortId = resort.Id,
    //     Name = "Test Run 2",
    //     HasMoguls = false,
    //     Difficulty = RunDifficulty.Beginner,
    //     ParentRunId = run1.Id
    // };

    // run1.AccessibleRuns.Add(run2);
    // lift.AccessibleRuns.Add(run1);
    // resort.Lifts.Add(lift);

    // context.Resorts.Add(resort);
    // context.Lifts.Add(lift);
    // context.Runs.Add(run1);
    context.Runs.Add(run3);

    context.SaveChanges();
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();



record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
