using Application.DTOs.Sprint;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Sprint
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepository _sprintRepository;

        public SprintService(ISprintRepository sprintRepository)
        {
            _sprintRepository = sprintRepository;
        }

        public async System.Threading.Tasks.Task AddSprintAsync(SprintCreateDto dto)
        {
            await _sprintRepository.AddAsync(new Domain.Entities.Sprint
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            });
        }

        public async System.Threading.Tasks.Task DeleteSprintAsync(int id)
        {
            await _sprintRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SprintDto>> GetAllSprintsAsync()
        {
            var sprints =  await _sprintRepository.GetAllAsync();
            return sprints.Select(s => new SprintDto
            {
                Id = s.Id,
                Name = s.Name,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Tasks = s.Tasks?.Select(t => new Application.DTOs.Task.TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status.ToString(),
                    CreatedAt = t.CreatedAt,
                    SprintId = t.SprintId,
                    Assignee = t.Assignee != null ? new Application.DTOs.AssigneeDto
                    {
                        Id = t.Assignee.Id,
                        Username = t.Assignee.Username
                    } : null
                }).ToList()
            });
        }

        public async Task<SprintDto?> GetSprintByIdAsync(int id)
        {
            var sprint =  await _sprintRepository.GetByIdAsync(id);
            if (sprint == null) return null;
            return new SprintDto
            {
                Id = sprint.Id,
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                Tasks = sprint.Tasks?.Select(t => new Application.DTOs.Task.TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status.ToString(),
                    CreatedAt = t.CreatedAt,
                    SprintId = t.SprintId,
                    Assignee = t.Assignee != null ? new Application.DTOs.AssigneeDto
                    {
                        Id = t.Assignee.Id,
                        Username = t.Assignee.Username
                    } : null
                }).ToList()
            };
        }

        public async System.Threading.Tasks.Task UpdateSprintAsync(int sprintId, SprintUpdateDto sprint)
        {
            var existingSprint = await _sprintRepository.GetByIdAsync(sprintId);
            if (existingSprint == null)
            {
                throw new KeyNotFoundException($"Sprint with ID {sprintId} not found.");
            }
            existingSprint.Name = sprint.Name;
            existingSprint.StartDate = sprint.StartDate;
            existingSprint.EndDate = sprint.EndDate;
            await _sprintRepository.UpdateAsync(existingSprint);
        }
    }
}
