using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Model
{
    internal class OrderItem
    {
        private int orderItemId;
        public int OrderItemId
        {
            get { return orderItemId; }
            set { orderItemId = value; }
        }

        private int orderId;
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public OrderItem(int orderItemId, int orderId, int productId, int quantity)
        {
            OrderItemId = orderItemId;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }

    }
}
