using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(UserId id);
        Task<User> GetByEmailAsync(Email email);
        Task<User> GetByUsernameAsync(Username username);
        Task AddAsync(User user);
    }
}
