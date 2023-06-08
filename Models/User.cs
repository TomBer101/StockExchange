using System.ComponentModel.DataAnnotations;

namespace Ritzpa_Stock_Exchange.Models
{
    public class User
    {
        [Key]
        public string Name { get; set; }
        public Dictionary<string, int>? RseHoldings { get; set; }

    }
}
