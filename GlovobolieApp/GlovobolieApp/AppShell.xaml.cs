using GlovobolieApp.ViewModels;
using GlovobolieApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GlovobolieApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));

            Routing.RegisterRoute(nameof(ProductsListPage), typeof(ProductsListPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
            Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));
        }

    }
}
