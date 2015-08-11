using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace FSDM.Service
{
    class Program
    {
        static void Main(string[] args)
        {

            Topshelf.HostFactory.Run(x =>
            {
                x.Service<IService>(s =>
                {
                    s.ConstructUsing(name => new DomainService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("CQRSShop.Service");
                x.SetDisplayName("CQRSShop.Service");
                x.SetServiceName("CQRSShop.Service");
            });

        }
    }
}
