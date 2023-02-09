using GlovobolieApp.Exceptions;
using GlovobolieApp.Services.AuthService;
using GlovobolieApp.Services.UserService;
using GlovobolieApp.Services.ValidationService;
using GlovobolieApp.Views;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _errorMessage;

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool HasError { get => _errorMessage != null; }
        public string ErrorMessage 
        { 
            get => _errorMessage;
            set
            {
                SetProperty(
                    ref _errorMessage,
                    value,
                    nameof(ErrorMessage),
                    () => OnPropertyChanged(nameof(HasError))
                );
            }
        }
        public Command SignUpCommand { get; }
        public Command LoginCommand { get; }

        private IAuthService authService;
        public LoginViewModel() : base()
        {
            LoginCommand = new Command(OnLoginClicked);
            SignUpCommand = new Command(OnSignUpClicked);
        }

        private async void OnSignUpClicked() => await Shell.Current.GoToAsync(nameof(SignUpPage));
        private async void OnLoginClicked()
        {
            if (this.ErrorMessage != null)
            {
                this.ForceUpdateUI();
                return;
            }
            try
            {
                this.IsBusy = true;
                await authService.LoginAsync(UserName, Password);
                await Shell.Current.GoToAsync($"//Products");
            }
            catch (Exception ex)
            {
                if (ex is ValidationException)
                {
                    ErrorMessage = ex.Message;
                }
                else
                {
                    throw ex;
                }
            }
            finally 
            {
                this.IsBusy = false;
            }
        }
        private void ValidateForm()
        {
            if (ValidationService.ValidateEmail(UserName) != null)
            {
                this.ErrorMessage = ValidationService.ValidateEmail(UserName);
                return;
            }
            if (ValidationService.ValidatePassword(Password) != null)
            {
                this.ErrorMessage = ValidationService.ValidatePassword(Password);
                return;
            }
            this.ErrorMessage = null;
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
