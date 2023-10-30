using Ritzpa_Stock_Exchange.Models;

namespace RitzpaStockExchange.Interfaces.IRepository
{
    public interface IStocksRepository
    {
        Task<IEnumerable<Stock?>> GetAllAsync();
        Task<Stock?> GetStockAsync(string i_symbol);
        Task AddAsync(Stock stock);
        Task DeleteAsync(string symbol);
        Task UpdateAsync(string stoke, Stock newStock);

        void Update();
        //void submitCommand(Command newCommand);
    }
}
