using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Exceptions
{
    internal class CustomerNotFoundException:ApplicationException
    {
        public CustomerNotFoundException() : base()
        {
        }

        public CustomerNotFoundException(string message) : base(message)
        {
        }
    }
}
