using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SprintRepository : ISprintRepository
    {
        private readonly AppDbContext _context;

        public SprintRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sprint sprint)
        {
            _context.Sprints.Add(sprint);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existingSprint = await _context.Sprints.FindAsync(id);
            if (existingSprint != null)
            {
                _context.Sprints.Remove(existingSprint);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Sprint>> GetAllAsync()
        {
            return await _context.Sprints
                .Include(s => s.Tasks)
                .ToListAsync();
        }

        public async Task<Sprint?> GetByIdAsync(int id)
        {
            var sprint = await _context.Sprints
                .Include(s => s.Tasks)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sprint == null)
            {
                throw new Exception("Sprint not found");
            }

            return sprint;
        }

        public async Task UpdateAsync(Sprint sprint)
        {
            var existingSprint = _context.Sprints.Find(sprint.Id);

            if (existingSprint != null)
            {
                _context.Sprints.Update(existingSprint);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Sprint not found");
            }
        }
    }
}
