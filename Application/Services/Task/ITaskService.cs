using Application.DTOs.Task;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Task
{
    // Rename the namespace to avoid conflict with System.Threading.Tasks.Task
    // Suggest renaming to Application.Services.Tasks (plural)
    public interface ITaskService
    {
        System.Threading.Tasks.Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        System.Threading.Tasks.Task<TaskDto?> GetTaskByIdAsync(int id);
        System.Threading.Tasks.Task AddTaskAsync(TaskCreateDto dto);
        System.Threading.Tasks.Task UpdateTaskAsync(int taskId, TaskUpdateDto task);
        System.Threading.Tasks.Task DeleteTaskAsync(int id);
    }
}
