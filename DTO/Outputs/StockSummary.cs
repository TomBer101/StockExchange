namespace Ritzpa_Stock_Exchange.DTO.Outputs
{
    public class StockSummary
    {
        public string StockName { get; set; }
        public string Company { get; set; }
        public int CurrentPrice { get; set; }
        public int? AmountOfTrades { get; set; }
        public int? TotalTradesSum { get; set; }
    }
}
