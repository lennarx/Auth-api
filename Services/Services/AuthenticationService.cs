using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthContext _context;
        private byte[] _key;

        public AuthenticationService(AuthContext context)
        {
            _context = context;            
        }

        public void SetKey(string key) => _key = Encoding.ASCII.GetBytes(key);
        public async Task<string> Authenticate(string username, string password)
        {
            var cryptedPassword = Encoding.ASCII.GetBytes(password);

            if ( ! await _context.Users.AnyAsync(x => x.UserName == username && x.Password == cryptedPassword))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task CreateUser(string username, string password, string name)
        {
            var user = new User
            {
                UserName = username,
                Password = Encoding.ASCII.GetBytes(password),
                Name = name
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();     
        }
    }
}
