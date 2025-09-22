using Application.DTOs.Sprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Sprint
{
    public interface ISprintService
    {
        Task<IEnumerable<SprintDto>> GetAllSprintsAsync();
        Task<SprintDto?> GetSprintByIdAsync(int id);
        System.Threading.Tasks.Task AddSprintAsync(SprintCreateDto dto);
        System.Threading.Tasks.Task UpdateSprintAsync(int sprintId, SprintUpdateDto sprint);
        System.Threading.Tasks.Task DeleteSprintAsync(int id);
    }
}
