using Microsoft.IdentityModel.Tokens;
using Ritzpa_Stock_Exchange.DTO.Inputs;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.Interfaces.IRepository;
using RitzpaStockExchange.Interfaces.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RitzpaStockExchange.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> Register(UserInput input)
        {
            if (_userRepository.GetUser(input.Email) != null)
            {
                throw new ArgumentException("This email is already register!");
            }

            createPasswordHash(input.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User()
            {
                Email = input.Email,
                PasswordHash = passwordHash,
                HashSalt = passwordSalt,
                Name = input.Name,
            };
            _userRepository.Add(user);
            return user;

        }

        public async Task<string> Login(UserInput input)
        {
            User user = _userRepository.GetUser(input.Email);
            if(user == null)
            {
                throw new ArgumentException("The email is not regitered, user not found!");
            }

            if(!verifyPasswordHash(input.Password, user.PasswordHash, user.HashSalt))
            {
                throw new ArgumentException("Wrong Password!");
            }

            string token = createToken(user);
            return token;
        }

        private string createToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt; ;
        }

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    
        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
