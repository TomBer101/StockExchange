using Ritzpa_Stock_Exchange.Managers;
using RitzpaStockExchange.Models;
using RitzpaStockExchange.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Ritzpa_Stock_Exchange.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] HashSalt { get; set; }

        //public Dictionary<string, Holding>? RseHoldings { get; set; } = new Dictionary<string, Holding>();
        public ICollection<UserStock>? UserStocks { get; set; }
        public ICollection<UserAction> UserActions { get; set; } 
        public int cashMoney { get; set; } = 0;

        private int sharesValue = 0;
        public int HoldingsValue 
        { 
            get
            {
                PrevMoney = sharesValue;
                sharesValue = 0;
                if (UserStocks != null && UserStocks.Any()) 
                {
                    sharesValue = UserStocks.Sum(item => item.Value); 
                }

                
                return sharesValue;
            } 
        }

        public User()
        {
            if (UserStocks != null && UserStocks.Any())
            {
                sharesValue = UserStocks.Sum(item => item.Value);
            }

            PrevMoney = sharesValue;
        }

        public int PrevMoney { get; private set; }

        public void UpdateUserHoldings(Stock stock, int amount)
        {
            var share = UserStocks?.FirstOrDefault(currentShare => currentShare.StockId == stock.StockName);
            int currencyBefor = this.PrevMoney + cashMoney;

            if(share != null)
            {
                share.Amount += amount;

                if (share.Amount == 0)
                {
                    UserStocks.Remove(share);
                }
            }
            else
            {
                UserStocks.Add(new UserStock 
                {
                    Amount = amount, 
                    Stock = stock,
                    StockId = stock.StockName,
                    User = this,
                    UserId = this.Name,
                });
            }

            UserActions.Add(new UserAction(this.Name, this, currencyBefor, HoldingsValue + cashMoney, stock.Price * amount, stock.StockName));
        }

        //private void updateStockPrise(string symbol, int newPrice)
        //{
        //    if(RseHoldings.TryGetValue(symbol, out Holding currentHolding))
        //    {
        //        currentHolding.Balance = newPrice;
        //        RseHoldings[symbol] = currentHolding;
        //    }
        //}

        //public void SubscribeToStocks(IEnumerable<Stock> stocks)
        //{
        //    foreach (var stock in stocks)
        //    {
        //        if(RseHoldings.ContainsKey(stock.StockName))
        //        {
        //            stock.PriceChanged += updateStockPrise;
        //        }
        //    }
        //}

    }
}
