using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISprintRepository
    {
        Task<IEnumerable<Sprint>> GetAllAsync();
        Task<Sprint?> GetByIdAsync(int id);
        Task AddAsync(Sprint sprint);
        Task UpdateAsync(Sprint sprint);
        Task DeleteAsync(int id);
    }
}
