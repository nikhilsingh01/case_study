using Ecom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Services
{
    internal interface IService
    {

        static Customer customer;
        public Customer Login();
        public void CreateProduct();
        public void GetAllProducts();

        public void CreateCustomer();

        public void DeleteProduct();

        public void DeleteCustomer();

        public void AddToCart();

        public void RemoveFromCart();

        public void GetAllFromCart();

        public void PlaceOrder();

        public void GetOrdersByCustomer();

        public Products FindProduct();
    }

}
