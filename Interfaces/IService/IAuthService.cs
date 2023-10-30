using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.Models;

namespace RitzpaStockExchange.Interfaces.IService
{
    public interface IAuthService
    {
        Task<User> Register(UserInput input);
        Task<string> Login(UserInput input);
    }
}
