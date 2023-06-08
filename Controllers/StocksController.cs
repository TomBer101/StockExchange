using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.DTO.Outputs;
using Ritzpa_Stock_Exchange.Interfaces;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;

namespace RitzpaStockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStocksService _stocksService;

        public StocksController(IStocksService stocksService)
        {
            _stocksService = stocksService;
        }


        [HttpGet("GetAllStocks")]
        public async Task<ActionResult<IEnumerable<StockSummary>>> GetAllStocks()
        {
            IEnumerable<StockSummary> stocks = _stocksService.GetAllStocks();

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

            StockDetailed stockDetailed = _stocksService.GetStock(symbol.ToUpper());
            if(stockDetailed == null)
            {
                return NotFound($"There is no stock calld {symbol}");
            }

            return Ok(stockDetailed);

        }

        [HttpPost("CreateStock")]
        public IActionResult CreateStock(StockInput stockInput)
        {
            try
            {
                _stocksService.Add(stockInput);
                return Ok();
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }

        [HttpPut("SubmmitOffer")]
        public async Task<ActionResult<SubmmitOfferResult>> SubmitOffer(CommandInput commandInput)
        {
            if(commandInput.Amount < 1)
            {
                return BadRequest("The amount has to be greater then 0!");
            }

            try
            {
                SubmmitOfferResult result =  _stocksService.submitCommand(commandInput);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("GetStocksLists")]
        public async Task<IEnumerable<StockLists>> GetStockLists()
        {
            return _stocksService.GetStocksLists();
        }


    }
}
