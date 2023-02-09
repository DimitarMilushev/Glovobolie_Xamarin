using GlovobolieApp.Artifacts.ProductService;
using GlovobolieApp.Models;
using GlovobolieApp.Services;
using GlovobolieApp.Services.ProductService;
using GlovobolieApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private Product _selectedItem;
        private int _cartItemsCount;
        public ObservableCollection<Product> Items { get; }
        public Command LoadProductsCommand { get; } 
        public Command AddProductCommand { get; }
        public Command<Product> ItemTapped { get; }
        public Command DismissPopupCommand { get; }
        public Command ClosePopupTapped { get; }
        public Command GoToCartTapped { get; }
        public Command OnAppearingCommand { get; }

        private IProductService productService;
        private SessionService sessionService;
        public ProductsViewModel() : base()
        {
            Title = "Products";
            Items = new ObservableCollection<Product>();
            LoadProductsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Product>(OnItemSelected);
            DismissPopupCommand = new Command(this.DismissProductPopup);
            ClosePopupTapped = new Command<object>(this.OnCloseModalTapped);
            GoToCartTapped = new Command(this.OnGoToCartTapped);
            AddProductCommand = new Command(OnAddItem);
            OnAppearingCommand = new Command(this.OnAppearing);
            Task.Run(this.ExecuteLoadItemsCommand);
        }

        public int CartItemsCount { 
            get => this._cartItemsCount;
            set 
            {
                SetProperty(
                   ref _cartItemsCount,
                   value,
                   nameof(CartItemsCount),
                   () => OnPropertyChanged(nameof(HasCartItems))
               );
            }
        }
        public bool HasCartItems { get => this._cartItemsCount > 0; }
        private async void OnGoToCartTapped() => await Shell.Current.GoToAsync(nameof(CartPage));

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                Items.Clear();
                var newItems = await productService.GetProductsAsync();
                foreach (var item in newItems)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Application.Current.MainPage.DisplayToastAsync(ex.Message, 2000);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Product SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(
                    ref _selectedItem, 
                    value, 
                    nameof(SelectedItem), 
                    () => OnPropertyChanged(nameof(HasSelectedItem))
                );
            }
        }
        public bool HasSelectedItem { get => this._selectedItem != null; }

        private async void OnAddItem()
        {
            var currentPage = Application.Current.MainPage;

            if (this.SelectedItem == null)
            {
                this.DismissProductPopup();
                await currentPage.DisplayToastAsync("Failed to add item to cart :(", 2000);
                return;
            }
           this.sessionService.Data.AddToCart(this.SelectedItem);
          
            this.DismissProductPopup();
            this.CartItemsCount = this.sessionService.Data.GetProducts().Sum(x => x.Quantity);
        }
        private void DismissProductPopup()
        {
            this.SelectedItem = null;
            Debug.WriteLine("Dismissed product popup");
        }
        // Animates closing of the pop-up
        private async void OnCloseModalTapped(object sender)
        {
            var button = sender as ImageButton;
            await button.RotateTo(180, 500, Easing.CubicInOut);
            this.DismissProductPopup();
        }

        void OnItemSelected(Product item)
        {
            this.SelectedItem = item;
            Debug.WriteLine($"Selected item {item.Title}");
        }
        public void OnAppearing()
        {
            this.CartItemsCount = this.sessionService.Data.GetProducts().Sum(x => x.Quantity);
        }

        protected override void InitDependencies()
        {
            sessionService = DependencyService.Get<SessionService>();
            if (EnvConfig.CurrentEnvironment == EnvConfig.Env.TEST)
            {
                productService = DependencyService.Get<ProductServiceMock>();
            }
            else
            {
                productService = DependencyService.Get<ProductService>();
            }
        }
    }
}