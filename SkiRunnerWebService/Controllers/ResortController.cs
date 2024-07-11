using Microsoft.AspNetCore.Mvc;
using SkiRunnerWebService.Models;
using SkiRunnerWebService.Services.ResortService;

namespace SkiRunnerWebService.Controllers
{
    [Route($"api/[controller]")]
    [ApiController]
    public class ResortController(IResortService _resortService) : ControllerBase
    {
        [HttpGet]
        public async Task<ServiceResponse<Resort>> GetResort(Guid id)
        {
            return await _resortService.GetResortById(id);
        }

        [HttpGet]
        [Route("all")]
        public ServiceResponse<HashSet<Resort>> GetAllResorts() {
            return _resortService.GetAllResorts();
        }

        [HttpPost]
        public async Task<ServiceResponse<bool>> SaveResort(Resort resort) {
            resort = new() {
                Name = "Test Resort 1"
            };
            
            return await _resortService.SaveResort(resort);
        }

        [HttpDelete]
        public async Task<ServiceResponse<bool>> DeleteResort(Guid id) {
            return await _resortService.DeleteResort(id);
        }
    }
}