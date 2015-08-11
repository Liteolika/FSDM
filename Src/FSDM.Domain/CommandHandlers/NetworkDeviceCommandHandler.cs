using FSDM.Contracts.Commands;
using FSDM.Domain.Aggregates;
using FSDM.Domain.Exceptions;
using FSDM.Infrastructure;
using FSDM.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Domain.CommandHandlers
{
    internal class NetworkDeviceCommandHandler : 
        IHandle<CreateNetworkDevice>,
        IHandle<SetNetworkDeviceInactive>,
        IHandle<SetNetworkDeviceActive>
    {

        private readonly IDomainRepository _domainRepository;

        public NetworkDeviceCommandHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public IAggregate Handle(CreateNetworkDevice command)
        {
            try
            {
                var device = _domainRepository.GetById<NetworkDevice>(command.Id);
                throw new NetworkDeviceAlreadyExists(command.Id);
            }
            catch (AggregateNotFoundException)
            { }
            return NetworkDevice.Create(command.Id, command.Hostname);
        }

        public IAggregate Handle(SetNetworkDeviceInactive command)
        {
            var device = _domainRepository.GetById<NetworkDevice>(command.Id);
            device.SetAsInactive();
            return device;
        }

        public IAggregate Handle(SetNetworkDeviceActive command)
        {
            var device = _domainRepository.GetById<NetworkDevice>(command.Id);
            device.SetAsActive();
            return device;
        }
    }
}
