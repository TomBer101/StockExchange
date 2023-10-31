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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersService(IUserRepository userRepository, IStocksRepository stocksRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _stockRepository = stocksRepository;
            _httpContextAccessor = httpContextAccessor;
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

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }

            return result;
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

        public void UpdateUser(User user)
        {
            try
            {
                _userRepository.Update(user.Name, user);
            }
            catch(Exception ex) { throw; }
        }
    }
}
