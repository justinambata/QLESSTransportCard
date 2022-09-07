using System;
using Business.QLESS_Transport_Card.Models.TransportCard;

namespace Business.QLESS_Transport_Card.Strategies.Reloading
{
    public abstract class ReloadingStrategy
    {
        public virtual decimal Reload(TransportCard transportCard, DateTime timestamp, decimal amountToLoad, decimal amountPaid)
        {
            if (amountToLoad < 100 || amountToLoad > 1000) throw new Exception("Load amount invalid.");
            if (amountPaid < amountToLoad) throw new Exception("Amount paid is less than the amount to be loaded.");

            var balance = transportCard.Load;
            var maxLoadableAmount = 10000m - balance;
            var actualLoadedAmount = maxLoadableAmount < amountToLoad ? maxLoadableAmount : amountToLoad;
            //transportCard.Load += actualLoadedAmount;
            transportCard.AddLoad(actualLoadedAmount);
            var change = amountPaid - actualLoadedAmount;

#if DEBUG
            Console.WriteLine($"> Starting Balance = {balance}");
            Console.WriteLine($"> Amount Paid = {amountPaid}");
            Console.WriteLine($"> Amount To Load = {amountToLoad}");
            Console.WriteLine($"> Max Loadable Amount = {maxLoadableAmount}");
            Console.WriteLine($"> Actual Loaded Amount = {actualLoadedAmount}");
            Console.WriteLine($"> Change = {change}");
            Console.WriteLine($"> Ending Balance = {transportCard.Load}");
            Console.WriteLine();
#endif

            return change;
        }
    }
}
