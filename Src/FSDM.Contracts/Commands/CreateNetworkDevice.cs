using FSDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Contracts.Commands
{
    public class CreateNetworkDevice : ICommand
    {
        public Guid Id { get; private set; }
        public string Hostname { get; private set; }

        public CreateNetworkDevice(Guid id, string hostname)
        {
            this.Id = id;
            this.Hostname = hostname;
        }
    }
}
