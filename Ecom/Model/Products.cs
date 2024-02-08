using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Model
{
    public class Products    {
        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int stockQuantity;
        public int StockQuantity
        {
            get { return stockQuantity; }
            set { stockQuantity = value; }
        }

        public Products(int productId, string name, decimal price, string description, int stockQuantity)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            Description = description;
            StockQuantity = stockQuantity;
        }

        public Products()
        {
        }

    }
}
