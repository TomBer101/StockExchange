using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.Models;

namespace RitzpaStockExchange.Interfaces.IManagers
{
    public interface IStockManager
    {

        IEnumerable<Stock> GetStocks();

        Stock GetStock(int id);

        bool CreateStock(StockInput stock);
        

    }
}