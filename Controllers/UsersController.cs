using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RitzpaStockExchange.Data;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.DTO.Outputs;
using Ritzpa_Stock_Exchange.DTO.Inputs;
using RitzpaStockExchange.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;

namespace RitzpaStockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

       

        // GET: api/Users
        [HttpGet("GetUsers")] // only an admin can see all of the users
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            IEnumerable<UserDto> result =  _usersService.GetUsers();
            if(!result.Any())
            {
                return NotFound("There are no users in the system.");
            }

            return Ok(result);
        }

        // GET: api/Users/5
        [HttpGet("{userEmail}")]
        public async Task<ActionResult<UserDto>> GetUser(string userEmail)
        {
            User user = _usersService.GetUser(userEmail);


            if (user == null)
            {
                return NotFound($"The user:{userEmail}, does not exists.");
            }

            UserDto userDTO = new UserDto(user);
            return Ok(userDTO);
        }

        

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(string id, User user)
        //{
        //    if (id != user.Name)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(UserInput user)
        //{
            
        //    try
        //    {
        //        _usersService.AddUser(user);
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (UserExists(user.Name))
        //        {
        //            return Conflict($"The username {user.Name} is already in use.");
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetUser", new { id = user.Name }, user);
        //}

        // DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(string id)
        //{
        //    if (_context.Users == null)
        //    {
        //        return NotFound();
        //    }
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UserExists(string name)
        {
            return _usersService.IsExists(name);
        }
    }
}
