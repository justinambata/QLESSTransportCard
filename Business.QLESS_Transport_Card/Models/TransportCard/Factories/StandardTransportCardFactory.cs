using Business.QLESS_Transport_Card.Strategies.Reloading;
using Business.QLESS_Transport_Card.Strategies.Trip;

namespace Business.QLESS_Transport_Card.Models.TransportCard.Factories
{
    public class StandardTransportCardFactory : TransportCardFactory
    {
        public override TransportCard CreateTransportCard()
        {
            var card = new StandardTransportCard
            {
                //Load = 100m,
                TripStrategy = new StandardTransportCardTripStrategy(),
                ReloadingStrategy = new StandardTransportCardReloadingStrategy()
            };
            card.AddLoad(100m);
            return card;
        }
    }
}
