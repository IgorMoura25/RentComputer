using RC.Core.Messages;

namespace RC.MessageBus.Mediator
{
    public class MediatREventList
    {
        private readonly IMediatRHandler _mediatRHandler;
        private List<MediatREvent>? _events;
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

        public void Remove(MediatREvent eventToRemove)
        {
            _events.Remove(eventToRemove);
        }

        private void ClearAll()
        {
            _events.Clear();
        }

        public async Task PublishEventsAsync()
        {
            var eventsToPublish = new List<MediatREvent>();
            eventsToPublish.AddRange(Events);
            ClearAll();

            foreach (var eventToPublish in eventsToPublish)
            {
                await _mediatRHandler.PublishEventAsync(eventToPublish);
            }
        }
    }
}
