namespace Ritzpa_Stock_Exchange.DTO.Outputs
{
    public class CommandDTO
    {
        public string Type { get; set; }
        public string CommandWay { get; set; }
        public string StockSymbol { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }

        public string TimeStamp { get; set; }

        public int Total { get
            {
                return Amount * Price;
            } }

    }
}
