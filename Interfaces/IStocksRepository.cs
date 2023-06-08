using Ritzpa_Stock_Exchange.Models;

namespace Ritzpa_Stock_Exchange.Interfaces
{
    public interface IStocksRepository
    {
        IEnumerable<Stock> GetAll();
        Stock GetStock(string i_symbol);
        void Add(Stock stock);
        void Delete(string symbol);
        void Update(string stoke, Stock newStock);

        void Update();
        //void submitCommand(Command newCommand);
    }
}
