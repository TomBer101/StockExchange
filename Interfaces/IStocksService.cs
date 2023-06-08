using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.DTO.Outputs;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;

namespace Ritzpa_Stock_Exchange.Interfaces
{
    public interface IStocksService
    {
        void Add(StockInput stockInput);
        IEnumerable<StockSummary> GetAllStocks();
        StockDetailed GetStock(string stockSymbol);
        void Delete(string StockSymbol);
        SubmmitOfferResult submitCommand(CommandInput commandInput);
        public IEnumerable<StockLists> GetStocksLists();

    }
}
