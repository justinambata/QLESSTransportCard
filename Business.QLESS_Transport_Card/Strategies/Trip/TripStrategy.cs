using System;
using Business.QLESS_Transport_Card.Models.TransportCard;
using Business.QLESS_Transport_Card.Models.Trip;

namespace Business.QLESS_Transport_Card.Strategies.Trip
{
    public abstract class TripStrategy
    {
        public virtual void TapIn(TransportCard transportCard, DateTime timestamp, string station)
        {
            var tap = new Tap(transportCard.Guid, timestamp, station);
            var trip = new Models.Trip.Trip
            {
                TransportCardGuid = transportCard.Guid,
                TapIn = tap
            };
            transportCard.Trips.Add(trip);
            transportCard.LastUsed = timestamp;
        }

        public abstract void TapOut(TransportCard transportCard, DateTime timestamp, string station, Guid tripGuid);
    }
}
