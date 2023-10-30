using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.Managers;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;
using RitzpaStockExchange.Interfaces.IService;
using RitzpaStockExchange.Services;

namespace RitzpaStockExchange.Managers
{
    public class SubmitOfferManager
    {
        private readonly IStocksService _stocksService;
        private readonly IUsersService _usersService;
        public SubmitOfferManager(IStocksService stocksService, IUsersService userService)
        {
            _stocksService = stocksService;
            _usersService = userService;
        }

        public async Task<SubmmitOfferResult> SubmitOffer(CommandInput commandInput)
        {
            if(commandInput.Amount < 1)
            {
                throw new ArgumentException("The amount has to be greater thrn 0!");
            }

            try
            {
                User user = _usersService.GetUser(commandInput.Initiator);
                SubmmitOfferResult result = await _stocksService.submitOfferAsync(commandInput, user);
                return result;
            } catch (Exception ex)
            {
                Console.WriteLine("There was an error trying to submit an offer: ", ex.Message);
                throw;
            }
        }
    }
}
