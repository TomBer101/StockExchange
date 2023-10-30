using Microsoft.AspNetCore.Mvc.Diagnostics;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;
using RitzpaStockExchange.Interfaces.IStrategy;

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
            var itemsToRemove = new List<Command>();

            foreach (Command sellCommand in  sellCommands)
            {
                if (sellCommand.InitiatorId == buyCommand.InitiatorId) { continue; }

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
                        StockPrice = stockPrice,
                        Buyer = buyCommand.Initiator.Email,
                        Seller = sellCommand.Initiator.Email,
                        
                    });

                    buyCommand.Amount -= tradeAmount;
                    sellCommand.Amount -= tradeAmount;

                    int holder = buyCommand.Initiator.HoldingsValue;
                    holder = sellCommand.Initiator.HoldingsValue;
                    stock.Price = stockPrice;

                    buyCommand.Initiator.UpdateUserHoldings(stock, tradeAmount);
                    sellCommand.Initiator.UpdateUserHoldings(stock, tradeAmount * (-1));

                    if(sellCommand.Amount == 0)
                    {
                        itemsToRemove.Add(sellCommand);
                    }

                }
                else break;
            }

            foreach (var item in itemsToRemove)
            {
                stock.Sells.Remove(item);
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
            var itemsToRemove = new List<Command>();

            result = buyCommands.Where(buyCommand => buyCommand.CurrentStockRate >= sellCommand.CurrentStockRate &&
                                        sellCommand.InitiatorId != buyCommand.InitiatorId)
                .Select(buyCommand =>
                {
                    int tradeAmount = Math.Min(buyCommand.Amount, sellCommand.Amount);
                    int tradePrice = (int)buyCommand.CurrentStockRate;

                    sellCommand.Amount -= tradeAmount;
                    buyCommand.Amount -= tradeAmount;

                    int holder = buyCommand.Initiator.HoldingsValue;
                    holder = sellCommand.Initiator.HoldingsValue;
                    stock.Price = tradePrice;

                    buyCommand.Initiator.UpdateUserHoldings(stock, tradeAmount);
                    sellCommand.Initiator.UpdateUserHoldings(stock, tradeAmount * (-1));

                    if (buyCommand.Amount == 0)
                    {
                        itemsToRemove.Add(buyCommand);
                    }

                    return new Trade
                    {
                        Amount = tradeAmount,
                        Date = DateTime.Now,
                        Stock = stock,
                        StockName = stock.StockName,
                        StockPrice = tradePrice,
                        Buyer = buyCommand.Initiator.Name,
                        Seller= sellCommand.Initiator.Name
                    };
                }).ToList();

            foreach (var item in itemsToRemove)
            {
                stock.Buys.Remove(item);
            }

            if (sellCommand.Amount > 0)
            {
                stock.Sells.Add(sellCommand);
            }

            if (result.Count() == 0) result = null;

            return result;
        }
    }
}
