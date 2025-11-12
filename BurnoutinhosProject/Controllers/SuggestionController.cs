using BurnoutinhosProject.Models;
using BurnoutinhosProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace BurnoutinhosProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuggestionController : ControllerBase
    {
        private readonly SuggestionService suggestionService;

        public SuggestionController(SuggestionService suggestionService)
        {
            this.suggestionService = suggestionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suggestion>>> GetSuggestions()
        {
            var suggestions = await suggestionService.GetAllAsync();
            return Ok(suggestions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Suggestion>> GetSuggestionById(int id)
        {
            var suggestion = await suggestionService.GetByIdAsync(id);
            if (suggestion == null)
            {
                return NotFound();
            }
            return Ok(suggestion);
        }

        [HttpPost]
        public async Task<ActionResult<Suggestion>> CreateSuggestion([FromBody] Suggestion suggestion)
        {
            var createdSuggestion = await suggestionService.AddAsync(suggestion);
            return CreatedAtAction(nameof(GetSuggestionById), new { id = createdSuggestion.Id }, createdSuggestion);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Suggestion>> UpdateSuggestion(int id, [FromBody] Suggestion suggestion)
        {
            if (id != suggestion.Id)
            {
                return BadRequest("Ids incongruentes.");
            }
            var updatedSuggestion = await suggestionService.UpdateAsync(suggestion);
            if (updatedSuggestion == null)
            {
                return NotFound();
            }
            return Ok(updatedSuggestion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuggestion(int id)
        {
            var deleted = await suggestionService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
