using GlovobolieApp.Models;
using GlovobolieApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GlovobolieApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public PersonalData UserData { get; private set; }
        public Dictionary<String, String> PersonalInformation { get; private set; }
        public Dictionary<String, String> LoginInformation { get; private set; }

        private SessionService sessionService;
        public ProfileViewModel() :base()
        {
            this.UserData = sessionService.Data.PersonalData;
            this.PersonalInformation = GetMappedPersonalInformation();
            this.LoginInformation = GetMappedLoginInformation();
        }
        private Dictionary<String, String> GetMappedPersonalInformation()
        {
            return new Dictionary<String, String>
            {
                ["First Name"] = UserData.FirstName,
                ["Last Name"] = UserData.LastName,
                ["Country"] = UserData.Country,
                ["City"] = UserData.City,
                ["Phone Number"] = UserData.PhoneNumber,
                ["Address"] = UserData.Address,
            };
        }
        private Dictionary<String, String> GetMappedLoginInformation()
        {
            return new Dictionary<String, String>
            {
                ["Email"] = sessionService.Data.Email
            };
        }
        protected override void InitDependencies()
        {
            this.sessionService = DependencyService.Get<SessionService>();
        }
    }
}
