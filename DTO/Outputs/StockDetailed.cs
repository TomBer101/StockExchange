namespace Ritzpa_Stock_Exchange.DTO.Outputs
{
    public class StockDetailed : StockSummary
    {
        public List<TradeDTO> Trades { get; set; }
    }
}
