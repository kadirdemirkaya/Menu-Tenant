﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shared.Application.Abstractions;
using Shared.Application.Extensions;
using Shared.Domain.Models;
using Shared.Domain.Models.Configs;
using Shared.Infrastructure.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shared.Infrastructure.Services
{
    public class JwtTokenService(IConfiguration _configuration) : IJwtTokenService
    {
        private readonly JwtTokenConfigs _jwtTokenConfig = _configuration.GetOptions<JwtTokenConfigs>("JwtTokenConfigs");

        public Token GenerateToken(TenantModel tenantInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtTokenConfig.SecretKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("TenantModel", JsonExtension.SerialJson(tenantInfo))
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtTokenConfig.TokenLifeTime),
                Issuer = _jwtTokenConfig.ValidIssuer,
                Audience = _jwtTokenConfig.ValidAudience,
                NotBefore = DateTime.UtcNow,
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            Shared.Domain.Models.Token tokenModel = new();
            tokenModel.Expiration = DateTime.UtcNow.AddMinutes(_jwtTokenConfig.TokenLifeTime);
            tokenModel.AccessToken = tokenHandler.WriteToken(token);

            return tokenModel;
        }

        public string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;

            return stringClaimValue;
        }

        public bool ValidateCurrentToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = _jwtTokenConfig.ValidateIssuer,
                    ValidIssuer = _jwtTokenConfig.ValidIssuer,
                    ValidateAudience = _jwtTokenConfig.ValidateAudience,
                    ValidAudience = _jwtTokenConfig.ValidAudience,
                    ValidateLifetime = _jwtTokenConfig.ValidateLifetime,
                    ValidateIssuerSigningKey = _jwtTokenConfig.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtTokenConfig.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
