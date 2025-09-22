using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.User
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async System.Threading.Tasks.Task AddUserAsync(Domain.Entities.User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async System.Threading.Tasks.Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        public async Task<IEnumerable<Domain.Entities.User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<Domain.Entities.User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async System.Threading.Tasks.Task UpdateUserAsync(Domain.Entities.User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }
    }
}
