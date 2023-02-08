using GlovobolieApp.Models;
using GlovobolieApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private ObservableCollection<Product> products;
        public bool HasItems { get => this.ItemsCount > 0; }
        public double Total { get => this.Products.Sum(x => x.Price * x.Quantity); }
        public int ItemsCount { get => this.Products.Sum(x => x.Quantity); }
        public Command CheckoutCommand { get; }
        public Command RemoveItemCommand { get; }
        public ObservableCollection<Product> Products 
        { 
            get => this.products;
            set 
            {
                SetProperty(ref this.products, value, nameof(Products), () =>
                {
                    OnPropertyChanged(nameof(Total));
                    OnPropertyChanged(nameof(ItemsCount));
                    OnPropertyChanged(nameof(HasItems));
                });
            }
        }
        private SessionService sessionService;
        public CartViewModel() : base()
        {
            this.Title = "My Cart";
            this.Products = new ObservableCollection<Product>(sessionService.Data.GetProducts());
            CheckoutCommand = new Command(this.OnCheckoutTapped);
            RemoveItemCommand = new Command<Product>(this.OnRemoveItemTapped);
        }

        private void OnRemoveItemTapped(Product product)
        {
            if (product.Quantity > 1)
                this.DecrementItemCount(product);
            else
                this.RemoveItem(product);

            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(ItemsCount));
            OnPropertyChanged(nameof(HasItems));
        }
        private void RemoveItem(Product product)
        {
            this.Products.Remove(product);
            this.sessionService.Data.RemoveFromCart(product);
        }
        private void DecrementItemCount(Product product)
        {
            var itemIndex = this.Products.IndexOf(product);
            --product.Quantity;
            this.Products[itemIndex] = product;
            this.sessionService.Data.UpdateCartItem(product);
        }
        private async void OnCheckoutTapped()
        {
            this.IsBusy = true;
            try
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    sessionService.Data.EmptyCart();
                });
                await Application.Current.MainPage.Navigation.PopAsync();
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
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
