using Ritzpa_Stock_Exchange.DTO.Outputs;
using Ritzpa_Stock_Exchange.Models;

namespace RitzpaStockExchange.DTO.Outputs
{
    public class SubmmitOfferResult // Maybe to do an abstarc class or interface for all the results?
    {
        public enum OfferStatus { All, Some, None};

        public OfferStatus Status { get; set; }
        public IEnumerable<TradeDTO>? Trades { get; }

        public SubmmitOfferResult(Command command, IEnumerable<Trade> trades) 
        {
            if (trades == null) Status = OfferStatus.None;
            else if(command.Amount == 0) Status = OfferStatus.All;
            else Status = OfferStatus.Some;

            Trades = convertTradesToDTO(trades);
        }

        private IEnumerable<TradeDTO> convertTradesToDTO(IEnumerable<Trade> trades)
        {
            List<TradeDTO>? result = null;


            if (trades != null)
            {
                result = new List<TradeDTO>();
                result = trades.Select(trade => new TradeDTO { StockAmount = trade.Amount, StockPrice = trade.StockPrice, TradeDate = trade.Date }).ToList();
            }

            return result;
        }
    }
}
