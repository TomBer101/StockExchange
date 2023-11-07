using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.Interfaces.IManagers;

namespace RitzpaStockExchange.Managers
{
    public class StockManager :IStockManager
    {

        public StockManager()
        {
            
        }

        public bool CreateStock(StockInput stock)
        {
            throw new NotImplementedException();
        }

        public Stock GetStock(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stock> GetStocks()
        {
            throw new NotImplementedException();
        }
    }
}
