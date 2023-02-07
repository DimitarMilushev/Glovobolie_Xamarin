using GlovobolieApp.Models;
using GlovobolieApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        public Command CheckoutCommand { get; }
        public ObservableCollection<Product> Products { get; set; }
        private SessionService sessionService;
        public CartViewModel() : base()
        {
            this.Title = "My Cart";
            this.Products = new ObservableCollection<Product>(sessionService.Data.Cart);
            CheckoutCommand = new Command(this.OnCheckoutTapped);
        }

        private async void OnCheckoutTapped()
        {
            this.IsBusy = true;
            try
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    sessionService.Data.Cart.Clear();
                });
                await Application.Current.MainPage.Navigation.PopAsync();
            } catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayToastAsync("Failed to checkout :(", 2000);
            }
            finally { this.IsBusy = false; }
        }
        
        protected override void InitDependencies()
        {
            this.sessionService = DependencyService.Get<SessionService>();
        }
    }
}
