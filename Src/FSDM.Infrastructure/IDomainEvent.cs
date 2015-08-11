using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Infrastructure
{
    public interface IDomainEvent
    {
        Guid Id { get; }
    }
}
