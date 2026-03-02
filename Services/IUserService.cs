using LaundryApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryApplication.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(User user);
    }
}
