using BurnoutinhosProject.Models;
using BurnoutinhosProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace BurnoutinhosProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeBlockController : ControllerBase
    {
        private readonly TimeBlockService _timeBlockService;

        public TimeBlockController(TimeBlockService timeBlockService)
        {
            _timeBlockService = timeBlockService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeBlock>>> GetTimeBlocks()
        {
            var timeBlocks = await _timeBlockService.GetAllAsync();
            return Ok(timeBlocks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeBlock>> GetTimeBlock(int id)
        {
            var timeBlock = await _timeBlockService.GetByIdAsync(id);
            if (timeBlock == null)
            {
                return NotFound();
            }
            return Ok(timeBlock);
        }

        [HttpPost]
        public async Task<ActionResult<TimeBlock>> CreateTimeBlock([FromBody] TimeBlock timeBlock)
        {
            var createdTimeBlock = await _timeBlockService.AddAsync(timeBlock);
            return CreatedAtAction(nameof(GetTimeBlock), new { id = createdTimeBlock.Id }, createdTimeBlock);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TimeBlock>> UpdateTimeBlock(int id, [FromBody] TimeBlock timeBlock)
        {
            if (id != timeBlock.Id)
            {
                return BadRequest();
            }
            var updatedTimeBlock = await _timeBlockService.UpdateAsync(timeBlock);
            if (updatedTimeBlock == null)
            {
                return NotFound();
            }
            return Ok(updatedTimeBlock);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeBlock(int id)
        {
            var deleted = await _timeBlockService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
