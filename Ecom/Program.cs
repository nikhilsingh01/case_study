using Ecom.Model;
using Ecom.Services;

namespace Ecom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IService service = new ServiceImplemetation();
            // service.createProduct();
            //service.GetAllProducts();
            //service.deleteProduct();
            //service.deleteCustomer();
            // Customer loggedUser = null;

            //Console.WriteLine("Welcome to Ecommerce Application");
            // Console.WriteLine("1.Login");
            //Console.WriteLine("2/Register");



            //int userInput= int.Parse(Console.ReadLine());
            //if(userInput == 1)
            //{
            //loggedUser=service.Login();

            //Console.WriteLine($"Loggggggggggggg{loggedUser.Name}");
            //service.GetAllProducts();
            //}

            //service.finproduct();


            
            Customer loggedInCustomer = null;

            Console.WriteLine("Welcome to Ecommerce Application");

            while (true)
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");

                int userInput = int.Parse(Console.ReadLine());

                switch (userInput)
                {
                    case 1:
                        loggedInCustomer = service.Login();
                        if (loggedInCustomer != null)
                        {
                            Console.WriteLine($"Login successful. Welcome, {loggedInCustomer.Name}!");

                            // Display menu for the logged-in user
                            while (true)
                            {   
                                Console.WriteLine();
                                Console.WriteLine("Menu:");
                                Console.WriteLine("1. Create Product");
                                Console.WriteLine("2. Get All Products");
                                Console.WriteLine("3. Delete Product");
                                Console.WriteLine("4. Add to Cart");
                                Console.WriteLine("5. Remove from Cart");
                                Console.WriteLine("6. View Cart");
                                Console.WriteLine("7. Place Order");
                                Console.WriteLine("8. View Orders");
                                Console.WriteLine("9. Logout");

                                int userinput = int.Parse(Console.ReadLine());

                                switch (userinput)
                                {
                                    case 1:
                                        service.CreateProduct();
                                        break;

                                    case 2:
                                        service.GetAllProducts();
                                        break;

                                    case 3:
                                        service.DeleteProduct();
                                        break;

                                    case 4:
                                        service.AddToCart();
                                        break;

                                    case 5:
                                        service.RemoveFromCart();
                                        break;

                                    case 6:
                                        service.GetAllFromCart();
                                        break;

                                    case 7:
                                        service.PlaceOrder();
                                        break;

                                    case 8:
                                        service.GetOrdersByCustomer();
                                        break;

                                    case 9:
                                        Console.WriteLine("Logging out. Goodbye!");
                                        return;

                                    default:
                                        Console.WriteLine("Invalid option. Please choose a valid option.");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Login failed. Invalid email or password.");
                        }
                        break;

                    case 2:
                        service.CreateCustomer();
                        break;

                    case 3:
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please choose a valid option.");
                        break;
                }
            }
        }

        
    }



}
    

