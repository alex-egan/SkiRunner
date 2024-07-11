using Microsoft.EntityFrameworkCore;
using Serilog;
using SkiRunnerWebService.Models;

namespace SkiRunnerWebService.Services.ResortService;

public class ResortService(SkiRunnerContext context) : IResortService
{
    public async Task<ServiceResponse<bool>> DeleteResort(Guid id)
    {
        try {
            Log.Information($"Deleting Resort with ID: {id}");
            Resort? resort = await GetResort(id);
            
            if (resort == null) {
                return new ServiceResponse<bool>($"Could not find Resort with ID: {id}");
            }

            context.Resorts.Remove(resort);
            await context.SaveChangesAsync();
            Log.Information($"Resort deleted successfully with ID: {id}");
            return new ServiceResponse<bool>(true, true, "");
        }
        catch (Exception e) {
            Log.Error($"{e.Message} --> {e.StackTrace}");
            return new ServiceResponse<bool>($"Error grabbing Resort with ID: {id}");
        }
    }

    public ServiceResponse<HashSet<Resort>> GetAllResorts()
    {
        try {
            context.Database.EnsureCreated();
            Log.Information($"Getting All Resorts");
            HashSet<Resort> resorts = [.. context.Resorts];

            Log.Information($"Retrieved all resorts.");
            return new ServiceResponse<HashSet<Resort>>(resorts, true, "");
        }
        catch (Exception e) {
            Log.Error($"{e.Message} --> {e.StackTrace}");
            return new ServiceResponse<HashSet<Resort>>($"Error grabbing Resorts");
        }
    }

    public async Task<ServiceResponse<Resort>> GetResortById(Guid id)
    {
        try {
            Log.Information($"Getting Resort with ID: {id}");
            Resort? resort = await GetResort(id);
            
            if (resort == null) {
                return new ServiceResponse<Resort>($"Could not get Resort with ID: {id}");
            }

            Log.Information($"Resort retrieved successfully with ID: {id}");
            return new ServiceResponse<Resort>(resort, true, "");
        }
        catch (Exception e) {
            Log.Error($"{e.Message} --> {e.StackTrace}");
            return new ServiceResponse<Resort>($"Error grabbing Resort with ID: {id}");
        }
    }

    public async Task<ServiceResponse<bool>> SaveResort(Resort resort)
    {
        try {
            if (resort.Id == null) {
                Log.Information($"Creating new resort: {resort.Name}");
                context.Resorts.Add(resort);
            }
            else {
                Log.Information($"Creating Resort with ID: {resort.Id}");
                context.Resorts.Update(resort);
            }

            await context.SaveChangesAsync();
            Log.Information($"Resort saved successfully with ID: {resort.Id}");
            return new ServiceResponse<bool>(true, true, "");
        }
        catch (Exception e) {
            Log.Error($"{e.Message} --> {e.StackTrace}");
            return new ServiceResponse<bool>($"Error grabbing Resort with ID: {resort.Id}");
        }
    }

    private async Task<Resort?> GetResort(Guid id) {
        return await context.Resorts.FindAsync(id);
    }
}