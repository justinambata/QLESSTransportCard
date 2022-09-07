using System;
using System.Linq;
using Business.QLESS_Transport_Card.Models.TransportCard;
using Business.QLESS_Transport_Card.Models.Trip;

namespace Business.QLESS_Transport_Card.Strategies.Trip
{
    public class StandardTransportCardTripStrategy : TripStrategy
    {
        public override void TapOut(TransportCard transportCard, DateTime timestamp, string station, Guid tripGuid)
        {
            var tap = new Tap(transportCard.Guid, timestamp, station);
            var trip = transportCard.Trips.FirstOrDefault(x => x.Guid == tripGuid);
            if (trip == null) throw new Exception("Trip not found.");
            trip.TapOut = tap;
            trip.Cost = FareRule.ComputeFare(trip.TapIn, trip.TapOut); // get trip cost based from fare rules
            var totalTripCost = trip.Cost + 15m; // 15.00 fix rate additional deduction regardless of station
            //transportCard.Load -= totalTripCost;
            transportCard.SubtractLoad(totalTripCost); // use encapsulation
#if DEBUG
            Console.WriteLine($"> Total Trip Cost: {totalTripCost}");
#endif
            transportCard.LastUsed = timestamp;
        }
    }
}
