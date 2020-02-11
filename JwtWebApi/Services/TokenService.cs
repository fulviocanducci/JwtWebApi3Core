using JwtModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtWebApi.Services
{
   public sealed class TokenService
   {
      public TokenResult GenerateTokenResult(User user)
      {
         DateTime expires = DateTime.UtcNow.AddHours(2);
         JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
         SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
         {
            Subject = new ClaimsIdentity(new Claim[]
            {
                  new Claim(ClaimTypes.Name, user.Email), 
                  new Claim(ClaimTypes.Email, user.Email)
                  //new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = expires,
            SigningCredentials = new SigningCredentials
            (
               new SymmetricSecurityKey(Settings.SecretToByte()),
               SecurityAlgorithms.HmacSha256Signature
            )
         };
         SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);         
         return new TokenResult(tokenHandler.WriteToken(token), expires, true);
      }
   }
}
