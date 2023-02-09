using GlovobolieApp.Models;
using GlovobolieApp.Services.OrderService;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private IOrderService _orderService;
        private ObservableCollection<Order> _orders;
        public Command RefreshOrdersCommand { get; }
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }
        public HistoryViewModel() : base()
        {
            this.Title = "My History";
            this.RefreshOrdersCommand = new Command(this.ReloadOrders);
            Task.Run(this.LoadOrders);
        }
        private async void LoadOrders()
        {
            this.IsBusy = true;
            try
            {   
              Orders = new ObservableCollection<Order>(await this._orderService.GetAllOrdersAsync());
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            } finally { this.IsBusy = false; }
        }
        private void ReloadOrders()
        {
            // Return if already loading
            if (this.IsBusy) return;

            Orders.Clear();
            SetProperty(ref _orders, Orders);
            this.LoadOrders();
        }
        protected override void InitDependencies()
        {
            if (EnvConfig.CurrentEnvironment == EnvConfig.Env.TEST)
            {
                _orderService = DependencyService.Get<OrderServiceMock>();
            } else
            {
                _orderService = DependencyService.Get<OrderService>();
            }
        }
    }
}
