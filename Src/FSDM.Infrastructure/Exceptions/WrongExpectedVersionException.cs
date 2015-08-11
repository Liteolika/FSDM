using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Infrastructure.Exceptions
{
    public class WrongExpectedVersionException : Exception
    {
        public WrongExpectedVersionException(string message)
            : base(message)
        {

        }
    }
}
