using Application.DTOs.Sprint;
using Application.Services.Sprint;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly ISprintService _sprintService;

        public SprintController(ISprintService sprintService)
        {
            _sprintService = sprintService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSprints()
        {
            var sprints = await _sprintService.GetAllSprintsAsync();
            return Ok(sprints);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSprintById(int id)
        {
            var sprint = await _sprintService.GetSprintByIdAsync(id);
            if (sprint == null)
            {
                return NotFound();
            }
            return Ok(sprint);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSprint([FromBody] SprintCreateDto sprint)
        {
            if (sprint == null)
            {
                return BadRequest();
            }
            await _sprintService.AddSprintAsync(sprint);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSprint(int id, [FromBody] SprintUpdateDto sprint)
        {
            if (sprint == null)
            {
                return BadRequest();
            }
            var existingSprint = await _sprintService.GetSprintByIdAsync(id);
            if (existingSprint == null)
            {
                return NotFound();
            }
            await _sprintService.UpdateSprintAsync(id, sprint);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSprint(int id)
        {
            var existingSprint = await _sprintService.GetSprintByIdAsync(id);
            if (existingSprint == null)
            {
                return NotFound();
            }
            await _sprintService.DeleteSprintAsync(id);
            return NoContent();
        }
    }
}
