using System.ComponentModel.DataAnnotations;

namespace Ritzpa_Stock_Exchange.Models
{
    public class Stock
    {
        [Key]
        public string? StockName { get; set;}
        public string? CompanyName { get; set; }
        public int Price { get; set;}
        public int TradeCycle
        {
            get
            {
                return Trades?.Sum(trade => trade.Total) ?? 0;
            }
            set
            {
                _tradeCycle = value;
            }
        }

        private int _tradeCycle = 0;

        public List<Trade>? Trades { get; set; } = new();

        public virtual List<Command>? Sells { get;  } = new();
        public virtual List<Command>? Buys { get; set; } = new();

        public IEnumerable<Command>? GetSortedSells()
        {
            return Sells?.OrderBy(command => command.CurrentStockRate).ThenBy(command => command.TimeStamp);
        }

        public IEnumerable<Command>? GetSortedBuys()
        {
            return Buys?.OrderByDescending(command => command.CurrentStockRate).ThenBy(command => command.TimeStamp);
        }

    }
}
