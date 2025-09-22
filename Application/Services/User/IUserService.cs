using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.User
{
    public interface IUserService
    {
        System.Threading.Tasks.Task AddUserAsync(Domain.Entities.User user);
        System.Threading.Tasks.Task<Domain.Entities.User?> GetUserByIdAsync(int id);
        System.Threading.Tasks.Task<Domain.Entities.User?> GetUserByUsernameAsync(string username);
        System.Threading.Tasks.Task<IEnumerable<Domain.Entities.User>> GetAllUsersAsync();
        System.Threading.Tasks.Task UpdateUserAsync(Domain.Entities.User user);
        System.Threading.Tasks.Task DeleteUserAsync(int id);
    }
}
