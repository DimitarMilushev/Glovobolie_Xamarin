using GlovobolieApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace GlovobolieApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}