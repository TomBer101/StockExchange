using Ritzpa_Stock_Exchange.Models;

namespace RitzpaStockExchange.Interfaces.IRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetUser(string userEmail);
        void Add(User user);
        void Delete(string userName);
        void Update(string user, User newUser);

        void Update();
        bool IsExists(string name);
        //void submitCommand(Command newCommand);
    }
}
