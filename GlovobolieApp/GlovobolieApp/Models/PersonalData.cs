using System;
using System.Collections.Generic;
using System.Text;

namespace GlovobolieApp.Models
{
    public class PersonalData
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }
        public PersonalData(string firstName, string lastName, string address, string phoneNumber, string country, string city)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.Country = country;
            this.City = city;
        }
    }
}
