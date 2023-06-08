using Ritzpa_Stock_Exchange.DTO.Outputs;
using Ritzpa_Stock_Exchange.Models;

namespace RitzpaStockExchange.DTO.Outputs
{
    public class StockLists
    { 
        public string StockName { get; } = string.Empty;
        public IEnumerable<CommandDTO>? SellCommands { get; } = null;
        public IEnumerable<CommandDTO>? BuyCommands { get; } = null;
        public IEnumerable<TradeDTO>? Trades { get; } = null;

        public int SellsTotal { get
            {
                return SellCommands.Sum(command => command.Total);
            } }

        public int BuyTotal { get
            {
                return BuyCommands.Sum(command => command.Total);
            } }


        public StockLists(Stock stock) 
        {
            StockName = stock.StockName;

            BuyCommands = stock.Buys.OrderByDescending(buyCommand => buyCommand.CurrentStockRate)
                .ThenBy(buyCommand => buyCommand.TimeStamp).Select(buyCommand => new CommandDTO
                {
                    Amount = buyCommand.Amount,
                    Price = (int)buyCommand.CurrentStockRate,
                    TimeStamp = buyCommand.TimeStamp.ToString("HH:mm:ss:SS")
                }).ToList();

            SellCommands = stock.Sells.OrderBy(sellCommand => sellCommand.CurrentStockRate)
                .ThenBy(sellCommand => sellCommand.TimeStamp)
                .Select(sellCommand => new CommandDTO
                {
                    Amount = sellCommand.Amount,
                    Price = (int)sellCommand.CurrentStockRate,
                    TimeStamp = sellCommand.TimeStamp.ToString("HH:mm:ss:SS")
                }).ToList();

            Trades = stock.Trades.OrderBy(trade => trade.Date)
                .Select(trade => new TradeDTO
                {
                    StockAmount = trade.Amount,
                    StockPrice = trade.StockPrice,
                    TradeDate = trade.Date
                }).ToList();
        }
    }
}
