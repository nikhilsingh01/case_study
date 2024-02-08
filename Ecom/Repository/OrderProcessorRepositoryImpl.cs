using Ecom.Exceptions;
using Ecom.Exceptions;
using Ecom.Model;
using Ecom.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ecom.Repository
{
    public class OrderProcessorRepositoryImpl : IOrderProcessorRepository
    {


        //sql connection class
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;
        public OrderProcessorRepositoryImpl()
        {

            sqlconnection = new SqlConnection(DBConnUtil.GetConnectionString());
            cmd = new SqlCommand();
        }



        public Customer CheckLoginInDatabase(string email, string password)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnUtil.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT * FROM Customers WHERE email = @email AND password = @password;";
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", password);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {                          
                            Customer customer = new Customer();
                            customer.CustomerId = (int)reader["customer_id"];
                            customer.Name = reader["name"].ToString();
                            customer.Email = reader["email"].ToString();
                            customer.Password = reader["password"].ToString();
                            return customer;
                        }
                        else
                        {
                            throw new CustomerNotFoundException($"Customer with email '{email}' not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }

        }

        public List<Products> GetAllProducts()
        {
            List<Products> productList = new List<Products>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnUtil.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Products", sqlConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Products product = new Products();
                                product.ProductId = (int)reader["product_id"];
                                product.Name = (string)reader["name"];
                                product.Price = (int)reader["price"];
                                product.Description = (string)reader["description"];
                                product.StockQuantity = (int)reader["stockQuantity"];
                                productList.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return productList;
        }


        public Products FindProduct(int productId)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnUtil.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT * FROM Products WHERE product_id = @product_id;";
                        cmd.Parameters.AddWithValue("@product_id", productId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            Products product = new Products();
                            product.ProductId = (int)reader["product_id"];
                            product.Name = (string)reader["name"];
                            product.Price = (int)reader["price"];
                            product.Description = (string)reader["description"];
                            product.StockQuantity = (int)reader["stockQuantity"];

                            return product;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public bool CreateProduct(Products product)
        {

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnUtil.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO Products VALUES (@product_id, @Name, @Price, @Description, @stockQuantity)";
                        cmd.Parameters.AddWithValue("@product_id", product.ProductId);
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Description", product.Description);
                        cmd.Parameters.AddWithValue("@stockQuantity", product.StockQuantity);
                        cmd.ExecuteNonQuery();
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public bool CreateCustomer(Customer customer)
        {

            try
            {
                Console.WriteLine($"{customer.Email}");

                using (SqlConnection sqlConnection = new SqlConnection(DBConnUtil.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO Customers VALUES (@customer_id, @Name, @Email, @password)";
                        cmd.Parameters.AddWithValue("@customer_id", customer.CustomerId);
                        cmd.Parameters.AddWithValue("@Name", customer.Name);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        cmd.Parameters.AddWithValue("@password", customer.Password);

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public bool DeleteProduct(int productid)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnUtil.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DELETE FROM cart WHERE product_id = @product_id;";
                        cmd.Parameters.AddWithValue("@product_id", productid);
                        int cartRowsAffected = cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandText = "DELETE FROM order_items WHERE product_id = @product_id;";
                        cmd.Parameters.AddWithValue("@product_id", productid);
                        int orderItemsRowsAffected = cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandText = "DELETE FROM Products WHERE product_id = @product_id;";
                        cmd.Parameters.AddWithValue("@product_id", productid);
                        int productsRowsAffected = cmd.ExecuteNonQuery();
                        if (cartRowsAffected > 0 || orderItemsRowsAffected > 0 || productsRowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {

                            throw new ProductNotFoundException($"Product with ProductID: '{productid}' not found.");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public bool DeleteCustomer(int customerid)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnUtil.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DELETE FROM orders WHERE customer_id = @customer_id;";
                        cmd.Parameters.AddWithValue("@customer_id", customerid);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DELETE FROM cart WHERE customer_id = @customer_id;";
                        cmd.Parameters.AddWithValue("@customer_id", customerid);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DELETE FROM customers WHERE customer_id = @customer_id;";
                        cmd.Parameters.AddWithValue("@customer_id", customerid);
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public bool AddToCart(Customer customer, Products product, int quantity)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnUtil.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO Cart (customer_id, product_id, quantity) VALUES (@customer_id, @product_id, @quantity);";
                        cmd.Parameters.AddWithValue("@customer_id", customer.CustomerId);
                        cmd.Parameters.AddWithValue("@product_id", product.ProductId);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }

        }


        public bool RemoveFromCart(Customer customer, Products product)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnUtil.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DELETE FROM Cart WHERE customer_id = @customer_id AND product_id = @product_id;";
                        cmd.Parameters.AddWithValue("@customer_id", customer.CustomerId);
                        cmd.Parameters.AddWithValue("@product_id", product.ProductId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0) 
                        {
                            Console.WriteLine("Error: Product not found in the cart.");
                            return false;
                        }



                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public List<Products> GetAllFromCart(Customer customer)
        {
            List<Products> cartProducts = new List<Products>();

            try
            {


                using (SqlConnection sqlConnection = new SqlConnection(DBConnUtil.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT P.* FROM Cart C INNER JOIN Products P ON C.product_id = P.product_id WHERE C.customer_id = @customer_id;";
                        cmd.Parameters.AddWithValue("@customer_id", customer.CustomerId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Products product = new Products();
                                product.ProductId = (int)reader["product_id"];
                                product.Name = reader["name"].ToString();
                                product.Price = (int)reader["price"];
                                product.Description = reader["description"].ToString();
                                product.StockQuantity = (int)reader["stockQuantity"];
                                cartProducts.Add(product);
                            }
                        }
                    }
                }

                return cartProducts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }

        }

        public bool PlaceOrder(Customer customer, List<Tuple<Products, int>> productsAndQuantities, string shippingAddress)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnUtil.GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = sqlConnection.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO Orders (customer_id, order_date, total_price, shipping_address) OUTPUT INSERTED.order_id VALUES (@customer_id, @order_date, @total_price, @shipping_address);";
                        cmd.Parameters.AddWithValue("@customer_id", customer.CustomerId);
                        cmd.Parameters.AddWithValue("@order_date", DateTime.Now);
                        decimal totalPrice = productsAndQuantities.Sum(tuple => tuple.Item1.Price * tuple.Item2);
                        cmd.Parameters.AddWithValue("@total_price", totalPrice);
                        cmd.Parameters.AddWithValue("@shipping_address", shippingAddress);

                        int orderId = (int)cmd.ExecuteScalar();

                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO order_items (order_id, product_id, quantity) VALUES (@order_id, @product_id, @quantity);";
                        foreach (var tuple in productsAndQuantities)
                        {
                            cmd.Parameters.AddWithValue("@order_id", orderId);
                            cmd.Parameters.AddWithValue("@product_id", tuple.Item1.ProductId);
                            cmd.Parameters.AddWithValue("@quantity", tuple.Item2);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                      
                        foreach (var tuple in productsAndQuantities)
                        {
                            cmd.CommandText = "UPDATE Products SET stockQuantity = stockQuantity - @quantity WHERE product_id = @product_id;";
                            cmd.Parameters.AddWithValue("@quantity", tuple.Item2);
                            cmd.Parameters.AddWithValue("@product_id", tuple.Item1.ProductId);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        public List<Tuple<Products, int>> GetOrdersByCustomer(int customerId)
        {
            List<Tuple<Products, int>> ordersDetails = new List<Tuple<Products, int>>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DBConnUtil.GetConnectionString()))
                {
                    sqlConnection.Open();

                    string query = "SELECT P.product_id, P.name, P.price, P.description, P.stockQuantity, OI.quantity " +
                                   "FROM Orders O " +
                                   "JOIN order_items OI ON O.order_id = OI.order_id " +
                                   "JOIN Products P ON OI.product_id = P.product_id " +
                                   "WHERE O.customer_id = @customer_id;";

                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@customer_id", customerId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int productId = reader.GetInt32(0);
                                string productName = reader.GetString(1);
                                int productPrice = reader.GetInt32(5);
                                string productDescription = reader.GetString(3);
                                int productStockQuantity = reader.GetInt32(4);
                                int quantity = reader.GetInt32(5);

                                Products product = new Products
                                {
                                    ProductId = productId,
                                    Name = productName,
                                    Price = productPrice,
                                    Description = productDescription,
                                    StockQuantity = productStockQuantity
                                };

                                ordersDetails.Add(Tuple.Create(product, quantity));
                            }
                        }
                    }
                }
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return ordersDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return ordersDetails;
            }

            return ordersDetails;
        }

    }
}
