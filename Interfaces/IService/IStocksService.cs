using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.DTO.Outputs;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;

namespace RitzpaStockExchange.Interfaces.IService
{
    public interface IStocksService
    {
        Task<Stock> AddAsync(StockInput stockInput);
        Task<IEnumerable<StockSummary>> GetAllStocksAsync();
        Task<StockDetailed> GetStockAsync(string stockSymbol);
        Task DeleteAsync(string StockSymbol);
        Task<SubmmitOfferResult> submitOfferAsync(CommandInput commandInput, User user);
        //public IEnumerable<StockLists> GetStocksLists();
        Task<StockLists> GetStockListsAsync(string stockSymbol);

    }
}
