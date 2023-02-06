using GlovobolieApp.Services.AuthService;
using GlovobolieApp.Services.UserService;
using GlovobolieApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
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
            this.IsBusy = true;
            await Task.Run(() => {
                Thread.Sleep(2000);
                this.IsBusy = false;
            });
            //await Shell.Current.GoToAsync(nameof(ProductsListPage));
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
