using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.DTO.Outputs;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;
using RitzpaStockExchange.Factories;
using RitzpaStockExchange.Interfaces.IRepository;
using RitzpaStockExchange.Interfaces.IService;
using RitzpaStockExchange.Interfaces.IStrategy;

namespace Ritzpa_Stock_Exchange.Managers
{
    public class StocksService : IStocksService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task<Stock> AddAsync(StockInput stockInput)
        {
            var stock = new Stock
            {
                StockName = stockInput.StockName.ToUpper(),
                Price = stockInput.Price,
                CompanyName = stockInput.CompanyName
            };

            await _stocksRepository.AddAsync(stock);
            return stock;
        }

        public async Task DeleteAsync(string StockSymbol)
        {
            await _stocksRepository.DeleteAsync(StockSymbol.ToUpper());
        }

        public async Task<IEnumerable<StockSummary>> GetAllStocksAsync()
        {
            IEnumerable<StockSummary>? result = null;
            IEnumerable<Stock?> stocks = await _stocksRepository.GetAllAsync();
            
            if(stocks.Count() != 0)
            {
                result = stocks.Select(stock => new StockSummary
                {
                    StockName = stock.StockName,
                    Company = stock.CompanyName,
                    CurrentPrice = stock.Price,
                    AmountOfTrades = stock.Trades?.Count,
                    TotalTradesSum = stock.Trades?.Sum(trade => trade.Total)
                });
            }
            return result;
        }

        public async Task<StockDetailed> GetStockAsync(string stockSymbol)
        {
            StockDetailed? result = null;
            Stock stock = await _stocksRepository.GetStockAsync(stockSymbol.ToUpper());

            if(stock != null)
            {
                result = new StockDetailed()
                {
                    StockName = stock.StockName.ToUpper(),
                    Company = stock.CompanyName,
                    CurrentPrice = stock.Price,
                    AmountOfTrades = stock.Trades?.Count,
                    TotalTradesSum = stock.Trades?.Sum(trade => trade.Total),
                    Trades = stock.Trades?.Select(trade => new TradeDTO { StockAmount = trade.Amount, StockPrice = trade.StockPrice, TradeDate = trade.Date }).ToList()
                };
            }

            return result;
        }

        public async Task<SubmmitOfferResult> submitOfferAsync(CommandInput commandInput, User user) // TODO: change this method to return list of trades that has been made.
        {
            Stock stock = await _stocksRepository.GetStockAsync(commandInput.StockSymbol.ToUpper());

            if(stock == null)
            {
                throw new Exception($"There is no stock with the name {commandInput.StockSymbol}");
            }

            Command newCommand = new Command()
            {
                Amount = commandInput.Amount,
                CommandType = commandInput.CommandType,
                CommandWay = commandInput.Way,
            };

            newCommand.SetInitiator(user);

            ISubmmitOfferStradegy submmitStrategy = OfferMatchStrategyFactory.CreatSubmmitStrategy(newCommand.CommandType);
            SubmmitOfferResult resultToUser = submmitStrategy.HandleCommandSubmition(stock, newCommand);
            _stocksRepository.Update();
            return resultToUser;
        }

        //public IEnumerable<StockLists> GetStocksLists()
        //{
        //    IEnumerable<Stock> stocks = _stocksRepository.GetAll();
        //    //IEnumerable<StockLists> result = stocks.Select(stock => new StockLists(stock)).ToList();

        //    //return result;
        //    foreach(Stock stock in stocks)
        //    {
        //        yield return new StockLists(stock);
        //    }
        //}

        public async Task<StockLists> GetStockListsAsync(string stockSymbol)
        {
            Stock stock = await _stocksRepository.GetStockAsync(stockSymbol);
            return new StockLists(stock);
        }
    }
}
