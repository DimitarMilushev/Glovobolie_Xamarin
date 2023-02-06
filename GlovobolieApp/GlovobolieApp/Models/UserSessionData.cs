using System;
using System.Collections.Generic;
using System.Text;

namespace GlovobolieApp.Models
{
    public class UserSessionData
    {
        public int Id { get; }
        public string Email { get; }
        public PersonalData PersonalData { get; }
        public ICollection<Product> Cart { get; }

        public UserSessionData(int id, string email, ICollection<Product> cart, PersonalData personalData)
        {
            Id = id;
            Email = email;
            Cart = cart;
            PersonalData = personalData;
        }
    }
}
