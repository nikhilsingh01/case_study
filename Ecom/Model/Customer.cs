using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Model
{
    public class Customer
    {
        int customerId;
        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        string name; 
        public string Name
        {
            get { return name;}
            set { name = value; }
        }

        string email;
        public string Email
        { 
            get { return email; } 
            set { email = value; } 
        }

        string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        public Customer(int customerId, string name, string email, string password)
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
            Password = password;
        }

        public Customer()
        { }

    }
}
