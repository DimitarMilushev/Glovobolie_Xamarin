using GlovobolieApp.Artifacts.ProductService;
using GlovobolieApp.Artifacts.RepositoryService;
using GlovobolieApp.Services;
using GlovobolieApp.Services.AuthService;
using GlovobolieApp.Services.OrderService;
using GlovobolieApp.Services.ProductService;
using GlovobolieApp.Services.UserService;
using GlovobolieApp.Views;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GlovobolieApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            // Environment insensitive
            DependencyService.RegisterSingleton<SessionService>(SessionService.Instance);

            //TODO: Change based on environment
            if (EnvConfig.CurrentEnvironment == EnvConfig.Env.TEST)
            {
                DependencyService.Register<ProductServiceMock>();
                DependencyService.Register<AuthServiceMock>();
                DependencyService.Register<OrderServiceMock>();
            }
            else
            {
                DependencyService.RegisterSingleton<Repository>(new Repository());
                DependencyService.Register<AuthService>();
                DependencyService.Register<ProductService>();
                DependencyService.Register<OrderService>();
            }
            MainPage = new AppShell();
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
