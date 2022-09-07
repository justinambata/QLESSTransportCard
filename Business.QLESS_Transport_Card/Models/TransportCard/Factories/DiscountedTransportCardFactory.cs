using Business.QLESS_Transport_Card.Models.IdentificationCard.Factories;
using Business.QLESS_Transport_Card.Strategies.Reloading;
using Business.QLESS_Transport_Card.Strategies.Trip;

namespace Business.QLESS_Transport_Card.Models.TransportCard.Factories
{
    public class DiscountedTransportCardFactory : TransportCardFactory
    {
        public override TransportCard CreateTransportCard()
        {
            var card = new DiscountedTransportCard
            {
                //Load = 500m,
                TripStrategy = new DiscountedTransportCardTripStrategy(),
                ReloadingStrategy = new DiscountedTransportCardReloadingStrategy()
            };
            card.AddLoad(500m);
            return card;
        }

        public TransportCard CreateTransportCard(IIdentificationCardFactory identificationCardFactory, string id)
        {
            var card = new DiscountedTransportCard
            {
                //Load = 500m,
                TripStrategy = new DiscountedTransportCardTripStrategy(),
                ReloadingStrategy = new DiscountedTransportCardReloadingStrategy(),
                IdentificationCard = identificationCardFactory.CreateIdentificationCard(id)
            };
            card.AddLoad(500m);
            return card;
        }
    }
}
