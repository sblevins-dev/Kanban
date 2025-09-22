using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto?> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskCreateDto dto);
        Task UpdateTaskAsync(int taskId, TaskUpdateDto task);
        Task DeleteTaskAsync(int id);
    }
}
