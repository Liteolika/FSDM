using FSDM.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Contracts.Events
{
    [Serializable]
    public class NetworkDeviceCreated : IDomainEvent
    {

        public Guid Id { get; set; }
        public string Hostname { get; set; }

        public NetworkDeviceCreated()
        {

        }

        public NetworkDeviceCreated(Guid deviceId, string hostname)
        {
            this.Id = deviceId;
            this.Hostname = hostname;
        }


    }
    

}
