using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Linq;

namespace BLL
{
    public class ApiValidationBLL : IApiValidationBLL
    {
        private string Secret { get; set; }

        public ApiValidationBLL(IConfiguration configuration)
        {
            Secret = configuration["Secret"];
        }
        public int ExtractUserIdFromToken(string prefixedToken)
        {
            var matchedGroups = Regex.Match(prefixedToken, @"^Bearer\s*(.*)$").Groups;
            if (matchedGroups.Count < 2 || !matchedGroups[1].Success)
                throw new Exception("Invalid token");
            var token = matchedGroups[1].Value;
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            var claimId = jwtToken.Claims.First(claim => claim.Type == "unique_name").Value;
            return int.Parse(claimId);
        }
    

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                //Inject expiration from settings?
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string HashPassword(string inputString)
        {
            throw new NotImplementedException();
        }

        public void ValidateAndUpdateNewUserCredentials(User user)
        {
            {
                if (string.IsNullOrWhiteSpace(user.Username))
                    throw new Exception("Name can't be empty");
                if (string.IsNullOrWhiteSpace(user.Username))
                    throw new Exception("Username can't be empty");
                if (string.IsNullOrWhiteSpace(user.Password))
                    throw new Exception("Password can't be empty");
                if (user.Password.Length < 5)
                    throw new Exception("Password must be at least 5 characters long");
                if (user.Username.Length > 50)
                    throw new Exception("Username must be at most 50 characters long");
                if (user.Password.Length > 50)
                    throw new Exception("Password must be at most 50 characters long");
                
                user.Id = null;
                if (user.Role == 0)
                    user.Role = UserRole.Customer;
                user.Password = HashPassword(user.Password);
            }
        }

        public void ValidateId(string prefixedToken, int id)
        {
            if (ExtractUserIdFromToken(prefixedToken) != id)
                throw new UnauthorizedAccessException("Not authorized for that id");
        }

        public void ValidateUserCredentials(User user, string password)
        {
            if (user == null)
                throw new Exception("Username or Password incorrect");
            if (!user.Password.Equals(HashPassword(password)))
                throw new Exception("Username or Password incorrect");
        }
    }
}
