using Microsoft.Extensions.Options;
using Ritzpa_Stock_Exchange.Interfaces;
using Ritzpa_Stock_Exchange.Models;

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

        private static DataContext db;

        public StocksRepository(DataContext _db)
        {
           db = _db;
        }


        public void Add(Stock stock)
        {
            try
            {

                if (db.Stocks.FirstOrDefault(s => s.StockName == stock.StockName) == null)
                {
                    db.Stocks.Add(stock);
                    db.SaveChanges();
                }
                else { throw new Exception($"{stock.StockName} already exist!"); }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Delete(string symbol)
        {
            try
            {
                Stock stock = db.Stocks.FirstOrDefault(s => s.StockName == symbol);
                if (stock != null)
                {
                    db.Stocks.Remove(stock);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Stock> GetAll()
        {
            try
            {
                IEnumerable<Stock> stocks =  db.Stocks;
                foreach (Stock stock in stocks)
                {
                    db.Entry(stock).Collection(s => s.Sells).Load();
                    db.Entry(stock).Collection(s => s.Buys).Load();

                }

                return stocks;
            }
            catch (Exception ex)
            {

                throw;
            };
        }

        public Stock GetStock(string i_symbol)
        {
            try
            {
                Stock stock = db.Stocks.FirstOrDefault(s => s.StockName == i_symbol);
                if(stock != null)
                {
                    db.Entry(stock).Collection(s => s.Sells).Load();
                    db.Entry(stock).Collection(s => s.Buys).Load();
                }

                return stock;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Update(string stoke, Stock newStock)
        {
            try
            {
                Stock toRemove = db.Stocks.FirstOrDefault(s => s.StockName == stoke);
                if (toRemove != null)
                {
                    db.Stocks.Remove(toRemove);
                    db.Stocks.Add(newStock);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void Update()
        {
            db.SaveChanges();
        }
    }
}
