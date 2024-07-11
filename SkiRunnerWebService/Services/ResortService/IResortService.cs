using SkiRunnerWebService.Models;

namespace SkiRunnerWebService.Services.ResortService;

public interface IResortService {
    public ServiceResponse<HashSet<Resort>> GetAllResorts();
    public Task<ServiceResponse<Resort>> GetResortById(Guid id);
    public Task<ServiceResponse<bool>> SaveResort(Resort resort);
    public Task<ServiceResponse<bool>> DeleteResort(Guid id);
}