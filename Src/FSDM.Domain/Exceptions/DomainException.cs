using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        public DomainException()
        {
        }

        protected DomainException(string createMessage)
            : base(createMessage)
        {
        }
    }
}
