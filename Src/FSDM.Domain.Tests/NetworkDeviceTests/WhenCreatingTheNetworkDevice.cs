using FSDM.Contracts.Commands;
using FSDM.Contracts.Events;
using FSDM.Domain.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Domain.Tests.NetworkDeviceTests
{
    [TestFixture]
    public class WhenCreatingTheNetworkDevice : TestBase
    {

        [Test]
        public void TheDeviceShouldBeCreatedWithTheRightHostname()
        {
            Guid id = Guid.NewGuid();
            When(new CreateNetworkDevice(id, "ABC123"));
            Then(new NetworkDeviceCreated(id, "ABC123"));
        }

        [Test]
        public void ItShouldThrowOnDuplicateId()
        {
            Guid id = Guid.NewGuid();
            Given(new NetworkDeviceCreated(id, "HOSTNAME"));
            WhenThrows<NetworkDeviceAlreadyExists>(new CreateNetworkDevice(id, "HOSTNAME"));
        }

        [Test]
        public void SettingTheNetworkDeviceInactive()
        {
            Guid id = Guid.NewGuid();
            Given(new NetworkDeviceCreated(id, "ASD"));
            When(new SetNetworkDeviceInactive(id));
            Then(new NetworkDeviceMarkedAsInactive(id));
        }


    }
}
