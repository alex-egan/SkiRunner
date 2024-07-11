using Microsoft.EntityFrameworkCore;
using SkiRunnerWebService.Models;
using SkiRunnerWebService.Services.ResortService;
using SkiRunnerWebService.Models.Enums;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddScoped<IResortService, ResortService>();
builder.Services.AddSingleton<SkiRunnerContext>();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using var log = new LoggerConfiguration()
    .WriteTo.MySQL("server=localhost;database=SkiRunner;user=root;password=AlSnow13!!")
    .CreateLogger();

Log.Logger = log;

app.Run();



record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
