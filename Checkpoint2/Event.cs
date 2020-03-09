using System;

namespace Checkpoint2
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }

        public Event(String description)
        {
            Description = description;
        }

        public Event()
        {
        }

        public Event(int eventId, string eventName, DateTime startTime, DateTime endTime, string description)
        {
            EventId = eventId;
            EventName = eventName;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
        }

        public void Postpone(TimeSpan timeDelta)
        {
            StartTime = StartTime + timeDelta;
            EndTime = EndTime + timeDelta;
        }
    }
}