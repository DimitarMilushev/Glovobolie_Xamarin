using GlovobolieApp.Models;
using GlovobolieApp.Services;
using GlovobolieApp.Services.AuthService;
using GlovobolieApp.Services.UserService;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public Command LogoutCommand { get;  }
        public PersonalData UserData { get; private set; }
        public Dictionary<String, String> PersonalInformation { get; private set; }
        public Dictionary<String, String> LoginInformation { get; private set; }

        private SessionService _sessionService;
        private IAuthService _authService;
        public ProfileViewModel() :base()
        {
            this.Title = "My Profile";
            this.UserData = _sessionService.Data.PersonalData;
            this.PersonalInformation = GetMappedPersonalInformation();
            this.LoginInformation = GetMappedLoginInformation();
            this.LogoutCommand = new Command(OnLogoutTapped);
        }

        private async void OnLogoutTapped() => await _authService.LogoutAsync();

        private Dictionary<String, String> GetMappedPersonalInformation()
        {
            return new Dictionary<String, String>
            {
                ["First Name"] = UserData.FirstName,
                ["Last Name"] = UserData.LastName,
                ["Country"] = UserData.Country,
                ["City"] = UserData.City,
                ["Phone Number"] = UserData.PhoneNumber,
                ["Address"] = UserData.Address,
            };
        }
        private Dictionary<String, String> GetMappedLoginInformation()
        {
            return new Dictionary<String, String>
            {
                ["Email"] = _sessionService.Data.Email
            };
        }
        protected override void InitDependencies()
        {
            this._sessionService = DependencyService.Get<SessionService>();
            if (EnvConfig.CurrentEnvironment == EnvConfig.Env.TEST)
            {
                this._authService= DependencyService.Get<AuthServiceMock>();
            } else
            {
                this._authService = DependencyService.Get<AuthService>();
            }
        }
    }
}
