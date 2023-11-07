using Ritzpa_Stock_Exchange.Models;

namespace RitzpaStockExchange.Interfaces.IRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetUser(string userEmail);
        void Add(User user);
        void Delete(string userName);
        Task<User> UpdateAsync(string email, Stock stock,int amount );

        bool IsExists(string name);
        //void submitCommand(Command newCommand);
    }
}
