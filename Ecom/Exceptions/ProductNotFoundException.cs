using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Exceptions
{
    internal class ProductNotFoundException:ApplicationException
    {
        public ProductNotFoundException() { }

        public ProductNotFoundException(string message):base(message) { }
    }
}
