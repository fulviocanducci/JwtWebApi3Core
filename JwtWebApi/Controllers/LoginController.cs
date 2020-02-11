using JwtDatabase;
using JwtModels;
using JwtWebApi.Services;
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
      public TokenService TokenService { get; }

      public LoginController(DatabaseContext databaseContext, TokenService tokenService)
      {
         DatabaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
         TokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
      }

      [HttpPost]
      [Route("auth")]
      public IActionResult Authenticate(Login login)
      {
         if (ModelState.IsValid)
         {
            User user = DatabaseContext
               .User
               .Where(x => x.Email == login.Email && x.Password == login.Password)
               .FirstOrDefault();

            if (user == null)
            {
               return NotFound(new { message = "User no found" });
            }

            TokenResult tokenResult = TokenService.GenerateTokenResult(user);
            return new JsonResult(tokenResult);
         }         
         return BadRequest(ModelState);
      }
   }
}