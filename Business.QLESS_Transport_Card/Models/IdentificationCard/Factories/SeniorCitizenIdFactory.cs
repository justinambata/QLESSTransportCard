using System.IO;

namespace Business.QLESS_Transport_Card.Models.IdentificationCard.Factories
{
    public class SeniorCitizenIdFactory : IIdentificationCardFactory
    {
        public IIdentificationCard CreateIdentificationCard(string id)
        {
            var idCard = new SeniorCitizenId(id);
            if (!idCard.IsIdValid) throw new InvalidDataException("ID provided is invalid.");
            return idCard;
        }
    }
}
