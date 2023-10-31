using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Ritzpa_Stock_Exchange.Models;
using Ritzpa_Stock_Exchange.DTO.Inputs;
using RitzpaStockExchange.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;

namespace RitzpaStockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserInput input)
        {
            try
            {
                User addedUser = await _authService.Register(input);
                return Ok(addedUser);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserInput input)
        {
            try
            {
                string jwt = await _authService.Login(input);
                return Ok(jwt);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
