using System;

namespace JwtWebApi.Services
{
   public interface ITokenResult
   {
      DateTime Expires { get; }
      bool Status { get; }
      string Token { get; }
   }
}