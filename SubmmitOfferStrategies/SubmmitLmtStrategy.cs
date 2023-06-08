using Microsoft.AspNetCore.Mvc.Diagnostics;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;
using RitzpaStockExchange.Interfaces;

namespace RitzpaStockExchange.SubmmitOfferStrategies
{
    public class SubmmitLmtStrategy : ISubmmitOfferStradegy
    {
        public SubmmitOfferResult HandleCommandSubmition(Stock stock, Command command)
        {
            IEnumerable<Trade> trades = null;

            if(command.CommandWay == Command.Way.Buy)
            {
                command.BuyStock = stock;
                command.BuyStockName = stock.StockName;
                trades = buyOfferHelper(stock, command);
            } 
            else
            {
                command.SellStock = stock;
                command.SellStockName = stock.StockName;
                trades = sellOfferHelper(stock, command);
            }

            return new SubmmitOfferResult(command, trades);

        }

        private IEnumerable<Trade> buyOfferHelper(Stock stock, Command buyCommand)
        {
            List<Trade> result = new List<Trade>();
            IEnumerable<Command> sellCommands = stock.GetSortedSells();

            foreach (Command sellCommand in  sellCommands)
            {
                if (sellCommand.CurrentStockRate <= buyCommand.CurrentStockRate && buyCommand.Amount > 0)
                {
                    int tradeAmount = Math.Min(sellCommand.Amount, buyCommand.Amount);
                    int stockPrice = (int)sellCommand.CurrentStockRate;

                    result.Add(new Trade()
                    {
                        Amount = tradeAmount,
                        Date = new DateTime(),
                        Stock = stock,
                        StockName = stock.StockName,
                        StockPrice = stockPrice
                    });

                    buyCommand.Amount -= tradeAmount;
                    sellCommand.Amount -= tradeAmount;
                    stock.Price = stockPrice;

                    if(sellCommand.Amount == 0)
                    {
                        stock.Sells.Remove(sellCommand);
                    }

                }
                else break;
            }

            if (buyCommand.Amount > 0)
            {
                stock.Buys.Add(buyCommand);
            }

            if (result.Count == 0) result = null;

            return result;
        }

        private IEnumerable<Trade> sellOfferHelper(Stock stock, Command sellCommand)
        {
            IEnumerable<Trade> result = new List<Trade>();
            IEnumerable<Command> buyCommands = stock.GetSortedBuys();

            result = buyCommands.Select(buyCommand =>
            {
                int tradeAmount = Math.Min(buyCommand.Amount, sellCommand.Amount);
                int tradePrice = (int)buyCommand.CurrentStockRate;

                sellCommand.Amount -= tradeAmount;
                buyCommand.Amount -= tradeAmount;
                stock.Price = tradePrice;

                if (buyCommand.Amount == 0)
                {
                    stock.Buys.Remove(buyCommand);
                }

                return new Trade
                {
                    Amount = tradeAmount,
                    Date = DateTime.Now,
                    Stock = stock,
                    StockName = stock.StockName,
                    StockPrice = tradePrice,
                };
            }).ToList();

            if(sellCommand.Amount > 0)
            {
                stock.Sells.Add(sellCommand);
            }

            if (result.Count() == 0) result = null;

            return result;
        }
    }
}
