using System.IO;

namespace Business.QLESS_Transport_Card.Models.IdentificationCard.Factories
{
    public class PersonWithDisabilityIdFactory : IIdentificationCardFactory
    {
        public IIdentificationCard CreateIdentificationCard(string id)
        {
            var idCard = new PersonWithDisabilityId(id);
            if (!idCard.IsIdValid) throw new InvalidDataException("ID provided is invalid.");
            return idCard;
        }
    }
}
