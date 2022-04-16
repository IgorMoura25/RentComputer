using RC.Core.Messages;

namespace RC.Core.Mediator
{
    public class MediatREventList
    {
        private readonly IMediatRHandler _mediatRHandler;
        private List<MediatREvent> _events;
        public IReadOnlyCollection<MediatREvent> Events => _events?.AsReadOnly();

        public MediatREventList(IMediatRHandler mediatorHandler)
        {
            _mediatRHandler = mediatorHandler;
        }

        public void AddEvent(MediatREvent eventToAdd)
        {
            _events = _events ?? new List<MediatREvent>();
            _events.Add(eventToAdd);
        }

        public void RemoveEvent(MediatREvent eventToRemove)
        {
            _events?.Remove(eventToRemove);
        }

        public async Task PublishEventsAsync()
        {
            var eventsToPublish = new List<MediatREvent>();
            eventsToPublish.AddRange(_events);
            ClearEvents();

            foreach (var eventToPublish in eventsToPublish)
            {
                await _mediatRHandler.PublishEventAsync(eventToPublish);
            }
        }

        private void ClearEvents()
        {
            _events?.Clear();
        }
    }
}
