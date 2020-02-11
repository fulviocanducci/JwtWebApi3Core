using System;

namespace JwtWebApi.Services
{
   public sealed class TokenResult : ITokenResult
   {
      public string Token { get; }
      public DateTime Expires { get; }
      public bool Status { get; }
      public TokenResult(string token, DateTime expires, bool status)
      {
         Token = token ?? throw new ArgumentNullException(nameof(token));
         Expires = expires;
         Status = status;
      }
   }
}
