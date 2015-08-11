using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Domain.Exceptions
{
    public abstract class DuplicateAggregateException : DomainException
    {
        protected DuplicateAggregateException(Guid id)
            : base(CreateMessage(id))
        {

        }

        private static string CreateMessage(Guid id)
        {
            return string.Format("Aggregate already exists with id {0}", id);
        }
    }
}
