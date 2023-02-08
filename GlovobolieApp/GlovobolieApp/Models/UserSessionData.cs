using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Internals;

namespace GlovobolieApp.Models
{
    public class UserSessionData
    {
        public int Id { get; }
        public string Email { get; }
        public PersonalData PersonalData { get; }
        private ISet<Product> Cart { get; }

        public UserSessionData(int id, string email, ISet<Product> cart, PersonalData personalData)
        {
            Id = id;
            Email = email;
            Cart = cart;
            PersonalData = personalData;
        }

        public ICollection<Product> GetProducts() => this.Cart.ToList();
        public void AddToCart(Product product)
        {
            bool alreadyExists = !this.Cart.Add(product);
            if (alreadyExists) 
            {
                var itemRef = this.Cart.First(x => x.Id == product.Id);
                ++itemRef.Quantity;
            }
        }

        public void RemoveFromCart(Product product)
        {
            if (this.Cart.Contains(product))
            {
                this.Cart.Remove(product);
            }
        }
        public void UpdateCartItem(Product product)
        {
            if (this.Cart.Contains(product))
            {
                var itemRef = this.Cart.First(x => x.Id == product.Id);
                itemRef = product;
            }
        }
        public int CartItemsCount => this.Cart.Count;
        public void EmptyCart() => this.Cart.Clear();
    }
}
