using System;
using System.Collections.Generic;
using System.Text;

namespace GlovobolieApp.Models
{
    public class PersonalData
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public PersonalData(string FirstName, string LastName, string Address) {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
        }
    }
}
