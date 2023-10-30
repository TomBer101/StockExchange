using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ritzpa_Stock_Exchange.Models
{
    public class Trade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int StockPrice { get; set; }
        public int Total { get { return Amount * StockPrice; } }

        public string StockName { get; set; }
        public virtual Stock Stock { get; set; }

        public string Buyer { get; set; }
        public string Seller { get; set; }

    }
}
