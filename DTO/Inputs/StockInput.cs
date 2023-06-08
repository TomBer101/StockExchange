using Ritzpa_Stock_Exchange.Models;

namespace Ritzpa_Stock_Exchange.DTO.Inputs
{
    public class StockInput
    {
        public string? CompanyName { get; set; }
        public string? StockName { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
    }

}
