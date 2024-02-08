using Ecom.Exceptions;
using Ecom.Model;
using Ecom.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Services
{
    internal class ServiceImplemetation : IService
    {
        IOrderProcessorRepository repository = new OrderProcessorRepositoryImpl();




        Customer customer;


        public Customer Login()
        {
            try
            {
                Console.Write("Enter customer email: ");
                string email = Console.ReadLine();

                Console.Write("Enter password: ");
                string password=Console.ReadLine();

                Customer loggedInCustomer = repository.CheckLoginInDatabase(email, password);

                if (loggedInCustomer != null)
                {
                    Console.WriteLine("Login successful.");
                    Console.WriteLine($"Customer Details: {loggedInCustomer.Name}");
                    // Now you can use loggedInCustomer for future use
                    customer= loggedInCustomer;
                    return loggedInCustomer;
                }
                else { return null; }
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

      

        public void GetAllProducts()
        {
            List<Products> allProducts = repository.GetAllProducts();
            foreach (Products product in allProducts)
            {
                Console.WriteLine($"Product ID: {product.ProductId}");
                Console.WriteLine($"Product Name: {product.Name}");
                Console.WriteLine($"Product Price: {product.Price}");
                Console.WriteLine($"Product Description: {product.Description}");
                Console.WriteLine($"Product Stock Quantity: {product.StockQuantity}");
                Console.WriteLine();
            }
        }
       
        public void CreateProduct()
        {
         

            Console.WriteLine("Enter product details:");


            Console.Write("Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Product Name: ");
            string productName = Console.ReadLine();

            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Stock Quantity: ");
            int stockQuantity = int.Parse(Console.ReadLine());

            Products newProduct = new Products(productId, productName, price, description, stockQuantity);
            try
            {
                bool status = repository.CreateProduct(newProduct);

                if (status)
                {
                    Console.WriteLine("Registration success");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }

        public void CreateCustomer()
        {
            Console.WriteLine("Enter customer details:");
            Console.Write("Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();
            Customer newCustomer = new Customer(customerId, name, email, password);
            Console.WriteLine($"{newCustomer.Name}");

            bool status = repository.CreateCustomer(newCustomer);
               
                if (status)
                {
                    Console.WriteLine("Registration success");
                }
            
            
        }



        public void DeleteProduct()
        {
            Console.Write("Enter the Product ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                bool isProductDeleted = repository.DeleteProduct(productId);

                if (isProductDeleted)
                {
                    Console.WriteLine($"Product with ID {productId} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to delete product with ID {productId}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid Product ID.");
            }

        }

        public void DeleteCustomer()
        {
            Console.Write("Enter the Customer ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                bool isCustomerDeleted = repository.DeleteCustomer(customerId);

                if (isCustomerDeleted)
                {
                    Console.WriteLine($"Product with ID {customerId} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to delete product with ID {customerId}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid Customer ID.");
            }
        }

        public void AddToCart()
        {


            Console.Write("Enter product Id: ");
            int productid = int.Parse(Console.ReadLine());

            Products product = repository.FindProduct(productid);
            Console.WriteLine($"{product.Name}");


            Console.Write("Enter Quantity ");
            int quantity = int.Parse(Console.ReadLine());



            bool status = repository.AddToCart(customer, product, quantity);
            if (status)
            {
                Console.WriteLine("Product added to cart successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add product to cart.");
            }
        }

        public void RemoveFromCart()
        {

            {
                Console.Write("Enter product Id to remove from cart: ");
                int productIdToRemove = int.Parse(Console.ReadLine());

                Products productToRemove = repository.FindProduct(productIdToRemove);

                if (productToRemove != null)
                {
                    bool status = repository.RemoveFromCart(customer, productToRemove);

                    if (status)
                    {
                        Console.WriteLine("Product removed from cart successfully.");

                    }
                    else
                    {
                        Console.WriteLine("Failed to remove product from cart.");

                    }
                }
                else
                {
                    Console.WriteLine("Product not found in the cart. Unable to remove from cart.");

                }
            }
        }

        public void GetAllFromCart()
        {
            List<Products> cartProducts = repository.GetAllFromCart(customer);

            if(cartProducts.Count()>0)
            {
                
            
            foreach (var product in cartProducts)
                    {
                        Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price}, Description: {product.Description}, Stock Quantity: {product.StockQuantity}");
                    }
            }
           if(cartProducts.Count()==0)
            {
                  Console.WriteLine("There is no Product in your Cart."); 
            }
        }

        public void PlaceOrder()
        {
            Console.Write("Enter shipping address: ");
            string shippingAddress = Console.ReadLine();

            List<Tuple<Products, int>> productsAndQuantities = new List<Tuple<Products, int>>();

            Console.WriteLine("Enter products and quantities to add to the order. Enter 'done' when finished.");
            int totalCost = 0;
            while (true)
            {
                Console.Write("Enter product ID: ");
                string productIdInput = Console.ReadLine();

                if (productIdInput.ToLower() == "done")
                {
                    break;
                }

                if (int.TryParse(productIdInput, out int productId))
                {
                    Console.Write("Enter quantity: ");
                    int quantity;
                    if (int.TryParse(Console.ReadLine(), out quantity))
                    {
                        Products product = repository.FindProduct(productId);
                        if (product != null)
                        {
                            Console.WriteLine($"{product.Name}");
                            productsAndQuantities.Add(Tuple.Create(product, quantity));
                            totalCost += Convert.ToInt32(product.Price) * quantity;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid product ID. Please enter a valid number.");
                }

                Console.Write("Do you want to add another product? (yes/no): ");
                string addMoreInput = Console.ReadLine().ToLower();
                if (addMoreInput != "yes")
                {
                    break;
                }
            }

            bool orderPlaced = repository.PlaceOrder(customer, productsAndQuantities, shippingAddress);
            if (orderPlaced)
            {
                Console.WriteLine("Order placed successfully!");
                Console.WriteLine($"Your Total Cost is Rs {totalCost}");
            }
            else
            {
                Console.WriteLine("Failed to place the order. Please try again.");
            }
        }

        public void GetOrdersByCustomer()


        {
            List<Tuple<Products, int>> orderDetails=  repository.GetOrdersByCustomer(customer.CustomerId);

            if(orderDetails.Count == 0)
            {
                Console.WriteLine("You don't have any Order in the Cart."); 
            }
            foreach (var orderDetail in orderDetails)
            {
                Products product = orderDetail.Item1;
                int quantity = orderDetail.Item2;

                Console.WriteLine($"Product ID: {product.ProductId}");
                Console.WriteLine($"Product Name: {product.Name}");
                Console.WriteLine($"Product Price: {product.Price}");
                Console.WriteLine($"Product Description: {product.Description}");
                Console.WriteLine($"Product Stock Quantity: {product.StockQuantity}");
                Console.WriteLine($"Quantity Ordered: {quantity}");
                Console.WriteLine();
            }
        }

        public Products FindProduct()
        {
            Console.Write("Enter product Id: ");
            int productid = int.Parse(Console.ReadLine());

            Products product = repository.FindProduct(productid);
            Console.WriteLine($"{product.Name}");

            return product;
        }
    }
    
}
