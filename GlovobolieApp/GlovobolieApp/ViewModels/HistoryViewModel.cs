using GlovobolieApp.Models;
using GlovobolieApp.Services;
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
        private SessionService _sessionService;
        private ObservableCollection<Order> _orders;
        public Command OnAppearingCommand { get; }
        public Command OnDisappearingCommand { get; }

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }
        public HistoryViewModel() : base()
        {
            this.Title = "My History";
            OnAppearingCommand = new Command(this.OnAppearing);
            OnDisappearingCommand = new Command(this.OnDisappearing);
        }

        private async void OnAppearing()
        {
            await Task.Run(this.LoadOrders);
        }
        private void OnDisappearing()
        {
            _orders = null;
        }

        private async void LoadOrders()
        {
            if (IsBusy) return;
            this.IsBusy = true;
            try
            {   
              Orders = new ObservableCollection<Order>(await this._orderService.GetAllOrdersByUserAsync(_sessionService.Data.Id));
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            } finally { this.IsBusy = false; }
        }
        protected override void InitDependencies()
        {
            this._sessionService = DependencyService.Get<SessionService>();
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
