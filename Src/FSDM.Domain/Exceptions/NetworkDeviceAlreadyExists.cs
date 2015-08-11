using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Domain.Exceptions
{
    public class NetworkDeviceAlreadyExists : DuplicateAggregateException
    {

        public NetworkDeviceAlreadyExists(Guid id) : base(id)
        {

        }

    }
}
