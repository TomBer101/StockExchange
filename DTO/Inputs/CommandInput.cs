using Ritzpa_Stock_Exchange.Models;

namespace Ritzpa_Stock_Exchange.DTO.Inputs
{
    public class CommandInput
    {
        public Command.Type CommandType { get; set; }
        public Command.Way Way { get; set; }
        public int Amount { get; set; }
        public int? Balance { get; set; }
        public string StockSymbol { get; set; }


    }
}
