using GlovobolieApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GlovobolieApp.Services.UserService
{
    public class AuthService : DataServiceBase, IAuthService
    {
        public Task<bool> CheckCredentials(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<PersonalData> GetPersonalDataAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task SignUpAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
