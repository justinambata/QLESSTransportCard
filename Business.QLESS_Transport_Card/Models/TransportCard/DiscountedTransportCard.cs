using System;
using System.Globalization;
using Business.QLESS_Transport_Card.Models.IdentificationCard;

namespace Business.QLESS_Transport_Card.Models.TransportCard
{
    public class DiscountedTransportCard : TransportCard
    {
        public IIdentificationCard IdentificationCard
        {
            get => (IIdentificationCard)CustomProperties["IdentificationCard"];
            set => CustomProperties["IdentificationCard"] = value;
        }

        public override DateTime ValidUntil => LastUsed.AddYears(3);

        public override void PrintContent()
        {
            Console.WriteLine();
            Console.WriteLine("TRANSPORT CARD DETAILS:");
            Console.WriteLine("Card Type: Discounted");
            Console.WriteLine($"Guid: {Guid}");
            Console.WriteLine($"Last Used: {LastUsed.ToString(CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Valid Until: {ValidUntil.ToString(CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Load: {Load}");
            Console.WriteLine($"ID: {IdentificationCard?.Id}");
            Console.WriteLine();
        }
    }
}
