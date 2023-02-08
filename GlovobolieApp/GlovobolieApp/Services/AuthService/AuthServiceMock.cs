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
            if (!(await CheckCredentials(email, password)))
            {
                throw new ValidationException("Credentials do not match!");
            }
            var data = await GetPersonalDataAsync(email);

            DependencyService.Get<SessionService>().UpdateSession(1, email, data);
        }
        public async Task SignUpAsync(string email, string password, PersonalData data)
        {
            if (await CheckEmail(email))
            {
                throw new ValidationException("User already exists!");
            }
        }
        public Task<bool> CheckCredentials(string email, string password) =>
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                return email == "test@test.com" && password == "Tester01";
            });
        public Task<bool> CheckEmail(string email) =>
            Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    return email == "test@test.com";
                });

        public Task<PersonalData> GetPersonalDataAsync(string email)
        => Task.Run(() => new PersonalData("John", "Doe", "Blagoevgrad, Bul. Sv. sv. Kiril & Metodii 17", "0888 777 7777", "Bulgaria", "Blagoevgrad"));

        public async Task LogoutAsync()
        {
            DependencyService.Get<SessionService>().DisposeSession();
            await Shell.Current.GoToAsync("//Login");
        }
    }
}
