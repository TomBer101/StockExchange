using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.Managers;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;
using RitzpaStockExchange.Interfaces.IRepository;
using RitzpaStockExchange.Interfaces.IService;
using System.Security.Claims;

namespace RitzpaStockExchange.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStocksRepository _stockRepository;


        public UsersService(IUserRepository userRepository, IStocksRepository stocksRepository)
        {
            _userRepository = userRepository;
            _stockRepository = stocksRepository;
        }

        public void AddUser(UserInput userInputu)
        {
            User newUser = new User() { Name = userInputu.Name };

            //List<Stock> usersStocks = new List<Stock>();
            //foreach (var item in userInputu.Holdings)
            //{
            //    Stock currStock = _stockRepository.GetStock(item.Key);
            //    newUser.RseHoldings.Add(item.Key, new Models.Holding() { Amount = item.Value, Balance = currStock.Price, Symbol = item.Key.ToUpper()} );
            //    usersStocks.Add(currStock);
            //}
            //newUser.SubscribeToStocks(usersStocks);

            _userRepository.Add(newUser);
        }

        public User GetUser(string userEmail)
        {
            try
            {
                //UserDto? result = null;
                User user = _userRepository.GetUser(userEmail);
                //if(user != null)
                //{
                //    result = new UserDto(user);
                //}

                return user;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

       
        public IEnumerable<UserDto> GetUsers()
        {
            IEnumerable<User> users = _userRepository.GetAll();
            List<UserDto> result = new List<UserDto>();

            foreach (var user in users)
            {
                result.Add(new UserDto(user));
            }

            return result.ToArray();
        }

        public bool IsExists(string name)
        {
            return _userRepository.IsExists(name);
        }

        public async Task<User> UpdateUserStocks(string email, Stock stock, int amount)
        {
            try
            {
               var res = await _userRepository.UpdateAsync(email, stock,amount);
                return res;
            }
            catch(Exception ex) { throw; }
        }

        
    }
}
