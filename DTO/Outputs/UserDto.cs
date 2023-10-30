using Ritzpa_Stock_Exchange.Models;

namespace RitzpaStockExchange.DTO.Outputs
{
    public class UserDto
    {
        public string Name { get; }
        public Dictionary<string, int> Holdings { get; } = new Dictionary<string, int>();
        public int TotalValue { get; }

        public UserDto(User user) 
        {
            Name = user.Name;
            TotalValue = user.HoldingsValue;
            foreach (var item in user.UserStocks)
            {
                Holdings.Add(item.StockId, item.Value);
            }
        }

    }
}
