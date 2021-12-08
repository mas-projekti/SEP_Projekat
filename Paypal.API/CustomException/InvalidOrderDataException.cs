using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.CustomException
{
    public class InvalidOrderDataException : Exception
    {
        public InvalidOrderDataException(string message) : base(message)
        {
        }
    }
}
