using GlovobolieApp.Models;
using GlovobolieApp.Services;
using GlovobolieApp.Services.AuthService;
using GlovobolieApp.Services.UserService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public Command LogoutCommand { get;  }
        public PersonalData PersonalData 
        {
            get => _personalData;
            private set
            {
                SetProperty(ref _personalData, value, nameof(PersonalData), () => {
                    OnPropertyChanged(nameof(PersonalInformation));
                    OnPropertyChanged(nameof(LoginInformation));
                });
            }
        }
        public Dictionary<String, String> PersonalInformation { get => GetMappedPersonalInformation(); }
        public Dictionary<String, String> LoginInformation { get => GetMappedLoginInformation(); }
        public Command OnAppearingCommand { get; }
        public Command OnDisappearingCommand { get; }

        private PersonalData _personalData;
        private SessionService _sessionService;
        private IAuthService _authService;
        public ProfileViewModel() :base()
        {
            this.Title = "My Profile";
            this.LogoutCommand = new Command(OnLogoutTapped);
            this.OnAppearingCommand = new Command(OnAppearing);
            this.OnDisappearingCommand = new Command(OnDisappearing);
            this.PersonalData = _sessionService.Data.PersonalData;
        }
        private void OnAppearing()
        {
           this.PersonalData = _sessionService.Data.PersonalData;
        }
        // Dispose without trigerring rebuild
        private void OnDisappearing()
        {
            this._personalData = null;
        }

        private async void OnLogoutTapped() => await _authService.LogoutAsync();

        private Dictionary<String, String> GetMappedPersonalInformation()
        {
            if (_personalData == null) return new Dictionary<String, String>();
            return new Dictionary<String, String>
            {
                ["First Name"] = PersonalData.FirstName,
                ["Last Name"] = PersonalData.LastName,
                ["Country"] = PersonalData.Country,
                ["City"] = PersonalData.City,
                ["Phone Number"] = PersonalData.PhoneNumber,
                ["Address"] = PersonalData.Address,
            };
        }
        private Dictionary<String, String> GetMappedLoginInformation()
        {
            if (_personalData == null) return new Dictionary<String, String>();
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
