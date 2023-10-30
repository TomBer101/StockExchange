using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.DTO.Outputs;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;
using RitzpaStockExchange.Interfaces.IService;

namespace RitzpaStockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStocksService _stocksService;
        private readonly IUsersService _usersService;

        public StocksController(IStocksService stocksService, IUsersService userService)
        {
            _stocksService = stocksService;
            _usersService = userService;
        }


        [HttpGet("GetAllStocks")]
        public async Task<ActionResult<IEnumerable<StockSummary>>> GetAllStocks() // naybe use yeild return because manipulating each stock
        {
            IEnumerable<StockSummary> stocks = await _stocksService.GetAllStocksAsync();

            if(stocks == null)
            {
                return NotFound("There are no stocks.");
            }         

            return Ok(stocks);

        }

        [HttpGet("GetStock/{symbol}")]
        public async Task<ActionResult<StockDetailed>> GetStock(string symbol)
        {
            if (!symbol.All(Char.IsLetter))
            {
                return BadRequest("A stock name must contain letters only!");
            }

            StockDetailed stockDetailed = await _stocksService.GetStockAsync(symbol.ToUpper());
            if(stockDetailed == null)
            {
                return NotFound($"There is no stock calld {symbol}");
            }

            return Ok(stockDetailed);

        }

        [HttpPost("CreateStock")]
        public async Task<IActionResult> CreateStockAsync(StockInput stockInput)
        {
            try
            {
                await _stocksService.AddAsync(stockInput);
                return Ok();
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }

        //[HttpPut("SubmmitOffer")]
        //public async Task<ActionResult<SubmmitOfferResult>> SubmitOffer(CommandInput commandInput)
        //{
        //    if(commandInput.Amount < 1)
        //    {
        //        return BadRequest("The amount has to be greater then 0!");
        //    }

        //    try
        //    {
        //        User user = _usersService.GetUser(commandInput.Initiator);
        //        SubmmitOfferResult result =  _stocksService.submitCommand(commandInput, user);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }

        //}

        //[HttpGet("GetStocksLists")]
        //public async Task<IEnumerable<StockLists>> GetStockLists()
        //{
        //    return await _stocksService.GetStocksListsAsync();
        //}

        [HttpGet("GetStockLists/{symbol}")]
        public async Task<ActionResult<StockLists>> GetStockLists(string symbol)
        {
            return Ok(await _stocksService.GetStockListsAsync(symbol));
        }

        [HttpGet("GetStocksNames")]
        public async Task<ActionResult<IEnumerable<string>>> GetStocksNames() // use yeild return
        {
            return NotFound("Function was not implamented yet");
        }

    }
}
