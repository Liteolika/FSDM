using FSDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Contracts.Commands
{
    public class SetNetworkDeviceInactive : ICommand
    {
        public Guid Id { get; private set; }

        public SetNetworkDeviceInactive(Guid id)
        {
            this.Id = id;
        }

    }
}
