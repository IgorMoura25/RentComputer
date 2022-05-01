namespace RC.Core.Messages
{
    public abstract class GenericEventList<T>
    {
        private List<T> _events;
        public IReadOnlyCollection<T> Events => _events?.AsReadOnly();

        public void Add(T eventToAdd)
        {
            _events = _events ?? new List<T>();
            _events.Add(eventToAdd);
        }

        protected void Remove(T eventToRemove)
        {
            _events?.Remove(eventToRemove);
        }

        public abstract Task PublishEventsAsync();
        //{
        //    var eventsToPublish = new List<T>();
        //    eventsToPublish.AddRange(_events);
        //    ClearEvents();

        //    foreach (var eventToPublish in eventsToPublish)
        //    {
        //        await _mediatRHandler.PublishEventAsync(eventToPublish);
        //    }
        //}

        protected void ClearAll()
        {
            _events?.Clear();
        }
    }
}
