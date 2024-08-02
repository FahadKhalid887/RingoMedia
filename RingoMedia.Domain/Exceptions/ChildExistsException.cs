using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Domain.Exceptions
{
    public class ChildExistsException : Exception
    {
        public ChildExistsException()
            : base("A child with the same identifier already exists.")
        {
        }

        public ChildExistsException(string message)
            : base(message)
        {
        }

        public ChildExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
