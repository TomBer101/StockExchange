using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.DTO.Outputs;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;

namespace RitzpaStockExchange.Interfaces.IService
{
    public interface IUsersService
    {
        public User GetUser(string userEmail);
        public void UpdateUser(User user);
        public void AddUser(UserInput userInputu);

        IEnumerable<UserDto> GetUsers();
        bool IsExists(string name);
    }
}
