using JwtDatabase;
using JwtModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace JwtWebApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class LoginController : ControllerBase
   {
      public DatabaseContext DatabaseContext { get; }
      public LoginController(DatabaseContext databaseContext)
      {
         DatabaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
      }

      [HttpPost]
      [Route("auth")]
      public IActionResult Authenticate(Login login)
      {
         User user = DatabaseContext
            .User
            .Where(x => x.Email == login.Email && x.Password == x.Password)
            .FirstOrDefault();

         if (user == null)
         {
            return NotFound(new { message = "User no found" });
         }

         string token = TokenService.GenerateToken(user);

         return new JsonResult(new
         {
            token
         });
      }
   }
}