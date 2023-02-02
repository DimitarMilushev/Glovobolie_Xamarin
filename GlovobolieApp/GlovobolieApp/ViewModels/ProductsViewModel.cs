using GlovobolieApp.Models;
using GlovobolieApp.Services;
using GlovobolieApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private Product _selectedItem;

        public ObservableCollection<Product> Items { get; }
        public Command LoadProductsCommand { get; }
        public Command AddProductCommand { get; }
        public Command<Product> ItemTapped { get; }
        public ProductsViewModel()
        {
            Title = "Products";
            Items = new ObservableCollection<Product>();
            LoadProductsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Product>(OnItemSelected);

            AddProductCommand = new Command(OnAddItem);
           
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Product SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Product item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}