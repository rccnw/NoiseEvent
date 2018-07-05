using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{


    public class EventNotFoundException : Exception
    {
        public EventNotFoundException(int id) : base($"No Event found with id {id}")
        {
        }
        public EventNotFoundException(Guid eventId) : base($"No Event found with id {eventId}")
        {
        }

        protected EventNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public EventNotFoundException(string message) : base(message)
        {
        }

        public EventNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
