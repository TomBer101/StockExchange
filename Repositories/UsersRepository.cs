using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.Interfaces.IRepository;
using RitzpaStockExchange.Models;

namespace RitzpaStockExchange.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private static DataContext context;

        public UsersRepository(DataContext _context)
        {
            context = _context;
        }

        public void Add(User user)
        {
            try
            {
                if (context.Users.FirstOrDefault(u => u.Name == user.Name) == null)
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                else { throw new Exception($"{user.Name} is already in use."); }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(string userName)
        {
            try
            {
                User user = context.Users.FirstOrDefault(u => u.Name == userName);
                if (user != null)
                {
                    context.Users.Remove(user);
                }
            }
            catch (Exception ex) { throw; }

        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                IEnumerable<User> users = context.Users;
                foreach (User user in users)
                {
                    //context.Entry(user).Collection(user => user.RseHoldings).Load();
                }
                return users;
            }
            catch (Exception ex) { throw; }
        }

        public User GetUser(string userEmail)
        {
            try
            {
                User user = context.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user != null)
                {

                }
                return user;
            }
            catch (Exception e) { throw; }
        }

        public bool IsExists(string name)
        {
            if (context.Users.FirstOrDefault(user => user.Name == name) != null) return true;
            return false;
        }

        public async Task<User> UpdateAsync(string email, Stock stock, int amount)
        {
            try
            {

                User? newUser = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (newUser != null)
                {
                    //context.Users.Remove(newUser);
                    var userStock = new UserStock()
                    {
                        Amount = amount,
                        Stock = stock,
                        StockId = stock.StockName,
                        User = newUser,
                        UserId = newUser.Email
                    };
                    newUser?.UserStocks?.Add(userStock);

                    //await context.Users.AddAsync(newUser);
                    await context.SaveChangesAsync();

                    return newUser;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update user", ex);
            }

        }


    }
}
