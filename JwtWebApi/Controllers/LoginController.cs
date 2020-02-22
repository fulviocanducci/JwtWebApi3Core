using JwtDatabase;
using JwtModels;
using JwtWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
namespace JwtWebApi.Controllers
{
   [Route("api/login")]
   [ApiController]
   public class LoginController : ControllerBase
   {
      private DatabaseContext Context { get; }
      private ITokenService TokenService { get; }
      public LoginController(DatabaseContext context, ITokenService tokenService)
      {
         Context = context ?? throw new ArgumentNullException(nameof(context));
         TokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
      }

      [HttpPost]
      [Route("auth")]
      [SwaggerOperation(
         Summary = "Login of User",
         Description = "Login of User",
         OperationId = "CheckLogin",
         Tags = new[] { "Login" }
      )]
      public IActionResult Authenticate(Login login)
      {
         if (ModelState.IsValid)
         {
            User user = CheckLogin(login);
            if (user == null)
            {
               return NotFound(new { message = "User no found" });
            }
            TokenResult tokenResult = TokenService.GenerateTokenResult(user);
            return new JsonResult(tokenResult);
         }
         return BadRequest(ModelState);
      }

      [NonAction]
      protected User CheckLogin(Login login)
      {
         return Context
            .User
            .Where(x => x.Email == login.Email && x.Password == login.Password)
            .FirstOrDefault();
      }
   }
}