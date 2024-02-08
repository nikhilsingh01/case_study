using Ecom.Model;
using Ecom.Repository;
namespace Ecom.Test

{
    public class Tests
    {
        OrderProcessorRepositoryImpl orderRepo;

        [SetUp]
        public void Setup()
        {
            orderRepo = new OrderProcessorRepositoryImpl();
        }

        [Test]
        
        public void Test_To_GetAllProducts()
        {
            

            var allUsers=orderRepo.GetAllProducts();

            Assert.AreEqual(6, allUsers.Count());
        }

        [Test]
        
        public void Test_To_CheckLoginDatabse() 
        {
            var email = "john.doe@email.com";
            var password = "password123";

           
            var result = orderRepo.CheckLoginInDatabase(email, password);

            
            Assert.IsNotNull(result, "Expected a customer object to be returned.");
            Assert.AreEqual(email, result.Email, "Email of the returned customer should match.");
        }

        [Test]
        
        public void Test_To_FindProduct()
        {
            
            int productId = 1; 

         
            var result = orderRepo.FindProduct(productId);

            
            Assert.IsNotNull(result, "Expected a product object to be returned.");
            Assert.AreEqual(productId, result.ProductId, "ID of the returned product should match.");
        }

        [Test]
        
        public void Test_To_CreateProduct()
        {
           
            var product = new Products
            {
                ProductId = 10,
                Name = "Test Product",
                Price = 1400,
                Description = "Test description",
                StockQuantity = 100
            };

            bool result = orderRepo.CreateProduct(product);

           
            Assert.IsTrue(result, "Expected product creation to be successful.");
        }
        [Test]
        
        public void Test_To_CreateCustomer()
        {
           
            var customer = new Customer
            {
                CustomerId = 7, 
                Name = "Nikhil Singh",
                Email = "nikhil.singh@email.com",
                Password = "password123"
            };

            
            bool result = orderRepo.CreateCustomer(customer);

            
            Assert.IsTrue(result, "Expected customer creation to be successful.");
        }

        [Test]
        
        public void Test_To_DeleteProduct()
        {
            
            int productIdToDelete = 10; 

          
            bool result = orderRepo.DeleteProduct(productIdToDelete);

           
            Assert.IsTrue(result, "Expected product deletion to be successful.");
        }

        [Test]
        
        public void Test_To_DeleteCustomer()
        {
            int customerIdToDelete = 10;


            bool result = orderRepo.DeleteProduct(customerIdToDelete);


            Assert.IsTrue(result, "Expected customer deletion to be successful.");
        }

        [Test]
        
        public void Test_To_AddtoCart()
        {
            Products product =new Products { };
            product= orderRepo.FindProduct(1);
            Customer customer = new Customer { };
            customer = orderRepo.CheckLoginInDatabase("john.doe@email.com","password123");
            int quanity = 10;

            bool result = orderRepo.AddToCart(customer,product, quanity);

            Assert.IsTrue(result, "Expected method to be successful.");
        }

        [Test]
        
        public void Test_To_RemovefromCart()
        {
            Products product = new Products { };
            product = orderRepo.FindProduct(1);
            Customer customer = new Customer { };
            customer = orderRepo.CheckLoginInDatabase("john.doe@email.com", "password123");

            bool result = orderRepo.RemoveFromCart(customer,product);

            Assert.IsTrue(result, "Expected method to be successful.");
        }

        [Test]
        
        public void Test_To_GetAllFromCart()
        {
            Customer customer = new Customer { };
            customer = orderRepo.CheckLoginInDatabase("john.doe@email.com", "password123");

            var allproducts = orderRepo.GetAllFromCart(customer);

            Assert.AreEqual(3, allproducts.Count());
        }

        [Test]
        
        public void Test_To_placeOrder()
        {
            Customer customer = new Customer { };
            customer = orderRepo.CheckLoginInDatabase("john.doe@email.com", "password123");

            List<Tuple<Products, int>> list = new List<Tuple<Products, int>> { };
            var productsAndQuantities = list;
            int productId = 1;
            Products product = orderRepo.FindProduct(productId);

            string shippingAddress = "123 Main St, City, Country";
            

           
            bool result = orderRepo.PlaceOrder(customer, productsAndQuantities, shippingAddress);

           
            Assert.IsTrue(result, "Expected order placement to be successful.");
        }

        [Test]
        
        public void Test_To_GetOrdersByCustomer()
        {
            int customerId = 1;
            var result = orderRepo.GetOrdersByCustomer(customerId);

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        
        public void Test_To_CustomerNotFoundException()
        {
            var email = "john.die@email.com";
            var password = "password123";


            var result = orderRepo.CheckLoginInDatabase(email, password);


            Assert.IsNull(result);
        }
        [Test]
       
        public void Test_To_ProductNotFoundException()
        {
            int productid = 50;
            var result = orderRepo.FindProduct(productid);

            Assert.IsNull(result);
        }
    }
}