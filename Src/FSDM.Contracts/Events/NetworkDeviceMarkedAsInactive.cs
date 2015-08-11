using FSDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Contracts.Events
{
    public class NetworkDeviceMarkedAsInactive : IDomainEvent
    {
        public Guid Id { get; set; }

        public NetworkDeviceMarkedAsInactive()
        {

        }

        public NetworkDeviceMarkedAsInactive(Guid deviceId)
        {
            this.Id = deviceId;
        }


        
    }
}
