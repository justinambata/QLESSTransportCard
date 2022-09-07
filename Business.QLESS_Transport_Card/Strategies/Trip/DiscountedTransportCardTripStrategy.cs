using System;
using System.Collections.Generic;
using System.Linq;
using Business.QLESS_Transport_Card.Models.TransportCard;
using Business.QLESS_Transport_Card.Models.Trip;

namespace Business.QLESS_Transport_Card.Strategies.Trip
{
    public class DiscountedTransportCardTripStrategy : TripStrategy
    {
        public override void TapOut(TransportCard transportCard, DateTime timestamp, string station, Guid tripGuid)
        {
            var tap = new Tap(transportCard.Guid, timestamp, station);
            var trip = transportCard.Trips.FirstOrDefault(x => x.Guid == tripGuid);
            if (trip == null) throw new Exception("Trip not found.");
            trip.TapOut = tap;
            trip.Cost = ComputeFareWithDiscount(trip, transportCard.Trips);
            var totalTripCost = trip.Cost + 10m; // 10.00 fix rate additional deduction regardless of station
            //transportCard.Load -= totalTripCost;
            transportCard.SubtractLoad(totalTripCost); // use encapsulation
#if DEBUG
            Console.WriteLine($"> Total Trip Cost: {totalTripCost}");
#endif
            transportCard.LastUsed = timestamp;
        }

        private static decimal ComputeFareWithDiscount(Models.Trip.Trip activeTrip, IEnumerable<Models.Trip.Trip> trips)
        {
            var basicFare = FareRule.ComputeFare(activeTrip.TapIn, activeTrip.TapOut); // get trip cost based from fare rules
            var discount = 0.20m; // 20% base discount

            // 2nd trip - 5th trip? +3% discount = 23%
            var sameDayTrips = trips.Where(t => t.TripTimestamp.Date == activeTrip.TapOut.Timestamp.Date)
                .OrderBy(t => t.TripTimestamp)
                .ToList();
            var tripIndex = sameDayTrips.IndexOf(activeTrip);
            if (tripIndex >= 1 && tripIndex <= 4)
            {
                discount += .03m;
            }

            var discountedFare = basicFare - (basicFare * discount);
#if DEBUG
            Console.WriteLine($"> Trip # {(tripIndex + 1)} for the day.");
            Console.WriteLine($"> Discounted Fare: {discountedFare} ({discount:p} discount applied)");
#endif
            return discountedFare;
        }
    }
}
