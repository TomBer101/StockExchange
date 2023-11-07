using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.Interfaces.IManagers;
using RitzpaStockExchange.Interfaces.IService;
using System.Security.Claims;

namespace RitzpaStockExchange.Managers
{
    public class PublishStockManager : IPublishStockManager
    {
        private  IStocksService _stocksService;
        private  IUsersService _usersService;
        private IHttpContextAccessor _httpContextAccessor;

        public PublishStockManager(IStocksService stocksService, IUsersService userService,
            IHttpContextAccessor httpContextAccessor) // Should i also pass the input to have it as a property?
        {
            _stocksService = stocksService;
            _usersService = userService;
            _httpContextAccessor = httpContextAccessor;

        }

         public async Task<Stock> PublishStock(StockInput stockInput)
        {
            try
            {
                var userEmail = string.Empty;
                if (_httpContextAccessor.HttpContext != null)
                {
                    userEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                }

                Stock stock = await _stocksService.AddAsync(stockInput);

                if (stock != null)
                {

                    var user = _usersService.UpdateUserStocks(userEmail, stock, stockInput.Amount);

                    Console.WriteLine(user);

                    if (user != null)
                    {

                        return stock;
                    }

                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to Publish Stock", ex);
            }
        }
    }
}
