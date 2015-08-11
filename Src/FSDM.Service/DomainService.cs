using FSDM.Contracts.Commands;
using FSDM.Domain;
using FSDM.Infrastructure;
using FSDM.Infrastructure.Storage;
using FSDM.Infrastructure.Storage.EntityFramework;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Service
{
    public class DomainService : IService
    {
        public void Start()
        {
            //IServiceBus bus = ServiceBusFactory.New(cfg =>
            //{
            //    cfg.ReceiveFrom("loopback://localhost/temp");
            //});

            //InMemoryDomainRespository repo = new InMemoryDomainRespository();


            DomainRepository repo = new DomainRepository(EventDbContext.Create("EventStore"));

            DomainEntry entry = new DomainEntry(repo);

            Guid id = Guid.NewGuid();

            entry.ExecuteCommand<CreateNetworkDevice>(new CreateNetworkDevice(id, "SESM-01"));
            entry.ExecuteCommand<SetNetworkDeviceInactive>(new SetNetworkDeviceInactive(id));
            entry.ExecuteCommand<SetNetworkDeviceActive>(new SetNetworkDeviceActive(id));

            var a = 1;

            //bus.Publish(new CreateNetworkDevice(Guid.NewGuid(), "TESTAR"));

            
            


        }

        public void Stop()
        {
            
        }
    }

    

}
