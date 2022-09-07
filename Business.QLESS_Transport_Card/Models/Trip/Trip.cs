using System;

namespace Business.QLESS_Transport_Card.Models.Trip
{
    public class Trip
    {
        public Trip()
        {
            Guid = Guid.NewGuid();
        }

        public Guid Guid { get; set; }
        public Guid TransportCardGuid { get; set; }
        public decimal Cost { get; set; }
        public Tap TapIn { get; set; }
        public Tap TapOut { get; set; }
        public DateTime TripTimestamp => TapOut?.Timestamp ?? TapIn.Timestamp;
    }
}
