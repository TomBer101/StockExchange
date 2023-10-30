using Microsoft.EntityFrameworkCore.Storage;
using Ritzpa_Stock_Exchange.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RitzpaStockExchange.Models
{
    public class UserAction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; } // Foreign key for User
        public User User { get; set; } // Navigation property to User
        public DateTime Time = new();
        public int CurrencyBefore { get; set; }
        public int CurrencyAfter { get; set; }
        public string? Stock { get; set; }
        public string Type { get; set; } = "Money Deposit";
        public int Price { get; set; }

        public UserAction(string _userId, User _user, int _currencyBefore,int _currencyAfter,int _price, string? _stock = null) 
        {
            UserId = _userId;
            User = _user;
            CurrencyBefore = _currencyBefore;
            Price = _price;
            if (_stock != null)
            {
                Stock = _stock;
                Type = _price > 0 ? "Sell" : "Buy";
            }

        }

        public UserAction() { }
    }
}
