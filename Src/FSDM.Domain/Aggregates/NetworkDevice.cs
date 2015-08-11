using FSDM.Contracts.Events;
using FSDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Domain.Aggregates
{
    internal class NetworkDevice : AggregateBase
    {
        // Properties

        internal bool IsActive = true;
        internal string Hostname;

        // Constructors

        public NetworkDevice()
        {
            RegisterTransition<NetworkDeviceCreated>(Apply);
            RegisterTransition<NetworkDeviceMarkedAsInactive>(Apply);
            RegisterTransition<NetworkDeviceMarkedAsActive>(Apply);
        }

        private NetworkDevice(Guid deviceId, string hostname) : this()
        {
            RaiseEvent(new NetworkDeviceCreated(deviceId, hostname));
        }

        // Methods
        internal void SetAsInactive()
        {
            RaiseEvent(new NetworkDeviceMarkedAsInactive(this.Id));
        }

        internal void SetAsActive()
        {
            RaiseEvent(new NetworkDeviceMarkedAsActive(this.Id));
        }

        // Domain Event Transitions

        private void Apply(NetworkDeviceCreated evt)
        {
            Id = evt.Id;
            Hostname = evt.Hostname;

        }

        private void Apply(NetworkDeviceMarkedAsInactive evt)
        {
            IsActive = false;
        }

        private void Apply(NetworkDeviceMarkedAsActive evt)
        {
            IsActive = true;
        }

        // Factory methods

        internal static IAggregate Create(Guid deviceId, string hostname)
        {
            return new NetworkDevice(deviceId, hostname);
        }

        
    }
}
