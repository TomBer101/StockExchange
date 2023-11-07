using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.Models;

namespace RitzpaStockExchange.Interfaces.IManagers
{
    public interface IPublishStockManager
    {
        Task<Stock> PublishStock(StockInput stockInput);
    }
}
