using GlovobolieApp.Exceptions;
using GlovobolieApp.Models;
using GlovobolieApp.Services.AuthService;
using GlovobolieApp.Services.UserService;
using GlovobolieApp.Services.ValidationService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
	public class SignUpViewModel : BaseViewModel
	{
        private string _loginErrorMessage;
        private string _personalDataErrorMessage;

        public Command RegisterButtonCommand { get; }
        public Command BackButtonCommand { get; }
        public User UserDTO { get; set; }
        public string ConfirmPassword { get; set; }
        public string LoginErrorMessage { get => this._loginErrorMessage;
        set
            {
               SetProperty(
                    ref _loginErrorMessage,
                    value,
                    nameof(_loginErrorMessage),
                    () => OnPropertyChanged(nameof(HasLoginError)));
            }
        }
        public bool HasLoginError { get => this._loginErrorMessage != null; }
        // Personal Data
        public string PersonalDataErrorMessage { get => this._personalDataErrorMessage; 
            set
            {
                SetProperty(
                    ref _personalDataErrorMessage,
                    value,
                    nameof(_personalDataErrorMessage),
                    () => OnPropertyChanged(nameof(HasPersonalDataError))
                );
            }
        }
        public bool HasPersonalDataError { get => this._personalDataErrorMessage != null; }

        private IAuthService authService;
		public SignUpViewModel ()
		{
			this.Title = "Register";
            RegisterButtonCommand = new Command(this.OnRegisterPressed);
            BackButtonCommand = new Command(this.OnBackPressed);
            UserDTO = new User();
		}

        private async void OnRegisterPressed()
        {
            this.ValidateLoginInfo();
            this.ValidatePersonalInfo();
            if (this.HasLoginError || this.HasPersonalDataError)
            {
                this.ForceUpdateUI();
                return;
            }

            try
            {
                this.IsBusy = true;
                await Task.Run(() =>
                {
                    Thread.Sleep(1000);
                });
            } catch (Exception ex)
            {
                if (ex is ValidationException)
                {
                    _personalDataErrorMessage = ex.Message;
                }
                else
                {
                    throw ex;
                }
            } finally
            {
                this.IsBusy = false;
            }
        }

        private async void OnBackPressed() => await Shell.Current.Navigation.PopAsync();

        private void ValidateLoginInfo()
        {

            if (ValidationService.ValidateEmail(this.UserDTO.Email) != null)
            {
                this.LoginErrorMessage = ValidationService.ValidateEmail(this.UserDTO.Email);
                return;
            }
            if (ValidationService.ValidatePassword(this.UserDTO.Password) != null)
            {
                this.LoginErrorMessage = ValidationService.ValidatePassword(this.UserDTO.Password);
                return;
            }
            if (ValidationService.ValidatePasswordsMatch(this.UserDTO.Password, this.ConfirmPassword) != null)
            {
                this.LoginErrorMessage = ValidationService.ValidatePasswordsMatch(this.UserDTO.Password, this.ConfirmPassword);
                return;
            }
            this.LoginErrorMessage = null;
        }
        private void ValidatePersonalInfo() 
        {
            if (ValidationService.ValidateNonNullable(nameof(UserDTO.FirstName), UserDTO.FirstName) != null) {
                this.PersonalDataErrorMessage = ValidationService.ValidateNonNullable(nameof(UserDTO.FirstName), UserDTO.FirstName);
                return;
            }
            if (ValidationService.ValidateNonNullable(nameof(UserDTO.LastName), UserDTO.LastName) != null)
            {
                this.PersonalDataErrorMessage = ValidationService.ValidateNonNullable(nameof(UserDTO.LastName), UserDTO.LastName);
                return;
            }
            if (ValidationService.ValidateNonNullable(nameof(UserDTO.Country), UserDTO.Country) != null)
            {
                this.PersonalDataErrorMessage = ValidationService.ValidateNonNullable(nameof(UserDTO.Country), UserDTO.Country);
                return;
            }
            if (ValidationService.ValidateNonNullable(nameof(UserDTO.City), UserDTO.City) != null)
            {
                this.PersonalDataErrorMessage = ValidationService.ValidateNonNullable(nameof(UserDTO.City), UserDTO.City);
                return;
            }
            if (ValidationService.ValidateNonNullable(nameof(UserDTO.PhoneNumber), UserDTO.PhoneNumber) != null)
            {
                this.PersonalDataErrorMessage = ValidationService.ValidateNonNullable(nameof(UserDTO.PhoneNumber), UserDTO.PhoneNumber);
                return;
            }
            if (ValidationService.ValidateNonNullable(nameof(UserDTO.Address), UserDTO.Address) != null)
            {
                this.PersonalDataErrorMessage = ValidationService.ValidateNonNullable(nameof(UserDTO.Address), UserDTO.Address);
                return;
            }
            this.PersonalDataErrorMessage = null;
        }

        

        protected override void InitDependencies()
        {
            if (EnvConfig.CurrentEnvironment == EnvConfig.Env.TEST)
            {
                authService = DependencyService.Get<AuthServiceMock>();
            }
            else
            {
                authService = DependencyService.Get<AuthService>();
            }
        }
    }
}