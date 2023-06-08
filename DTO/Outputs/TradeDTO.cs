namespace Ritzpa_Stock_Exchange.DTO.Outputs
{
    public class TradeDTO
    {
        public DateTime? TradeDate { get; set; }
        public int StockAmount { get; set; }
        public int StockPrice { get; set; }
        public int TotalPrice { get { return StockAmount * StockPrice; } }
    }
}
