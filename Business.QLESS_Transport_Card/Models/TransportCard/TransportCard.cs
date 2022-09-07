using System;
using System.Collections.Generic;
using System.Linq;
using Business.QLESS_Transport_Card.Strategies.Reloading;
using Business.QLESS_Transport_Card.Strategies.Trip;

namespace Business.QLESS_Transport_Card.Models.TransportCard
{
    public abstract class TransportCard
    {
        protected TransportCard()
        {
            Guid = Guid.NewGuid();
            LastUsed = DateTime.UtcNow;
            Load = 0m;
            CustomProperties = new Dictionary<string, object>();
        }

        public Guid Guid { get; set; }
        public DateTime LastUsed { get; set; }
        public decimal Load { get; private set; } // apply encapsulation, use private set

        public abstract DateTime ValidUntil { get; }
        public Dictionary<string, object> CustomProperties { get; set; }

        // Trip
        public TripStrategy TripStrategy { get; set; }
        public IList<Trip.Trip> Trips { get; } = new List<Trip.Trip>();
        protected Trip.Trip GetActiveTrip() => Trips.FirstOrDefault(x => x.TapIn != null && x.TapOut == null);
        public void TapIn(DateTime timestamp, string station)
        {
            TripStrategy.TapIn(this, timestamp, station);
        }

        public void TapOut(DateTime timestamp, string station)
        {
            var activeTrip = GetActiveTrip();
            if (activeTrip == null) throw new Exception("No active trip.");
            TripStrategy.TapOut(this, timestamp, station, activeTrip.Guid);
        }

        // Reloading
        public ReloadingStrategy ReloadingStrategy { get; set; }

        public decimal Reload(DateTime timestamp, decimal amountToReload, decimal amountPaid) => ReloadingStrategy.Reload(this, timestamp, amountToReload, amountPaid);

        public void AddLoad(decimal amount)
        {
            Load += amount;
        }

        public void SubtractLoad(decimal amount)
        {
            Load -= amount;
        }

        // Debugging
        public abstract void PrintContent();
    }
}
