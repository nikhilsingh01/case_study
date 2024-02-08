using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Model
{
    internal class Orders
    {
        private int orderId;
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        private int customerId;
        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        private DateTime orderDate;
        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        private decimal totalPrice;
        public decimal TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        private string shippingAddress;
        public string ShippingAddress
        {
            get { return shippingAddress; }
            set { shippingAddress = value; }
        }

        public Orders(int orderId, int customerId, DateTime orderDate, decimal totalPrice, string shippingAddress)
        {
            OrderId = orderId;
            CustomerId = customerId;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            ShippingAddress = shippingAddress;
        }

    }
}
