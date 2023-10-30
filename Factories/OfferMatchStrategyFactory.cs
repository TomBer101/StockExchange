using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.Interfaces.IStrategy;
using RitzpaStockExchange.SubmmitOfferStrategies;

namespace RitzpaStockExchange.Factories
{
    public class OfferMatchStrategyFactory
    {
        public static ISubmmitOfferStradegy CreatSubmmitStrategy(Command.Type commandType)
        {
            switch (commandType)
            {
                case Command.Type.LMT:
                    return new SubmmitLmtStrategy();
                case Command.Type.MKT:
                    return new SubmmitMktStrategy();
                default:
                    throw new ArgumentException($"There is no such type {commandType.ToString()}");
            }
        }
    }
}
