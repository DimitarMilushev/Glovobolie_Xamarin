using GlovobolieApp.Exceptions;
using GlovobolieApp.Models;
using GlovobolieApp.Services.UserService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GlovobolieApp.Services.AuthService
{
    class AuthServiceMock : IAuthService
    {
        public async Task LoginAsync(string email, string password)
        {
            if (!(await CheckCredentialsAsync(email, password)))
            {
                throw new ValidationException("Credentials do not match!");
            }
            var data = await GetUserDataAsync(email);
            var personalData = new PersonalData(data.FirstName, data.LastName, data.Address, data.PhoneNumber, data.Country, data.City);
            DependencyService.Get<SessionService>().UpdateSession(data.Id, data.Email, personalData);
        }
        public async Task SignUpAsync(string email, string password, User data)
        {
            if (await CheckEmailAsync(email))
            {
                throw new ValidationException("User already exists!");
            }
        }
        public Task<bool> CheckCredentialsAsync(string email, string password) =>
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                return (email == "test@test.com" && password == "Tester01");
            });
        public Task<bool> CheckEmailAsync(string email) =>
            Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    return email == "test@test.com";
                });

        public Task<User> GetUserDataAsync(string email)
        => Task.Run(() => new User {
            Id = 1,
            Email = email,
            FirstName = "John",
            LastName = "Doe",
            City = "Blagoevgrad",
            Address = "Bul. Sv. sv. Kiril & Metodii 17",
            PhoneNumber = "0888 777 7777",
            Country = "Bulgaria"
        });

        public async Task LogoutAsync()
        {
            DependencyService.Get<SessionService>().DisposeSession();
            await Shell.Current.GoToAsync("//Login");
        }
    }
}
