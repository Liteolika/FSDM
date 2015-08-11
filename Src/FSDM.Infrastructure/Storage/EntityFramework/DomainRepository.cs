using FSDM.Infrastructure.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Infrastructure.Storage.EntityFramework
{
    public class DomainRepository : DomainRepositoryBase
    {

        //public Dictionary<Guid, List<string>> _eventStore = new Dictionary<Guid, List<string>>();

        private List<IDomainEvent> _latestEvents = new List<IDomainEvent>();
        private JsonSerializerSettings _serializationSettings;

        private readonly EventDbContext _eventStore;

        public DomainRepository(EventDbContext dbContext)
        {
            _eventStore = dbContext;

            _serializationSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }
        
        public override IEnumerable<IDomainEvent> Save<TAggregate>(TAggregate aggregate)
        {
            var events = aggregate.UncommitedEvents().ToList();
            var expectedVersion = CalculateExpectedVersion(aggregate, events);
            var eventData = events.Select(CreateEventData).ToList();
            var currentVersion = expectedVersion;

            if (expectedVersion < 1)
            {
                foreach (var @event in eventData)
                {
                    @event.AggregateVersion = currentVersion;
                    currentVersion++;
                }

                _eventStore.Events.AddRange(eventData);
                _eventStore.SaveChanges();
            }
            else
            {
                var existingEvents = _eventStore.Events.Where(x => x.AggregateId == aggregate.Id);
                if (currentVersion != expectedVersion)
                {
                    throw new WrongExpectedVersionException("Expected version " + expectedVersion +
                                                            " but the version is " + currentVersion);
                }

                foreach (var @event in eventData)
                    @event.AggregateVersion = expectedVersion++;

                _eventStore.Events.AddRange(eventData);
                _eventStore.SaveChanges();
                
            }

            _latestEvents.AddRange(events);
            aggregate.ClearUncommitedEvents();

            return events;
        }


        public override TResult GetById<TResult>(Guid id)
        {
            var hasEvents = _eventStore.Events.Where(x => x.AggregateId == id).Any();
            if (hasEvents)
            {
                var events = _eventStore.Events.Where(x => x.AggregateId == id).ToList();

                IList<IDomainEvent> deserializedEvents = new List<IDomainEvent>();
 
                events.ForEach((x) =>
                {
                    deserializedEvents.Add(JsonConvert.DeserializeObject(x.Data, _serializationSettings) as IDomainEvent);
                });
                
                //var deserializedEvents = events.Select(x => 
                //    JsonConvert.DeserializeObject(x.Data, _serializationSettings) as IDomainEvent);
                var aggregate = BuildAggregate<TResult>(deserializedEvents);
                return aggregate;

            }
            throw new AggregateNotFoundException("Could not found aggregate of type " + typeof(TResult) + " and id " + id);
        }

        private EventData CreateEventData(IDomainEvent @event)
        {
            var type = @event.GetType().FullName;
            var data = Serialize(@event);
            
            var eventData = new EventData()
            {
                EventId = Guid.NewGuid(),
                AggregateId = @event.Id,
                AggregateVersion = -1,
                Data = data,
                Type = type
            };
            return eventData;
        }

        private string Serialize(IDomainEvent arg)
        {
            return JsonConvert.SerializeObject(arg, _serializationSettings);
        }

    }

}
