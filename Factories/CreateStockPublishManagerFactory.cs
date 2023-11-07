using RitzpaStockExchange.Interfaces.IManagers;
using RitzpaStockExchange.Interfaces.IService;
using RitzpaStockExchange.Managers;

namespace RitzpaStockExchange.Factories
{
    public class CreateStockManagerFactory
    {
        private readonly IStocksService _stockService;
        private readonly IUsersService _userService;


        public CreateStockManagerFactory(IStocksService stockService, IUsersService userService)
        {
            _stockService = stockService;
            _userService = userService;
        }

        //public IPublishStockManager CreateManager()
        //{
        //    //return new PublishStockManager(_stockService, _userService);
        //}
    }

}
