using Ecom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Repository
{
    internal interface IOrderProcessorRepository 
    {


        public Products FindProduct(int productId);
        public Customer CheckLoginInDatabase(string email, string password);
        public bool CreateProduct(Products product);
        List<Products> GetAllProducts();

        public bool CreateCustomer(Customer customer);

        public bool DeleteProduct(int productid);

        public bool DeleteCustomer(int customerid);

        public bool AddToCart(Customer customer, Products product, int quantity);

        public bool RemoveFromCart(Customer customer, Products product);

        public List<Products> GetAllFromCart(Customer customer);

        public bool PlaceOrder(Customer customer, List<Tuple<Products,int>> productsAndQuantities, string shippingAddress);

        public List<Tuple<Products, int>> GetOrdersByCustomer(int customerid);
        
    }
}
