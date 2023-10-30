using System.ComponentModel.DataAnnotations.Schema;

namespace RitzpaStockExchange.Models
{
    public class Holding
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Amount { get; set; }
        public int Balance { get; set; }
        public string Symbol { get; set; }
    }
}
