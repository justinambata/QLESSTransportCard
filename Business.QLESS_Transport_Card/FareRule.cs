using Business.QLESS_Transport_Card.Models.Trip;

namespace Business.QLESS_Transport_Card
{
    public class FareRule
    {
        public static decimal ComputeFare(Tap tapIn, Tap tapOut) => 10m; // sample only
    }
}
