using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Infrastructure.Storage.EntityFramework
{
    public class EventData
    {
        [Key]
        public Guid EventId { get; set; }

        public Guid AggregateId { get; set; }
        public string Data { get; set; }
        public int AggregateVersion { get; set; }
        public string Type { get; set; }

    }
}
