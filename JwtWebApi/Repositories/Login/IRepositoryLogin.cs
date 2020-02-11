using JwtModels;

namespace JwtWebApi.Repositories.Login
{
   public interface IRepositoryLogin
   {
      public User CheckLogin(JwtModels.Login login);
   }
}
