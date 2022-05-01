using RC.Core.Messages;
using RC.MessageBus.EasyNetQ;

namespace RC.MessageBus
{
    public class EventList : GenericEventList<Event>
    {
        private readonly IEasyNetQBus _bus;

        public EventList(IEasyNetQBus bus)
        {
            _bus = bus;
        }

        public void AddEvent(MediatREvent eventToAdd)
        {
            base.Add(eventToAdd);
        }

        public void RemoveEvent(MediatREvent eventToRemove)
        {
            base.Remove(eventToRemove);
        }

        private void ClearEvents()
        {
            base.ClearAll();
        }

        public override async Task PublishEventsAsync()
        {
            var eventsToPublish = new List<Event>();
            eventsToPublish.AddRange(base.Events);
            ClearEvents();

            foreach (var eventToPublish in eventsToPublish)
            {
                await _bus.PublishAsync(eventToPublish);
            }
        }
    }
}
