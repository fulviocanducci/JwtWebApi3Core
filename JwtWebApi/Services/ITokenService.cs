using JwtModels;

namespace JwtWebApi.Services
{
   public interface ITokenService
   {
      TokenResult GenerateTokenResult(User user);
   }
}