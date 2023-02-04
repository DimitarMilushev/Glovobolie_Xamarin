using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel() : base()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }

        protected override void InitDependencies()
        {
        }
    }
}