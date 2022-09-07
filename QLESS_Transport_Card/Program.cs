using System;
using Business.QLESS_Transport_Card.Models.IdentificationCard.Factories;
using Business.QLESS_Transport_Card.Models.TransportCard.Factories;

namespace QLESS_Transport_Card
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("QLESS Transport Card");

            #region Sections A & B Part 1: Purchasing of transport card
            // Sections A & B Part 1: Purchasing of transport card
            Console.WriteLine("*** Sections A & B Part 1: Purchase");
            var standard = new StandardTransportCardFactory().CreateTransportCard();
            var discountedSeniorCitizen = new DiscountedTransportCardFactory().CreateTransportCard(new SeniorCitizenIdFactory(), "12-3456-7890");
            var discountedPWD = new DiscountedTransportCardFactory().CreateTransportCard(new PersonWithDisabilityIdFactory(), "1234-5678-90ab");
            
            // Output: After purchasing transport card
            standard.PrintContent();
            discountedSeniorCitizen.PrintContent();
            discountedPWD.PrintContent();
            #endregion

            #region Sections A & B Part 2 Exit turnstile scenario
            // Sections A & B Part 2: Exit turnstile scenario
            Console.WriteLine("*** Sections A & B Part 2: Exit turnstile scenario");

            // Input: turnstile Exit scenario
            standard.TapIn(DateTime.UtcNow, "station 1");
            standard.TapOut(DateTime.UtcNow, "station 5");
            discountedSeniorCitizen.TapIn(DateTime.UtcNow, "station 1");
            discountedSeniorCitizen.TapOut(DateTime.UtcNow, "station 5");

            // Output: After exiting turnstile
            standard.PrintContent();
            discountedSeniorCitizen.PrintContent();
            discountedPWD.PrintContent(); 
            #endregion
            
            #region Section C: Discount Definitions
            // Section C: Discount Definitions
            Console.WriteLine("*** Section C: Discount Definitions");

            // I/O 1 (20%)
            discountedPWD.TapIn(DateTime.UtcNow, "station 1");
            discountedPWD.TapOut(DateTime.UtcNow, "station 5");
            discountedPWD.PrintContent();

            // I/O 2 (23%)
            discountedPWD.TapIn(DateTime.UtcNow, "station 5");
            discountedPWD.TapOut(DateTime.UtcNow, "station 2");
            discountedPWD.PrintContent();

            // I/O 3 (20%)
            discountedPWD.TapIn(DateTime.UtcNow, "station 2");
            discountedPWD.TapOut(DateTime.UtcNow.AddDays(1), "station 1"); // next day tap out
            discountedPWD.PrintContent();

            // I/O 4 (23%)
            discountedPWD.TapIn(DateTime.UtcNow.AddDays(1), "station 1");
            discountedPWD.TapOut(DateTime.UtcNow.AddDays(1), "station 2");
            discountedPWD.PrintContent();

            // I/O 5 (23%)
            discountedPWD.TapIn(DateTime.UtcNow.AddDays(1), "station 2");
            discountedPWD.TapOut(DateTime.UtcNow.AddDays(1), "station 3");
            discountedPWD.PrintContent();

            // I/O 6 (23%)
            discountedPWD.TapIn(DateTime.UtcNow.AddDays(1), "station 3");
            discountedPWD.TapOut(DateTime.UtcNow.AddDays(1), "station 4");
            discountedPWD.PrintContent();

            // I/O 7 (23%)
            discountedPWD.TapIn(DateTime.UtcNow.AddDays(1), "station 4");
            discountedPWD.TapOut(DateTime.UtcNow.AddDays(1), "station 5");
            discountedPWD.PrintContent();

            // I/O 8 (20%)
            discountedPWD.TapIn(DateTime.UtcNow.AddDays(1), "station 5");
            discountedPWD.TapOut(DateTime.UtcNow.AddDays(1), "station 1");
            discountedPWD.PrintContent(); 
            #endregion
            
            #region Section D: Reloading
            // Section D: Reloading
            Console.WriteLine("*** Section D: Reloading");
            standard.PrintContent();
            standard.Reload(DateTime.UtcNow, 1000, 1000); // exact no change; end balance = 1,075.00
            standard.Reload(DateTime.UtcNow, 1000, 1500); // 500.00 change; end balance = 2,075.00
            standard.Reload(DateTime.UtcNow, 1000, 1100); // 100.00 change; end balance = 3,075.00
            standard.Reload(DateTime.UtcNow, 500, 1000); // 500.00 change; end balance = 3,575.00
            standard.Reload(DateTime.UtcNow, 1000, 1000); // exact no change; end balance = 4,575.00
            standard.Reload(DateTime.UtcNow, 1000, 1000); // exact no change; end balance = 5,575.00
            standard.Reload(DateTime.UtcNow, 1000, 1000); // exact no change; end balance = 6,575.00
            standard.PrintContent();
            standard.Reload(DateTime.UtcNow, 1000, 2000); // 1,000.00 change; end balance = 7,575.00
            standard.Reload(DateTime.UtcNow, 1000, 1000); // exact no change; end balance = 8,575.00
            standard.Reload(DateTime.UtcNow, 1000, 1000); // exact no change; end balance = 9,575.00
            standard.Reload(DateTime.UtcNow, 1000, 1000); // 575.00 change; end balance = 10,000.00
            standard.PrintContent();
            standard.Reload(DateTime.UtcNow, 100, 500); // 500.00 change; end balance = 10,000.00
            standard.PrintContent(); 
            #endregion
        }
    }
}
