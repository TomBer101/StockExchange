using Ritzpa_Stock_Exchange.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RitzpaStockExchange.Models
{
    public class UserStock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; } // Foreign key for User
        public User User { get; set; } // Navigation property to User
        public string StockId { get; set; } // Foreign key for Stock
        public Stock Stock { get; set; } // Navigation property to Stock
        public int Amount { get; set; }

        public int Value
        {
            get
            {
                return Amount * Stock.Price;
            }
        }

    }
}
