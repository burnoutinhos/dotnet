using BurnoutinhosProject.Models;
using BurnoutinhosProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace BurnoutinhosProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly AnalyticsService analyticsService;

        public AnalyticsController(AnalyticsService analyticsService)
        {
            this.analyticsService = analyticsService;
        }

        [HttpGet("summary/{userId}")]
        public async Task<ActionResult<IEnumerable<Analytics>>> GetUserAnalyticsSummary(int userId)
        {
            var summary = await analyticsService.GetByUserIdAsync(userId);
            return Ok(summary);
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Analytics>>> GetAllAnalytics()
        {
            var analytics = await analyticsService.GetAllAsync();
            return Ok(analytics);
        }

        [HttpPost]
        public async Task<ActionResult<Analytics>> CreateAnalytics([FromBody] Analytics analytics)
        {
            var createdAnalytics = await analyticsService.AddAsync(analytics);
            return Ok(createdAnalytics);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Analytics>> UpdateAnalytics([FromQuery] int id ,[FromBody] Analytics analytics)
        {
            if (analytics.Id != id)
            {
                return BadRequest("Ids inconguentes.");
            }
            var updatedAnalytics = await analyticsService.UpdateAsync(analytics);
            if (updatedAnalytics == null)
            {
                return NotFound("Analytics não encontrada.");
            }
            return Ok(updatedAnalytics);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnalytics(int id)
        {
            var result = await analyticsService.DeleteAsync(id);
            if (!result)
            {
                return NotFound("Analytics não encontrada.");
            }
            return Ok("Analytics deletada com sucesso.");
        }
    }
}
