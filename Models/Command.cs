using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ritzpa_Stock_Exchange.Models
{
    public class Command // TODO: remove enum 
    {
        public enum Way
        {
            Sell, Buy
        }

        public enum Type
        {
            LMT, MKT, FOK, IOC
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? CurrentStockRate { get; set; }
        public Type CommandType { get; set; }
        public Way CommandWay { get; set; }
        public int Amount { get; set; }
        public DateTime TimeStamp { get; } = new DateTime().ToLocalTime();

        public string? BuyStockName { get; set; }
        public virtual Stock BuyStock { get; set; }

        public string? SellStockName { get; set; }
        public virtual Stock SellStock { get; set; }

        public string? InitiatorId { get; private set; }
        public User? Initiator { get; private set; }

        public void SetInitiator(User initiator)
        {
            Initiator = initiator;
            InitiatorId = initiator.Email;
        }

        //public Command(User initiator)
        //{
        //    Initiator = initiator;
        //    InitiatorId = initiator.Name;
        //}
    }
}
