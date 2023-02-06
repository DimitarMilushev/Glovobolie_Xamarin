using System;
using System.Collections.Generic;
using System.Text;

namespace GlovobolieApp.Exceptions
{
    class ValidationException: Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
