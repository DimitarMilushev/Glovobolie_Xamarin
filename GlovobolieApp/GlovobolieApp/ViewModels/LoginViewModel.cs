using GlovobolieApp.Exceptions;
using GlovobolieApp.Services.AuthService;
using GlovobolieApp.Services.UserService;
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
        private async void OnLoginClicked(object obj)
        {

            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            try
            {
                this.IsBusy = true;
                await authService.LoginAsync(Title, Password);
                await Shell.Current.GoToAsync(nameof(ProductsListPage));
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
