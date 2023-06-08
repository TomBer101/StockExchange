using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.Interfaces;
using RitzpaStockExchange.SubmmitOfferStrategies;

namespace RitzpaStockExchange.Services
{
    public class CommandMatchStrategyFactory
    {
        public static ISubmmitOfferStradegy CreatSubmmitStrategy(Command.Type commandType)
        {
            switch(commandType)
            {
                case Command.Type.LMT:
                    return new SubmmitLmtStrategy();
                default:
                    throw new ArgumentException($"There is no such type {commandType.ToString()}");
            }
        }
    }
}
