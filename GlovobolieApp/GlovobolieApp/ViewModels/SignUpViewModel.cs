using GlovobolieApp.Exceptions;
using GlovobolieApp.Models;
using GlovobolieApp.Services.AuthService;
using GlovobolieApp.Services.UserService;
using GlovobolieApp.Services.ValidationService;
using GlovobolieApp.Views;
using System;

using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
	public class SignUpViewModel : BaseViewModel
	{
        private string _loginErrorMessage;
        private string _personalDataErrorMessage;

        public Command RegisterButtonCommand { get; }
        public Command BackButtonCommand { get; }
        public string Email { get; set; }
        public string Password { get; set; }
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

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Address{ get; set; }

        private IAuthService authService;
		public SignUpViewModel ()
		{
			this.Title = "Register";
            RegisterButtonCommand = new Command(this.OnRegisterPressed);
            BackButtonCommand = new Command(this.OnBackPressed);
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
                await authService.SignUpAsync(Email, Password, new User
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Address = Address,
                    PhoneNumber = PhoneNumber,
                    Country = Country,
                    City = City
                });
                await Shell.Current.GoToAsync(nameof(LoginPage));
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

            if (ValidationService.ValidateEmail(this.Email) != null)
            {
                this.LoginErrorMessage = ValidationService.ValidateEmail(this.Email);
                return;
            }
            if (ValidationService.ValidatePassword(this.Password) != null)
            {
                this.LoginErrorMessage = ValidationService.ValidatePassword(this.Password);
                return;
            }
            if (ValidationService.ValidatePasswordsMatch(this.Password, this.ConfirmPassword) != null)
            {
                this.LoginErrorMessage = ValidationService.ValidatePasswordsMatch(this.Password, this.ConfirmPassword);
                return;
            }
            this.LoginErrorMessage = null;
        }
        private void ValidatePersonalInfo() 
        {
            if (ValidationService.ValidateNonNullable(nameof(FirstName), FirstName) != null) {
                this.PersonalDataErrorMessage = ValidationService.ValidateNonNullable(nameof(FirstName), FirstName);
                return;
            }
            if (ValidationService.ValidateNonNullable(nameof(LastName), LastName) != null)
            {
                this.PersonalDataErrorMessage = ValidationService.ValidateNonNullable(nameof(LastName), LastName);
                return;
            }
            if (ValidationService.ValidateNonNullable(nameof(Country), Country) != null)
            {
                this.PersonalDataErrorMessage = ValidationService.ValidateNonNullable(nameof(Country), Country);
                return;
            }
            if (ValidationService.ValidateNonNullable(nameof(City), City) != null)
            {
                this.PersonalDataErrorMessage = ValidationService.ValidateNonNullable(nameof(City), City);
                return;
            }
            if (ValidationService.ValidateNonNullable(nameof(PhoneNumber), PhoneNumber) != null)
            {
                this.PersonalDataErrorMessage = ValidationService.ValidateNonNullable(nameof(PhoneNumber), PhoneNumber);
                return;
            }
            if (ValidationService.ValidateNonNullable(nameof(Address), Address) != null)
            {
                this.PersonalDataErrorMessage = ValidationService.ValidateNonNullable(nameof(Address), Address);
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