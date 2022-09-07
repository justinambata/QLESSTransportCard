using System;
using System.Globalization;

namespace Business.QLESS_Transport_Card.Models.TransportCard
{
    public class StandardTransportCard : TransportCard
    {
        public override DateTime ValidUntil => LastUsed.AddYears(5);

        // Debug
        public override void PrintContent()
        {
            Console.WriteLine();
            Console.WriteLine("TRANSPORT CARD DETAILS:");
            Console.WriteLine("Card Type: Standard");
            Console.WriteLine($"Guid: {Guid}");
            Console.WriteLine($"Last Used: {LastUsed.ToString(CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Valid Until: {ValidUntil.ToString(CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Load: {Load}");
            Console.WriteLine();
        }
    }
}
