using System;

namespace Business.QLESS_Transport_Card.Models.Trip
{
    public class Tap
    {
        public Tap()
        {
            Guid = Guid.NewGuid();
        }

        public Tap(Guid transportCardGuid, DateTime timestamp, string station)
            : this()
        {
            TransportCardGuid = transportCardGuid;
            Timestamp = timestamp;
            Station = station;
        }

        public Guid Guid { get; set; }
        public Guid TransportCardGuid { get; set; }
        public DateTime Timestamp { get; set; }
        public string Station { get; set; }
    }
}
