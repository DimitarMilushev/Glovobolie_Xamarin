using GlovobolieApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GlovobolieApp.Services.UserService
{
    interface IAuthService
    {
        Task LoginAsync(string email, string password);
        Task SignUpAsync(string email, string password, User data);
        Task<bool> CheckCredentialsAsync(string email, string password);
        Task<bool> CheckEmailAsync(string email);
        Task<User> GetUserDataAsync(string email);
        Task LogoutAsync();
    }
}
