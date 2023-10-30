using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;

namespace RitzpaStockExchange.Interfaces.IStrategy
{
    public interface ISubmmitOfferStradegy
    {
        SubmmitOfferResult HandleCommandSubmition(Stock stock, Command command);
    }
}
