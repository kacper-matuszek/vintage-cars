using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nop.Core.Domain.Customers;
using VintageCars.Domain.Configs;

namespace VintageCars.Service.Infrastructure
{
    public class JwtService : IJwtService
    {
        private readonly AuthorizationConfig _config;

        public JwtService(IConfiguration configuration)
        {
            _config = new AuthorizationConfig();
            configuration.Bind(nameof(AuthorizationConfig).Replace("Config", null), _config);
        }

        public string GenerateToken(Customer customer, IEnumerable<CustomerRole> customerRole)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SecretKey));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, customer.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, customer.Email),
                new Claim(ClaimTypes.Role, string.Join("|", customerRole.Select(cr => cr.Name))),
            };

            var expiration = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                issuer: _config.Issuer,
                signingCredentials: credentials,
                claims: claims,
                expires: expiration);

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodeToken;
        }
    }
}
