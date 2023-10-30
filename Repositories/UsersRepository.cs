using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.Interfaces.IRepository;

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
                if(context.Users.FirstOrDefault(u => u.Name == user.Name) == null)
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
                if(user != null)
                {
                    context.Users.Remove(user);
                }
            }
            catch(Exception ex) { throw; }

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
            catch( Exception ex ) { throw; }
        }

        public User GetUser(string userEmail)
        {
            try
            {
                User user = context.Users.FirstOrDefault(u => u.Email == userEmail);
                if(user != null)
                {

                }
                return user;
            }
            catch(Exception e) { throw; }
        }

        public bool IsExists(string name)
        {
            if (context.Users.FirstOrDefault(user => user.Name == name) != null) return true;
            return false;
        }

        public void Update(string user, User newUser)
        {
            try
            {
                User userToRemove = context.Users.FirstOrDefault(u => u.Name == user);
                if(userToRemove != null)
                {
                    context.Users.Remove(userToRemove);
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }
            } catch(Exception ex) { throw; }
        }

        public void Update()
        {
            context.SaveChanges();
        }
    }
}
