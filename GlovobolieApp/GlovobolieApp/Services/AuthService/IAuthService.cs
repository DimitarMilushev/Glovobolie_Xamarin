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
        Task SignUpAsync(string email, string password);
        Task<bool> CheckCredentials(string email, string password);
        Task<bool> CheckEmail(string email);
        Task<PersonalData> GetPersonalDataAsync(string email);
    }
}
