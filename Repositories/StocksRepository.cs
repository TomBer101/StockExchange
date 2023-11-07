using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.Interfaces.IRepository;

namespace RitzpaStockExchange.Repositories
{
    public class StocksRepository : IStocksRepository
    {
        // TODO: Exmine what, where and how things should be async.

        //private static List<Stock> stocks = new List<Stock>()  // TODO: change it to use a dbContext
        //{
        //    new Stock() 
        //    {
        //        Buys = new List<Command>(),
        //        CompanyName = "Tomer",
        //        Price = 100,
        //        Sells = new List<Command>(),
        //        StockName = "TTT",
        //        Trades = new List<Trade>()
        //    }
        //};

        private static DataContext? db;

        public StocksRepository(DataContext _db)
        {
           db = _db;
        }

        public Stock GetAsync(string id)
        {
            return db.Stocks.FirstOrDefaultAsync(s => s.StockName == id).Result;
        }

        public async Task<bool> AddAsync(Stock stock)
        {
            try
            {
                var isStock = db?.Stocks.FirstOrDefaultAsync(s => s.StockName == stock.StockName).Result;
                if (isStock  == null)
                {
                    await addAndSaveAsync(stock);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;

        }

        public async Task DeleteAsync(string symbol)
        {
            try
            {
                Stock? stock = await db.Stocks.FirstOrDefaultAsync(s => s.StockName == symbol);
                if (stock != null)
                {
                    db.Stocks.Remove(stock);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            try
            {
                var stocks = await db.Stocks.ToListAsync();
                foreach (Stock stock in stocks)
                {
                    await db.Entry(stock).Collection(s => s.Sells).LoadAsync();
                    await db.Entry(stock).Collection(s => s.Buys).LoadAsync();

                }

                return stocks;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Stock>();
            };
        }

        public async Task<Stock?> GetStockAsync(string i_symbol)
        {
            try
            {
                Stock stock = await db.Stocks.FirstOrDefaultAsync(s => s.StockName == i_symbol);
                if(stock != null)
                {
                    await db.Entry(stock).Collection(s => s.Sells).LoadAsync();
                    await db.Entry(stock).Collection(s => s.Buys).LoadAsync();
                }

                return stock;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task UpdateAsync(string stoke, Stock newStock)
        {
            try
            {
                var stockToRemove = await db.Stocks.FirstOrDefaultAsync(s => s.StockName == stoke);
                if (stockToRemove != null)
                {
                    db.Stocks.Remove(stockToRemove);
                    await db.Stocks.AddAsync(newStock);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private async Task addAndSaveAsync(Stock stock)
        {
            await db.Stocks.AddAsync(stock);
            await db.SaveChangesAsync();
        }

        public void Update()
        {
            db.SaveChanges();
        }
    }
}
