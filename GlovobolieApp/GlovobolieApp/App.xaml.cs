using GlovobolieApp.Artifacts.ProductService;
using GlovobolieApp.Artifacts.RepositoryService;
using GlovobolieApp.Services;
using GlovobolieApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GlovobolieApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.RegisterSingleton<Repository>(new Repository());
            DependencyService.Register<ProductService>();
            MainPage = new AppShell();
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
