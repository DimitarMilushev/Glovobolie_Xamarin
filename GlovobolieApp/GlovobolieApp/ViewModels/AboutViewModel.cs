using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public Dictionary<String, String> AboutInformation { get; private set; }

        public AboutViewModel() : base()
        {
            Title = "About";
            AboutInformation = new Dictionary<String, String>()
            {
                ["Name"] = "Dimitar Milushev",
                ["Phone Number"] = "088 721 7575",
                ["Occupation"] = "Student/Programmer-Analyst",
                ["Faculty Nr."] = "19251421043",
                ["Time spent"] = "About 4-5 days",
                ["Could've been better?"] = "Most likely",
                ["Name origin"] = "Glovo + what I got from the process",
                ["Current Environment"] = EnvConfig.CurrentEnvironment.ToString(),
            };
        }


        protected override void InitDependencies()
        {
        }
    }
}