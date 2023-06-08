using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;

namespace RitzpaStockExchange.Interfaces
{
    public interface ISubmmitOfferStradegy
    {
        SubmmitOfferResult HandleCommandSubmition(Stock stock, Command command);
    }
}
