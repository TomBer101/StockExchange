using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;
using RitzpaStockExchange.Interfaces.IStrategy;

namespace RitzpaStockExchange.SubmmitOfferStrategies
{
    public class SubmmitMktStrategy : ISubmmitOfferStradegy
    {
        //public SubmmitOfferResult HandleCommandSubmition(Stock stock, Command command)
        //{
        //    IEnumerable<Trade> trades = null;

        //    if (command.CommandWay == Command.Way.Buy)
        //    {
        //        command.BuyStock = stock;
        //        command.BuyStockName = stock.StockName;
        //        trades = buyOfferHelper(stock, command);
        //    }
        //    else
        //    {
        //        command.SellStock = stock;
        //        command.SellStockName = stock.StockName;
        //        trades = sellOfferHelper(stock, command);
        //    }

        //    return new SubmmitOfferResult(command, trades);
        //}

        //private IEnumerable<Trade> buyOfferHelper(Stock stock, Command command)
        //{

        //}
        public SubmmitOfferResult HandleCommandSubmition(Stock stock, Command command)
        {
            throw new NotImplementedException();
        }
    }
}
