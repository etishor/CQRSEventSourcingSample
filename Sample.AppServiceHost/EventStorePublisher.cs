using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventStore.Dispatcher;

namespace Sample.AppServiceHost
{
    public class EventStorePublisher : IPublishMessages
    {
        private bool disposed;
        private readonly NanoMessageBus.IPublishMessages publisher;

        public EventStorePublisher(NanoMessageBus.IPublishMessages publisher)
        {
            this.publisher = publisher;
        }

        public void Publish(EventStore.Commit commit)
        {
            publisher.Publish(commit.Events.Select(e => e.Body).ToArray());
        }

        ~EventStorePublisher()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed || !disposing)
				return;

			this.disposed = true;
		} 
    }
}
