using RC.Core.Messages;

namespace RC.MessageBus.Mediator
{
    public class MediatREventList : GenericEventList<MediatREvent>
    {
        private readonly IMediatRHandler _mediatRHandler;
        //private List<MediatREvent> _events;
        //public IReadOnlyCollection<MediatREvent> Events => _events?.AsReadOnly();

        public MediatREventList(IMediatRHandler mediatorHandler)
        {
            _mediatRHandler = mediatorHandler;
        }

        public void AddEvent(MediatREvent eventToAdd)
        {
            //_events = _events ?? new List<MediatREvent>();
            //_events.Add(eventToAdd);

            Add(eventToAdd);
        }

        public void RemoveEvent(MediatREvent eventToRemove)
        {
            Remove(eventToRemove);
        }

        private void ClearEvents()
        {
            ClearAll();
        }

        public override async Task PublishEventsAsync()
        {
            var eventsToPublish = new List<MediatREvent>();
            eventsToPublish.AddRange(Events);
            ClearEvents();

            foreach (var eventToPublish in eventsToPublish)
            {
                await _mediatRHandler.PublishEventAsync(eventToPublish);
            }
        }
    }
}
