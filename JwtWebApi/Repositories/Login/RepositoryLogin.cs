using JwtDatabase;
using JwtModels;
using System;
using System.Linq;

namespace JwtWebApi.Repositories.Login
{
   public sealed class RepositoryLogin : IRepositoryLogin
   {
      private DatabaseContext DatabaseContext { get; }

      public RepositoryLogin(DatabaseContext databaseContext)
      {
         DatabaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
      }

      public User CheckLogin(JwtModels.Login login)
      {
         return DatabaseContext
                    .User
                    .Where(x => x.Email == login.Email && x.Password == login.Password)
                    .FirstOrDefault();
      }
   }
}
