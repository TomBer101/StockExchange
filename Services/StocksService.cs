using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.DTO.Outputs;
using Ritzpa_Stock_Exchange.Interfaces;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;
using RitzpaStockExchange.Interfaces;
using RitzpaStockExchange.Services;

namespace Ritzpa_Stock_Exchange.Managers
{
    public class StocksService : IStocksService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public void Add(StockInput stockInput)
        {
            var stock = new Stock
            {
                StockName = stockInput.StockName.ToUpper(),
                Price = stockInput.Price,
                CompanyName = stockInput.CompanyName
            };

            _stocksRepository.Add(stock);
        }

        public void Delete(string StockSymbol)
        {
            _stocksRepository.Delete(StockSymbol.ToUpper());
        }

        public IEnumerable<StockSummary> GetAllStocks()
        {
            IEnumerable<StockSummary>? result = null;
            IEnumerable<Stock> stocks = _stocksRepository.GetAll();
            
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

        public StockDetailed GetStock(string stockSymbol)
        {
            StockDetailed? result = null;
            Stock stock = _stocksRepository.GetStock(stockSymbol.ToUpper());

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

        public SubmmitOfferResult submitCommand(CommandInput commandInput) // TODO: change this method to return list of trades that has been made.
        {
            Stock stock = _stocksRepository.GetStock(commandInput.StockSymbol.ToUpper());

            if(stock == null)
            {
                throw new Exception($"There is no stock with the name {commandInput.StockSymbol}");
            }

            Command newCommand = new Command()
            {
                Amount = commandInput.Amount,
                CommandType = commandInput.CommandType,
                CommandWay = commandInput.Way,
                CurrentStockRate = commandInput.Balance,
                //Stock = stock,
                //StockName = stock.StockName
            };

            ISubmmitOfferStradegy submmitStrategy = CommandMatchStrategyFactory.CreatSubmmitStrategy(newCommand.CommandType);
            SubmmitOfferResult resultToUser = submmitStrategy.HandleCommandSubmition(stock, newCommand);
            _stocksRepository.Update();
            return resultToUser;
        }

        public IEnumerable<StockLists> GetStocksLists()
        {
            IEnumerable<Stock> stocks = _stocksRepository.GetAll();
            IEnumerable<StockLists> result = stocks.Select(stock => new StockLists(stock)).ToList();

            return result;
        }
    }
}
